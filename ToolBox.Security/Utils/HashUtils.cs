using System.Security.Cryptography;
using System.Text;

namespace ToolBox.Security.Utils
{
    public class HashUtils
    {
        public static byte[] HashPassword(string plainPassword, Guid salt)
        {
            return SHA512.HashData(Encoding.UTF8.GetBytes(plainPassword + salt.ToString()));
        }

        public static bool VerifyPassword(byte[] hashedPassword, string plainPassword, Guid salt)
        {
            return hashedPassword.SequenceEqual(HashPassword(plainPassword, salt));
        }
    }
}
