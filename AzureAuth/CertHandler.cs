using System.CommandLine;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;

namespace AzureAuth;

public static class CertHandler
{
    public static Option<string> CertThumbprint = new(["--cert"], "Certificate thumbprint used for authentication")
    {
        IsRequired = true
    };
    
    public static async Task Handle(string tenantId, string clientId, string certThumbprint, string[] scopes)
    {
        var cert = GetCertificate(certThumbprint);
        var credential = new ClientCertificateCredential(tenantId, clientId, cert);
        var token = await credential.GetTokenAsync(new(scopes));
        Console.Write(token.Token);
    }

    private static X509Certificate2 GetCertificate(string thumbprint)
    {
        using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser, OpenFlags.ReadOnly);
        var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
        return certificates.First();
    }
}