using System;
using System.Collections.Generic;
using GlobalNameSpace;

public class ChatGPT
{
    public static readonly ApiAccess ChatGPTApiAccess = CreateChatGPTApiAccess();
    public static readonly AiModel Gpt4o = CreateGpt4o();
    public static readonly AiModel Gpt4Turbo = CreateGpt4Turbo();
    public static readonly AiModel Gpt35Turbo = CreateGpt35Turbo();

    private static ApiAccess CreateChatGPTApiAccess()
    {
        ApiAccessStrategy strategy = ApiAccessStrategy.ChatGPT;
        string endpoint = "https://api.openai.com/v1/chat/completions";
        string apiKey = MyLocalConfigs.ApiKeyOpenAI;
        ChatRoles chatRoles = new("user", "assistant", "system");

        return new ApiAccess(
            strategy,
            endpoint,
            apiKey,
            chatRoles
        );
    }

    private static AiModel CreateGpt4o()
    {
        string modelName = "gpt-4o";
        string displayName = "ChatGPT-4o";
        ApiAccess apiAccess = ChatGPTApiAccess;
        int contextWindow = 128000;
        int maxOutputTokens = 4096;
        double inputPricePerMTokensInUSD = 5.00;
        double outputPricePerMTokensInUSD = 15.00;
        DateTime trainingCutoff = new(2023, 12, 1);

        return new AiModel
        (
            modelName,
            displayName,
            apiAccess,
            contextWindow,
            maxOutputTokens,
            inputPricePerMTokensInUSD,
            outputPricePerMTokensInUSD,
            trainingCutoff
        );
    }

    private static AiModel CreateGpt4Turbo()
    {
        string modelName = "gpt-4-turbo";
        string displayName = "ChatGPT-4 Turbo";
        ApiAccess apiAccess = ChatGPTApiAccess;
        int contextWindow = 128000;
        int maxOutputTokens = 4096;
        double inputPricePerMTokensInUSD = 10.00;
        double outputPricePerMTokensInUSD = 30.00;
        DateTime trainingCutoff = new(2023, 12, 1);

        return new AiModel
        (
            modelName,
            displayName,
            apiAccess,
            contextWindow,
            maxOutputTokens,
            inputPricePerMTokensInUSD,
            outputPricePerMTokensInUSD,
            trainingCutoff
        );
    }

    private static AiModel CreateGpt35Turbo()
    {
        string modelName = "gpt-3.5-turbo";
        string displayName = "ChatGPT-3.5 Turbo";
        ApiAccess apiAccess = ChatGPTApiAccess;
        int contextWindow = 16385;
        int maxOutputTokens = 4096;
        double inputPricePerMTokensInUSD = 0.50;
        double outputPricePerMTokensInUSD = 1.50;
        DateTime trainingCutoff = new(2021, 9, 1);

        return new AiModel
        (
            modelName,
            displayName,
            apiAccess,
            contextWindow,
            maxOutputTokens,
            inputPricePerMTokensInUSD,
            outputPricePerMTokensInUSD,
            trainingCutoff
        );
    }

}
