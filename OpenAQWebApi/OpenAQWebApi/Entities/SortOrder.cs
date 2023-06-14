using System.Runtime.Serialization;

namespace OpenAQWebApi.Entities
{
    public enum SortOrder : byte
    {
        [EnumMember(Value = "asc")]
        Ascending = 0,

        [EnumMember(Value = "desc")]
        Descending = 10
    }
}
