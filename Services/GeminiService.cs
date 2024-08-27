
using Google.Cloud.AIPlatform.V1;
namespace WEB_APP_Panaderia.Services
{
    public class GeminiService
    {
        private readonly PredictionServiceClient _predictionServiceClient;
        private readonly string _projectId;
        private readonly string _location;
        private readonly string _publisher;
        private readonly string _model;

        public GeminiService(IConfiguration configuration)
        {
            _projectId = configuration["GeminiSettings:ProjectId"];
            _location = configuration["GeminiSettings:Location"];
            _publisher = configuration["GeminiSettings:Publisher"];
            _model = configuration["GeminiSettings:Model"];

            _predictionServiceClient = new PredictionServiceClientBuilder
            {
                Endpoint = $"{_location}-aiplatform.googleapis.com"
            }.Build();
        }

        public async Task<string> GetRecommendation(string prompt)
        {
            var generateContentRequest = new GenerateContentRequest
            {
                Model = $"projects/{_projectId}/locations/{_location}/publishers/{_publisher}/models/{_model}",
                Contents =
                {
                    new Content
                    {
                        Role = "USER",
                        Parts =
                        {
                            new Part { Text = prompt }
                        }
                    }
                }
            };

            GenerateContentResponse response = await _predictionServiceClient.GenerateContentAsync(generateContentRequest);
            return response.Candidates[0].Content.Parts[0].Text;
        }
    }
}