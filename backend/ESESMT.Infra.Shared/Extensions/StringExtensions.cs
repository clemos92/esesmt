using System;
using System.Text;

namespace ESESMT.Infra.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string Encrypt(this string value)
        {
            return Convert.ToBase64String(new UTF8Encoding().GetBytes(value));
        }

        public static string Decrypt(this string base64String)
        {
            var data = Convert.FromBase64String(base64String);
            var decodedString = Encoding.UTF8.GetString(data);

            return decodedString;
        }
    }
}
