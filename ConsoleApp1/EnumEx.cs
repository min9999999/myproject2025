using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class EnumEx
    {
        public enum DialogResult {Yes = 10, No, Cancel, Confirm = 50, OK}

        public static void PrintDialogResults()
        {
            Console.WriteLine((int)DialogResult.Yes);
            Console.WriteLine((int)DialogResult.No);
            Console.WriteLine((int)DialogResult.Cancel);
            Console.WriteLine((int)DialogResult.Confirm);
            Console.WriteLine((int)DialogResult.OK);
        }
    }
}
