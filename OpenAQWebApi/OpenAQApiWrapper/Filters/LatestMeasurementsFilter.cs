﻿using OpenAQApiWrapper.Entities;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public sealed class LatestMeasurementsFilter : PagingFilter
    {
        public LatestMeasurementsFilter() 
        {
            Countries = Enumerable.Empty<string>();
            Cities = Enumerable.Empty<string>();
        }

        [JsonPropertyName("country")]
        public IEnumerable<string> Countries { get; init; }

        [JsonPropertyName("city")]
        public IEnumerable<string> Cities { get; init; }

        [JsonPropertyName("order_by")]
        public MeasurementsOrder? OrderBy { get; init; } 
    }
}