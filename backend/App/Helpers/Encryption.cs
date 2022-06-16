using System.Security.Cryptography;
using System.Text;

namespace backend.App.Helpers
{
    public class Encryption
    {

        public static string salt_text = "3jdhGwIjlwQCkFYu";
        public static byte[] iv = Encoding.UTF8.GetBytes(salt_text);
        // Encrypt text using AES with key


        public static string Encryprt(string text,string key)
        {
            int myIterations = 1000;
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(key, iv,myIterations);
            byte[] keyg = k1.GetBytes(16);
            try
            {
                byte[] encrypted;
                // encrypt using aes encryption 
                using (Aes aesAlg = Aes.Create())
                {
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(keyg, iv);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(text);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(encrypted);
            } catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }


        public static string Decrypt (string cipherText, string key)
        {
            string decryptedText = null;
            byte[] cipherTextB = Convert.FromBase64String(cipherText);
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(key, iv);
                    aesAlg.Key = k1.GetBytes(16);

                    aesAlg.IV = iv;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextB))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                decryptedText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                return decryptedText;


            } catch
            {
                return null;
            }
        }



    }
}
