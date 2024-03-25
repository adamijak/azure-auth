using Azure.Core;

namespace AzureAuth.Extensions;

public static class AccessTokenExtensions
{
    public static void ConsoleWrite(this AccessToken token, bool raw)
    {
        if (raw)
        {
            Console.Write(token.Token);
        }
        else
        {
            Console.WriteLine("expires on: {0}", token.ExpiresOn);
            Console.WriteLine("token: {0}", token.Token);
        }
    }
}