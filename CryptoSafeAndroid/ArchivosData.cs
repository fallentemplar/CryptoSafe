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
            /*var temp = new List<Archivo>();
            AgregarArchivo(temp);

            Archivos = temp;*/
            Archivos = new List<Archivo>();
        }

        public static void AgregarArchivo(List<Archivo> archivos)
        {
            
        }
    }
}