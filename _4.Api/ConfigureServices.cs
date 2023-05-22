using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Api.Health;
using Api.Middlewares;
using Api.Services;
using Application.Common.Interfaces;
using Application.Common.Interfaces.AuthThirdParty;
using Application.Health;
using Application.Hubs.Chart;
using Application.Hubs.Chat;
using Application.Hubs.ConversationBlocks;
using Application.Hubs.ConversationInvitations;
using Application.Hubs.Conversations;
using Application.Hubs.DeletedMessages;
using Application.Hubs.MessageAttachments;
using Application.Hubs.MessageEmotes;
using Application.Hubs.Messages;
using Application.Hubs.Participants;
using Application.Hubs.Test;
using Application.Hubs.Users;
using Application.Services.IServices;
using Domain.Common;
using HealthChecks.UI.Client;
using Infrastructure.Health;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using ZymLabs.NSwag.FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        Appsettings appsettings)
    {
        // add api versioning
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // add cors
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        // services.AddDatabaseDeveloperPageExceptionFilter();

        // add middlewares
        services.AddSingleton<ExceptionMiddleware>();
        services.AddSingleton<Stopwatch>(); // for performance middleware
        services.AddSingleton<PerformanceMiddleware>();
        // add services
        services.AddHttpClient<IAuthThirdPartyHealthService, AuthThirdPartyHealthService>();
        services.AddSingleton<IAuthThirdPartyHealthService, AuthThirdPartyHealthService>();
        services.AddHttpClient<IAuthThirdPartyService, AuthThirdPartyService>();
        services.AddScoped<IAuthThirdPartyService, AuthThirdPartyService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        // add validations
        services.AddScoped<FluentValidationSchemaProcessor>(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // add controllers
        services.AddControllers(options =>
        {
            options.CacheProfiles.Add(
                "Default30",
                new CacheProfile()
                {
                    Duration = 30
                });
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });//.AddXmlSerializerFormatters();

        services.AddResponseCaching();

        // add jwt authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = appsettings.Jwt.Issuer,
                ValidAudience = appsettings.Jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings.Jwt.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
        // add authorization
        services.AddAuthorization();

        // add signalr
        services.AddSignalR(options =>
        {
            options.KeepAliveInterval = TimeSpan.FromSeconds(16);
        }).AddJsonProtocol();

        // additionals
        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddCheck<MyHealthyHealthCheck>(
                name: nameof(MyHealthyHealthCheck),
                tags: new[] { "healthy-for-sure" })
            .AddCheck<AuthThirdPartyHealthCheck>(
                name: nameof(AuthThirdPartyHealthCheck),
                tags: new[] { "third-party", "login", "register" })
            .AddCheck(
                name: nameof(SqlConnectionHealthCheck),
                instance: new SqlConnectionHealthCheck(appsettings.ConnectionStrings.DefaultConnection),
                tags: new string[] { "dokurodb", "sql" })
            .AddSqlServer(
                connectionString: appsettings.ConnectionStrings.DefaultConnection,
                name: "CK_DefaultConnection",
                tags: new[] { "DefaultConnection" })
            .AddCheck<ApiHealthCheck>(
                name: nameof(ApiHealthCheck),
                tags: new string[] { "api", "http" })
            .AddDbContextCheck<ApplicationDbContext>(tags: new[] { "CK_ApplicationDbContext" })
            .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024), tags: new[] { "C_Drive" })
            .AddProcessAllocatedMemoryHealthCheck(512, tags: new[] { "CK_ProcessAllocatedMemory" })
            .AddProcessHealthCheck("_4.Api", p => p.Length > 0, tags: new[] { "_4.Api" })
            .AddVirtualMemorySizeHealthCheck(long.MaxValue, tags: new[] { "VirtualMemorySize" })
            .AddWindowsServiceHealthCheck("Power", s => s.Status == ServiceControllerStatus.Running, tags: new[] { "Power" })
            .AddUrlGroup(new Uri("https://9gag.com"), tags: new[] { "9gag" })
            .AddSignalRHub(appsettings.SignalR.HubUrl, tags: new[] { "SignalRHub" })
            .AddPingHealthCheck(s => s.AddHost("9gag.com", 9), tags: new[] { "ping-9gag" })
            .AddWorkingSetHealthCheck(long.MaxValue, tags: new[] { "WorkingSet" });
        services.AddHealthChecksUI(options =>
        {
            options.AddHealthCheckEndpoint("Health Check API", "/hc");
            options.SetEvaluationTimeInSeconds(69);
            options.SetApiMaxActiveRequests(2);
            options.DisableDatabaseMigrations();
        })  //.AddSqliteStorage("Data Source=healthchecks.db");
            //.AddSqlServerStorage(appsettings.ConnectionStrings.DefaultConnectionV2);
            .AddInMemoryStorage();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // Add JWT authentication support in Swagger
            var securityScheme = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securityScheme, new[] { "Bearer" }
                }
            };

            options.AddSecurityRequirement(securityRequirement);

            // add swagger doc
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "DoVT2CleanArchitecture V1",
                Description = "CleanArchitecture API",
                Contact = new OpenApiContact
                {
                    Name = "DoVT2",
                    Email = "tamthoidetrong@gmail.com",
                    Url = new Uri("https://gitlab.com/DokuroGitHub/DoVT2CleanArchitecture")
                },
            });
            options.SwaggerDoc("v1.1", new OpenApiInfo
            {
                Version = "v1.1",
                Title = "DoVT2CleanArchitecture V1.1",
                Description = "CleanArchitecture API",
                Contact = new OpenApiContact
                {
                    Name = "DoVT2",
                    Email = "tamthoidetrong@gmail.com",
                    Url = new Uri("https://gitlab.com/DokuroGitHub/DoVT2CleanArchitecture")
                },
            });
        });

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || true)
        {
            // app.UseMigrationsEndPoint();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
           {
               options.SwaggerEndpoint("v1/swagger.json", "DoVT2CleanArchitecture v1");
               options.SwaggerEndpoint("v1.1/swagger.json", "DoVT2CleanArchitecture beta");
           });
        }
        else
        {
            // The null HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseExceptionMiddleware();
        app.UsePerformanceMiddleware();
        // app.UseHealthChecks("/health");
        app.MapHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
        });
        app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
        // app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        // signalR
        app.MapHub<ChartHub>("/Chart");
        app.MapHub<ChatHub>("/ChatHub");
        app.MapHub<ConversationBlocksHub>("/ConversationBlocks");
        app.MapHub<ConversationInvitationsHub>("/ConversationInvitations");
        app.MapHub<ConversationsHub>("/Conversations");
        app.MapHub<DeletedMessagesHub>("/DeletedMessages");
        app.MapHub<MessageAttachmentsHub>("/MessageAttachments");
        app.MapHub<MessageEmotesHub>("/MessageEmotes");
        app.MapHub<MessagesHub>("/Messages");
        app.MapHub<ParticipantsHub>("/Participants");
        app.MapHub<TestHub>("/Test");
        app.MapHub<UsersHub>("/Users");

        return app;
    }
}
