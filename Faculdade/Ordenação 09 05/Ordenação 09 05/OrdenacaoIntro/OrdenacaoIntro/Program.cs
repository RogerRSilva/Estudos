using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenacaoIntro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite uma lista de palavras separadas por espaço:");
            string input = Console.ReadLine();
            string[] words = input.Split(' '); // Dividir a entrada do usuário em um array de palavras.
            InsertionSort(words); // Chamar a função de ordenação por inserção.
            Console.WriteLine("Lista ordenada em ordem alfabética:");
            foreach (string word in words)
            {
                Console.WriteLine(word + " "); // Exibir a lista ordenada em ordem alfabética.
                Console.ReadKey();
            }
        }
        static void InsertionSort(string[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                string key = arr[i];
                int j = i - 1;
                while (j >= 0 && string.Compare(arr[j], key) > 0)
                {
                    arr[j + 1] = arr[j]; // Deslocar palavras maiores para a direita.
                    j--;
                }
                arr[j + 1] = key; // Inserir a palavra na posição correta.
            }
        }
        /*
        int[] cvrValores = new int[5];
        string vlrInserido;

        Console.WriteLine("Insira 5 valores numéricos: ");
        for (int ii = 0; ii < cvrValores.Length; ii++)
        {
            vlrInserido = Console.ReadLine();
            if (int.TryParse(vlrInserido, out int numero))
            {
                cvrValores[ii] = int.Parse(vlrInserido);
            }
        }

        int i = 0; // Inicializa o índice 'i'
        int j = 1; // Inicializa o índice 'j'
        int aux = 0; // Variável auxiliar para armazenar o elemento a ser inserido
        while (j < 5) // Enquanto 'j' for menor que o tamanho do vetor
        {
            aux = cvrValores[j]; // Armazena o valor de v[j] em 'aux'
            i = j - 1; // Inicializa 'i' com o índice anterior a 'j'
                       // Enquanto 'i' for válido e o elemento atual for maior que 'aux'
            while ((i >= 0) && (cvrValores[i] > aux))
            {
                cvrValores[i + 1] = cvrValores[i]; // Move o elemento atual para a próxima posição
                i = i - 1; // Decrementa 'i'
            }
            cvrValores[i + 1] = aux; // Insere 'aux' na posição correta
            j = j + 1; // Incrementa 'j' para avançar para o próximo elemento
        }

        Console.WriteLine("Os valores inseridos em ordem crescente é: ");
        for (int y = 0; y < cvrValores.Length; y++)
        {
            Console.WriteLine("");    
            Console.WriteLine(cvrValores[y]);
        }
        Console.ReadKey();
        */
    }

    
}
