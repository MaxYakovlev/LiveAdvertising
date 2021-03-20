using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveAdvertising.Security
{
    public class Cryptography
    {
        private readonly static string Key = "!@#my$%^secret*&/key?`~3";

        public static string EncryptPassword(string password)
        {
            string result = password + Key;

            byte[] passwordBytes = Encoding.UTF8.GetBytes(result);

            return Convert.ToBase64String(passwordBytes);
        }
    }
}
