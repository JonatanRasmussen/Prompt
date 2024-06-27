using System;
using System.Collections.Generic;
namespace GlobalNameSpace;

public enum BlockTypes
{
    PlainText,
}

public class MessageBlock
{
    public BlockTypes Type { get; private set; } = BlockTypes.PlainText;
    public bool IsEnabled { get; set; } = true;
    public bool IsValid { get; private set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public MessageBlock()
    {
        ResetAllProperties();
    }

    public void GenerateContent()
    {
        ResetContentRelatedProperties();
        bool success = MessageBlockStrategySwitch.GenerateContent(this);
        if (success)
        {
            IsValid = true;
        }
        else
        {
            Content = string.Empty;
            IsValid = false;
        }
    }

    public bool ContentIsEmpty()
    {
        return Content == string.Empty;
    }

    public void ChangeType(BlockTypes blockType)
    {
        ResetAllProperties();
        Type = blockType;
    }

    private void ResetAllProperties()
    {
        Type = BlockTypes.PlainText;
        IsEnabled = true;
        Input = string.Empty;
        ResetContentRelatedProperties();
    }

    private void ResetContentRelatedProperties()
    {
        IsValid = false;
        ErrorMessage = string.Empty;
        Content = string.Empty;
    }
}

public static class MessageBlockStrategySwitch
{
    public static bool GenerateContent(MessageBlock msgBlock)
    {
        BlockTypes blockType = msgBlock.Type;
        bool success = blockType switch
        {
            BlockTypes.PlainText => ExecutePlainTextStrategy(msgBlock),
            _ => UnknownType(msgBlock),
        };
        return success;
    }

    private static bool ExecutePlainTextStrategy(MessageBlock msgBlock)
    {
        msgBlock.Content = msgBlock.Input;
        return true;
    }

    private static bool UnknownType(MessageBlock msgBlock)
    {
        string errorMsg = $"BudoError: Unknown type {nameof(msgBlock.Type)}";
        msgBlock.ErrorMessage = errorMsg;
        Console.WriteLine(errorMsg);
        throw new ArgumentOutOfRangeException(errorMsg);
    }
}