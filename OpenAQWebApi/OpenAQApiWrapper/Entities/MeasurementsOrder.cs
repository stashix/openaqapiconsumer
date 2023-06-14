using System.Runtime.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public enum MeasurementsOrder : byte
    {
        [EnumMember(Value = "city")]
        City = 0,

        [EnumMember(Value = "country")]
        Country = 10,

        [EnumMember(Value = "location")]
        Location = 20,

        [EnumMember(Value = "firstUpdated")]
        FirstUpdated = 40,

        [EnumMember(Value = "lastUpdated")]
        LastUpdated = 50,

        [EnumMember(Value = "count")]
        Count = 60
    }
}
