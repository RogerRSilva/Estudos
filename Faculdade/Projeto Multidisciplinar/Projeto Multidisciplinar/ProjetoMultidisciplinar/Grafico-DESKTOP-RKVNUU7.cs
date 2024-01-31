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

        public static double altura, vlcInicial, anguloRad;
        public static int angulo;
        public static double Vx, Vy, tVoo, tTotal, alcanceX, alcanceY, posPontoY;

        public Grafico()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("montou o gráfico");

            //Limpa os campos
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
            anguloRad = angulo * (Math.PI / 180.0);
            Vx = vlcInicial * Math.Cos(anguloRad);
            Vy = vlcInicial * Math.Sin(anguloRad);
            tVoo = Math.Abs(Vy / 9.81);
            tTotal = 2 * tVoo;
            alcanceY = ((Vy * Vy) / (2 * 9.81)) + altura;
            alcanceX = Vx * tTotal;

            textBox6.Text = tVoo.ToString("0.00");
            textBox5.Text = alcanceY.ToString("0.00");
            textBox4.Text = alcanceX.ToString("0.00");

            for (double i = 0; i <= alcanceX; i = i + 0.1)
            {
                double tempo = i / Vx;

                posPontoY = altura + Vy * tempo - (0.5 * (9.81 * (tempo * tempo)));

                // Adiciona os pontos
                series.Points.AddXY(i, posPontoY);
                if (altura > 0)
                {
                    alcanceX++;
                }   
                    
                if (posPontoY <= 0 && i != 0)
                {
                    alcanceX = 0;
                }
                
            }
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            angulo = hScrollBar1.Value;
            textBox2.Text = angulo.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            altura = double.Parse(textBox1.Text);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            angulo = int.Parse(textBox2.Text);

            hScrollBar1.Value = angulo;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            vlcInicial = double.Parse(textBox3.Text);
        }
    }
}
