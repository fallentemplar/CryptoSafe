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
        private static readonly byte[] claveMaestra = new byte[3] { 1, 2, 3 };
        private static readonly CryptoRandom rnd = new CryptoRandom();

        private static void Main(string[] args)
        {
            string textoPlano = "Este es un texto plano";
            var array_byte_sal = new byte[rnd.Next(0, 64)];
            rnd.NextBytes(array_byte_sal);
            //var textoCifrado = Cifrar(textoPlano, array_byte_sal.ToB64Url());
            var textoCifrado = Cifrar(textoPlano);
            Console.WriteLine("Texto plano: " + textoPlano);
            Console.WriteLine("Texto cifrado: " + textoCifrado);
            Console.WriteLine("Texto descifrado: " + Descifrar(textoCifrado));
            Console.Read();
        }

        public static string Cifrar(string textoPlano)
        {
            var seg_array_texto = Utils.SafeUTF8.GetBytes(textoPlano).AsArraySegment();
            var textoCifrado = SuiteB.Encrypt(claveMaestra, seg_array_texto);
            return textoCifrado.ToB64Url();
        }

        public static string Descifrar(string textoCifrado)
        {
            var textoCifradoArrSeg = textoCifrado.FromB64Url().AsArraySegment();
            //var textoCifrado_y_sal = seg_array_sal.Array;

            var decryptedPassword = SuiteB.Decrypt(
                claveMaestra,
                //new ArraySegment<byte>(textoCifrado_y_sal, longitudSal, textoCifrado_y_sal.Length - longitudSal),
                //new ArraySegment<byte>(textoCifrado_y_sal, 0, longitudSal)
                textoCifradoArrSeg
                );

            return Utils.SafeUTF8.GetString(decryptedPassword);
        }
    }
}
