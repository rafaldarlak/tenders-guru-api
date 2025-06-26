using System.Text.Json.Serialization;

namespace Tenders.Guru.Http.Client.Models;

internal record SupplierDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; }
}