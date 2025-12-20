using System.Text.Json.Serialization;

namespace BackendApiTests.Models
{
    public class DeleteObjectResponse
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}