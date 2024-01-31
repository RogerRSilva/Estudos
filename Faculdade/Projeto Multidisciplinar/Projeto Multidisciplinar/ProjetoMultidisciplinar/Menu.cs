using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteGrafico
{
    public partial class Menu : Form
    {
        public Menu()
        {
            //Inicia o formulario Menu
            InitializeComponent();
            //Impede que o usuário redimensione as bordas
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Inicia o programa de lançamento obliquo
            Grafico grafico = new Grafico();
            grafico.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Inicia o programma de calculo de malhas de circuitos eletricos
            Malha malha = new Malha();
            malha.Show();
            this.Hide();
        }
    }
}
