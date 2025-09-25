using Domain.Entities;

namespace Infrastructure.Client;

public interface IBedrockService
{
    Task<List<NextSelectionResponse>> GetNextSelectionsAsync(NextSelectionRequest request);
}
