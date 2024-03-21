// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using AzureAuth;

var tenantId = new Option<string>(["-t", "--tenant"])
{
    IsRequired = true
};
var clientId = new Option<string>(["-c", "--client"])
{
    IsRequired = true
};
var certThumbprint = new Option<string>(["--cert"])
{
    IsRequired = true
};
var scopes = new Option<string[]>(["--scopes"])
{
    AllowMultipleArgumentsPerToken = true,
    Arity = ArgumentArity.OneOrMore,
    IsRequired = true,
};

var rootCmd = new RootCommand();
var certCmd = new Command("cert", "Certificate")
{
    tenantId,
    clientId,
    certThumbprint,
    scopes,
};

certCmd.SetHandler(CertHandler.Handle, tenantId, clientId, certThumbprint,scopes);
rootCmd.Add(certCmd);

await rootCmd.InvokeAsync(args);