// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using AzureAuth;

var tenantId = new Option<string>(["-t", "--tenant"], "Home tenant of client")
{
    IsRequired = true
};
var clientId = new Option<string>(["-c", "--client"], "Client ID used for authentication")
{
    IsRequired = true
};

var scopes = new Option<string[]>(["--scopes"])
{
    AllowMultipleArgumentsPerToken = true,
    Arity = ArgumentArity.OneOrMore,
    IsRequired = true,
};

var secretCmd = new Command("secret", "Use secret for client authentication")
{
    tenantId,
    clientId,
    SecretHandler.Secret,
    scopes
};

var certCmd = new Command("cert", "Use certificate for client authentication")
{
    tenantId,
    clientId,
    CertHandler.CertThumbprint,
    scopes,
};

var envCmd = new Command("env", "Use environment variables for client authentication")
{
    scopes
};

certCmd.SetHandler(CertHandler.Handle, tenantId, clientId, CertHandler.CertThumbprint ,scopes);

var rootCmd = new RootCommand()
{
    secretCmd,
    certCmd,
    envCmd,
};

await rootCmd.InvokeAsync(args);