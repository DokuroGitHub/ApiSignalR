# SignalR + Clean Architecture + UnitOfWork + Repository Pattern + CQRS + MediatR + FluentValidation + JWT

```bash
# Change DefaultConnection at src._4.Api.appsettings.Development.json.ConnectionStrings.DefaultConnection

# migration
dotnet ef migrations add InitDb -p "_3.Infrastructure" -s "_4.Api"

dotnet ef database update -p "_3.Infrastructure" -s "_4.Api"

cd "_4.Api"
dotnet watch

```

## test postman

connect: <wss://dokurosignalr.azurewebsites.net/users>
send message:

```json
// handshake
{
    "protocol": "json",
    "version": 1
}

// join group
// UsersChanged is group name
// 0 is connection id
// JoinGroup is method name
// UsersChanged is method argument
{
    "type": 1,
    "invocationId": "0",
    "target": "JoinGroup",
    "arguments": [
        "UsersChanged"
    ]
}

// create user
{
    "type": 1,
    "invocationId": "0",
    "target": "Create",
    "arguments": [
        {
            "FirstName": "haha",
            "LastName": "hehe",
            "Email": "hehe@gmail.com",
            "Username": "hehehe",
            "Password": "hehehe"
        }
    ]
}

// update user
{
    "type": 1,
    "invocationId": "0",
    "target": "Update",
    "arguments": [
        3,
        {
            "FirstName": "haha",
            "LastName": "hehe"
        }
    ]
}

// delete user
{
    "type": 1,
    "invocationId": "0",
    "target": "Delete",
    "arguments": [
        3
    ]
}

```

## cmd

```bash
dotnet new sln
dotnet sln "ApiSignalR.sln" add "_4.Api"

dotnet new gitignore
git init
git add .
git commit -m "first commit"
git push --set-upstream "https://gitlab.com/DokuroSignalR/ApiSignalR.git" master
git push --set-upstream "https://github.com/DokuroGitHub/ApiSignalR.git" master

git remote add gitlab https://gitlab.com/DokuroSignalR/ApiSignalR.git
git remote add github https://github.com/DokuroGitHub/ApiSignalR.git

```
