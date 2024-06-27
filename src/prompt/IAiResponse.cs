namespace GlobalNameSpace;

public interface IAiResponse
{
    ChatMessage GetMessage();
    string GetId();
    string GetModel();
    string GetRequestType();
    string GetStopReason();
    string GetTimeCreated();
    int GetInputTokens();
    int GetOutputTokens();
}

public class AiErrorResponse : IAiResponse
{
    private readonly string errorMsg;

    public AiErrorResponse(string errorMsg)
    {
        this.errorMsg = errorMsg;
    }

    public ChatMessage GetMessage() => new ChatMessage("Error", errorMsg);
    public string GetId() => string.Empty;
    public string GetModel() => string.Empty;
    public string GetRequestType() => string.Empty;
    public string GetStopReason() => string.Empty;
    public string GetTimeCreated() => string.Empty;
    public int GetInputTokens() => 0;
    public int GetOutputTokens() => 0;
}