using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    User = 0,
    Admin = 1
}
