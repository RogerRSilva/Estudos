using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteGrafico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Configura componentes visuais do windows para deixar o formulário mais
            agradável visualmente e inicia o menu*/

            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            Application.Run(new Menu());
        }
    }
}
