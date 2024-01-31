using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace tarefas
{
    public partial class Atividades : Form
    {
        public static string tarefa;
        public Atividades()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tarefa = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adicionar adiciona = new Adicionar();
            adiciona.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = File.ReadAllText(Adicionar.arquivo);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
