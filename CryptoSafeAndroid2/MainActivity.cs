using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;

namespace CryptoSafeAndroid2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<Archivo> archivos;
        ArrayAdapter<Archivo> adaptador;
        int indice = 0;

    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var componenteListaArchivos = (ListView)FindViewById(Resource.Id.listViewArchivos);
            Archivo archivo1 = new Archivo();
            archivos = new List<Archivo>
            {
                archivo1
            };
            adaptador = new ArrayAdapter<Archivo>(this,Resource.Layout.listViewElement,archivos);
            
            componenteListaArchivos.Adapter = adaptador;

            componenteListaArchivos.Click += ComponenteListaArchivos_Click;
        }

        private void ComponenteListaArchivos_Click(object sender, System.EventArgs e)
        {
            adaptador.Add(indice.ToString());
            indice++;
            adaptador.NotifyDataSetChanged();
            Android.Widget.Toast.MakeText(this, "Se ha dado clic", ToastLength.Short).Show();
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
            if (id== Resource.Id.search)
            {
                Toast.MakeText(this, "Buscar",ToastLength.Short).Show();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}