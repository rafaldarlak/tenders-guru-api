using System.Text.Json.Serialization;

namespace Tenders.Guru.Http.Client.Models;

internal record TendersGuruResponse<TResponse>
{
    [JsonPropertyName("data")] 
    public TResponse Data { get; set; }
}
