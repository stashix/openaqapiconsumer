using System.Runtime.Serialization;

namespace OpenAQWebApi.Entities
{
    public enum CityOrder : byte
    {
        [EnumMember(Value = "city")]
        City = 0,

        [EnumMember(Value = "country")]
        Country = 10,

        [EnumMember(Value = "firstUpdated")]
        FirstUpdated = 20,

        [EnumMember(Value = "lastUpdated")]
        LastUpdated = 30
    }
}
