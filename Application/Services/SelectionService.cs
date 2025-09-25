using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Client;

namespace Application.Services;

public class SelectionService : ISelectionService
{
    private readonly IBedrockService _bedrock;

    public SelectionService(IBedrockService bedrock)
    {
        _bedrock = bedrock;
    }

    public async Task<List<NextSelectionResponse>> GetNextCombinedSelectionsAsync(NextSelectionRequest request)
    {
        return await _bedrock.GetNextSelectionsAsync(request);
    }
}