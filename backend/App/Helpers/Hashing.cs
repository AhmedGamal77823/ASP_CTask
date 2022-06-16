using System.Security.Cryptography;
using System.Text;

namespace backend.App.Helpers
{
    public class Hashing
    {
        public static string HashPassword(string password)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    result.Append(bytes[i].ToString("x2"));
                }
                return result.ToString();
            }
        }

        public static bool Compare(string providedPassword, string hashedPassword)
        {
            string NewHashed = HashPassword(providedPassword);

            if (NewHashed != hashedPassword) return false;
            return true;
        }
    }
}
