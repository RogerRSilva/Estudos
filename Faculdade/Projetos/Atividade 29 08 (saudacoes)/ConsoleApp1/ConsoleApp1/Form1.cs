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
//Bibliotecas declaradas

namespace ConsoleApp1
{
    public partial class Saudações : Form
    {
        //variaveis que irão armazenar as informações
        static string nome, saudacaoNova, diretorio, nomePasta, pasta, Arquivo;

        //variavel numerica que impede que crie arquivos com nome repetidos e que gere conflito
        private int contaArquivo = 1;

        //inicia o formulário
        public Saudações()
        {
            InitializeComponent();
            //inicia com os campos desabilitados até que seja definido se é uma mensagem nova ou existente
            listView1.Enabled = false;
            textBox4.Enabled = false;
            textBox3.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button4.Enabled = false;
            textBox2.Enabled = false;
            button6.Enabled = false;
        }
        //armazena o nome do Usuário
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            nome = textBox1.Text;
        }
        //armazena a mensagem que será criada
        private void textBox3_TextChanged(object sender, EventArgs e)
        { 
            saudacaoNova = textBox3.Text;
        }

        //classe que cria o arquivo com a mensagem
        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            { 
                //nomeia o arquivo
                string ArquivoNome = "saudacao" + contaArquivo.ToString() + ".txt";
                //cria o arquivo na pasta nova
                Arquivo = Path.Combine(pasta, ArquivoNome);
                File.Create(Arquivo).Close();
                //notifica o usuario
                MessageBox.Show("Arquivo criado com sucesso!");
                //altera o conteudo do arquivo para aquilo que o usuario digitou
                File.WriteAllText(Arquivo, saudacaoNova);
                //adiciona uma unidade no contador de arquivos após esse novo arquivo ter sido criado
                contaArquivo++;
            }
            //trata o erro de exceção caso não tenha conseguido criar o arquivo
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar o arquivo: " + ex.Message);
            }
        }

        //armazena o caminho inserido pelo usuário
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            diretorio = textBox2.Text;
        }

        //armazena o nome que o usuario deseja dar para a pasta
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            nomePasta = textBox4.Text;
        }

        //Verifica se o diretório digitado pelo usuário é um diretório valido
        private void button6_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(diretorio))
            {
                // O diretório existe
                MessageBox.Show("Diretório válido");
                //o campo pasta armazena aquele diretório
                pasta = diretorio;
                //habilita a lista e os botões de buscar e selecionar os arquivos daquela pasta
                button4.Enabled = true;
                listView1.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                //o diretório não é valido
                //se ele tiver inserido um diretório valido e depois um invalido, os botoes e os campos são desabilitados novamente
                button4.Enabled = false;
                listView1.Enabled = false;
                button7.Enabled = false;
                //notifica o usuário
                MessageBox.Show("Diretório inválido ou inexistente.");
            }
        }

        //Evento que possibilita o usuário selecionar apenas um arquivo por vez 
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string nomeArquivoSelecionado = listView1.SelectedItems[0].Text;
            }
        }
        //evento ativado ao clicar no botão "Selecionar", que irá abrir a mensagem do arquivo escolhido
        private void button7_Click(object sender, EventArgs e)
        {
            //se existir pelo menos um arquivo
            if (listView1.SelectedItems.Count > 0)
            {
                //string que armazena todo o caminho até o arquivo selecionado pelo usuario
                string caminhoArquivoSelecionado = Path.Combine(pasta, listView1.SelectedItems[0].Text);

                try
                {
                    //string que armazena o conteudo do arquivo 
                    string conteudoArquivo = File.ReadAllText(caminhoArquivoSelecionado);
                    //mostra na tela o nome inserido no programa mais a mensagem seleciona numa messageBox
                    MessageBox.Show(conteudoArquivo, "Para " + nome, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //trata erro de exceção caso não seja possível abrir o arquivo
                //ou nenhum arquivo tenha sido selecionado antes de clicar no botão
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao ler o arquivo: " + ex.Message);
                }
            }
        }
        //ao clicar no botão Sair fecha o programa
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Evento acionado ao clicar no botãO "BUSCAR" 
        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear(); // Limpa a ListView antes de preenchê-la novamente

            try
            {
                // Obtém a lista de arquivos na pasta especificada
                string[] arquivos = Directory.GetFiles(pasta);

                foreach (string arquivo in arquivos)
                {
                    // Adiciona o nome do arquivo à ListView
                    listView1.Items.Add(Path.GetFileName(arquivo));
                }
            }
            //trata o erro de exceção caso n seja possível listar os arquivos
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }
        //evento acionado ao criar um nome para uma pasta
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //Armazena o diretorio e o nome da pasta
                pasta = Path.Combine(diretorio, nomePasta);
                //se a pasta não existir    
                if (!Directory.Exists(pasta))
                {
                    //cria a pasta
                    Directory.CreateDirectory(pasta);
                    //notifica o usuario
                    MessageBox.Show("Pasta criada com sucesso!");
                }
                //Notifica o usuário caso não tenha sido possível criar a pasta
                else
                {
                    MessageBox.Show("Erro ao criar pasta.");
                }
            }
            //notifica o usuário caso não tenha sido possível criar a pasta quando ela já existe
            //ou o nome não foi especificado
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar a pasta: " + ex.Message);
            }
        }
        //Evento acionado ao selecionar a checkBox "Mensagem Nova"
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //se a caixa estiver marcada
            if (checkBox1.Checked)
            {
                //desabilita os campos utilizados quando irá usar uma mensagem existente
                checkBox2.Enabled = false;
                listView1.Enabled = false;
                button4.Enabled = false;

                //habilita os campos usados para criar uma mensagem
                button2.Enabled = true;

                textBox4.Enabled = true;
                textBox3.Enabled = true;
                button3.Enabled = true;
                button5.Enabled = true;
                textBox2.Enabled = true;
                button6.Enabled = true;
            }
            //se a caixa não estiver mais marcada
            else
            {
                //desabilita todos os campos novamente
                checkBox2.Enabled = true;
                textBox4.Enabled = false;
                textBox3.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = false;
                textBox2.Enabled = false;
                button6.Enabled = false;
            }
        }
        //Evento acionado ao selecionar a checkBox "Mensagem Existente"
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //se a caixa estiver marcada
            if (checkBox2.Checked)
            {
                //desabilita os campos utilizados quando irá criar uma mensagem do zero
                checkBox1.Enabled = false;
                //habilita os campos que servem para usar uma mensagem existente
                button2.Enabled = true;
                textBox2.Enabled = true;
                button6.Enabled = true;
            }
            //se a caixa não estiver marcada
            else
            {
                //desabilita todos os campos (que foram ativados) novamente
                checkBox1.Enabled = true;

                textBox2.Enabled = false;
                button6.Enabled = false;
            }
        }
    }
}
