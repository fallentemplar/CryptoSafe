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
using Xamarin.Android;

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
        const string tag = "MyApp";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InicializarComponentesInterfaz();

            adaptador = new AdaptadorPersonalizado(ArchivosData.Archivos);
            listaArchivosSeleccionados.Adapter = adaptador;

            Toast.MakeText(this, "Versión 1.0.0.0", ToastLength.Long).Show();

            if (Intent.Action == Intent.ActionSend)
            {
                adaptador.LimpiarLista();
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

        private Task<bool> ConfirmarEliminacionArchivos()
        {
            var respuesta = new TaskCompletionSource<bool>();
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Confirmación");
            alert.SetMessage("¿Desea eliminar los archivos originales después del proceso?");
            alert.SetButton("Sí", (c, ev) =>
            {
                respuesta.SetResult(true);
            });
            alert.SetButton2("No", (c, ev) => { respuesta.SetResult(false); });
            alert.Show();
            return respuesta.Task;
        }


        private string ObtenerRutaArchivo(Android.Net.Uri uri)
        {
            string[] proj = { MediaStore.Images.ImageColumns.Data };
            var cursor = ManagedQuery(uri, proj, null, null, null);
            var colIndex = cursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);
            cursor.MoveToFirst();
            return cursor.GetString(colIndex);
        }

        private async void BotonDescifrar_Click(object sender, EventArgs e)
        {
            Log.Info(tag, "Botón de descifrar presionado");
            if (adaptador.archivos.Count > 0)
            {
                string contrasena = campoContrasena.Text;
                Log.Debug(tag, "Contraseña: " + contrasena);
                byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
                bool eliminarArchivosOriginales = await ConfirmarEliminacionArchivos();
                await DescifrarArchivos(keyMaterial, eliminarArchivosOriginales);
                Toast.MakeText(this, "Tarea de descifrado terminada", ToastLength.Long).Show();
                Log.Debug(tag, "Tarea de descifrado terminada");
            }
            else
                Toast.MakeText(this, "Actualmente no hay archivos en la lista", ToastLength.Short).Show();
            adaptador.LimpiarLista();
        }

        private async void BotonCifrar_Click(object sender, System.EventArgs e)
        {
            Log.Info(tag, "Botón de cifrar presionado");
            if (adaptador.archivos.Count > 0)
            {
                string contrasena = campoContrasena.Text;
                Log.Debug(tag, "Contraseña: " + contrasena);
                byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
                bool eliminarArchivosOriginales = await ConfirmarEliminacionArchivos();
                Toast.MakeText(this, "Eliminar: "+eliminarArchivosOriginales, ToastLength.Long).Show();
                await CifrarArchivos(keyMaterial, eliminarArchivosOriginales);
                Toast.MakeText(this, "Tarea de cifrado terminada", ToastLength.Long).Show();
                Log.Debug(tag, "Tarea de cifrado terminada");
            }
            else
                Toast.MakeText(this, "Actualmente no hay archivos en la lista", ToastLength.Short).Show();
            adaptador.LimpiarLista();
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
            adaptador.AgregarArchivo(new Archivo
            {
                Nombre = Path.GetFullPath(ruta),
                Extension = Path.GetExtension(ruta),
                Tamano = (informacion.Length / 1024) + "KB"
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
            else if (id == Resource.Id.removeFiles)
            {
                if(adaptador.archivos.Count>0)
                    adaptador.LimpiarLista();
                else
                    Toast.MakeText(this, "Actualmente no hay archivos en la lista", ToastLength.Short).Show();
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
                    Log.Debug(tag, "Ruta destino: " + rutaDestino);
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