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
        //if (request == null || string.IsNullOrWhiteSpace(request.UserId))
        //    return BadRequest("Invalid request payload.");

        //var result = await _selectionService.GetNextCombinedSelectionsAsync(request);

        var result = new List<NextSelectionResponse>
        {
            new NextSelectionResponse
            {
                MarketId = 894679767126058,
                SelectionId = 894679767986199,
                Label = "Selection 894679767986199"
            },
            new NextSelectionResponse
            {
                MarketId = 899186208231424,
                SelectionId = 899186209284097,
                Label = "Selection 899186209284097"
            },
            new NextSelectionResponse
            {
                MarketId = 894679767126056,
                SelectionId = 894679767986194,
                Label = "Selection 894679767986194"
            },
            new NextSelectionResponse
            {
                MarketId = 902544252948480,
                SelectionId = 902544655601667,
                Label = "Selection 902544655601667"
            },
            new NextSelectionResponse
            {
                MarketId = 902277543448584,
                SelectionId = 902277544501354,
                Label = "Selection 902277544501354"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380248,
                SelectionId = 894681716240498,
                Label = "Selection 894681716240498"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380257,
                SelectionId = 894681716429076,
                Label = "Selection 894681716429076"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715191824,
                SelectionId = 894681716428922,
                Label = "Selection 894681716428922"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380229,
                SelectionId = 894681716428875,
                Label = "Selection 894681716428875"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715191892,
                SelectionId = 894681716429008,
                Label = "Selection 894681716429008"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380256,
                SelectionId = 894681716429053,
                Label = "Selection 894681716429053"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380246,
                SelectionId = 894681716240491,
                Label = "Selection 894681716240491"
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380226,
                SelectionId = 894681716428816,
                Label = "Selection 894681716428816"
            }
        };

        return Ok(result);
    }
}