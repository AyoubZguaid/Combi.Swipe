using Amazon;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using Amazon.Runtime;
using Domain.Entities;
using Infrastructure.Configuration;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Client;

public class BedrockService : IBedrockService
{
    private readonly IAmazonBedrockRuntime _client;
    private readonly string _modelId;

    public BedrockService(AwsSettings settings)
    {
        if (!string.IsNullOrEmpty(settings.AccessKey) && !string.IsNullOrEmpty(settings.SecretKey))
        {
            var creds = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
            _client = new AmazonBedrockRuntimeClient(creds, RegionEndpoint.GetBySystemName(settings.Region));
        }
        else
        {
            _client = new AmazonBedrockRuntimeClient(RegionEndpoint.GetBySystemName(settings.Region));
        }

        _modelId = settings.ModelId;
    }

    public async Task<List<NextSelectionResponse>> GetNextSelectionsAsync(NextSelectionRequest request)
    {
        var prompt = $@"
                        En fonction des paramètres suivants : 
                        userId={request.UserId}, sportCode={request.SportCode}, matchId={request.MatchId}
                        Retourne uniquement un tableau JSON avec : selectionId, code, label.
                        ";

        var modelRequest = new InvokeModelRequest
        {
            ModelId = _modelId,
            ContentType = "application/json",
            Accept = "application/json",
            Body = new MemoryStream(Encoding.UTF8.GetBytes(
                $"{{ \"prompt\": \"{prompt}\", \"max_tokens_to_sample\": 300 }}"
            ))
        };

        var response = await _client.InvokeModelAsync(modelRequest);

        using var reader = new StreamReader(response.Body);
        var body = await reader.ReadToEndAsync();

        try
        {
            return JsonSerializer.Deserialize<List<NextSelectionResponse>>(body)
                   ?? new List<NextSelectionResponse>();
        }
        catch
        {
            return new List<NextSelectionResponse>();
        }
    }
}
