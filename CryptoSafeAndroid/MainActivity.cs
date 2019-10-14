using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;

namespace CryptoSafeAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView listaArchivosSeleccionados;
        AdaptadorPersonalizado adaptador;

        int indice = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            listaArchivosSeleccionados = FindViewById<ListView>(Resource.Id.listViewArchivos);
            adaptador = new AdaptadorPersonalizado(ArchivosData.Archivos);
            listaArchivosSeleccionados.Adapter = adaptador;

            Button botonCifrar = FindViewById<Button>(Resource.Id.botonCifrar);
            Button botonDescifrar = FindViewById<Button>(Resource.Id.botonDescifrar);

            botonCifrar.Click += BotonCifrar_Click;
        }

        private void BotonCifrar_Click(object sender, System.EventArgs e)
        {
            Toast.MakeText(this, "Boton cifrar\nNo implementado", ToastLength.Short).Show();
            adaptador.AgregarArchivoALista(new Archivo
            {
               Nombre="Banana",
               Extension="zip" ,
               Tamano="4020.KB"
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.share)
            {
                Toast.MakeText(this, "Compartir\nNo implementado", ToastLength.Short).Show();
                return true;
            }
            else if (id == Resource.Id.addFiles)
            {
                adaptador.AgregarArchivoALista(new Archivo
                {
                    Nombre = "Archivo"+indice,
                    Extension = "zip",
                    Tamano = "4020.KB"
                });
                indice++;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}