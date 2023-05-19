using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AttachmentType
{
    File = 0,
    Image = 1,
    Video = 2,
}
