using System.Text.Json.Serialization;

namespace BackendApiTests.Models
{
    public class PostObjectRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, object>? Data { get; set; }
    }
}