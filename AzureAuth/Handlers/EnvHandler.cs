using Azure.Identity;
using AzureAuth.Extensions;

namespace AzureAuth.Handlers;

public static class EnvHandler
{
    public static async Task Handle(bool raw, string[] scopes)
    {
        var credential = new EnvironmentCredential();
        var token = await credential.GetTokenAsync(new(scopes));
        token.ConsoleWrite(raw);
    }
}