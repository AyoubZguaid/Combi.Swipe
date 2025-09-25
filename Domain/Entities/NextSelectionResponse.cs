namespace Domain.Entities;

public class NextSelectionResponse
{
    public long Market_Id { get; set; }
    public long Selection_Id { get; set; }
    public string Market_Name { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public int Relevancy_Score { get; set; }
}
