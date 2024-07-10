using System;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissorsGame
{
    public class HMACGenerator
    {
        private byte[] key;

        public HMACGenerator(int keyLength = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                key = new byte[keyLength];
                rng.GetBytes(key);
            }
        }

        public string GenerateHMAC(string message)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }

        public string GetKey()
        {
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }
    }
}
