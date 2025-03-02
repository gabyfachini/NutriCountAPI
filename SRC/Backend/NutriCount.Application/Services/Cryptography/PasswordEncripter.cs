using System.Security.Cryptography;
using System.Text;

namespace NutriCount.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        public string Encrypt (string password)
        {
            var chaveAdicional = "ABC";
            var newPassword = $"{password}{chaveAdicional}";
            var bytes = Encoding.UTF8.GetBytes (newPassword); //geração dos bits pra string da senha
            var hashBytes = SHA512.HashData (bytes);
            return StringBytes(hashBytes);
        }
        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(b);
            }
            return sb.ToString();
        }
    }
}
