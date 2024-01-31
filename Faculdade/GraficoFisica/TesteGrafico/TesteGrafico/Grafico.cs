using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TesteGrafico
{
    public partial class Grafico : Form
    {

        public static double altura, distancia, gravidade = 9.8, anguloMinimo;
        public static double anguloLancamento, tangenteLancamento, velocidadeInicial, valorY = 0;

        public Grafico()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Limpa os campos
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Legends.Clear();
            //quando ele clica no componente um gráfico é criado
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
            tangenteLancamento = calculaTangenteLancamento(anguloLancamento);
            velocidadeInicial = calculaVelocidade(altura, distancia, gravidade, tangenteLancamento);
            double velocidadeInicialX = calculaVelInicialX(velocidadeInicial, anguloLancamento);
            double velocidadeInicialY = calculaVelInicialY(velocidadeInicial, anguloLancamento);
            double tempo = calculaTempo(distancia, velocidadeInicial);
            double componenteVertical = calculaComponenteVertical(velocidadeInicial, gravidade, tempo);

            double anguloRad = anguloLancamento * (Math.PI / 180.0);
            double Vx = velocidadeInicial * Math.Cos(anguloRad);
            double Vy = velocidadeInicial * Math.Sin(anguloRad);
            double tmpSubida = Math.Abs(Vy / gravidade);
            double tTotal = 2 * tmpSubida;
            double alcanceX = Vx * tTotal;
            double alcanceY = ((Vy * Vy) / (2 * gravidade));

            if (componenteVertical > 0)
            {

            }
            else
            {

            }
            string equacaoTrajetoria = montaEquacaoTrajetoria(tangenteLancamento, gravidade, velocidadeInicial);

            textBox6.Text = tempo.ToString();
            textBox5.Text = velocidadeInicial.ToString();
            textBox4.Text = equacaoTrajetoria.ToString();

            button1.Visible = false;

            double posPontoY = 0;

            for (double i = 0; i <= alcanceX; i = i + 0.01)
            {
                double tempoEstimado = i / Vx;

                posPontoY = Vy * tempoEstimado - (0.5 * (9.81 * (tempoEstimado * tempoEstimado)));

                // Adiciona os pontos
                series.Points.AddXY(i, posPontoY);
            }
        }
        private int EncontrarIndicePonto(Series serie, double x, double y)
        {
            int indice = 0;
            for (int i = 0; i < serie.Points.Count; i++)
            {
                DataPoint ponto = serie.Points[i];
                if (ponto.XValue == x && ponto.YValues[0] == y)
                {
                    indice = i; // Retorna o índice do ponto se encontrado
                    return indice;
                }
                
            }
            return indice;
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            anguloLancamento = hScrollBar1.Value;
            textBox2.Text = anguloLancamento.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double valor))
            {
                altura = double.Parse(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Valor inválido.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "0";
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int valor))
            {
                anguloLancamento = int.Parse(textBox2.Text);
            }
            else
            {
                MessageBox.Show("Valor inválido.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "0";
            }

            hScrollBar1.Value = (int)anguloLancamento;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double anguloMinimo = calculaAngulo(altura, distancia);
            MessageBox.Show("Angulo mínimo: " + anguloMinimo, "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Informe um ângulo maior do que esse e menor que 90º:", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hScrollBar1.Visible = true;
            textBox2.ReadOnly = false;
            button1.Visible = true;
            hScrollBar1.Minimum = (int)anguloMinimo;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox3.Text, out double valor))
            {
                distancia= double.Parse(textBox3.Text);
            }
            else
            {
                MessageBox.Show("Valor inválido.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Text = "0";
            }
            
        }
        public static double calculaAngulo(double a, double d)
        {
            return Math.Atan(a / d) * (180 / Math.PI);
        }

        public static double calculaVelocidade(double a, double d, double g, double tan)
        {
            return Math.Sqrt(-(g * Math.Pow(d, 2) * (1 + Math.Pow(tan, 2))) / (2 * (a - d * tan)));
        }

        public static double calculaTempo(double d, double v)
        {
            return d / v;
        }

        public static double calculaComponenteVertical(double v, double g, double t)
        {
            return v - g * t;
        }

        public static double calculaVelInicialX(double v, double ang)
        {
            return v * Math.Cos(ang);
        }

        public static double calculaVelInicialY(double v, double ang)
        {
            return v * Math.Sin(ang);
        }

        public static double calculaTangenteLancamento(double ang)
        {
            ang = ang * (Math.PI / 180);
            return Math.Tan(ang);
        }

        public static string montaEquacaoTrajetoria(double tan, double g, double v)
        {
            return $"{tan}x - {(g * (1 + Math.Pow(tan, 2))) / (2 * Math.Pow(v, 2))}x²";
        }
    }
}
