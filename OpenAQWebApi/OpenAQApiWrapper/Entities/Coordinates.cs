﻿using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public sealed class Coordinates
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; init; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; init; }
    }
}
