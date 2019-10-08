using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace ProyectoCifrado3
{
    public partial class CriptoSafe : Form
    {
        const string colorPrimario = "#FF00BCD4";
        const string colorSecundario = "#FF000000";
        const string colorBordes = "#FFFFFFFF";
        const string colorBotones = "#FF03A9F4";

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
                byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
                CifrarArchivos(keyMaterial, eliminarArchivos);
                campo_contrasena.Clear();
                caja_archivos.Items.Clear();
            }
        }

        private async Task CifrarArchivos(byte[] keyMaterial, bool eliminarArchivos)
        {
            Procesando barraProgreso = new Procesando(listaArchivos.Length);
            barraProgreso.Show();
            foreach (var archivo in listaArchivos)
            {
                try
                {
                    string rutaDestino = Path.Combine(Path.GetDirectoryName(archivo), Path.GetFileName(archivo)) + ".crypt";

                    await ProyectoCifrado3.Crypto.CifradoDescifradoAsincrono(keyMaterial, archivo, rutaDestino, true);

                    if (eliminarArchivos)
                        Archivos.EliminarArchivo(archivo.ToString());
                }
                catch (System.Security.Cryptography.CryptographicException)
                {
                    MessageBox.Show("El archivo " + Path.GetFileName(archivo) + " no pudo ser cifrado");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("El archivo '" + Path.GetFileName(archivo) + "' no pudo ser cifrado\n" +
                        "El archivo especificado no existe");
                }
                barraProgreso.Avanzar();
            }
            barraProgreso.Hide();
            MessageBox.Show("La tarea ha finalizado");
        }

        private void Boton_descifrar_Click(object sender, EventArgs e)
        {
            if (caja_archivos.Items.Count == 0){
                DialogResult dialogoSinArchivos = MessageBox.Show("No hay archivos seleccionados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if (campo_contrasena.TextLength > 0){
                DialogResult dialogoEliminarArchivos = MessageBox.Show("¿Desea eliminar los archivos cifrados después de la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                eliminarArchivos = dialogoEliminarArchivos == DialogResult.Yes ? true : false;
                contrasena = campo_contrasena.Text;
                listaArchivos = caja_archivos.Items.OfType<string>().ToArray();
                byte[] keyMaterial = Crypto.DerivarClaveDeContrasena(contrasena, 256);
                DescifrarArchivos(keyMaterial, eliminarArchivos);
                campo_contrasena.Clear();
                caja_archivos.Items.Clear();
            }
        }

        private async Task DescifrarArchivos(byte[] keyMaterial, bool eliminarArchivos)
        {
            Procesando barraProgreso = new Procesando(listaArchivos.Length);
            barraProgreso.Show();
            foreach (var archivo in listaArchivos)
            {
                try
                {
                    string rutaDestino = Path.Combine(Path.GetDirectoryName(archivo), Path.GetFileNameWithoutExtension(archivo));

                    await ProyectoCifrado3.Crypto.CifradoDescifradoAsincrono(keyMaterial, archivo, rutaDestino, false);

                    if (eliminarArchivos)
                        Archivos.EliminarArchivo(archivo.ToString());
                }
                catch (System.Security.Cryptography.CryptographicException)
                {
                    Archivos.EliminarArchivo(Path.Combine(Path.GetDirectoryName(archivo),Path.GetFileNameWithoutExtension(archivo)));
                    MessageBox.Show("El archivo '" + Path.GetFileName(archivo) + "' no pudo ser descifrado\n" +
                        "Esto puede deberse a una de las siguientes razones:" +
                        "\n1-El archivo está dañado\n2-La contraseña es incorrecta\n3-El archivo no fue cifrado con CryptoSafe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("El archivo '" + Path.GetFileName(archivo) + "' no pudo ser descifrado\n" +
                        "El archivo especificado no existe");
                }
                barraProgreso.Avanzar();
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