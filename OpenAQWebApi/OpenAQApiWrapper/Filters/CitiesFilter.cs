﻿using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public sealed class CitiesFilter : PagingFilter
    {
        [RegularExpression("[A-Za-z][A-Za-z]")]
        [JsonPropertyName("country_id")]
        public string? CountryId { get; init; }

        [JsonPropertyName("country")]
        public IEnumerable<string>? Countries { get; init; }

        [JsonPropertyName("city")]
        public IEnumerable<string>? Cities { get; init; }

        [JsonPropertyName("order_by")]
        public CityOrder? OrderBy { get; init; }
    }
}
