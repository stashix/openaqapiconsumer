using System.Runtime.Serialization;

namespace OpenAQWebApi.Entities
{
    public enum CountryOrder : byte
    {
        [EnumMember(Value = "code")]
        Code = 0,

        [EnumMember(Value = "name")]
        Name = 10,

        [EnumMember(Value = "locations")]
        Locations = 20,

        [EnumMember(Value = "firstUpdated")]
        FirstUpdated = 30,

        [EnumMember(Value = "lastUpdated")]
        LastUpdated = 40,

        [EnumMember(Value = "parameters")]
        Parameters = 50,

        [EnumMember(Value = "count")]
        Count = 60,

        [EnumMember(Value = "cities")]
        Cities = 70,

        [EnumMember(Value = "sources")]
        Sources = 80
    }
}
