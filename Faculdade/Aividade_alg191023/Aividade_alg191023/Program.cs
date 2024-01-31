using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aividade_alg191023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Questão 1!  

            /*int[] num = new int[5];
            bool erro = false;
            float media = 0;           
          
            for (int i = 0; i < num.Length; i++)
            {
                do
                {
                    erro = false;
                    try
                    {
                        Console.WriteLine($"digite o {i + 1}° número: ");
                        num[i] = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Digite Novamente!");
                        erro = true;
                    }

                } while (erro);                 
            }
            media = (num[0] + num[1] + num[2]) / 5;

            Console.WriteLine($" Número 1: {num[0]}\n Número 2: {num[1]}\n Número 3: {num[2]}\n Número 4: {num[3]}\n Número 5: {num[4]}\n Média: {media:F2} ");

            Console.ReadKey();

            char[] num = new char[100];
            
            for(int p = 0; p < num.Length; p++)
            {
                if (p % 2 == 0)
                {
                    num[p] = 'P';
                }
                else
                {
                    num[p] = 'I';
                }
            
            }
            for (int p = 0; p < num.Length; p++)
            {
                Console.WriteLine(num[p]+ $" {p}° elemento\n");
            }
            Console.ReadKey();*/

            /*int[,] num = new int[3, 3];
            int soma=0;
            int print=0;

            for(int i = 0; i<3; i++)
            {
                for(int j = 0; j<3; j++)
                {
                    print++;
                    Console.WriteLine($"Digite o {print}º número: ");
                    num[i, j] = int.Parse(Console.ReadLine());               
                soma = soma + num[i, j];                
                }
            }
            Console.WriteLine($"A soma dos elementos da matriz é: {soma}");

            Console.ReadKey();*/

            int[,] numeros = new int[4, 4];

            Console.WriteLine("Digite o valor mínimo presente na matriz: ");
            int mn = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor máximo presente na matriz: ");
            int mx = int.Parse(Console.ReadLine());


            for(int i = 0; int<4)
            {
                for()
                {



                    Random rnd = new Random();
                    int num = rnd.Next(mn, mx);

                }
            }



























        }
    }
}
