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
    public class ViewHolder : Java.Lang.Object
    {
        //public ImageView Photo { get; set; }
        public TextView Nombre { get; set; }
        public TextView Tamano { get; set; }
        public TextView Extension { get; set; }
    }
}