using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    class Program
    {
        static void Main(string[] args)
        {
            //double[,] A = new double[,] { { 1, 2, -1 }, { 2, -1, 3 }, { -1, 3, 1 } };

            //List<double> b = new List<double>();
            //b.Add(4);
            //b.Add(3);
            //b.Add(6);

            double[,] A = new double[,] { { 1, -2, 5 }, { 1, -1, 3 }, { 3, -6, -1 } };

            List<double> b = new List<double>();
            b.Add(9);
            b.Add(2);
            b.Add(25);

            //double[,] A = new double[,] { { Math.Pow(10, -17), 1 }, { 1, 1 } };

            //List<double> b = new List<double>();
            //b.Add(1);
            //b.Add(2);

            //double[,] A = new double[,] { { 16, 136 }, { 136, 1496} };

            //List<double> b = new List<double>();
            //b.Add(2576);
            //b.Add(24104);

            List<double> x = GaussMethod(A, b);

            int i = 1;
            foreach (double item in x)
            {
                Console.WriteLine("x{0}= {1}", i, item);
                ++i;
            }
            Console.ReadKey();
        }

        #region Gauss Elimináció megvalósítása
        public static List<double> GaussMethod(double[,] A, List<double> b)
        {
            List<double> x = new List<double>();

            //0-dik dimenzió elemszáma, azaz a sorok száma ha 1-est írnék ,akkor az az oszlopok száma
            int n = A.GetLength(0); 

            for(int k = 0; k < n-1; ++k)
            {
                for(int i=k+1; i < n; ++i)
                {
                    // Részleges főelemkiválasztást hajtunk végre, ha a k-adik pivotelem 0 vagy 0-hoz közeli.
                    if (A[k, k] == 0 || A[k, k] < Math.Pow(10,-1))
                    {
                        //Részleges főelemkiválasztás (bemenő paraméterek: A együttható mátrix, b vektor, n a sorok száma
                        // és k az aktuális lépés.
                        PartialPivoting(ref A, ref b, n, k);
                    }

                    double gammaik = A[i, k] / A[k,k];

                    for (int h = k + 1; h < n; ++h)
                    {
                        A[i, h] = A[i, h] - gammaik * A[k, h];
                    }

                    b[i] -= gammaik * b[k];
                }
            }

            //Visszahelyettesítés
            x = Replacement(A, b, n);

            //Eredményvektor
            return x;
        }
        #endregion

        #region Részleges főelemkiválasztás
        private static void PartialPivoting(ref double[,] A, ref List<double> b, int n, int k)
        {
            //A[k,k] a k-adik pivotelem, mely alap esetben nem megfelelő, ha 0 értékű.
            // Részleges főelemkiválasztással adjuk meg A[k,k] értékét.
            double maxAkk = Math.Abs(A[k, k]);
            int maxIndex = k;

            for (int g = k; g < n; ++g)
            {
                if (Math.Abs(A[k, g]) > maxAkk)
                {
                    maxAkk = Math.Abs(A[g, k]);
                    maxIndex = g;
                }
            }
            //Sorcsere : maxindex és a k sorok cseréje ( A mátrixban és a b vektorban )
            RowSwap(maxIndex, k, ref A, ref b);
        }
        #endregion

        #region Sorok cseréje ( A mátrix és b vektor esetén )
        private static void RowSwap(int maxIndex, int k, ref double[,] A, ref List<double> b)
        {
            List<double> maxIndexRow = new List<double>();
            List<double> kIndexRow = new List<double>();

            for (int j = 0; j < A.GetLength(0); j++)
            {
                maxIndexRow.Add(A[maxIndex, j]);
            }

            for (int j = 0; j < A.GetLength(0); j++)
            {
                kIndexRow.Add(A[k, j]);
            }

            // Sorcsere az A mátrixban
            for (int i = 0; i < A.GetLength(0); ++i)
            {
                for (int j = 0; j < A.GetLength(0); j++)
                {
                   if(i==maxIndex)
                   {
                        A[i, j] = kIndexRow[j];
                   }
                   else if (i==k)
                   {
                        A[i, j] = maxIndexRow[j];
                   }
                }
            }

            // b vektor elemeinek cseréje
            double kIndexnItem = b[k];
            double maxIndexbItem = b[maxIndex];

            for (int i = 0; i < b.Count; i++)
            {
                if (i == k)
                {
                    b[i] = maxIndexbItem;
                }
                else if (i == maxIndex)
                {
                    b[i] = kIndexnItem;
                }
            }
        }
        #endregion

        #region Visszahelyettesítés
        private static List<double> Replacement(double[,] A, List<double> b, int n)
        {
            //x a megoldásvektorunk
            List<double> x = new List<double>();

            //Kezdetben feltöltjük az x vektort n db nullával. todo: tömörebben
            for (int k = 0; k < n; ++k)
                x.Add(0);

            //Az x vektorunk n hosszú, tehát az indexelése 0..(n-1) intervallum  között történik
            x[n - 1] = b[n - 1] / A[n-1, n-1];

            for(int i=n-2; i>=0; --i)
            {
                //Összegzés programozási tétel
                double S = 0;
                for(int j = i+1; j < n; ++j)
                {
                    S += A[i, j] * x[j];
                }

                x[i] = (b[i] - S) / A[i, i];
            }

            return x;
        }
        #endregion 
    }
}
