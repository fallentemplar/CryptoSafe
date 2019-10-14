using System;
using System.Collections.Generic;
using System.IO;
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
    public class Archivo
    {
        public string Nombre { get; set; }
        public string Tamano { get; set; }
        public string Extension { get; set; }

        public string ArchivoSinExtension()
        {
            return Path.GetFileNameWithoutExtension(Nombre);
        }
    }
}