using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoAlgoritimo2_03_08
{
    public partial class JogoAlgoritmo : Form
    {
        public static int validaN;
        //Instancia a classe random
        public static Random random = new Random();
        //numero que o computador irá escolher
        public static int nComputador;

        public static int nJogador;

        public static int tentativas = 0;

        public JogoAlgoritmo()
        {
            InitializeComponent();
            EscolheN();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool nTrue = true;
            if (!int.TryParse(textBox1.Text, out validaN))
            {
                MessageBox.Show("Digite um número válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = ""; // Limpa o conteúdo da caixa de texto
                nTrue = false;
            }
            if(nTrue == true)
            {
                nJogador = int.Parse(textBox1.Text);
                ComparaNum();
            }
        }
        void EscolheN()
        {
            nComputador = random.Next(1, 101);
        }

        void ComparaNum()
        {
            if(tentativas > 3)
            {
                Desistir desiste = new Desistir();
                desiste.Show();
            }
            if (nJogador < nComputador)
            {
                tentativas++;
                MessageBox.Show("O número é maior.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = ""; // Limpa o conteúdo da caixa de texto
            }
            if(nJogador > nComputador)
            {
                tentativas++;
                MessageBox.Show("O número é menor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = ""; // Limpa o conteúdo da caixa de texto
            }
            if (nJogador == nComputador)
            {
                MessageBox.Show("Parabéns, você acertou!", "Ótimo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = ""; // Limpa o conteúdo da caixa de texto
                TentarNovamente tentarNovamente= new TentarNovamente();
                tentarNovamente.Show();
                this.Hide();
            }
        }
    }
}
