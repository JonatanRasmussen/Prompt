using System;
using System.Collections.Generic;
using GlobalNameSpace;

public class Claude3
{
    public static readonly ApiAccess Claude3ApiAccess = CreateClaude3ApiAccess();
    public static readonly AiModel Claude3Opus = CreateClaude3Opus();
    public static readonly AiModel Claude3Sonnet = CreateClaude3Sonnet();
    public static readonly AiModel Claude3Haiku = CreateClaude3Haiku();

    private static ApiAccess CreateClaude3ApiAccess()
    {
        ApiAccessStrategy strategy = ApiAccessStrategy.Claude3;
        string endpoint = "https://api.anthropic.com/v1/messages";
        string apiKey = MyLocalConfigs.ApiKeyAnthropic;
        ChatRoles chatRoles = new("user", "assistant", "system");

        return new ApiAccess(
            strategy,
            endpoint,
            apiKey,
            chatRoles
        );
    }

    private static AiModel CreateClaude3Opus()
    {
        string modelName = "claude-3-opus-20240229";
        string displayName = "Claude-3 Opus";
        ApiAccess apiAccess = Claude3ApiAccess;
        int contextWindow = 200000;
        int maxOutputTokens = 4096;
        double inputPricePerMTokensInUSD = 15.00;
        double outputPricePerMTokensInUSD = 75.00;
        DateTime trainingCutoff = new(2023, 8, 1);
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

    private static AiModel CreateClaude3Sonnet()
    {
        string modelName = "claude-3-sonnet-20240229";
        string displayName = "Claude-3 Sonnet";
        ApiAccess apiAccess = Claude3ApiAccess;
        int contextWindow = 200000;
        int maxOutputTokens = 4096;
        double inputPricePerMTokensInUSD = 3.00;
        double outputPricePerMTokensInUSD = 15.00;
        DateTime trainingCutoff = new(2023, 8, 1);

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

    private static AiModel CreateClaude3Haiku()
    {
        string modelName = "claude-3-haiku-20240307";
        string displayName = "Claude 3 Haiku";
        ApiAccess apiAccess = Claude3ApiAccess;
        int contextWindow = 200000;
        int maxOutputTokens = 4096;
        double inputPricePerMTokensInUSD = 0.25;
        double outputPricePerMTokensInUSD = 1.25;
        DateTime trainingCutoff = new(2023, 8, 1);

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