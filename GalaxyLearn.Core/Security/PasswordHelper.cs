using System.Security.Cryptography;
using System.Text;

namespace GalaxyLearn.Core.Security
{
    public class PasswordHelper
    {
        public static string EncodePasswordSha256(string pass)
        {
            byte[] originalBytes;
            byte[] encodedBytes;

            // Instantiate SHA256Managed, get bytes for original password, and compute hash (encoded password)
            using (SHA256 sha256 = new SHA256Managed())
            {
                originalBytes = Encoding.UTF8.GetBytes(pass);
                encodedBytes = sha256.ComputeHash(originalBytes);
            }

            // Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}
