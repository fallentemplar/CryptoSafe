using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SecurityDriven.Inferno;
//using SecurityDriven.Inferno.Extensions;


namespace CriptoZorro
{
    class Criptografia
    {
        static byte[] claveMaestra;

        public Criptografia(String contrasena)
        {

        }

        public static string Cifrar(string textoPlano)
        {
            /*var seg_array_texto = Utils.SafeUTF8.GetBytes(textoPlano).AsArraySegment();
            var textoCifrado = SuiteB.Encrypt(claveMaestra, seg_array_texto);
            return textoCifrado.ToB64Url();*/
            return null;
        }

        public static string Descifrar(string textoCifrado)
        {
            /*var textoCifradoArrSeg = textoCifrado.FromB64Url().AsArraySegment();
            var decryptedPassword = SuiteB.Decrypt(
                claveMaestra,
                textoCifradoArrSeg
                );

            return Utils.SafeUTF8.GetString(decryptedPassword);*/
            return null;
        }

    }
}
