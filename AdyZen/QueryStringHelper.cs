using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AdyZen
{
    public class QueryStringHelper
    {
        private static readonly string EncryptionKey = "my_secret_key"; // Use a better key for production!

        public static string Encrypt(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(textBytes);
            return base64;
        }

        public static string Decrypt(string encryptedText)
        {
            System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

            System.Diagnostics.Debug.WriteLine(encryptedText);
            byte[] textBytes = Convert.FromBase64String(encryptedText);
            string text = Encoding.UTF8.GetString(textBytes);
            return text;
        }
    }
}
