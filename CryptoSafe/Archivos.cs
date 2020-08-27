using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ProyectoCifrado3
{
    static class Archivos
    {
        public static bool EscribirArchivo(byte[] bytesArchivoCifrado,string ruta, bool cifrar)
        {
            StringBuilder rutaNueva = new StringBuilder();
            rutaNueva.Append(Path.GetDirectoryName(ruta));
            rutaNueva.Append("\\");
            if (cifrar)
            {
                rutaNueva.Append(Path.GetFileName(ruta));
                rutaNueva.Append(".crypt");
            }else
                rutaNueva.Append(Path.GetFileNameWithoutExtension(ruta));
            if (!File.Exists(rutaNueva.ToString()))//Si el archivo no existe
            {
                using (FileStream archivoNuevo = new FileStream(rutaNueva.ToString(), FileMode.CreateNew, FileAccess.Write))
                {
                    archivoNuevo.Write(bytesArchivoCifrado, 0, bytesArchivoCifrado.Length);
                }
                return true;
            }
            else //Si el archivo ya existe en ese directorio
            {
                DialogResult archivoExistente = MessageBox.Show("El archivo '"+ rutaNueva.ToString()+"' ya existe en el directorio, ¿desea sobreescribirlo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(archivoExistente == DialogResult.Yes)
                {
                    using (FileStream archivoNuevo = new FileStream(rutaNueva.ToString(), FileMode.Create, FileAccess.Write))
                        archivoNuevo.Write(bytesArchivoCifrado, 0, bytesArchivoCifrado.Length);
                    return true;
                }
                return false;
            }
            
        }

        public static void EliminarArchivo(string ruta)
        {
            File.Delete(ruta);
        }
    }
}
