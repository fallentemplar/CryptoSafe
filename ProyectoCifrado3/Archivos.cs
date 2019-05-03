using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoCifrado3
{
    static class Archivos
    {
        public static void EscribirArchivo(byte[] bytesArchivoCifrado,string ruta, bool cifrar)
        {
            StringBuilder rutaNueva = new StringBuilder();
            rutaNueva.Append(Path.GetDirectoryName(ruta));
            rutaNueva.Append("\\");
            rutaNueva.Append(Path.GetFileNameWithoutExtension(ruta));
            if (cifrar)
                rutaNueva.Append(".crypt");
            else
                rutaNueva.Append(".jpg");
            System.Windows.Forms.MessageBox.Show(rutaNueva.ToString());
            using (FileStream archivoNuevo = new FileStream(rutaNueva.ToString(), FileMode.Create, FileAccess.Write)){
                archivoNuevo.Write(bytesArchivoCifrado, 0, bytesArchivoCifrado.Length);
            }
        }
    }
}
