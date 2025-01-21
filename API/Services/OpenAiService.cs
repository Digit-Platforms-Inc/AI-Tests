using OpenAI.Chat;

namespace API.Services;

public class OpenAiService
{
    private readonly ChatClient _chatClient;

    public OpenAiService(IConfiguration configuration)
    {
        _chatClient = new ChatClient(model: "gpt-4o-mini", apiKey: configuration["OpenAiKey"]);
    }

    public async Task<string> GenerateResponse(string prompt)
    {
        var completion = await _chatClient.CompleteChatAsync(prompt);

        var response = completion.Value.Content[0].Text;

        return response;
    }
}