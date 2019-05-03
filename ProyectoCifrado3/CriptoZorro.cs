using System;
using System.IO;
using System.Windows.Forms;


namespace ProyectoCifrado3
{
    public partial class CriptoZorro : Form
    {
        public CriptoZorro()
        {
            InitializeComponent();
            caja_archivos.AllowDrop = true;
            caja_archivos.DragDrop += Caja_archivos_DragDrop;
            caja_archivos.DragEnter += Caja_archivos_DragEnter;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
            if(caja_archivos.Items.Count==0){
                DialogResult dialogoSinArchivos = MessageBox.Show("No hay archivos seleccionados","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }else if (campo_contrasena.TextLength > 0){
                //Procesando p = new Procesando();
                //p.ShowDialog();
                string contrasena=campo_contrasena.Text;
                foreach(var archivo in caja_archivos.Items){
                    byte[] bytesCifrados = Criptografia.Cifrar(archivo.ToString(), contrasena);
                    Archivos.EscribirArchivo(bytesCifrados,archivo.ToString(),true);
                }
                caja_archivos.Items.Clear();
            }
        }

        private void Boton_descifrar_Click(object sender, EventArgs e)
        {
            if (caja_archivos.Items.Count == 0){
                DialogResult dialogoSinArchivos = MessageBox.Show("No hay archivos seleccionados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if (campo_contrasena.TextLength > 0){
                /*Procesando p = new Procesando();
                p.ShowDialog();*/
                //var textoDescifrado = Criptografia.Descifrar(textoCifrado, campo_contrasena.Text);
                //MessageBox.Show(textoDescifrado);
                string contrasena = campo_contrasena.Text;
                foreach (var archivo in caja_archivos.Items){
                    byte[] bytesCifrados = Criptografia.Descifrar(archivo.ToString(), contrasena);
                    Archivos.EscribirArchivo(bytesCifrados, archivo.ToString(), false);
                }
                caja_archivos.Items.Clear();
            }
        }

        private void Boton_eliminar_Click(object sender, EventArgs e)
        {
            caja_archivos.Items.Clear();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe acerca = new AcercaDe();
            acerca.ShowDialog();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;
                string[] archivos;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    archivos = openFileDialog.FileNames;
                    foreach (string archivo in archivos)
                    {
                        caja_archivos.Items.Add(archivo);
                    }
                }
            }
        }

        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}