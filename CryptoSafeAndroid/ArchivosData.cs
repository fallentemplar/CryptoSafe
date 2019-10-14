using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CryptoSafeAndroid
{
    public static class ArchivosData
    {
        public static List<Archivo> Archivos { get; private set; }

        static ArchivosData()
        {
            var temp = new List<Archivo>();
            AgregarArchivo(temp);

            Archivos = temp;
        }

        public static void AgregarArchivo(List<Archivo> archivos)
        {
            archivos.Add(new Archivo()
            {
                Nombre = "Archivo1",
                Tamano = "59KB",
                Extension = "jpg"
            });

            archivos.Add(new Archivo()
            {
                Nombre = "Archivo2",
                Tamano = "665KB",
                Extension = "png"
            });
        }
    }
}