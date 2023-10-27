namespace TeamFury_API.Services;

public interface IKeyVaultManager
{
    Task<string> GetSecret(string secretName);
}