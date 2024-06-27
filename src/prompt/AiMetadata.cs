using System.Text.Json;
namespace GlobalNameSpace
{
    public class AiMetadata
    {
        public string Model { get; set; }
        public string Id { get; set; }
        public string RequestType { get; set; }
        public string StopReason { get; set; }
        public string TimeCreated { get; set; }
        public int InputTokens { get; set; }
        public int OutputTokens { get; set; }
        public int MaxTokens { get; set; }
        public double Temperature { get; set; }
        public bool Stream { get; set; }
        public double InputPriceInUSD { get; set; }
        public double OutputPriceInUSD { get; set; }

        public AiMetadata(AiRequest req, IAiResponse response)
        {
            Model = response.GetModel();
            Id = response.GetId();
            RequestType = response.GetRequestType();
            StopReason = response.GetStopReason();
            TimeCreated = response.GetTimeCreated();
            InputTokens = response.GetInputTokens();
            OutputTokens = response.GetOutputTokens();
            MaxTokens = req.MaxOutputTokens;
            Temperature = req.Temperature;
            Stream = req.Stream;
            InputPriceInUSD = InputCost(response.GetInputTokens(), req.Model);
            OutputPriceInUSD = OutputCost(response.GetOutputTokens(), req.Model);
        }

        public static AiMetadata CreateEmpty()
        {
            IAiModel model = new AiEmptyModel();
            AiRequest req = new(model);
            IAiResponse response = new AiErrorResponse(string.Empty);
            return new AiMetadata(req, response);
        }

        // Method to convert the object to JSON string
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        // Method to create an object from JSON string
        public static AiMetadata FromJson(string json)
        {
            return JsonSerializer.Deserialize<AiMetadata>(json) ?? CreateEmpty();
        }

        private static double InputCost(int tokens, IAiModel model)
        {
            return PromptCostCalculator.CalculateCost(tokens, model.InputPricePerMTokensInUSD);
        }

        private static double OutputCost(int tokens, IAiModel model)
        {
            return PromptCostCalculator.CalculateCost(tokens, model.InputPricePerMTokensInUSD);
        }
    }
}
