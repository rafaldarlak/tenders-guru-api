using System.Text.Json.Serialization;

namespace Tenders.Guru.Http.Client.Models;

internal record TenderAwardDto
{
    [JsonPropertyName("suppliers")]
    public IList<SupplierDto> Suppliers { get; init; } = new List<SupplierDto>();
}