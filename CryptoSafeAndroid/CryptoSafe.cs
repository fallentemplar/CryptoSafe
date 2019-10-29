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
using Android.Util;
using Android.Provider;

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
        Button botonCifrar;
        Button botonDescifrar;
        const string tag = "CryptoSafe";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InicializarComponentesInterfaz();



            adaptador = new AdaptadorPersonalizado(ArchivosData.Archivos);
            listaArchivosSeleccionados.Adapter = adaptador;

            if (Intent.Action == Intent.ActionSend)
            {
                var rutaArchivo = ObtenerRutaArchivo((Android.Net.Uri)Intent.Extras.GetParcelable(Intent.ExtraStream));
                Toast.MakeText(this, rutaArchivo, ToastLength.Long).Show();
                AgregarArchivoALista(rutaArchivo);   
            }
            else if(Intent.Action == Intent.ActionSendMultiple)
            {
                //if (Intent.Type.StartsWith("image/"))
                //Toast.MakeText(this, "¡Esto es una imágen!", ToastLength.Long).Show();
                //ClipData.Item value = Intent.ClipData.GetItemAt(0);
                //intent.getParcelableArrayListExtra<Parcelable>(Intent.EXTRA_STREAM)?.let {
                var rutasArchivos = Intent.GetParcelableArrayListExtra(Intent.ExtraStream);
            }
        }

        private string ObtenerRutaArchivo(Android.Net.Uri uri)
        {
            string[] proj = { MediaStore.Images.ImageColumns.Data };
            var cursor = ManagedQuery(uri, proj, null, null, null);
            var colIndex = cursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);
            cursor.MoveToFirst();
            return cursor.GetString(colIndex);
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
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        private void AgregarArchivoALista(string ruta)
        {
            FileInfo informacion = new FileInfo(ruta);
            adaptador.AgregarArchivoALista(new Archivo
            {
                Nombre = Path.GetFullPath(ruta),
                Extension = Path.GetExtension(ruta),
                Tamano = (informacion.Length / 1024) + " KB"
            });
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
                    Log.Debug(tag, rutaDestino);
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
                catch (Exception e)
                {
                    Log.Warn(tag, "excepción D: " + e.Message);
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

        private void InicializarComponentesInterfaz()
        {
            listaArchivosSeleccionados = FindViewById<ListView>(Resource.Id.listViewArchivos);
            campoContrasena = FindViewById<EditText>(Resource.Id.campoContrasena);

            botonCifrar = FindViewById<Button>(Resource.Id.botonCifrar);
            botonCifrar.Click += BotonCifrar_Click;

            botonDescifrar = FindViewById<Button>(Resource.Id.botonDescifrar);
            botonDescifrar.Click += BotonDescifrar_Click;
        }
    }
}