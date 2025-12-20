using System.Text.Json.Serialization;

namespace BackendApiTests.Models
{
    public class PatchObjectRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}