using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SD_TimeTable{
class Ausgabe{

            public static void TitelAusgabe(string name, int semester)
            {
                Console.WriteLine("########################### " + name + " : " + semester + " ################################");
                Console.WriteLine("___________________________________________________________________");

                Console.WriteLine("      MO         DI            MI              DO             FR");
                Console.WriteLine("___________________________________________________________________");

            }

            public static void PlanAusgabe(KursFinal[,] KursplanNachStudiengangUndSemester)
            {

        for (int block = 0; block < 4; block++)
        {
            for (int tag = 0; tag < 5; tag++)
            {

                try
                {
                    Console.Write(" " + KursplanNachStudiengangUndSemester[tag, block].Name + "  " + "|");
                }
                catch (Exception)
                {
                    Console.Write(" keine Vorl. |");
                }
            }
            Console.WriteLine();
        }
    }
}
}
