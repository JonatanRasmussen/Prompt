using System;
using System.Collections.Generic;
using System.Text.Json;
namespace GlobalNameSpace;

public class Completion
{
    public Prompt Prompt { get; }
    public bool CompletionIsFinished { get; set; } = false;
    public List<string> StreamedTokens { get; set; } = new();
    public string Response { get; set; } = string.Empty;
    public string RawApiResponse { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
    public string StopReason { get; set; } = string.Empty;
    public string TimeCreated { get; set; } = string.Empty;
    public int InputTokens { get; set; } = 0;
    public int OutputTokens { get; set; } = 0;

    public Completion(Prompt prompt)
    {
        Prompt = prompt;
    }

    public static Completion CreateEmpty()
    {
        Prompt prompt = new();
        return new(prompt);
    }

    public static Completion FromJson(string json)
    {
        return JsonSerializer.Deserialize<Completion>(json) ?? CreateEmpty();
    }

    public void FillWithMockData()
    {
        string response = "hey this is a test. This was the prompt: \n";
        string promptMessage = Prompt.Message.Content;
        Response = response + promptMessage;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public bool AccessApi()
    {
        bool success = ApiAccessStrategySwitch.AccessApi(this);
        if (success)
        {
            CompletionIsFinished = true;
            return true;
        }
        Response = string.Empty;
        return false;
    }

    public void ParseResponse(string responseString, IAiResponse response)
    {
        CompletionIsFinished = true;
        RawApiResponse = responseString;
        Response = response.GetMessage().Content;
        Id = response.GetId();
        Model = response.GetModel();
        RequestType = response.GetRequestType();
        StopReason = response.GetStopReason();
        TimeCreated = response.GetTimeCreated();
        InputTokens = response.GetInputTokens();
        OutputTokens = response.GetOutputTokens();
    }

    public void PrintCost()
    {
        AiModel aiModel = Prompt.AiModel;
        string totalCost = PromptCostCalculator.GetCost(aiModel, InputTokens, OutputTokens);
        Console.WriteLine(totalCost);
    }
}