using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ejercicio 2: Tres urnas: A (3 rojas, 5 negras), B (2 rojas, 1 negra),
// C (2 rojas, 3 negras). Se elige una urna al azar y se extrae una bola.
// Si la bola es roja, ¿cuál es la probabilidad de que sea de la urna A? P(A|R)


namespace Ejercicio2_Urnas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string[] urnas = { "A", "B", "C" };
            double[] prior = { 1.0 / 3, 1.0 / 3, 1.0 / 3 };   // P(A), P(B), P(C)
            double[] roja = { 3.0 / 8, 2.0 / 3, 2.0 / 5 };     // P(R|A), P(R|B), P(R|C)

            // Probabilidad total: P(R) = sum P(Ui) * P(R|Ui)
            double pR = 0;
            for (int i = 0; i < urnas.Length; i++)
            {
                pR += prior[i] * roja[i];
            }

            Console.WriteLine("EJERCICIO 2: Tres urnas");
            Console.WriteLine("P(R) = " + pR.ToString("F4"));

            // Teorema de Bayes: P(Ui|R) = P(Ui) * P(R|Ui) / P(R)
            for (int i = 0; i < urnas.Length; i++)
            {
                double post = prior[i] * roja[i] / pR;
                Console.WriteLine("P(" + urnas[i] + "|R) = " + post.ToString("F3"));
            }

            // Resultado exacto: con denominador común 120,
            // P(R|A)=45/120, P(R|B)=80/120, P(R|C)=48/120 (el 1/3 de cada prior se cancela)
            int num = 45;
            int den = 45 + 80 + 48;
            Console.WriteLine("\nP(A|R) exacto = " + num + "/" + den + " ≈ " + ((double)num / den).ToString("F3"));

            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();
        }
    }
}