using System.Text.Json.Serialization;

namespace Framework.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RecordStatusEnum
    {
        Active,
        InActive,
        Remove
    }
}
