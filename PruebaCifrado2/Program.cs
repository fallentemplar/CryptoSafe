using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDriven.Inferno.Extensions;
using SecurityDriven.Inferno;

namespace PruebaCifrado2
{
    internal static class Program
    {
        private static readonly byte[] masterKey = new byte[3] { 1, 2, 3 };
        private static readonly CryptoRandom rnd = new CryptoRandom();

        private static void Main(string[] args)
        {
            string plainPassword = "THIS IS A PLAIN PASSWORD";
            var saltByteArr = new byte[rnd.Next(0, 64)];
            rnd.NextBytes(saltByteArr);
            var encryptedPassword = Encrypt(plainPassword, saltByteArr.ToB64Url());
            Console.WriteLine("Plain password: " + plainPassword);
            Console.WriteLine("Encrypted password: " + encryptedPassword);
            Console.WriteLine("Decrypted password: " + Decrypt(encryptedPassword, saltByteArr.ToB64Url()));
            Console.Read();
        }

        public static string Encrypt(string plainPassword, string salt)
        {
            var passwordArrSeg = Utils.SafeUTF8.GetBytes(plainPassword).AsArraySegment();
            var saltArrSeg = salt.FromB64Url().AsArraySegment();
            var encryptedPassword = SuiteB.Encrypt(masterKey, passwordArrSeg, saltArrSeg);
            return encryptedPassword.ToB64Url();
        }

        public static string Decrypt(string encryptedPassword, string salt)
        {
            var encryptedPasswordArrSeg = encryptedPassword.FromB64Url().AsArraySegment();
            var saltArrSeg = salt.FromB64Url().AsArraySegment();
            var saltLength = saltArrSeg.Count;
            var encryptedPassword_and_salt = Utils.Combine(saltArrSeg.Array, encryptedPasswordArrSeg.Array);

            var decryptedPassword = SuiteB.Decrypt(
                masterKey,
                new ArraySegment<byte>(encryptedPassword_and_salt, saltLength, encryptedPassword_and_salt.Length - saltLength),
                new ArraySegment<byte>(encryptedPassword_and_salt, 0, saltLength)
                );

            return Utils.SafeUTF8.GetString(decryptedPassword);
        }
    }
}
