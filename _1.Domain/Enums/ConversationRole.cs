using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ConversationRole
{
    Member = 0,
    Admin = 1
}
