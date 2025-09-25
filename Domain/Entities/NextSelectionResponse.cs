namespace Domain.Entities;

public class NextSelectionResponse
{
    public long MarketId { get; set; }
    public long SelectionId { get; set; }
    public string MarketName { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public int Relevancy_Score { get; set; }
}
