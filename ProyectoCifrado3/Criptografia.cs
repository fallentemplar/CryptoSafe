using SecurityDriven.Inferno;
using SecurityDriven.Inferno.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ProyectoCifrado3
{
    static class Criptografia
    {
        /*public static string Cifrar(string textoPlano,string contrasena)
        {
            var seg_array_texto = Utils.SafeUTF8.GetBytes(textoPlano).AsArraySegment();
            var textoCifrado = SuiteB.Encrypt(contrasena.ToBytes(), seg_array_texto);
            return textoCifrado.ToB64Url();
        }*/

        public static byte[] Cifrar(string rutaArchivo, string contrasena)
        {
            try
            {
                byte[] bytesArchivo;
                using (FileStream archivoFuente = new FileStream(rutaArchivo,FileMode.Open, FileAccess.Read))
                {
                    byte[] extension = Path.GetExtension(rutaArchivo).ToBytes();
                    
                    bytesArchivo = new byte[archivoFuente.Length+extension.Length];
                    int noBytesPorLeer = (int)archivoFuente.Length;
                    int noBitsLeidos = 0;

                    while (noBytesPorLeer > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = archivoFuente.Read(bytesArchivo, noBitsLeidos, noBytesPorLeer);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        noBitsLeidos += n;
                        noBytesPorLeer -= n;
                    }
                    noBytesPorLeer = bytesArchivo.Length;
                    archivoFuente.Dispose();
                }

                byte[] archivoCifrado = SuiteB.Encrypt(contrasena.ToBytes(), bytesArchivo.AsArraySegment<byte>());
                return archivoCifrado;
            }catch(FileNotFoundException){
                MessageBox.Show("El archivo seleccionado no existe");
                return null;
            }
        }

        public static byte[] Descifrar(string rutaArchivo, string contrasena)
        {
            /*var textoCifradoArrSeg = textoCifrado.FromB64Url().AsArraySegment();
            var decryptedPassword = SuiteB.Decrypt(
                contrasena.ToBytes(),
                textoCifradoArrSeg
                );
            return Utils.SafeUTF8.GetString(decryptedPassword);*/
            try
            {
                byte[] bytesArchivo;
                using (FileStream archivoFuente = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read)){
                    // Read the source file into a byte array.
                    bytesArchivo = new byte[archivoFuente.Length];
                    int noBytesPorLeer = (int)archivoFuente.Length;
                    int noBitsLeidos = 0;
                    while (noBytesPorLeer > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = archivoFuente.Read(bytesArchivo, noBitsLeidos, noBytesPorLeer);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        noBitsLeidos += n;
                        noBytesPorLeer -= n;
                    }
                    noBytesPorLeer = bytesArchivo.Length;
                    archivoFuente.Dispose();
                }
                var textoDescifrado = SuiteB.Decrypt(
                contrasena.ToBytes(),
                bytesArchivo.AsArraySegment<byte>()
                );
                return textoDescifrado;//Utils.SafeUTF8.GetString(textoDescifrado);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("El archivo solicitado no existe");
                return null;
            }
        }
    }
}
