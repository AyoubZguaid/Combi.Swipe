namespace Domain.Entities;

public class NextSelectionRequest
{
    public string UserId { get; set; } = string.Empty;
    public string SportCode { get; set; } = string.Empty;
    public string MatchId { get; set; } = string.Empty;
}

