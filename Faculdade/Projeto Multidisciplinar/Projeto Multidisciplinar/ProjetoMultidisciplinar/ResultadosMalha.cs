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
    public partial class ResultadosMalha : Form
    {
        public ResultadosMalha()
        {
            // Inicializa os componentes do formulário.
            InitializeComponent();

            // Configura o estilo da borda do formulário para Fixa.
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Adiciona informações sobre as correntes ao TextBox.
            textBox1.AppendText($"1ª Corrente: {Malha.corrente1.ToString("0.00")} \r\n");
            textBox1.AppendText($"2ª Corrente: {Malha.corrente2.ToString("0.00")} \r\n");
            textBox1.AppendText($"3ª Corrente: {Malha.corrente3.ToString("0.00")} \r\n");
            textBox1.AppendText($"4ª Corrente: {Malha.corrente4.ToString("0.00")} \r\n");
            textBox1.AppendText($"5ª Corrente: {Malha.corrente5.ToString("0.00")} \r\n");
            textBox1.AppendText($"6ª Corrente: {Malha.corrente6.ToString("0.00")} \r\n");
            textBox1.AppendText($"7ª Corrente: {Malha.corrente7.ToString("0.00")} \r\n");
        }

        // Manipulador de evento para o botão de fechar o formulário.
        private void button1_Click(object sender, EventArgs e)
        {
            // Fecha o formulário.
            this.Close();
        }

    }
}
