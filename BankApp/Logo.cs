using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    internal class Logo
    {
        public static void BankLogo()
        {
            // for our bank app ???

            string[] logo = { @" 
               __________
              /          \
             /   -------  \
            |   | 5555 |   |
            |   | 5    |   | 
            |   | 555  |   |
            |   |    5 |   |
            |   |    5 |   |
             \  | 555  |  /
              \  ------  /
               `________´

          _____________________
         /                    /|
        /____________________/ |
        |  ________________  | |
        | |   ________    |  | |
        | |  | $$$$ | |   |  | |
        | |  | $$$$ | |   |  | |
        | |  | $$$$ | |   |  | |
        | |  |______| |   |  | |
        | |  FiveBANK |   |  | |
        | |___________|   |  | |
        |_________________|_/_/
                                    " };

            for (int i = 0; i < logo.Length; i++)
            {
                string line = logo[i];

                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    if (c == '5' || c == '$')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if ("./\\_|=-´`".Contains(c))
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if ("FiveBANK".Contains(c))
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}
