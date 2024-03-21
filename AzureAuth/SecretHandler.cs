using System.CommandLine;
using Azure.Identity;

namespace AzureAuth;

public static class SecretHandler
{
    public static Option<string> Secret = new(["--secret"], "Client secret used for authentication")
    {
        IsRequired = true
    };
    
    public static async Task Handle(string tenantId, string clientId, string secret, string[] scopes)
    {
        var credential = new ClientSecretCredential(tenantId, clientId, secret);
        var token = await credential.GetTokenAsync(new(scopes));
        Console.Write(token.Token);
    }
}