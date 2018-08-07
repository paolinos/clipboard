using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace sharedclipboard.Models
{
    public class CookieModel
    {
        private IDictionary<string, string> _cookieData { get; set; } 

        public CookieModel()
        {
            _cookieData = new Dictionary<string, string>();
        }


        public CookieModel(string models)
        {
            _cookieData = new Dictionary<string, string>();

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dencryptedData = rsa.Decrypt( byteConverter.GetBytes(models), false );

            var modelDecypted =  byteConverter.GetString(dencryptedData);
        }

        public void Add(string key, string value)
        {

        }

        public string ToString()
        {
            

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] encryptedData = rsa.Encrypt( byteConverter.GetBytes("Data to Encrypt"), false );

            return byteConverter.GetString(encryptedData);
        }
    }
}