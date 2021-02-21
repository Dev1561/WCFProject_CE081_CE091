using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace WCFProject_CE091_CE081
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EncDecService" in both code and config file together.
    public class EncDecService : IEncDecService
    {
        public string DecryptData(string key, string data)
        {
            byte[] iv = new byte[16];
            byte[] decrypted_text = Convert.FromBase64String(data);

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream mem_stream = new MemoryStream(decrypted_text))
                    {
                        using (CryptoStream crypt_stream = new CryptoStream((Stream)mem_stream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader stream_reader = new StreamReader((Stream)crypt_stream))
                            {
                                return stream_reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch(CryptographicException err)
            {
                return ("Wrong Key entered");
            }
        }

        public string EncryptData(string key, string data)
        {
            byte[] iv = new byte[16];
            byte[] encrypted_data;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream mem_stream = new MemoryStream())
                {
                    using(CryptoStream crypt_stream = new CryptoStream((Stream)mem_stream, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter stream_writer = new StreamWriter((Stream)crypt_stream))
                        {
                            stream_writer.Write(data);
                        }
                        encrypted_data = mem_stream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted_data);
        }
    }
}
