# _2.Application

```bash
dotnet new classlib

dotnet add reference "../_1.Domain"

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions
dotnet add package FluentValidation.DependencyInjectionExtensions
# dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions
#
dotnet build

```
