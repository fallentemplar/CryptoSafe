using System;
using System.Text;
using System.Windows.Forms;
using SecurityDriven.Inferno;
using SecurityDriven.Inferno.Extensions;

namespace ProyectoCifrado2
{
    public partial class Form1 : Form
    {
        static byte[] claveMaestra;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void boton_cifrar_Click(object sender, EventArgs e)
        {
            if(campo_contrasena.TextLength>0 && campo_cifrar.TextLength > 0)
            {
                claveMaestra = Utils.SafeUTF8.GetBytes(campo_contrasena.Text);
                var textoCifrado = Cifrar(campo_cifrar.Text);
                campo_descifrar.Text = textoCifrado;
                campo_cifrar.Clear();
            }
        }

        private void boton_descifrar_Click(object sender, EventArgs e)
        {
            if (campo_contrasena.TextLength > 0 && campo_descifrar.TextLength>0)
            {
                claveMaestra = Utils.SafeUTF8.GetBytes(campo_contrasena.Text);
                var textoDescifrado = Descifrar(campo_descifrar.Text);
                campo_cifrar.Text = textoDescifrado;
                campo_descifrar.Clear();
            }
        }

        public static string Cifrar(string textoPlano)
        {
            var seg_array_texto = Utils.SafeUTF8.GetBytes(textoPlano).AsArraySegment();
            var textoCifrado = SuiteB.Encrypt(claveMaestra, seg_array_texto);
            return textoCifrado.ToB64Url();
        }

        public static string Descifrar(string textoCifrado)
        {
            var textoCifradoArrSeg = textoCifrado.FromB64Url().AsArraySegment();
            var decryptedPassword = SuiteB.Decrypt(
                claveMaestra,
                textoCifradoArrSeg
                );

            return Utils.SafeUTF8.GetString(decryptedPassword);
        }


    }
}
