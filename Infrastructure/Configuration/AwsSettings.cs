namespace Infrastructure.Configuration;

public class AwsSettings
{
    public required string Region { get; set; } = string.Empty;
    public required string ModelId { get; set; } = string.Empty;
    public required string AccessKey { get; set; } = string.Empty;
    public required string SecretKey { get; set; } = string.Empty;
}