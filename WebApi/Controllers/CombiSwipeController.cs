using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CombiSwipeController : ControllerBase
{
    private readonly ISelectionService _selectionService;

    public CombiSwipeController(ISelectionService selectionService)
    {
        _selectionService = selectionService;
    }

    /// <summary>
    /// Retourne les prochaines sélections combinées en appelant AWS Bedrock.
    /// </summary>
    /// <param name="request">Objet contenant userId, selectionIds[], sportCode, matchId</param>
    /// <returns>Liste d’objets [selectionId, code, label]</returns>
    [HttpPost("GetNextCombinedSelections")]
    [ProducesResponseType(typeof(List<NextSelectionResponse>), 200)]
    public async Task<ActionResult<List<NextSelectionResponse>>> GetNextCombinedSelections([FromBody] NextSelectionRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.UserId))
            return BadRequest("Invalid request payload.");

        var result = await _selectionService.GetNextCombinedSelectionsAsync(request);
        return Ok(result);
    }
}