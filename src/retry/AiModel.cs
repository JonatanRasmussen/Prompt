using System;
using System.Collections.Generic;
namespace GlobalNameSpace;

public record AiModel
(
    string ModelName,
    string DisplayName,
    ApiAccess ApiAccess,
    int ContextWindow,
    int MaxOutputTokens,
    double InputPricePerMTokensInUSD,
    double OutputPricePerMTokensInUSD,
    DateTime TrainingCutoff
)
{
    public static AiModel CreateEmpty()
    {
        string modelName = "empty";
        string displayName = "Empty";
        ApiAccess apiAccess = ApiAccess.CreateEmpty();
        int contextWindow = 0;
        int maxOutputTokens = 0;
        double inputPricePerMTokensInUSD = 0.0;
        double outputPricePerMTokensInUSD = 0.0;
        DateTime trainingCutoff = DateTime.Now;

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

