﻿using OpenAQApiWrapper.Entities;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public sealed class LatestMeasurementsFilter : PagingFilter
    {
        [JsonPropertyName("country_id")]
        public string? CountryId { get; init; }

        [JsonPropertyName("country")]
        public IEnumerable<string>? Countries { get; init; }

        [JsonPropertyName("city")]
        public IEnumerable<string>? Cities { get; init; }

        [JsonPropertyName("dumpRaw")]
        public bool DumpRaw { get; init; }

        [JsonPropertyName("order_by")]
        public MeasurementsOrder? OrderBy { get; init; } 
    }
}
