using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace GlobalNameSpace;

public class Prompt
{
    public AiModel AiModel { get; set; } = AiModel.CreateEmpty();
    public bool StreamingEnabled { get; set; } = false;
    public int? MaxOutputTokens { get; set; } = null;
    public double? Temperature { get; set; } = null;
    public string SystemInstruction { get; set; } = string.Empty;
    public ChatMessage Message { get; set; } = ChatMessage.CreateEmpty();
    public List<MessageBlock> MessageBlocks { get; set; } = new();
    public List<ChatMessage> PreviousMessages { get; set; } = new();

    public Prompt() { }

    public Completion CreateMockCompletion()
    {
        BuildMessage();
        Completion completion = new(this);
        completion.FillWithMockData();
        return completion;
    }

    public Completion CompletePrompt()
    {
        BuildMessage();
        Completion completion = new(this);
        completion.AccessApi();
        return completion;
    }

    public void BuildMessage()
    {
        StringBuilder sb = new();
        foreach (var block in MessageBlocks)
        {
            if (block.ContentIsEmpty())
            {
                block.GenerateContent();
            }
            if (block.IsValid)
            {
                sb.Append(block.Content);
            }
        }
        string role = AiModel.ApiAccess.ChatRoles.UserRole;
        Message = new(role, sb.ToString());
    }

    public List<object> MessagesToJsonObject()
    {
        var jsonMessages = new List<object>();
        foreach (ChatMessage previousMessage in PreviousMessages)
        {
            jsonMessages.Add(previousMessage.ToJsonObject());
        }
        BuildMessage();
        jsonMessages.Add(Message.ToJsonObject());
        return jsonMessages;
    }

    public string FormatJsonRequest()
    {
        var requestBody = new
        {
            model = AiModel.ModelName,
            messages = MessagesToJsonObject(),
            max_tokens = MaxOutputTokens != null ? MaxOutputTokens : null,
            stream = StreamingEnabled,
            temperature = Temperature != null ? Temperature : null
        };
        return JsonSerializer.Serialize(requestBody);
    }
}
