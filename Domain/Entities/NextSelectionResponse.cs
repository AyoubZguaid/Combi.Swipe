namespace Domain.Entities;

public class NextSelectionResponse
{
    public long MarketId { get; set; }
    public long SelectionId { get; set; }
    public string Label { get; set; } = string.Empty;
}
