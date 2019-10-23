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
                //Nombre = "/storage/DD7A-A973/test/IMG_20190504_232840.jpg",
                Nombre = "/storage/emulated/0/Download/IMG_20190504_232840.jpg",
                Tamano = "29292KB",
                Extension = "jpg"
            });

            archivos.Add(new Archivo()
            {
                //Nombre = "/storage/DD7A-A973/test/IMG_20190509_144404.jpg",
                Nombre = "/storage/emulated/0/Download/IMG_20190509_144404.jpg",
                Tamano = "665KB",
                Extension = "png"
            });
        }
    }
}