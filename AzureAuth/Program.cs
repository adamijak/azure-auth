// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using AzureAuth;
using AzureAuth.Handlers;

var raw = new Option<bool>(["-r", "--raw"], "display raw token");

var tenantId = new Option<string>(["-t", "--tenant"], "tenant ID")
{
    IsRequired = true
};
var clientId = new Option<string>(["-c", "--client"], "client ID")
{
    IsRequired = true
};


var scopes = new Option<string[]>(["--scopes"])
{
    AllowMultipleArgumentsPerToken = true,
    Arity = ArgumentArity.OneOrMore,
    IsRequired = true,
};

var secretCmd = new Command("secret", "authenticate using client secret")
{
    tenantId,
    clientId,
    SecretHandler.Secret,
    scopes,
};

var certCmd = new Command("cert", "authenticate using client certificate")
{
    tenantId,
    clientId,
    CertHandler.CertThumbprint,
    scopes,
};

var envCmd = new Command("env", "authenticate using env variables as described here https://learn.microsoft.com/en-us/dotnet/api/azure.identity.environmentcredential")
{
    scopes,
};

certCmd.SetHandler(CertHandler.Handle, raw, tenantId, clientId, CertHandler.CertThumbprint, scopes);
envCmd.SetHandler(EnvHandler.Handle, raw, scopes);
secretCmd.SetHandler(SecretHandler.Handle, raw, tenantId, clientId, SecretHandler.Secret, scopes);

var rootCmd = new RootCommand()
{
    secretCmd,
    certCmd,
    envCmd,
};
rootCmd.AddGlobalOption(raw);

await rootCmd.InvokeAsync(args);