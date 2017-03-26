using Parva.Application.Interfaces;
using Parva.Application.Interfaces.Encryption;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Parva.Infrastructure.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        public string CreateSaltKey()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[5];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public string CreatePasswordHash(string password, string saltkey)
        {
            string saltAndPassword = String.Concat(password, saltkey);
            var algorithm = HashAlgorithm.Create("SHA1");
            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }
    }
}