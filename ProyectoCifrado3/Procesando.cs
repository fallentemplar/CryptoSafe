using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCifrado3
{
    public partial class Procesando : Form
    {
        int Avance;
        public Procesando(int avance)
        {
            this.Text = "Progreso";
            InitializeComponent();
            Avance = 100/avance;
        }

        public void Avanzar()
        {
            barraProgreso.Value += Avance;
        }
    }
}
