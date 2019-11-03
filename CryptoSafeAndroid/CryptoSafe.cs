﻿using Android.App;
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
using System.Linq;
using Java.Net;
using Android;
using Android.Content.PM;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.App;

namespace CryptoSafeAndroid
{
    [Activity(Name ="com.cryptosafe.aaar.CryptoSafe", Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    //[IntentFilter(new string[] { Intent.ActionSend },Categories = new string[] { Intent.CategoryDefault },DataMimeType = "image/*")]
    //[IntentFilter(new string[] { Intent.ActionSendMultiple },Categories = new string[] { Intent.CategoryDefault },DataMimeType = "image/*")]
    public class CryptoSafe : AppCompatActivity
    {
        readonly string[] PermisosAlmacenamiento =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage
        };

        ListView listaArchivosSeleccionados;
        AdaptadorPersonalizado adaptador;
        EditText campoContrasena;
        Button botonCifrar;
        Button botonDescifrar;

        AlertDialog.Builder dialog;
        const string tag = "MyApp";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InicializarComponentesInterfaz();

            dialog = new AlertDialog.Builder(this);
            adaptador = new AdaptadorPersonalizado(ArchivosData.Archivos);
            listaArchivosSeleccionados.Adapter = adaptador;

            if (Intent.Action == Intent.ActionSend)
            {
                adaptador.LimpiarLista();
                var rutaArchivo = ObtenerRutaArchivo((Android.Net.Uri)Intent.Extras.GetParcelable(Intent.ExtraStream));
                Toast.MakeText(this, "Agregado: " + rutaArchivo, ToastLength.Long).Show();
                AgregarArchivoALista(rutaArchivo);
            }
            else if (Intent.Action == Intent.ActionSendMultiple)
            {
                for (int i = 0; i < Intent.ClipData.ItemCount; i++)
                {
                    var rutaArchivo = ObtenerRutaArchivo(Intent.ClipData.GetItemAt(i).Uri);
                    Toast.MakeText(this, "Agregado: " + rutaArchivo, ToastLength.Long).Show();
                    AgregarArchivoALista(rutaArchivo);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case 0:
                    if (grantResults[0] == Permission.Granted)
                        Toast.MakeText(this, "Permisos concedidos\nVuelva a dar clic en el botón", ToastLength.Long).Show();
                    else
                        Toast.MakeText(this, "Permisos no concedidos.\nEncrypto no puede funcionar sin esos permisos", ToastLength.Long).Show();
                    break;
            }
        }

        private Task<bool> ConfirmarEliminacionArchivos()
        {
            var respuesta = new TaskCompletionSource<bool>();
            
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
                if (ContextCompat.CheckSelfPermission(this, PermisosAlmacenamiento[0]) == (int)Permission.Granted && ContextCompat.CheckSelfPermission(this, PermisosAlmacenamiento[1]) == (int)Permission.Granted)
                {
                    string contrasena = campoContrasena.Text;
                    byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
                    bool eliminarArchivosOriginales = await ConfirmarEliminacionArchivos();
                    await DescifrarArchivos(keyMaterial, eliminarArchivosOriginales);
                    Toast.MakeText(this, "Tarea de descifrado terminada", ToastLength.Long).Show();
                    adaptador.LimpiarLista();
                }
                else if (ActivityCompat.ShouldShowRequestPermissionRationale(this, PermisosAlmacenamiento[0]) || ActivityCompat.ShouldShowRequestPermissionRationale(this, PermisosAlmacenamiento[1]))
                    ActivityCompat.RequestPermissions(this, PermisosAlmacenamiento, 0);
            }
            else
                Toast.MakeText(this, "Actualmente no hay archivos en la lista", ToastLength.Short).Show();
            
        }

        private async void BotonCifrar_Click(object sender, System.EventArgs e)
        {
            Log.Info(tag, "Botón de cifrar presionado");
            if (adaptador.archivos.Count > 0)
            {

                if (ContextCompat.CheckSelfPermission(this, PermisosAlmacenamiento[0]) == (int)Permission.Granted && ContextCompat.CheckSelfPermission(this, PermisosAlmacenamiento[1]) == (int)Permission.Granted)
                {
                    string contrasena = campoContrasena.Text;
                    byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
                    bool eliminarArchivosOriginales = await ConfirmarEliminacionArchivos();
                    await CifrarArchivos(keyMaterial, eliminarArchivosOriginales);
                    Toast.MakeText(this, "Tarea de cifrado terminada", ToastLength.Long).Show();
                    adaptador.LimpiarLista();
                }
                else if (ActivityCompat.ShouldShowRequestPermissionRationale(this, PermisosAlmacenamiento[0]) || ActivityCompat.ShouldShowRequestPermissionRationale(this, PermisosAlmacenamiento[1]))
                    ActivityCompat.RequestPermissions(this, PermisosAlmacenamiento, 0);
            }
            else
                Toast.MakeText(this, "Actualmente no hay archivos en la lista", ToastLength.Short).Show();
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
                    await Crypto.CifradoDescifradoAsincrono(keyMaterial, archivo.Nombre, rutaDestino, true);
                    if (eliminarArchivos)
                        File.Delete(archivo.Nombre);
                        
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
                    if (eliminarArchivos)
                        File.Delete(archivo.Nombre);
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