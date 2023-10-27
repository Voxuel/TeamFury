using Azure.Security.KeyVault.Secrets;

namespace TeamFury_API.Services.SecurityServices;

public class KeyVaultManager: IKeyVaultManager
{
    private readonly SecretClient _client;

    public KeyVaultManager(SecretClient client)
    {
        _client = client;
    }
    
    public async Task<string> GetSecret(string secretName)
    {
        try
        {
            KeyVaultSecret kvs = await _client.GetSecretAsync(secretName);
            return kvs.Value;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}