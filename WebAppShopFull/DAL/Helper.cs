using System;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    public static class Helper
    {
        public static int RandomInt()
        {
            Random random = new Random();
            return random.Next();
        }
        public static long RandomLong()
        {
            Random random = new Random();
            long a = random.Next();
            long b = random.Next();
            return a * b;
        }

        public static byte[] Hash(string plaintext)
        {
            HashAlgorithm algorithm = SHA512.Create();
            return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
        }
    }
}
