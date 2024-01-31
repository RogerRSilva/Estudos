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
    public partial class Malha : Form
    {

        // Variável estática para armazenar a quantidade de malhas.
        public static int qtdMalhas;

        // Flag para verificar se o valor inserido é um número.
        bool ehNumero = false;

        // Arrays para armazenar dados relacionados às malhas e cálculos.
        double[] malha = new double[2];
        double[,] malhas = new double[4, 5];
        double[,] calculo = new double[1, 2];
        double[,] calculo1 = new double[3, 4];
        double[,] calculo2 = new double[2, 3];
        double[,] calculo3 = new double[1, 2];

        // Variáveis estáticas para armazenar correntes em diferentes malhas.
        public static double corrente1, corrente2, corrente3, corrente4, corrente5, corrente6, corrente7;

        // Construtor da classe Malha.
        public Malha()
        {
            InitializeComponent();

            // Configuração de características do formulário.
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            comboBox1.Items.Add(1);
            comboBox1.Items.Add(2);
            comboBox1.Items.Add(3);
            comboBox1.Items.Add(4);
        }

        // Método chamado quando o botão 2 é clicado.
        private void button2_Click(object sender, EventArgs e)
        {
            // Verifica se os valores inseridos são números.
            ehNumero = VerificaValores();

            // Se não forem números, exibe uma mensagem de erro.
            if (ehNumero == false)
            {
                Erro1();
            }
            else
            {
                // Com base na quantidade de malhas, chama o método de cálculo apropriado.
                if (qtdMalhas == 1)
                {
                    CalcUmaMalha();
                }
                if (qtdMalhas == 2)
                {
                    CalcDuasMalhas();
                }
                if (qtdMalhas == 3)
                {
                    CalcTresMalhas();
                }
                if (qtdMalhas == 4)
                {
                    CalcQuatroMalhas();
                }
            }

            // Abre o formulário de resultados de malha.
            ResultadosMalha resultado = new ResultadosMalha();
            resultado.ShowDialog();
        }

        // Método chamado quando o botão 3 é clicado.
        private void button3_Click(object sender, EventArgs e)
        {
            // Cria uma nova instância do formulário Menu.
            Menu men = new Menu();

            // Exibe o formulário Menu.
            men.Show();

            // Fecha o formulário atual.
            this.Close();
        }

        // Método chamado quando o botão "Confirmar" é clicado.
        private void button1_Click(object sender, EventArgs e)
        {
            // Verifica se um item foi selecionado no ComboBox.
            if (comboBox1.SelectedItem != null)
            {
                // Obtém a quantidade de malhas a partir do item selecionado no ComboBox.
                qtdMalhas = (int)comboBox1.SelectedItem;

                // Chama o método reposicionaForm para ajustar o formulário com base na quantidade de malhas.
                reposicionaForm();
            }
        }

        // Método para verificar se os valores inseridos nas TextBoxes são números válidos.
        private bool VerificaValores()
        {
            // Itera sobre as TextBoxes no GroupBox1 e verifica se os valores são números válidos.
            foreach (Control verificaNumero in groupBox1.Controls)
            {
                if (verificaNumero is TextBox numero)
                {
                    if (!validaNumero(numero.Text))
                    {
                        return false;
                    }
                }
            }

            // Itera sobre as TextBoxes no GroupBox2 e verifica se os valores são números válidos.
            foreach (Control verificaNumero in groupBox2.Controls)
            {
                if (verificaNumero is TextBox numero)
                {
                    if (!validaNumero(numero.Text))
                    {
                        return false;
                    }
                }
            }

            // Itera sobre as TextBoxes no GroupBox3 e verifica se os valores são números válidos.
            foreach (Control verificaNumero in groupBox3.Controls)
            {
                if (verificaNumero is TextBox numero)
                {
                    if (!validaNumero(numero.Text))
                    {
                        return false;
                    }
                }
            }

            // Itera sobre as TextBoxes no GroupBox4 e verifica se os valores são números válidos.
            foreach (Control verificaNumero in groupBox4.Controls)
            {
                if (verificaNumero is TextBox numero)
                {
                    if (!validaNumero(numero.Text))
                    {
                        return false;
                    }
                }
            }

            // Retorna verdadeiro se todos os valores forem números válidos.
            return true;
        }

        // Método para validar se o texto é um número.
        private bool validaNumero(string texto)
        {
            return double.TryParse(texto, out _);
        }

        // Método de cálculo para uma malha.
        private void CalcUmaMalha()
        {
            // Obtém valores das TextBoxes e calcula a corrente na primeira malha.
            malha[0] = double.Parse(textBox1.Text);
            malha[1] = double.Parse(textBox2.Text);
            corrente1 = -malha[1] / malha[0];
        }

        // Método de cálculo para duas malhas.
        private void CalcDuasMalhas()
        {
            // Obtém valores das TextBoxes e realiza cálculos para correntes em duas malhas.
            malhas[0, 0] = double.Parse(textBox3.Text);
            malhas[0, 1] = double.Parse(textBox5.Text);
            malhas[0, 2] = double.Parse(textBox7.Text);
            malhas[1, 0] = double.Parse(textBox4.Text);
            malhas[1, 1] = double.Parse(textBox6.Text);
            malhas[1, 2] = double.Parse(textBox8.Text);

            // Simplifica o sistema de equações e calcula as correntes.
            calculo = simplificaSistema(malhas, 2);

            corrente2 = -calculo[0, 1] / calculo[0, 0];
            corrente1 = -(malhas[0, 1] * corrente2 + malhas[0, 2]) / malhas[0, 0];
            corrente3 = verificaCorrente(corrente1, corrente2);
        }

        // Método de cálculo para três malhas.
        private void CalcTresMalhas()
        {
            // Obtém valores das TextBoxes e realiza cálculos para correntes em três malhas.
            malhas[0, 0] = double.Parse(textBox9.Text);
            malhas[0, 1] = double.Parse(textBox10.Text);
            malhas[0, 2] = double.Parse(textBox11.Text);
            malhas[0, 3] = double.Parse(textBox12.Text);
            malhas[1, 0] = double.Parse(textBox16.Text);
            malhas[1, 1] = double.Parse(textBox15.Text);
            malhas[1, 2] = double.Parse(textBox14.Text);
            malhas[1, 3] = double.Parse(textBox13.Text);
            malhas[2, 0] = double.Parse(textBox20.Text);
            malhas[2, 1] = double.Parse(textBox19.Text);
            malhas[2, 2] = double.Parse(textBox18.Text);
            malhas[2, 3] = double.Parse(textBox17.Text);

            // Simplifica o sistema de equações duas vezes e calcula as correntes.
            calculo1 = simplificaSistema(malhas, 3);
            calculo2 = simplificaSistema(calculo1, 2);

            corrente3 = -calculo2[0, 1] / calculo2[0, 0];
            corrente2 = -(calculo1[0, 1] * corrente3 + calculo1[0, 2]) / calculo1[0, 0];
            corrente1 = -(malhas[0, 1] * corrente2 + malhas[0, 2] * corrente3 + malhas[0, 3]) / malhas[0, 0];

            corrente4 = verificaCorrente(corrente1, corrente2);
            corrente5 = verificaCorrente(corrente2, corrente3);
        }

        // Método chamado quando o "Limpar" é clicado.
        private void button4_Click(object sender, EventArgs e)
        {
            // Chama o método para limpar os valores de todas as GroupBoxes no formulário.
            LimparValoresTodasGroupBoxes(this);
        }

        // Método para limpar os valores de todas as TextBoxes em todas as GroupBoxes no formulário.
        private void LimparValoresTodasGroupBoxes(Form formulario)
        {
            // Itera sobre os controles do formulário.
            foreach (Control controle in formulario.Controls)
            {
                // Verifica se o controle é uma GroupBox.
                if (controle is GroupBox groupBox)
                {
                    // Chama o método para limpar os valores das TextBoxes na GroupBox.
                    LimparValoresTextBoxes(groupBox);
                }
            }
        }

        // Método para limpar os valores de todas as TextBoxes em uma GroupBox.
        private void LimparValoresTextBoxes(GroupBox groupBox)
        {
            // Itera sobre os controles na GroupBox.
            foreach (Control controle in groupBox.Controls)
            {
                // Verifica se o controle é uma TextBox.
                if (controle is TextBox textBox)
                {
                    // Define o texto da TextBox como "0" para limpar o valor.
                    textBox.Text = "0";
                }
            }
        }

        // Método de cálculo para quatro malhas.
        private void CalcQuatroMalhas()
        {
            // Obtém valores das TextBoxes e realiza cálculos para correntes em quatro malhas.
            malhas[0, 0] = double.Parse(textBox32.Text);
            malhas[0, 1] = double.Parse(textBox31.Text);
            malhas[0, 2] = double.Parse(textBox30.Text);
            malhas[0, 3] = double.Parse(textBox29.Text);
            malhas[0, 4] = double.Parse(textBox28.Text);
            malhas[1, 0] = double.Parse(textBox27.Text);
            malhas[1, 1] = double.Parse(textBox26.Text);
            malhas[1, 2] = double.Parse(textBox25.Text);
            malhas[1, 3] = double.Parse(textBox24.Text);
            malhas[1, 4] = double.Parse(textBox23.Text);
            malhas[2, 0] = double.Parse(textBox22.Text);
            malhas[2, 1] = double.Parse(textBox21.Text);
            malhas[2, 2] = double.Parse(textBox40.Text);
            malhas[2, 3] = double.Parse(textBox39.Text);
            malhas[2, 4] = double.Parse(textBox38.Text);
            malhas[3, 0] = double.Parse(textBox37.Text);
            malhas[3, 1] = double.Parse(textBox36.Text);
            malhas[3, 2] = double.Parse(textBox35.Text);
            malhas[3, 3] = double.Parse(textBox34.Text);
            malhas[3, 4] = double.Parse(textBox33.Text);

            // Simplifica o sistema de equações três vezes e calcula as correntes.
            calculo1 = simplificaSistema(malhas, 4);
            calculo2 = simplificaSistema(calculo1, 3);
            calculo3 = simplificaSistema(calculo2, 2);

            corrente4 = -calculo3[0, 1] / calculo3[0, 0];
            corrente3 = -(calculo2[0, 1] * corrente4 + calculo2[0, 2]) / calculo2[0, 0];
            corrente2 = -(calculo1[0, 1] * corrente3 + calculo1[0, 2] * corrente4 + calculo1[0, 3]) / calculo1[0, 0];
            corrente1 = -(malhas[0, 1] * corrente2 + malhas[0, 2] * corrente3 + malhas[0, 3] * corrente4 + malhas[0, 4]) / malhas[0, 0];

            corrente5 = verificaCorrente(corrente1, corrente2);
            corrente6 = verificaCorrente(corrente2, corrente3);
            corrente7 = verificaCorrente(corrente3, corrente4);
        }

        // Método para reposicionar o formulário com base na quantidade de malhas.
        public void reposicionaForm()
        {
            // Define o tamanho e visibilidade das GroupBoxes com base na quantidade de malhas.
            if (qtdMalhas == 1)
            {
                this.Size = new System.Drawing.Size(550, 172);
                groupBox1.Visible = true;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
            }
            else if (qtdMalhas == 2)
            {
                this.Size = new System.Drawing.Size(550, 213);
                groupBox1.Visible = false;
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
            }
            else if (qtdMalhas == 3)
            {
                this.Size = new System.Drawing.Size(550, 240);
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                groupBox4.Visible = false;
            }
            else if (qtdMalhas == 4)
            {
                this.Size = new System.Drawing.Size(550, 307);
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = true;
            }

            // Centraliza o formulário e reposiciona os botões.
            CentralizarFormulario();
            ReposicionaBotoes();
        }

        // Método para reposicionar os botões com base na quantidade de malhas.
        private void ReposicionaBotoes()
        {
            // Define as posições dos botões e labels com base na quantidade de malhas.
            if (qtdMalhas == 1)
            {
                button2.Location = new System.Drawing.Point(435, 104);
                button3.Location = new System.Drawing.Point(263, 104);
                button4.Location = new System.Drawing.Point(349, 104);

                label71.Location = new System.Drawing.Point(13, 104);
                label72.Location = new System.Drawing.Point(13, 116);
            }
            else if (qtdMalhas == 2)
            {
                button2.Location = new System.Drawing.Point(435, 142);
                button3.Location = new System.Drawing.Point(263, 142);
                button4.Location = new System.Drawing.Point(349, 142);

                label71.Location = new System.Drawing.Point(13, 142);
                label72.Location = new System.Drawing.Point(13, 154);
            }
            else if (qtdMalhas == 3)
            {
                button2.Location = new System.Drawing.Point(435, 168);
                button3.Location = new System.Drawing.Point(263, 168);
                button4.Location = new System.Drawing.Point(349, 168);

                label71.Location = new System.Drawing.Point(13, 168);
                label72.Location = new System.Drawing.Point(13, 180);
            }
            else if (qtdMalhas == 4)
            {
                button2.Location = new System.Drawing.Point(435, 235);
                button3.Location = new System.Drawing.Point(263, 235);
                button4.Location = new System.Drawing.Point(349, 235);

                label71.Location = new System.Drawing.Point(13, 235);
                label72.Location = new System.Drawing.Point(13, 247);
            }
        }

        private void CentralizarFormulario()
        {
            // Calcula a nova posição para manter o formulário no centro da tela
            int novaPosicaoX = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int novaPosicaoY = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

            // Define a nova posição do formulário
            this.Location = new System.Drawing.Point(novaPosicaoX, novaPosicaoY);
        }

        // Função que simplifica o sistema de equações lineares representado pela matriz malhas.
        // Recebe a matriz malhas e a quantidade de equações (quant), retorna uma nova matriz simplificada.
        public static double[,] simplificaSistema(double[,] malhas, int quant)
        {
            // Cria uma nova matriz para o sistema simplificado.
            double[,] sistema = new double[quant - 1, quant];

            // Itera sobre as linhas da nova matriz.
            for (int i = 0; i < quant - 1; i++)
            {
                // Itera sobre as colunas da nova matriz.
                for (int j = 0; j < quant; j++)
                {
                    // Calcula os elementos da nova matriz com base na regra de eliminação de Gauss.
                    sistema[i, j] = (malhas[0, 0] * malhas[i + 1, j + 1]) - (malhas[i + 1, 0] * malhas[0, j + 1]);
                }
            }

            // Retorna a matriz simplificada.
            return sistema;
        }

        // Função que verifica a corrente entre dois ramos, c1 e c2.
        // Retorna a diferença absoluta entre c1 e c2.
        public static double verificaCorrente(double c1, double c2)
        {
            // Verifica a diferença absoluta entre c1 e c2.
            if (c1 > c2)
            {
                return c1 - c2;
            }
            else
            {
                return c2 - c1;
            }
        }

        // Função para exibir uma mensagem de erro genérica para valores inválidos.
        private void Erro1()
        {
            MessageBox.Show("Valor inválido. Insira apenas tipos númericos.",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
