using SlackBotMessages;
using SlackBotMessages.Models;

namespace AzureProject.Shared.Services;

public static class SlackService
{
    /// <summary>
    ///     A simple example of a message
    /// </summary>
    public static async Task SendMessage(string userName, string text)
    {
        var slackUrl =
            Environment.GetEnvironmentVariable(KeyVaultEnum.SlackUrl.ToString()) ?? string.Empty;
        var client = new SbmClient(slackUrl);

        var message = new Message
        {
            Username = userName,
            Text = text,
            IconEmoji = Emoji.Bell
        };

        await client.Send(message);
    }
}

//https://github.com/prjseal/SlackBotMessages/
//https://www.nuget.org/packages/SlackBotMessages
//Install-Package SlackBotMessages