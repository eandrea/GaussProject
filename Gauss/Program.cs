using Gauss.MyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            SystemOfLinearEquations a = new SystemOfLinearEquations();
            a.SetA(3);
            a.Setb();
            List<double> x = a.GaussMethod();

            int i = 1;
            foreach (double item in x)
            {
                Console.WriteLine("x{0}= {1}", i, item);
                ++i;
            }

            Console.ReadKey();
        }
        #endregion
    }
}
