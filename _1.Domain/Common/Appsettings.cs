namespace Domain.Common;

#pragma warning disable
public class Appsettings
{
    public bool UseInMemoryDatabase { get; init; }
    public ConnectionStrings ConnectionStrings { get; init; }
    public Jwt Jwt { get; init; }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; init; }
}

public class Jwt
{
    public int ExpireDays { get; init; }
    public string Key { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
}
