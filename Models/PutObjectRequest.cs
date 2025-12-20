using System.Text.Json.Serialization;

namespace BackendApiTests.Models
{
    public class PutObjectRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, object>? Data { get; set; }
    }

}