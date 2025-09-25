namespace Domain.Entities;

public class NextSelectionRequest
{
    public long UserId { get; set; }
    public string SportCode { get; set; } = string.Empty;
    public long MatchId { get; set; }
}

