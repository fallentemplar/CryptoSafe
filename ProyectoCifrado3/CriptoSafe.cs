using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace ProyectoCifrado3
{
    public partial class CriptoSafe : Form
    {
        //const string colorPrimario = "#FF0288D1";
        const string colorPrimario = "#FF00BCD4";
        const string colorSecundario = "#FF000000";//"#FFB2EBF2";
        const string colorBordes = "#FFFFFFFF";
        const string colorBotones = "#FF03A9F4";
        //const string colorTexto = "#FFFFFFFF";


        string[] listaArchivos;
        string contrasena;
        bool eliminarArchivos = false;

        private int ObtenerRGB(string color)
        {
            return Int32.Parse(color.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);
        }

        public CriptoSafe()
        {
            InitializeComponent();
            caja_archivos.AllowDrop = true;
            caja_archivos.DragDrop += Caja_archivos_DragDrop;
            caja_archivos.DragEnter += Caja_archivos_DragEnter;
            FormBorderStyle = FormBorderStyle.None;
            
            caja_archivos.BackColor = Color.FromArgb(ObtenerRGB(colorPrimario));
            caja_archivos.BackColor = Color.FromArgb(ObtenerRGB(colorPrimario));
            caja_archivos.ForeColor = Color.White;

            BackColor = Color.FromArgb(ObtenerRGB(colorPrimario));

            boton_cifrar.BackColor= Color.FromArgb(ObtenerRGB(colorPrimario));
            boton_descifrar.BackColor = Color.FromArgb(ObtenerRGB(colorPrimario));
            boton_eliminar.BackColor = Color.FromArgb(ObtenerRGB(colorPrimario));
            campo_contrasena.BackColor = Color.FromArgb(ObtenerRGB(colorPrimario));

            campo_contrasena.ForeColor = Color.FromArgb(ObtenerRGB(colorBordes));
            etiqueta_contrasena.ForeColor = Color.FromArgb(ObtenerRGB(colorBordes));
        }

        private void Caja_archivos_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }


        private void Caja_archivos_DragDrop(object sender, DragEventArgs e)
        {
            string[] handles = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string path in handles)
            {
                if (File.Exists(path) && ListBox.NoMatches == caja_archivos.FindStringExact(path))
                {
                    caja_archivos.Items.Add(path.ToString());
                }
            }
            /*string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
                //caja_archivos.Text = files.First(); //select the first one  
                foreach(string file in files)
                {
                    caja_archivos.Items.Add(file.ToString());
                }
              */  
        }

        private void Boton_cifrar_Click(object sender, EventArgs e)
        {
            Thread hiloCifrar;
            if (caja_archivos.Items.Count == 0)
            {
                MessageBox.Show("No hay archivos seleccionados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (campo_contrasena.TextLength > 0)
            {
                DialogResult dialogoEliminarArchivos = MessageBox.Show("¿Desea eliminar los archivos originales después de la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                eliminarArchivos = dialogoEliminarArchivos == DialogResult.Yes ? true : false;
                contrasena = campo_contrasena.Text;
                listaArchivos = caja_archivos.Items.OfType<string>().ToArray();
                hiloCifrar = new Thread(CifrarArchivos);
                hiloCifrar.Start();
                campo_contrasena.Clear();
                caja_archivos.Items.Clear();
            }
        }

        private void CifrarArchivos()
        {
            Procesando barraProgreso = new Procesando(listaArchivos.Length);
            barraProgreso.Show();
            foreach (var archivo in listaArchivos)
            {
                byte[] bytesCifrados = Criptografia.Cifrar(archivo.ToString(), contrasena);
                bool archivoEscrito = Archivos.EscribirArchivo(bytesCifrados, archivo.ToString(), true);
                if (eliminarArchivos && archivoEscrito) //Si hay que borrar y se pudo escribir el nuevo
                    Archivos.EliminarArchivo(archivo.ToString());
                else if(eliminarArchivos && !archivoEscrito)
                    MessageBox.Show("El archivo '" + Path.GetFileName(archivo.ToString()) + "' no fue cifrado porque ya existe en el directorio destino\nSu archivo original no fue eliminado");
                else if(!archivoEscrito)
                    MessageBox.Show("El archivo '"+ Path.GetFileName(archivo.ToString())+"' no fue cifrado porque ya existe en el directorio destino");
                barraProgreso.Avanzar();
            }
            barraProgreso.Hide();
            MessageBox.Show("La tarea ha finalizado");
        }

        private void Boton_descifrar_Click(object sender, EventArgs e)
        {
            Thread hiloDescifrar;
            if (caja_archivos.Items.Count == 0){
                DialogResult dialogoSinArchivos = MessageBox.Show("No hay archivos seleccionados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if (campo_contrasena.TextLength > 0){
                DialogResult dialogoEliminarArchivos = MessageBox.Show("¿Desea eliminar los archivos cifrados después de la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                eliminarArchivos = dialogoEliminarArchivos == DialogResult.Yes ? true : false;
                contrasena = campo_contrasena.Text;
                listaArchivos = caja_archivos.Items.OfType<string>().ToArray();
                hiloDescifrar = new Thread(DescifrarArchivos);
                hiloDescifrar.Start();
                campo_contrasena.Clear();
                caja_archivos.Items.Clear();
            }
        }

        private void DescifrarArchivos()
        {
            Procesando barraProgreso = new Procesando(listaArchivos.Length);
            barraProgreso.Show();
            foreach (var archivo in listaArchivos)
            {
                byte[] bytesCifrados = Criptografia.Descifrar(archivo.ToString(), contrasena);
                bool archivoEscrito = false;
                if (bytesCifrados != null)
                {
                    archivoEscrito = Archivos.EscribirArchivo(bytesCifrados, archivo.ToString(), false);
                    if (eliminarArchivos && archivoEscrito) //Si hay que borrar y se pudo escribir el nuevo
                        Archivos.EliminarArchivo(archivo.ToString());
                    else if (eliminarArchivos && !archivoEscrito)
                        MessageBox.Show("El archivo '" + Path.GetFileName(archivo.ToString()) + "' no fue descifrado porque ya existe en el directorio destino\nSu archivo original no fue eliminado.");
                    else if (!archivoEscrito)
                        MessageBox.Show("El archivo '" + Path.GetFileName(archivo.ToString()) + "' no fue descifrado porque ya existe en el directorio destino.");
                }
                else
                    MessageBox.Show("El archivo '" + Path.GetFileName(archivo.ToString()) 
                        + "' no fue descifrado.\nEsto puede deberse a las siguientes razones:\n* El archivo está dañado\n* No es un archivo cifrado con CryptoSafe.\n* Contraseña incorrecta");
                barraProgreso.Avanzar();
                //Thread.Sleep(2000);
            }
            barraProgreso.Hide();
            MessageBox.Show("La tarea ha finalizado");
        }

        private void Boton_eliminar_Click(object sender, EventArgs e)
        {
            caja_archivos.Items.Clear();
        }
    }
}