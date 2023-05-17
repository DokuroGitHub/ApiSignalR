# _3.Infrastructure

```bash
dotnet new classlib

dotnet add reference "../_2.Application"

dotnet add package CsvHelper
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Newtonsoft.Json

dotnet add package Microsoft.EntityFrameworkCore.Design
#
dotnet build

```
