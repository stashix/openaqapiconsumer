using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using OpenAQApiWrapper.Filters;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Services
{
    internal class ReflectionQuerySerializer : IQuerySerializer
    {
        public string ConstructQuery<T>(string relativeUri, T filterModel) where T : PagingFilter
        {
            var props = filterModel.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead);

            var keyValuePairsList = new List<KeyValuePair<string, StringValues>>();
            foreach (var prop in props)
            {
                var jsonPropertyName = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
                if (string.IsNullOrWhiteSpace(jsonPropertyName))
                    continue;

                var propertyValue = prop.GetValue(filterModel);
                if (propertyValue is null)
                    continue;

                if (prop.PropertyType.IsEnum || Nullable.GetUnderlyingType(prop.PropertyType)?.IsEnum == true)
                {
                    var enumType = prop.PropertyType.IsEnum ? prop.PropertyType
                        : Nullable.GetUnderlyingType(prop.PropertyType);

                    var enumLabel = enumType?.GetTypeInfo()
                        .DeclaredMembers
                        .FirstOrDefault(x => x.Name == propertyValue.ToString())
                        ?.GetCustomAttribute<EnumMemberAttribute>()?.Value;

                    keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(enumLabel)));
                    continue;
                }

                if (prop.PropertyType.IsAssignableFrom(typeof(IEnumerable<string>)))
                {
                    var collectionValue = propertyValue as IEnumerable<string>;
                    keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(collectionValue?.ToArray())));
                    continue;
                }

                keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(propertyValue.ToString())));
            }

            return QueryHelpers.AddQueryString(relativeUri, keyValuePairsList);
        }
    }
}
