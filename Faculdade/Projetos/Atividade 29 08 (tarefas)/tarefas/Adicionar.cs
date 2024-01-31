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
using System.Text.RegularExpressions;

namespace tarefas
{
    public partial class Adicionar : Form
    {
        public static string diretorio, caminhoArq, arquivo, arquivoNome = "Tarefas.txt";

        public static int listaCont = 1;

        public Adicionar()
        {
            InitializeComponent();
            textBox1.Text = diretorio;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            diretorio = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(diretorio))
                {
                    arquivo = Path.Combine(diretorio, arquivoNome);
                    if (!File.Exists(arquivo))
                    { 
                        File.Create(arquivo).Close();
                        MessageBox.Show("Lista de Tarefas CRIADA com sucesso!", "Diretório Válido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        File.WriteAllText(arquivo, listaCont.ToString() + ". " + Atividades.tarefa + Environment.NewLine);
                        listaCont++;
                        this.Close();
                    }
                    else
                    {  
                        ReescreveArquivo();
                    }
                }
                else
                {
                    MessageBox.Show("Diretório Inválido", "",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    diretorio = "";
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Não foi possível criar a tarefa!" + ex.Message,"", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        void ReescreveArquivo()
        {
            try
            {
                File.AppendAllText(arquivo, listaCont.ToString() + ". " + Atividades.tarefa + Environment.NewLine);
                MessageBox.Show("Tarefa ADICIONADA a lista com sucesso!", "Diretório Válido",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                listaCont++;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Não foi possível criar a tarefa!" + ex.Message, "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
    }
}
