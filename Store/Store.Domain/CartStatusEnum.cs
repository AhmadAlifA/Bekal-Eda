using System.Text.Json.Serialization;

namespace Store.Domain
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
