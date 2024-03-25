using System.CommandLine;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using AzureAuth.Extensions;

namespace AzureAuth.Handlers;

public static class CertHandler
{
    public static readonly Option<string> CertThumbprint = new(["--cert"], "client certificate")
    {
        IsRequired = true
    };
    
    public static async Task Handle(bool raw, string tenantId, string clientId, string certThumbprint, string[] scopes)
    {
        var cert = GetCertificate(certThumbprint);
        var credential = new ClientCertificateCredential(tenantId, clientId, cert);
        var token = await credential.GetTokenAsync(new(scopes));
        token.ConsoleWrite(raw);
    }

    private static X509Certificate2 GetCertificate(string thumbprint)
    {
        using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser, OpenFlags.ReadOnly);
        var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
        return certificates.First();
    }
}