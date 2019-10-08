using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System.Collections;
using System;

namespace CryptoSafeAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView listViewArchivos;
        private ArrayAdapter<string> listAdapter;

        private List<string> archivos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            archivos = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H"
            };
            //https://www.youtube.com/watch?v=oNj1DFTLvG0
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            listViewArchivos = (ListView)FindViewById(Resource.Id.mainListView);
            listAdapter = new ArrayAdapter<string>(this, Resource.Layout.simplerow, archivos);

            listViewArchivos.Adapter = listAdapter;

            listViewArchivos.ItemClick += listViewArchivos_ItemClick;
        }

        private void listViewArchivos_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, "Hiciste click", ToastLength.Short);
        }
    }
}