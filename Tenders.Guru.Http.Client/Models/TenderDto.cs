using System.Text.Json.Serialization;

namespace Tenders.Guru.Http.Client.Models;

internal record TenderDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }
    
    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; init; }
    
    [JsonPropertyName("title")]
    public string Title { get; init; }
    
    [JsonPropertyName("description")]
    public string Description { get; init; }
    
    [JsonPropertyName("awarded_value_eur")]
    public decimal AmountEuro { get; init; }

    [JsonPropertyName("awarded")] 
    public IList<TenderAwardDto> Awards { get; init; } = new List<TenderAwardDto>();
}