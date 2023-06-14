using System.Runtime.Serialization;
using OpenAQWebApi.Entities;

namespace OpenAQWebApi.Responses
{
    [Serializable]
    [DataContract]
    public class PagedResponse<T> where T : class
    {
        [DataMember(Name = "meta")]
        public Metadata Metadata { get; init; }

        [DataMember(Name = "results")]
        public IList<T> Results { get; init; }
    }
}
