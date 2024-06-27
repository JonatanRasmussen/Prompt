using System;
using System.Collections.Generic;
namespace GlobalNameSpace;

public enum ApiAccessStrategy
{
    Empty,
    ChatGPT,
    Claude3,
}

public record ApiAccess
(
    ApiAccessStrategy Strategy,
    string Endpoint,
    string ApiKey,
    ChatRoles ChatRoles
)
{
    public static ApiAccess CreateEmpty()
    {
        ApiAccessStrategy strategy = ApiAccessStrategy.Empty;
        string endpoint = string.Empty;
        string apiKey = string.Empty;
        ChatRoles chatRoles = new(string.Empty, string.Empty, string.Empty);

        return new ApiAccess(
            strategy,
            endpoint,
            apiKey,
            chatRoles
        );
    }
}

public static class ApiAccessStrategySwitch
{
    public static bool AccessApi(Completion completion)
    {
        ApiAccessStrategy api = completion.Prompt.AiModel.ApiAccess.Strategy;
        bool success = api switch
        {
            ApiAccessStrategy.Empty => InvalidType(completion),
            ApiAccessStrategy.ChatGPT => ExecuteChatGPTStrategy(completion),
            ApiAccessStrategy.Claude3 => ExecuteClaude3Strategy(completion),
            _ => UnknownType(completion),
        };
        return success;
    }

    public static bool ExecuteChatGPTStrategy(Completion completion)
    {
        return ApiCaller.CallChatGPTApi(completion);
    }

    public static bool ExecuteClaude3Strategy(Completion completion)
    {
        return ApiCaller.CallChatGPTApi(completion);
    }

    private static bool InvalidType(Completion completion)
    {
        string errorMsg = $"BudoError: Invalid type {nameof(completion.Prompt.AiModel.ApiAccess.Strategy)}";
        completion.Response = errorMsg;
        Console.WriteLine(errorMsg);
        throw new ArgumentException(errorMsg);
    }

    private static bool UnknownType(Completion completion)
    {
        string errorMsg = $"BudoError: Unknown type {nameof(completion.Prompt.AiModel.ApiAccess.Strategy)}";
        completion.Response = errorMsg;
        Console.WriteLine(errorMsg);
        throw new ArgumentOutOfRangeException(errorMsg);
    }
}
