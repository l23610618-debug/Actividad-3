using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ejercicio 1: Tres máquinas A, B y C producen el 45%, 30% y 25% de las piezas.
// Porcentaje de piezas defectuosas: 3%, 4% y 5%.
//   a) P(D)   b) P(B|D)   c) ¿Qué máquina es más probable dada una pieza defectuosa?

namespace Ejercicio1_Maquinas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string[] maquinas = { "A", "B", "C" };
            double[] prior = { 0.45, 0.30, 0.25 };       // P(A), P(B), P(C)
            double[] defectuosa = { 0.03, 0.04, 0.05 };  // P(D|A), P(D|B), P(D|C)

            // a) Probabilidad total: P(D) = sum P(Mi) * P(D|Mi)
            double pD = 0;
            for (int i = 0; i < maquinas.Length; i++)
            {
                pD += prior[i] * defectuosa[i];
            }

            Console.WriteLine("EJERCICIO 1: Máquinas A, B y C");
            Console.WriteLine("a) P(D) = " + pD.ToString("F3"));

            // b) y c) Teorema de Bayes: P(Mi|D) = P(Mi) * P(D|Mi) / P(D)
            int mejor = 0;
            double[] posterior = new double[maquinas.Length];
            for (int i = 0; i < maquinas.Length; i++)
            {
                posterior[i] = prior[i] * defectuosa[i] / pD;
                if (posterior[i] > posterior[mejor])
                {
                    mejor = i;
                }
            }

            Console.WriteLine("b) P(B|D) = " + posterior[1].ToString("F3"));
            Console.WriteLine("c) Probabilidades a posteriori:");
            for (int i = 0; i < maquinas.Length; i++)
            {
                Console.WriteLine("   P(" + maquinas[i] + "|D) = " + posterior[i].ToString("F3"));
            }
            Console.WriteLine("   La máquina más probable es la " + maquinas[mejor] + ".");

            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();
        }
    }
}