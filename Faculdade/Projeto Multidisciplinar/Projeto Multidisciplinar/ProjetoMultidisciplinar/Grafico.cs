using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TesteGrafico
{
    public partial class Grafico : Form
    {

        //declara variaveis
        public static double altura, distancia, gravidade = 9.8, anguloMinimo;
        public static double anguloLancamento, tangenteLancamento, velocidadeInicial, valorY = 0;
        bool validaAngulo = false;
        bool verificaValor = false;

        //inicia formulario e impede que o usuário redimensione o tamanho
        public Grafico()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        //ao clicar no botão "Calcular"
        private void button1_Click(object sender, EventArgs e)
        {
            //Limpa os campos
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Legends.Clear();
            //quando ele clica o componente de gráfico é criado
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "ChartArea";
            chartArea.AxisX.Title = "Eixo X";
            chartArea.AxisY.Title = "Eixo Y";
            chart1.ChartAreas.Add(chartArea);

            // Criar uma série para o gráfico de linha
            Series series = new Series();
            series.ChartArea = "ChartArea";
            series.ChartType = SeriesChartType.Line; // Definir o tipo de gráfico como grafico de linha
            series.BorderWidth = 3;
            chart1.Series.Add(series);

            //CALCULOS
            VerificaAngulo();

            if (validaAngulo == true)
            {
                GeraGrafico(series);
                AdicionarAlvo();
            }

        }
        //Gera o grafico visualmente e realiza calculos
        private void GeraGrafico(Series s)
        {
            //calcula a tangente utilizando o angulo inserido pelo usuario
            tangenteLancamento = calculaTangenteLancamento(anguloLancamento);

            //calcula a velocidade inicial com as informações armazenadas
            velocidadeInicial = calculaVelocidade(altura, distancia, gravidade, tangenteLancamento);
            double velocidadeInicialX = calculaVelInicialX(velocidadeInicial, anguloLancamento);
            double velocidadeInicialY = calculaVelInicialY(velocidadeInicial, anguloLancamento);
            
            //calcula o tempo até o acerto do alvo
            double tempo = calculaTempo(distancia, velocidadeInicial);

            //calcula o componente que irá dizer se o alvo foi alcançado em movimento ascendente ou descendente
            double componenteVertical = calculaComponenteVertical(velocidadeInicial, gravidade, tempo);

            //converte o angulo inserido pelo usuário para radianos
            double anguloRad = anguloLancamento * (Math.PI / 180.0);

            //Calcula o alcance máximo do lançamento
            double alcanceX = (Math.Pow(velocidadeInicial, 2) * Math.Sin(2 * anguloRad)) / gravidade;

            //Printa a informação no campo do formulário
            if (componenteVertical > 0)
            {
                textBox7.Text = "Ascedente";
            }
            else
            {
                textBox7.Text = "Descedente";
            }
            //monta a equação da trajetória
            string equacaoTrajetoria = montaEquacaoTrajetoria(tangenteLancamento, gravidade, velocidadeInicial);

            //printa os valores adquiridos nos seus respectivos campos
            textBox6.Text = tempo.ToString();
            textBox5.Text = velocidadeInicial.ToString();
            textBox4.Text = equacaoTrajetoria.ToString();
            textBox8.Text = velocidadeInicialY.ToString();
            textBox9.Text = velocidadeInicialX.ToString();

            //cria a váriavel que irá armazenar os pontos no eixo y do gráfico
            double posPontoY = 0;

            //Coloca ponto por ponto no gráfico
            for (double i = 0.01; i <= alcanceX; i = i + 0.01)
            {

                //Calcula o ponto Y em funnção de X
                posPontoY = (tangenteLancamento) * i - ((gravidade * (1 + Math.Pow(tangenteLancamento, 2))) / ((2 * Math.Pow(velocidadeInicial, 2)))) * (i * i);

                double posPontoYArredondado = Math.Round(posPontoY, 5);

                // Adiciona os pontos
                s.Points.AddXY(i, posPontoYArredondado);

                //verifica quando a trajetória passa pelo alvo
                if (Math.Abs(i - distancia) < 0.01)
                {
                    //Próximo ponto da trajetoria no eixo X
                    double direcao = i + 0.01;

                    //Próximo ponto da trajetoria no eixo Y
                    double alvoPontoY = (tangenteLancamento) * direcao - ((gravidade * (1 + Math.Pow(tangenteLancamento, 2))) / ((2 * Math.Pow(velocidadeInicial, 2)))) * (direcao * direcao);

                    //Se o próximo ponto no eixo Y for maior que o atual
                    if (alvoPontoY - posPontoY > 0)
                    {
                        //movimento "ascendente", ou seja, para cima
                        textBox7.Text = "Ascedente";
                    }
                    //se for menor
                    else if (alvoPontoY - posPontoY < 0)
                    {
                        //movmento "descendente", ou seja, para baixo
                        textBox7.Text = "Descedente";
                    }
                    //se for igual
                    else
                    {
                        //Ponto de vértice, quando não está "caindo" nem "subindo"
                        textBox7.Text = "Ponto de vértice";
                    }
                }

            }
        }

        //Adiciona o alvo no gráfico
        private void AdicionarAlvo()
        {
            //Cria uma nova serie para adicionar o alvo
            Series seriePonto = new Series("SeriePonto");

            //utiliza as coordenadas que o usuário inseriu
            seriePonto.Points.AddXY(distancia, altura);

            //utiliza o tipo ponto
            seriePonto.ChartType = SeriesChartType.Point;
            //cor do alvo: vermelho
            seriePonto.BorderColor = Color.Red;  
            seriePonto.Color = Color.Red;
            //aumenta o tamanho para que possa ser destacado no gráfico
            seriePonto.MarkerSize = 8;

            chart1.Series.Add(seriePonto);

        }
        // Este método é chamado quando a barra de rolagem (ScrollBar) é movida.
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // Atualiza a variável anguloLancamento com o valor da barra de rolagem.
            anguloLancamento = hScrollBar1.Value;

            // Atualiza o texto da TextBox2 com o valor do ângulo de lançamento.
            textBox2.Text = anguloLancamento.ToString();
        }

        // Este método verifica se o ângulo inserido na TextBox2 é válido.
        private void VerificaAngulo()
        {
            // Inicializa a flag validaAngulo como falso.
            validaAngulo = false;

            // Tenta converter o texto da TextBox2 para um valor inteiro.
            if (int.TryParse(textBox2.Text, out int valor))
            {
                // Converte o valor inteiro para um valor double.
                double anguloInserido = int.Parse(textBox2.Text);

                // Verifica se o ângulo inserido está dentro dos limites especificados.
                if (anguloInserido < 90 && anguloInserido > anguloMinimo)
                {
                    // Atualiza a variável anguloLancamento com o valor inserido.
                    anguloLancamento = anguloInserido;

                    // Define a flag validaAngulo como verdadeira.
                    validaAngulo = true;

                    // Atualiza a posição da barra de rolagem para refletir o novo valor do ângulo.
                    hScrollBar1.Value = (int)anguloLancamento;
                }
                else
                {
                    // Exibe uma mensagem de erro se o ângulo inserido estiver fora dos limites.
                    MessageBox.Show("Valor inválido. Insira um valor menor que 90° e maior que o ângulo mínimo.",
                        "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Limpa o conteúdo da TextBox2.
                    textBox2.Text = "";
                }
            }
            else
            {
                // Chama o método Erro1 se a conversão para int falhar.
                Erro1();
            }
        }

        // Este método é chamado quando o botão 2 é clicado.
        private void button2_Click(object sender, EventArgs e)
        {
            // Variável para verificar se a conversão de valores foi bem-sucedida.
            bool confereValor = false;

            try
            {
                // Tenta converter os textos das TextBoxes para valores double.
                altura = double.Parse(textBox1.Text);
                distancia = double.Parse(textBox3.Text);
            }
            catch
            {
                // Se a conversão falhar, chama o método Erro1, limpa as TextBoxes e define a flag confereValor como verdadeira.
                Erro1();
                textBox3.Clear();
                textBox1.Clear();
                confereValor = true;
            }

            // Se a conversão foi bem-sucedida, chama o método validaValor.
            if (confereValor == false)
            {
                validaValor(distancia, altura);
            }

            // Se a validação do valor for verdadeira, realiza ações adicionais.
            if (verificaValor == true)
            {
                // Calcula o ângulo mínimo com base na altura e distância.
                anguloMinimo = calculaAngulo(altura, distancia);

                // Exibe mensagens informativas com base nos cálculos.
                MessageBox.Show("Angulo mínimo: " + anguloMinimo, "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Informe um ângulo maior do que esse e menor que 90º.", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Configura visibilidade e propriedades de controles adicionais.
                hScrollBar1.Visible = true;
                textBox2.ReadOnly = false;
                button1.Visible = true;

                // Arredonda o ângulo mínimo para calcular o valor mínimo na barra de rolagem.
                double anguloArredondado = Math.Round(anguloMinimo, 1);

                // Atualiza o valor mínimo na barra de rolagem.
                if (anguloArredondado < anguloMinimo + 0.49)
                {
                    hScrollBar1.Minimum = (int)anguloArredondado + 1;
                }
            }
        }

        // Este método valida se os valores de distância (dis) e altura (alt) são válidos.
        // Retorna a distância para ser utilizada em outros cálculos.
        private double validaValor(double dis, double alt)
        {
            // Verifica se a distância ou altura são não positivas e exibe uma mensagem de erro se necessário.
            if (dis <= 0 || alt <= 0)
            {
                Erro2(); // Chama o método Erro2 em caso de valor não positivo.
                textBox3.Clear();
                textBox1.Clear();
                verificaValor = false; // Define a flag verificaValor como falso.
            }
            else
            {
                verificaValor = true; // Se os valores são válidos, define a flag verificaValor como verdadeira.
            }

            return dis; // Retorna a distância para ser utilizada em outros cálculos.
        }

        // Este método exibe uma mensagem de erro quando a conversão de valores falha.
        private void Erro1()
        {
            MessageBox.Show("Valor inválido. Insira apenas tipos númericos.",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Este método exibe uma mensagem de erro quando valores não positivos são inseridos.
        private void Erro2()
        {
            MessageBox.Show("Valor inválido. Insira apenas valores positivos maiores que zero.",
                "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Este método calcula o ângulo com base na altura (a) e distância (d).
        public static double calculaAngulo(double a, double d)
        {
            return Math.Atan(a / d) * (180 / Math.PI);
        }

        // Este método calcula a velocidade com base na altura (a), distância (d), aceleração devido à gravidade (g) e tangente do ângulo (tan).
        public static double calculaVelocidade(double a, double d, double g, double tan)
        {
            return Math.Sqrt(-(g * Math.Pow(d, 2) * (1 + Math.Pow(tan, 2))) / (2 * (a - d * tan)));
        }

        // Este método é chamado quando o botão 3 é clicado.
        private void button3_Click(object sender, EventArgs e)
        {
            // Cria uma nova instância do formulário Menu.
            Menu men = new Menu();

            // Exibe o formulário Menu.
            men.Show();

            // Fecha o formulário atual.
            this.Close();
        }

        // Este método calcula o tempo de voo com base na distância (d) e na velocidade (v).
        public static double calculaTempo(double d, double v)
        {
            return d / v;
        }

        // Este método calcula a componente vertical do movimento com base na velocidade (v), na aceleração devido à gravidade (g) e no tempo (t).
        public static double calculaComponenteVertical(double v, double g, double t)
        {
            return v - g * t;
        }

        // Este método calcula a componente horizontal da velocidade inicial com base na velocidade (v) e no ângulo de lançamento (ang).
        public static double calculaVelInicialX(double v, double ang)
        {
            return v * Math.Cos(ang);
        }

        // Este método calcula a componente vertical da velocidade inicial com base na velocidade (v) e no ângulo de lançamento (ang).
        public static double calculaVelInicialY(double v, double ang)
        {
            return v * Math.Sin(ang);
        }

        // Este método calcula a tangente do ângulo de lançamento convertido de graus para radianos.
        public static double calculaTangenteLancamento(double ang)
        {
            ang = ang * (Math.PI / 180);
            return Math.Tan(ang);
        }

        // Este método monta a equação da trajetória do movimento com base na tangente (tan), na aceleração devido à gravidade (g) e na velocidade (v).
        public static string montaEquacaoTrajetoria(double tan, double g, double v)
        {
            return $"{tan}x - {(g * (1 + Math.Pow(tan, 2))) / (2 * Math.Pow(v, 2))}x²";
        }
    }
}
