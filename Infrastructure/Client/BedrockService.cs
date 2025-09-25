using Amazon;
using Amazon.BedrockAgentRuntime;
using Amazon.BedrockAgentRuntime.Model;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using Amazon.Runtime;
using Combi.Swipe.Infrastructure.CustomersProfiles;
using Combi.Swipe.Infrastructure.Selections;
using Domain.Entities;
using Infrastructure.Configuration;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Client;

public class BedrockService : IBedrockService
{
    private readonly IAmazonBedrockAgentRuntime _client;
    private readonly string _agentId;
    private readonly string _agentAliasId;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public BedrockService(AwsSettings settings)
    {

        _client = new AmazonBedrockAgentRuntimeClient(RegionEndpoint.GetBySystemName(settings.Region));

        _agentId = settings.AgentId ?? throw new ArgumentException("AWS:AgentId missing in config");
        _agentAliasId = settings.AgentAliasId ?? throw new ArgumentException("AWS:AgentAliasId missing in config");
    }


    private string BuildPrompt(NextSelectionRequest request)
    {
        try
        {
            var userId = request.UserId;

            var betHistory = File.ReadAllText(Path.Combine("Infrastructure", "CustomersProfiles", "bet_history_" + userId + ".json"));

            string CustomerAffinityJsonContent = File.ReadAllText(Path.Combine("Infrastructure", "CustomersProfiles", "customer_affinity_final_dedup.json"));

            var allCustomersAffinities = JsonSerializer.Deserialize<List<CustomerAffinityModel>>(CustomerAffinityJsonContent);

            var allCustomersAffinitiesDict = allCustomersAffinities.ToDictionary(x => x.CUSTOMER_ID);

            var customerAffinity = allCustomersAffinitiesDict[userId];

            string SelectionsJsonContent = File.ReadAllText(Path.Combine("Infrastructure", "Selections", "selections-formated 3.json"));

            List<SelectionModel> Selections = JsonSerializer.Deserialize<List<SelectionModel>>(SelectionsJsonContent);

            List<SelectionModel> PossibleSelections = Selections.Where(x => x.MatchId == request.MatchId).ToList();

            string prompt = $"""
                ## Input Data

                ### Player Profile
                ```json
                {JsonSerializer.Serialize(customerAffinity)}
                ```

                ### Available Selections
                ```json
                {JsonSerializer.Serialize(PossibleSelections)}
                ```

                ### Swip History
                ```json
                {betHistory}
                ```
                """;

            return prompt;
        }
        catch (Exception ex)
        {
            var prompt = """
                ## Input Data


                ### Player Profile
                ```json
                {
                    "risk_style": "dreamer",
                    "favorite_team": "Real Madrid",
                    "favorite_market_name": "Match Result"
                }
                ```


                ### Available Selections
                ```json
                [
                    {
                      "SELECTION_ID": "641831799014065V2",
                      "SELECTION_DESCRIPTION": "Champions League | Feyenoord - Sparta Prague | Igor Paixão / A. H. Moussa",
                      "MARKET_ID": 641831798276098,
                      "MARKET_NAME": "Either Player to Score",
                      "MATCH_ID": 631359424069632,
                      "ODDS": 1.8,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "638976494321684V2",
                      "SELECTION_DESCRIPTION": "Europa League | Ludogorets Razgrad - AZ | Ludogorets Razgrad",
                      "MARKET_ID": 638976494309378,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 638976493170688,
                      "ODDS": 3.05,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "641676457848832V2",
                      "SELECTION_DESCRIPTION": "Champions League | Juventus - Manchester City | Erling Haaland or substitute",
                      "MARKET_ID": 641669418631170,
                      "MARKET_NAME": "Goalscorer (Supersub)",
                      "MATCH_ID": 631380976377856,
                      "ODDS": 1.95,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631380995440641V2",
                      "SELECTION_DESCRIPTION": "Champions League | Milan - Red Star Belgrade | Milan or Red Star Belgrade",
                      "MARKET_ID": 631380995252224,
                      "MARKET_NAME": "Double Chance",
                      "MATCH_ID": 631380994379776,
                      "ODDS": 1.12,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "639358410764378V2",
                      "SELECTION_DESCRIPTION": "Champions League | Borussia Dortmund - FC Barcelona | Robert Lewandowski",
                      "MARKET_ID": 639358410739714,
                      "MARKET_NAME": "Goalscorer",
                      "MATCH_ID": 631380984082432,
                      "ODDS": 1.73781472,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631381115920513V2",
                      "SELECTION_DESCRIPTION": "Champions League | Arsenal - Monaco | Over 0.5",
                      "MARKET_ID": 631381116010523,
                      "MARKET_NAME": "Goal Total 0.5",
                      "MATCH_ID": 631379109036032,
                      "ODDS": 1.02,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631380983959559V2",
                      "SELECTION_DESCRIPTION": "Champions League | Borussia Dortmund - FC Barcelona | FC Barcelona",
                      "MARKET_ID": 631380984070146,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 631380984082432,
                      "ODDS": 1.73781472,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631381114106134V2",
                      "SELECTION_DESCRIPTION": "Champions League | Feyenoord - Sparta Prague | Yes",
                      "MARKET_ID": 631381113995337,
                      "MARKET_NAME": "Both Teams to Score",
                      "MATCH_ID": 631359424069632,
                      "ODDS": 1.8,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631386436677713V2",
                      "SELECTION_DESCRIPTION": "Champions League | Borussia Dortmund - FC Barcelona | Over 0.5",
                      "MARKET_ID": 631386435538957,
                      "MARKET_NAME": "Goal Total 0.5 - home",
                      "MATCH_ID": 631380984082432,
                      "ODDS": 1.17898261,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "638975950741515V2",
                      "SELECTION_DESCRIPTION": "Europa League | Hoffenheim - FCSB | Hoffenheim",
                      "MARKET_ID": 638975951056898,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 638975951032320,
                      "ODDS": 1.45,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "638976494321685V2",
                      "SELECTION_DESCRIPTION": "Europa League | Ludogorets Razgrad - AZ | Draw",
                      "MARKET_ID": 638976494309378,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 638976493170688,
                      "ODDS": 3.35,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "632071204950140V2",
                      "SELECTION_DESCRIPTION": "Champions League | Atletico Madrid - Slovan Bratislava | Over 3.5",
                      "MARKET_ID": 632071203803166,
                      "MARKET_NAME": "Goal Total 3.5",
                      "MATCH_ID": 632070859694080,
                      "ODDS": 1.7,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "633547404648454V2",
                      "SELECTION_DESCRIPTION": "EFL Cup | Tottenham - Manchester United | Manchester United",
                      "MARKET_ID": 633547404820482,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 633547403952128,
                      "ODDS": 2.86,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "633547404648452V2",
                      "SELECTION_DESCRIPTION": "EFL Cup | Tottenham - Manchester United | Tottenham",
                      "MARKET_ID": 633547404820482,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 633547403952128,
                      "ODDS": 2.08,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "634809535778950V2",
                      "SELECTION_DESCRIPTION": "Australian A-League | Central Coast Mariners - Adelaide United FC | Yes",
                      "MARKET_ID": 634809535815712,
                      "MARKET_NAME": "Both Teams to Score",
                      "MATCH_ID": 634808425545728,
                      "ODDS": 1.45,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631381542785026V2",
                      "SELECTION_DESCRIPTION": "Champions League | Juventus - Manchester City | Over 1.5",
                      "MARKET_ID": 631381542862849,
                      "MARKET_NAME": "Goal Total 1.5",
                      "MATCH_ID": 631380976377856,
                      "ODDS": 1.28,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "638976407187474V2",
                      "SELECTION_DESCRIPTION": "Europa Conference League | Dinamo Minsk - Larne | Dinamo Minsk",
                      "MARKET_ID": 638976407195667,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 638976407187456,
                      "ODDS": 1.41,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631380566749317V2",
                      "SELECTION_DESCRIPTION": "Champions League | Benfica - Bologna | Over 1.5",
                      "MARKET_ID": 631380565512221,
                      "MARKET_NAME": "Goal Total 1.5",
                      "MATCH_ID": 631379160416256,
                      "ODDS": 1.22,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631381542785024V2",
                      "SELECTION_DESCRIPTION": "Champions League | Juventus - Manchester City | Over 0.5",
                      "MARKET_ID": 631381542862848,
                      "MARKET_NAME": "Goal Total 0.5",
                      "MATCH_ID": 631380976377856,
                      "ODDS": 1.05,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631381415813122V2",
                      "SELECTION_DESCRIPTION": "Champions League | Stuttgart - Young Boys | Over 1.5",
                      "MARKET_ID": 631381415903233,
                      "MARKET_NAME": "Goal Total 1.5",
                      "MATCH_ID": 631381011222528,
                      "ODDS": 1.1045361,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "631380977598468V2",
                      "SELECTION_DESCRIPTION": "Champions League | Juventus - Manchester City | Juventus or draw",
                      "MARKET_ID": 631380976619522,
                      "MARKET_NAME": "Double Chance",
                      "MATCH_ID": 631380976377856,
                      "ODDS": 1.67630546,
                      "SPORT_ID": "FOOTBALL"
                    },
                    {
                      "SELECTION_ID": "638976141987853V2",
                      "SELECTION_DESCRIPTION": "Europa League | Olympiakos - Twente | Olympiakos",
                      "MARKET_ID": 638976141582353,
                      "MARKET_NAME": "Match Result",
                      "MATCH_ID": 638976141905920,
                      "ODDS": 1.77,
                      "SPORT_ID": "FOOTBALL"
                    }
                  ]
                ```;


                ### Swip History
                ```json
                [
                  {
                    "description": "Champions League | Feyenoord - Sparta Prague | Igor Paixão / A. H. Moussa",
                    "market_name": "Either Player to Score",
                    "odds": 1.8,
                    "accepted": true
                  },
                  {
                    "description": "Europa League | Ludogorets Razgrad - AZ | Ludogorets Razgrad",
                    "market_name": "Match Result",
                    "odds": 3.05,
                    "accepted": false
                  },
                  {
                    "description": "Champions League | Juventus - Manchester City | Erling Haaland or substitute",
                    "market_name": "Goalscorer (Supersub)",
                    "odds": 1.95,
                    "accepted": true
                  },
                  {
                    "description": "Champions League | Milan - Red Star Belgrade | Milan or Red Star Belgrade",
                    "market_name": "Double Chance",
                    "odds": 1.12,
                    "accepted": false
                  },
                  {
                    "description": "Champions League | Borussia Dortmund - FC Barcelona | Robert Lewandowski",
                    "market_name": "Goalscorer",
                    "odds": 1.73781472,
                    "accepted": true
                  }
                ]
                """;

            return prompt;
        }

    }

    public async Task<List<NextSelectionResponse>> GetNextSelectionsAsync(NextSelectionRequest request)
    {
        var prompt = BuildPrompt(request);

        var invokeReq = new InvokeAgentRequest
        {
            AgentId = _agentId,
            AgentAliasId = _agentAliasId,
            SessionId = Guid.NewGuid().ToString(),
            InputText = prompt
        };

        var response = await _client.InvokeAgentAsync(invokeReq);

        // Extract the response body from the completion stream
        var responseBody = new StringBuilder();
        
        await foreach (var chunk in response.Completion)
        {   
            if (chunk is Amazon.BedrockAgentRuntime.Model.PayloadPart payloadPart)
            {
                var chunkText = Encoding.UTF8.GetString(payloadPart.Bytes.ToArray());
                responseBody.Append(chunkText);
            }
        }

        var body = responseBody.ToString();

        try
        {
            return JsonSerializer.Deserialize<List<NextSelectionResponse>>(body, _jsonOptions)
                   ?? new List<NextSelectionResponse>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BedrockService] JSON parse error: {ex.Message}");
            Console.WriteLine(body);
            return new List<NextSelectionResponse>();
        }
    }

}
