using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GlobalNameSpace;

public static class ApiCaller
{
    public static bool CallChatGPTApi(Completion completion)
    {
        string apiKey = completion.Prompt.AiModel.ApiAccess.ApiKey;
        string endpoint = completion.Prompt.AiModel.ApiAccess.Endpoint;
        List<KeyValuePair<string, string>> headerData = new()
        {
            new ("Authorization", "Bearer " + apiKey)
        };
        return CallAPI<ChatGPTChatCompletion>(endpoint, headerData, completion);
    }

    public static bool CallClaude3Api(Completion completion)
    {
        string apiKey = completion.Prompt.AiModel.ApiAccess.ApiKey;
        string endpoint = completion.Prompt.AiModel.ApiAccess.Endpoint;
        string anthropicVersion = "2023-06-01";
        List<KeyValuePair<string, string>> headerData = new()
        {
            new ("x-api-key", apiKey),
            new ("anthropic-version", anthropicVersion)
        };
        return CallAPI<ClaudeChatCompletion>(endpoint, headerData, completion);
    }

/*         public static async Task RequestChatCompletionStream(Completion completion)
    {
        string apiKey = completion.Prompt.AiModel.ApiAccess.ApiKey;
        string endpoint = completion.Prompt.AiModel.ApiAccess.Endpoint;
        List<KeyValuePair<string, string>> headerData = new()
        {
            new("Authorization", "Bearer " + apiKey)
        };
        await ApiAccessUtils.StreamAPIAsync(endpoint, headerData, completion);
    } */

    private static bool CallAPI<T>(string endpoint, List<KeyValuePair<string, string>> headerData, Completion completion) where T : IAiResponse, new()
    {
        bool success;
        try
        {
            using HttpClient client = new();
            foreach (var headerPair in headerData)
            {
                client.DefaultRequestHeaders.Add(headerPair.Key, headerPair.Value);
            }
            string requestBody = completion.Prompt.FormatJsonRequest();
            StringContent content = new(requestBody, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = client.PostAsync(endpoint, content).Result;
            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().Result;
            IAiResponse aiResponse = JsonSerializer.Deserialize<T>(responseString) ?? new T();
            completion.ParseResponse(responseString, aiResponse);
            success = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("BudoError: " + ex.Message);
            success = false;
        }
        return success;
    }
}
