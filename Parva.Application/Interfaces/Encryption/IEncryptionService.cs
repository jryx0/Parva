namespace Parva.Application.Interfaces.Encryption
{
    public interface IEncryptionService
    {
        string CreateSaltKey();

        string CreatePasswordHash(string password, string saltkey);
    }
}