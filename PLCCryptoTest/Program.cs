using PCLCrypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCCryptoTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine();
            //byte[] keyMaterial = Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaa");
            byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
            await EncryptThenDecryptAsync(keyMaterial);
            Console.ReadLine();
        }

        static void Cifrar()
        {
            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine();
            //byte[] keyMaterial = Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaa");
            byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);

            Console.WriteLine(keyMaterial.Length);
            byte[] data = Encoding.UTF8.GetBytes("bbbbbbbb"); ;
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(PCLCrypto.SymmetricAlgorithm.AesCbcPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);

            // The IV may be null, but supplying a random IV increases security.
            // The IV is not a secret like the key is.
            // You can transmit the IV (w/o encryption) alongside the ciphertext.
            var iv = WinRTCrypto.CryptographicBuffer.GenerateRandom(provider.BlockLength);

            byte[] cipherText = WinRTCrypto.CryptographicEngine.Encrypt(key, data, iv);

            Console.WriteLine(Encoding.UTF8.GetChars(cipherText));
            // When decrypting, use the same IV that was passed to encrypt.
            byte[] plainText = WinRTCrypto.CryptographicEngine.Decrypt(key, cipherText, iv);
            Console.WriteLine(Encoding.UTF8.GetChars(plainText));
            
        }

        static void Descifrar()
        {

        }

        private const string archivoACifrar = @"E:\Alexis\Descargas\Cifrar\drop files.png";
        private const string archivoCifrado = @"E:\Alexis\Descargas\Cifrar\drop files.crypt";
        private const string archivoDescifrado = @"E:\Alexis\Descargas\Cifrar\drop files2.png";

        static async Task EncryptThenDecryptAsync(byte[] keyMaterial)
        {
            ISymmetricKeyAlgorithmProvider aesGcm = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            //byte[] keyMaterial = WinRTCrypto.CryptographicBuffer.GenerateRandom(32);
            var claveDerivada = aesGcm.CreateSymmetricKey(keyMaterial);
            var cifrador = new PCLCrypto.ICryptoTransform[] { WinRTCrypto.CryptographicEngine.CreateEncryptor(claveDerivada) };
            var descifrador = new PCLCrypto.ICryptoTransform[] { WinRTCrypto.CryptographicEngine.CreateDecryptor(claveDerivada) };
            await CryptoTransformFileAsync(archivoACifrar, archivoCifrado, cifrador);
            await CryptoTransformFileAsync(archivoCifrado, archivoDescifrado, descifrador);
        }

        static async Task CryptoTransformFileAsync(string sourcePath, string destinationPath, ICryptoTransform[] transforms, CancellationToken cancellationToken = default)
        {
            const int BufferSize = 4096;
            using (var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, useAsync: true))
            {
                using (var destinationStream = new FileStream(destinationPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, BufferSize, useAsync: true))
                {
                    using (var cryptoStream = PCLCrypto.CryptoStream.WriteTo(destinationStream, transforms))
                    {
                        await sourceStream.CopyToAsync(cryptoStream, BufferSize, cancellationToken);
                        await cryptoStream.FlushAsync(cancellationToken);
                        cryptoStream.FlushFinalBlock();
                    }
                }
            }
        }
    }
}
