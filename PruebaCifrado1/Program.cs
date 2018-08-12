using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDriven.Inferno;

namespace PruebaCifrado1
{
    class Program
    {
        static void Main(string[] args)
        {
            string mensaje = Console.ReadLine();
            string contrasena = Console.ReadLine();
            
            var textoP = Encoding.UTF8.GetBytes(mensaje);
            Console.WriteLine("Texto original: \"{0}\", longitud {1}\n\n",mensaje,mensaje.Length);

            var a = new ArraySegment<byte>(textoP, 0, textoP.Length);
            byte[] banana = SuiteB.Encrypt(Encoding.UTF8.GetBytes(contrasena), a);
            Console.WriteLine("\tTexto cifrado: " + Encoding.UTF8.GetString(banana) + "\n\n");
            
            a = new ArraySegment<byte>(banana, 0, banana.Length);
            banana = SuiteB.Decrypt(Encoding.UTF8.GetBytes(contrasena),a);
            Console.WriteLine("Texto descifrado: \"{0}\", longitud: {1}\n\n", Encoding.UTF8.GetString(banana), Encoding.UTF8.GetString(banana).Length);
            Console.ReadKey();
        }
    }
}
