namespace Infrastructure.Configuration;

public class AwsSettings
{
    public required string Region { get; set; } = string.Empty;
    public required string AgentId { get; set; } = string.Empty;
    public required string AgentAliasId { get; set; } = string.Empty;
}