using Domain.Entities;

namespace Application.Interfaces;

public interface ISelectionService
{
    Task<List<NextSelectionResponse>> GetNextCombinedSelectionsAsync(NextSelectionRequest request);
}
