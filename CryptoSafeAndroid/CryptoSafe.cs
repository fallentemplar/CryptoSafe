using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using AlertDialog = Android.App.AlertDialog;
using Android.Content;

namespace CryptoSafeAndroid
{
    [Activity(Name ="com.cryptosafe.aaar.CryptoSafe", Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    //[IntentFilter(new string[] { Intent.ActionSend },Categories = new string[] { Intent.CategoryDefault },DataMimeType = "image/*")]
    //[IntentFilter(new string[] { Intent.ActionSendMultiple },Categories = new string[] { Intent.CategoryDefault },DataMimeType = "image/*")]
    public class CryptoSafe : AppCompatActivity
    {
        
        ListView listaArchivosSeleccionados;
        AdaptadorPersonalizado adaptador;
        EditText campoContrasena;

        int indice = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {


            string tag = "myApp";

            Android.Util.Log.Info(tag, "Esto es Sparta "+ this.PackageName+"|"+this.Class);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            listaArchivosSeleccionados = FindViewById<ListView>(Resource.Id.listViewArchivos);
            campoContrasena = FindViewById<EditText>(Resource.Id.campoContrasena);

            adaptador = new AdaptadorPersonalizado(ArchivosData.Archivos);
            listaArchivosSeleccionados.Adapter = adaptador;

            Button botonCifrar = FindViewById<Button>(Resource.Id.botonCifrar);
            Button botonDescifrar = FindViewById<Button>(Resource.Id.botonDescifrar);

            botonCifrar.Click += BotonCifrar_Click;
            botonDescifrar.Click += BotonDescifrar_Click;
            /*
            try
            {
                if (Intent.Action == Intent.ActionSend)
                {
                    Toast.MakeText(this, "Holi", ToastLength.Long).Show();
                    ClipData.Item value = Intent.ClipData.GetItemAt(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }*/
            
        }

        protected override void OnNewIntent(Intent intent)
        {
            Toast.MakeText(this, "Holi", ToastLength.Long).Show();
        }

            private void BotonDescifrar_Click(object sender, EventArgs e)
        {
            string contrasena = campoContrasena.Text;
            byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
            var eliminarArchivos = false; //Temporal
            DescifrarArchivos(keyMaterial, eliminarArchivos);
        }

        private void BotonCifrar_Click(object sender, System.EventArgs e)
        {
            string contrasena = campoContrasena.Text;
            byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
            var eliminarArchivos = false; //Temporal
            CifrarArchivos(keyMaterial, eliminarArchivos);
            //Toast.MakeText(this, "Ya le di en CIFRAR", ToastLength.Short).Show();
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
                StartActivity(typeof(ExploradorArchivos));
                /*adaptador.AgregarArchivoALista(new Archivo
                {
                    Nombre = "Archivo"+indice,
                    Extension = "zip",
                    Tamano = "4020.KB"
                });
                indice++;*/
            }
            return base.OnOptionsItemSelected(item);
        }

        private async Task CifrarArchivos(byte[] keyMaterial, bool eliminarArchivos)
        {
            foreach (var archivo in adaptador.archivos)
            {
                try
                {
                    string rutaDestino = Path.Combine(Path.GetDirectoryName(archivo.Nombre), Path.GetFileName(archivo.Nombre)) + ".crypt";
                    //Toast.MakeText(this, rutaDestino, ToastLength.Long).Show();
                    await Crypto.CifradoDescifradoAsincrono(keyMaterial, archivo.Nombre, rutaDestino, true);
                }
                catch (System.Security.Cryptography.CryptographicException)
                {
                    AlertDialog.Builder mensajeInformacion = new AlertDialog.Builder(this);
                    mensajeInformacion.SetTitle("Error al cifrar");
                    mensajeInformacion.SetMessage("El archivo " + Path.GetFileName(archivo.Nombre) + " no pudo ser cifrado");
                    mensajeInformacion.Show();
                }
                catch (FileNotFoundException)
                {
                    //Toast.MakeText(this, "El archivo " + Path.GetFileName(archivo.Nombre) + " no pudo ser encontrado", ToastLength.Long).Show();
                    AlertDialog.Builder mensajeInformacion = new AlertDialog.Builder(this);
                    mensajeInformacion.SetTitle("Error al cifrar");
                    mensajeInformacion.SetMessage("El archivo " + Path.GetFileName(archivo.Nombre) + " no pudo ser encontrado");
                    mensajeInformacion.Show();
                }
                catch (DirectoryNotFoundException)
                {
                    AlertDialog.Builder mensajeInformacion = new AlertDialog.Builder(this);
                    mensajeInformacion.SetTitle("Error al cifrar");
                    mensajeInformacion.SetMessage("El directorio destino '" + Path.GetDirectoryName(archivo.Nombre) + "' no pudo ser encontrado");
                    mensajeInformacion.Show();
                }
            }
        }

        private async Task DescifrarArchivos(byte[] keyMaterial, bool eliminarArchivos)
        {
            foreach (var archivo in adaptador.archivos)
            {
                try
                {
                    string rutaDestino = Path.Combine(Path.GetDirectoryName(archivo.Nombre), Path.GetFileNameWithoutExtension(archivo.Nombre));
                    await Crypto.CifradoDescifradoAsincrono(keyMaterial, archivo.Nombre, rutaDestino, false);
                    //if (eliminarArchivos)
                    //Archivos.EliminarArchivo(archivo.ToString());
                }
                catch (System.Security.Cryptography.CryptographicException)
                {
                    //Archivos.EliminarArchivo(Path.Combine(Path.GetDirectoryName(archivo), Path.GetFileNameWithoutExtension(archivo)));
                    AlertDialog.Builder mensajeInformacion = new AlertDialog.Builder(this);
                    mensajeInformacion.SetTitle("Error al descifrar");
                    mensajeInformacion.SetMessage("Esto puede deberse a una de las siguientes razones:\n1-El archivo está dañado\n2-La contraseña es incorrecta\n3-El archivo no fue cifrado con CryptoSafe");
                    mensajeInformacion.Show();
                }
                catch (FileNotFoundException)
                {
                    AlertDialog.Builder mensajeInformacion = new AlertDialog.Builder(this);
                    mensajeInformacion.SetTitle("Error al descifrar");
                    mensajeInformacion.SetMessage("El archivo " + Path.GetFileName(archivo.Nombre) + " no pudo ser encontrado");
                    mensajeInformacion.Show();
                }
                catch(DirectoryNotFoundException)
                {
                    AlertDialog.Builder mensajeInformacion = new AlertDialog.Builder(this);
                    mensajeInformacion.SetTitle("Error al descifrar");
                    mensajeInformacion.SetMessage("El directorio destino '" + Path.GetDirectoryName(archivo.Nombre) + "' no pudo ser encontrado");
                    
                    mensajeInformacion.Show();
                }
            }
        }
    }
}