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
                MarketName = "Bons Plans",
                Label = "Pari audacieux : Morata et Leão en feu pour une soirée de buts !",
                Relevancy_Score = 85
            },
            new NextSelectionResponse
            {
                MarketId = 899186208231424,
                SelectionId = 899186209284097,
                MarketName = "Both Teams to Score",
                Label = "Les deux équipes marqueront : un festival de buts en perspective !",
                Relevancy_Score = 78
            },
            new NextSelectionResponse
            {
                MarketId = 894679767126056,
                SelectionId = 894679767986194,
                MarketName = "Goalscorer",
                Label = "Morata, le chasseur de buts : prêt à frapper encore !",
                Relevancy_Score = 72
            },
            new NextSelectionResponse
            {
                MarketId = 902544252948480,
                SelectionId = 902544655601667,
                MarketName = "Double Chance",
                Label = "Sécurité maximale : Milan ou Belgrade pour la victoire !",
                Relevancy_Score = 65
            },
            new NextSelectionResponse
            {
                MarketId = 902277543448584,
                SelectionId = 902277544501354,
                MarketName = "Goal Total 1.5",
                Label = "Plus de 1,5 buts : un minimum pour un match explosif !",
                Relevancy_Score = 62
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380248,
                SelectionId = 894681716240498,
                MarketName = "Double Chance",
                Label = "Milan ne perdra pas : pari sûr pour les audacieux !",
                Relevancy_Score = 60
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380257,
                SelectionId = 894681716429076,
                MarketName = "TBD",
                Label = "Mystère à venir : restez à l'affût pour cette opportunité !",
                Relevancy_Score = 50
            },
            new NextSelectionResponse
            {
                MarketId = 894681715191824,
                SelectionId = 894681716428922,
                MarketName = "TBD",
                Label = "Surprise en vue : un pari intriguant se prépare !",
                Relevancy_Score = 50
            },
            new NextSelectionResponse
            {
                MarketId = 894681715380229,
                SelectionId = 894681716428875,
                MarketName = "TBD",
                Label = "L'inconnu vous attend : osez l'aventure du pari mystère !",
                Relevancy_Score = 50
            },
            new NextSelectionResponse
            {
                MarketId = 894681715191892,
                SelectionId = 894681716429008,
                MarketName = "TBD",
                Label = "Pari énigmatique : la surprise qui pourrait tout changer !",
                Relevancy_Score = 50
            }
        };

        return Ok(result);
    }
}