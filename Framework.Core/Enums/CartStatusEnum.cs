using System.Text.Json.Serialization;

namespace Framework.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CartStatusEnum
    {
        Pending,
        Confirmed,
        Paid,
        Canceled
    }
}
