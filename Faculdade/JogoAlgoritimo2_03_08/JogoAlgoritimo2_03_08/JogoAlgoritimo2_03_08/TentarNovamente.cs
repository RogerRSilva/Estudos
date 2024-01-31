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
    public partial class TentarNovamente : Form
    {
        public TentarNovamente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JogoAlgoritmo jogo = new JogoAlgoritmo();
            jogo.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
