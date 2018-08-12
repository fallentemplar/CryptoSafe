using System;
using System.Text;
using System.Windows.Forms;
using SecurityDriven.Inferno;

namespace ProyectoCifrado2
{
    public partial class Form1 : Form
    {
        //static byte[] textoSalt = Encoding.UTF8.GetBytes("hola");
        //static ArraySegment <byte> salt = new ArraySegment<byte>(textoSalt, 0, 0);

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
                var contrasena = Encoding.UTF8.GetBytes(campo_contrasena.ToString());
                var cifrado = new ArraySegment<byte>(Encoding.UTF8.GetBytes(campo_cifrar.Text), 0, Encoding.UTF8.GetBytes(campo_cifrar.Text).Length);
                var salt = new ArraySegment<byte>(Encoding.UTF8.GetBytes("hola"), 0, 4);
                byte[] textoCifrado = SuiteB.Encrypt(contrasena, cifrado,salt);
                campo_descifrar.Text = Encoding.UTF8.GetString(textoCifrado);
                //campo_cifrar.Clear();
            }
        }

        private void boton_descifrar_Click(object sender, EventArgs e)
        {
            if (campo_contrasena.TextLength > 0 && campo_descifrar.TextLength>0)
            {
                
            }
        }
    }
}
