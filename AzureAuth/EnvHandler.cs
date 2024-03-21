using Azure.Identity;

namespace AzureAuth;

public static class EnvHandler
{
    public static async Task Handle(string[] scopes)
    {
        var credential = new EnvironmentCredential();
        var token = await credential.GetTokenAsync(new(scopes));
        Console.Write(token.Token);
    }
}