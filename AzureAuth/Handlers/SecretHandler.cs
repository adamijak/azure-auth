using System.CommandLine;
using Azure.Identity;
using AzureAuth.Extensions;

namespace AzureAuth.Handlers;

public static class SecretHandler
{
    public static readonly Option<string> Secret = new(["--secret"], "client secret")
    {
        IsRequired = true
    };
    
    public static async Task Handle(bool raw, string tenantId, string clientId, string secret, string[] scopes)
    {
        var credential = new ClientSecretCredential(tenantId, clientId, secret);
        var token = await credential.GetTokenAsync(new(scopes));
        token.ConsoleWrite(raw);
    }
}