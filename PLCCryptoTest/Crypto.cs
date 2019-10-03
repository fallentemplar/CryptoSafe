using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PLCCryptoTest
{
    class Crypto
    {/// <summary>
     /// Deriva una clave para usar con algoritmos simétricos
     /// </summary>
     /// <param name="contrasena">a password in plain text, perhaps an easy
     /// to remember one
     /// </param>
     /// <param name="tamano">The size in BITS of the key</param>
     /// <returns>A key for use in encryption</returns>
        public static byte[] DerivarClaveDeContrasena(string contrasena, int tamano)
        {
            return DerivarClave(contrasena, GenerarSal(8), tamano / 8);
        }

        /// <summary>
        /// Generate an IV (Initialization Vector) for use in Rijndael crypto
        /// algorithm
        /// </summary>
        /// <param name="tamano">size in bits of the vector</param>
        /// <returns>a new array o bytes with a random generated IV</returns>
        public byte[] GenerarIV(int tamano)
        {
            return GenerarSal(tamano / 8);
        }

        /// <summary>
        /// Generate a salt array, this is a vector of bytes used for the
        /// generation of strong keys.
        /// This method is used internally by DeriveKeyFromPassword()
        /// </summary>
        /// <param name="tamano">Size in bytes (watch out)</param>
        /// <returns>an array of random bytes of size bytes</returns>
        private static byte[] GenerarSal(int tamano)
        {
            /// Use a cryptographic random number generator
            RandomNumberGenerator generador = RandomNumberGenerator.Create();
            // create the result array and fill it with non-zero bytes
            byte[] resultado = new byte[tamano];
            generador.GetNonZeroBytes(resultado);
            return resultado;
        }

        /// <summary>
        /// Derive a key form a password. This internally used by DeriveKeyFromPassword
        /// This is an advanced implementation, when you know how to generate
        /// a salt array
        /// </summary>
        /// <param name="contrasena">A password, perhaps human readable and
        /// easy to remember </param>
        /// <param name="sal">A salt array used to generate a password</param>
        /// <param name="tamano">the size in bytes of the key</param>
        /// <returns>a key of size bytes</returns>
        private static byte[] DerivarClave(string contrasena, byte[] sal, int tamano)
        {
            PasswordDeriveBytes pder = new PasswordDeriveBytes(contrasena, sal);
            pder.IterationCount = 100;
            pder.HashName = "SHA1";
            return pder.GetBytes(tamano);
        }
    }
}
