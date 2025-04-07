using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService
{
    public class EncryptionService : IEncryptionService
    {
        private readonly string _encryptionKey;
        private readonly string _encryptionIV;

        public EncryptionService(IConfiguration configuration)
        {
            // Read the encryption key and IV from configuration
            _encryptionKey = configuration["EncryptionSettings:Key"];
            _encryptionIV = configuration["EncryptionSettings:IV"];
        }

        public string Encrypt(string plainText)
        {
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(_encryptionKey);
                aesAlg.IV = Convert.FromBase64String(_encryptionIV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(_encryptionKey);
                aesAlg.IV = Convert.FromBase64String(_encryptionIV);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
