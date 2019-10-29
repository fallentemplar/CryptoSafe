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
using static Android.Support.V7.Widget.RecyclerView;

namespace CryptoSafeAndroid
{
    class AdaptadorPersonalizado : BaseAdapter<Archivo>
    {
        public List<Archivo> archivos { get; }

        public AdaptadorPersonalizado(List<Archivo> archivos)
        {
            this.archivos = archivos;
        }

        public override Archivo this[int posicion] {
            get
            {
                return archivos[posicion];
            }
        }

        public override int Count
        {
            get
            {
                return archivos.Count;
            }
        }

        public override long GetItemId(int posicion)
        {
            return posicion;
        }

        public override View GetView(int posicion, View convertView, ViewGroup parent)
        {
            var vista = convertView;

            if (vista == null)
            {
                vista = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.archivo_registro, parent, false);

                TextView nombre = vista.FindViewById<TextView>(Resource.Id.nombreTextView);
                TextView tamano = vista.FindViewById<TextView>(Resource.Id.tamanoTextView);
                TextView extension = vista.FindViewById<TextView>(Resource.Id.extensionTextView);

                vista.Tag = new ViewHolder() { Nombre = nombre, Tamano = tamano, Extension = extension };
            }

            var holder = (ViewHolder)vista.Tag;

            holder.Nombre.Text = archivos[posicion].Nombre;
            holder.Tamano.Text = archivos[posicion].Tamano;
            holder.Extension.Text = archivos[posicion].Extension;
            return vista;
        }

        public void AgregarArchivoALista(Archivo archivo)
        {
            archivos.Add(archivo);
            this.NotifyDataSetChanged();
        }

        public void LimpiarLista()
        {
            archivos.Clear();
            this.NotifyDataSetChanged();
        }
    }
}