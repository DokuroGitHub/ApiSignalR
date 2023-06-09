namespace Domain.Common;

public class Appsettings
{
    public bool UseInMemoryDatabase { get; init; }
    public ConnectionStrings ConnectionStrings { get; init; } = new ConnectionStrings();
    public Jwt Jwt { get; init; } = new Jwt();
    public SignalR SignalR { get; init; } = new SignalR();
}

public class ConnectionStrings
{
    public string DefaultConnection { get; init; } = string.Empty;
    public string DefaultConnectionV2 { get; set; } = string.Empty;
}

public class Jwt
{
    public int ExpireDays { get; init; }
    public string Key { get; init; } = "HEHE YOU MIGHT THINK THIS IS THE REAL KEY BUT IT'S NOT";
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}

public class SignalR
{
    public string HubUrl { get; init; } = string.Empty;
}
