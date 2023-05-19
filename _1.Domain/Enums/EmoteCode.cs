using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EmoteCode
{
    Like = 0,
    Haha = 1,
    Love = 2,
    Wow = 3,
    Sad = 4,
    Angry = 5
}
