using System.Diagnostics;
using System.Text;
using Api.Middlewares;
using Api.Services;
using Application.Common.Interfaces;
using Application.Hubs.Chart;
using Application.Hubs.Test;
using Application.Hubs.Users;
using Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
        }).AddNewtonsoftJson();//.AddXmlSerializerFormatters();
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
        services.AddHealthChecks();
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
        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        // signalR
        app.MapHub<ChartHub>("/chart");
        app.MapHub<TestHub>("/test");
        app.MapHub<UsersHub>("/users");

        return app;
    }
}
