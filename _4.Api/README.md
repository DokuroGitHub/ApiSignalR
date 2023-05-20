# _4.Api

```bash
dotnet new webapi

dotnet add reference "../_3.Infrastructure"

# dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.JsonPatch
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

dotnet add package AspNetCore.HealthChecks.SqlServer
dotnet add package AspNetCore.HealthChecks.System
dotnet add package AspNetCore.HealthChecks.Uris
dotnet add package AspNetCore.HealthChecks.UI
dotnet add package AspNetCore.HealthChecks.UI.Client
dotnet add package AspNetCore.HealthChecks.UI.InMemory.Storage
dotnet add package AspNetCore.HealthChecks.UI.SqlServer.Storage
dotnet add package AspNetCore.HealthChecks.UI.SQLite.Storage
dotnet add package AspNetCore.HealthChecks.SignalR
dotnet add package AspNetCore.HealthChecks.Network
dotnet add package RestSharp
#
dotnet build

```
