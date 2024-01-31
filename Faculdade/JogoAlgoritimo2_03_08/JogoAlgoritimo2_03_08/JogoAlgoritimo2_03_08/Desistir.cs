using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace JogoAlgoritimo2_03_08
{
    public partial class Desistir : Form
    {
        public Desistir()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("O número é: " + JogoAlgoritmo.nComputador.ToString(), "Desistir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            JogoAlgoritmo.tentativas = 0;
            this.Hide();
        }
    }
}
