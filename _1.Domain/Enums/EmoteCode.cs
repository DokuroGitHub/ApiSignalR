using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EmoteCode
{
    Like,
    Haha,
    Love,
    Wow,
    Sad,
    Angry,
}
