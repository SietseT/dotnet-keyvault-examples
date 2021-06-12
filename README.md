# .NET Azure Key Vault examples
Contains an example integration of Azure Key Vault in a .NET (Framework) project.

---

# Prerequisites
- Active Azure subscription
  - Tip: You could have an MSDN Azure subscription with free credits via your workplace.
- [Azure Key Vault](https://docs.microsoft.com/en-us/azure/key-vault/general/quick-create-portal)
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

---

# Code 
In HomeController I've added code to retrieve a secret from the Azure Key Vault:
```
var client = new SecretClient(new Uri(ConfigurationManager.AppSettings["Azure.KeyVaultUri"]), new DefaultAzureCredential());
var secret = await client.GetSecretAsync("mysupersecretsecret");
```

# Setup
The example(s) can be found in the `samples` directory.

1. Create a secret in your Azure Key Vault. The code in this project expects a secret with the name `mysupersecretsecret`. You can use your own secret name, but make sure to change the code accordingly if doing so.
2. You can run the web app locally (on-premise) or in an Azure Web App. Follow the steps of your choice. 

## Local/on-premise
We need to create an app registration and we'll grant it access to the Key Vault. Then we need to configure Key Vault credentials in the local environment.

1. Create an Azure Active Directory (any pricing model will do)
2. [Create an App Registration](https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app) in the Active Directory
3. Create a secret for your App Registration (in left-side menu in the Azure Portal) and save it somewhere
4. Note the Client ID and Tenant ID of your App Registration somewhere alongside the secret.
5. Create an [access policy](https://docs.microsoft.com/en-us/azure/key-vault/general/assign-access-policy-portal). Make sure to select the app registration as principle and grant it the `Secret Management` permissions template. _If you don't want to use a template, make sure to grant the principal a Get secret permission._
6. Now that everything in Azure is configured, all that remains is the local environment. Add 3 user [environment variables](https://www.wikihow.com/Create-an-Environment-Variable-in-Windows-10):
    - AZURE_CLIENT_ID (Client ID from App Registration)
    - AZURE_TENANT_ID (Tenant ID from App Registration)
    - AZURE_CLIENT_SECRET (Secret you created in the app registration)
7. Run the website. You should see your secret on the homepage.

## Azure Web App
1. Create an Azure Active Directory (any pricing model will do)
2. Create an Azure Web App (any pricing tier will do)
3. Add a [system-assigned identity](https://docs.microsoft.com/en-us/azure/app-service/overview-managed-identity?tabs=dotnet#add-a-system-assigned-identity) for the Web App.
4. Create an [access policy](https://docs.microsoft.com/en-us/azure/key-vault/general/assign-access-policy-portal). Make sure to select the system-assigned identity as principle (usually the Web App name) and grant it the `Secret Management` permissions template. _If you don't want to use a template, make sure to grant the principal a Get secret permission._
5. [Deploy](https://docs.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=netcore31&pivots=development-environment-vs) the website to the Azure Web App.
6. Go the the URL of your Web App and you should see your secret value.