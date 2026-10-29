using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NaiveBayesSpam
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fuerza el punto decimal sin importar la configuración regional
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // Datos del problema
            var priors = new Dictionary<string, double>
            {
                { "Spam", 0.30 },
                { "No Spam", 0.70 }
            };

            var likelihoods = new Dictionary<string, Dictionary<string, double>>
            {
                { "Spam",    new Dictionary<string, double> { { "Gana", 0.60 }, { "Dinero", 0.80 } } },
                { "No Spam", new Dictionary<string, double> { { "Gana", 0.05 }, { "Dinero", 0.10 } } }
            };

            var mensaje = new List<string> { "Gana", "Dinero" };
            string textoMensaje = string.Join(" ", mensaje);

            Dictionary<string, double> scores;
            Dictionary<string, double> probs;
            NaiveBayes(priors, likelihoods, mensaje, out scores, out probs);

            Console.WriteLine("Mensaje: " + textoMensaje + "\n");

            Console.WriteLine("Paso 1 y 2: Puntuaciones proporcionales");
            foreach (var kv in scores)
                Console.WriteLine("  (" + kv.Key + ") = " + kv.Value.ToString("F4"));

            Console.WriteLine("\nPaso 3: Normalización");
            Console.WriteLine("  Suma de scores = " + scores.Values.Sum().ToString("F4"));
            foreach (var kv in probs)
                Console.WriteLine("  P(" + kv.Key + " | '" + textoMensaje + "') = " +
                                  kv.Value.ToString("F4") + " (" + (kv.Value * 100).ToString("F2") + "%)");

            var ganadora = probs.OrderByDescending(kv => kv.Value).First();
            Console.WriteLine("\nResultado: " +
                              ganadora.Key.ToUpper() + " con una confianza del " +
                              (ganadora.Value * 100).ToString("F2") + "%.");

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }

        static void NaiveBayes(
            Dictionary<string, double> priors,
            Dictionary<string, Dictionary<string, double>> likelihoods,
            List<string> palabras,
            out Dictionary<string, double> scores,
            out Dictionary<string, double> probs)
        {
            scores = new Dictionary<string, double>();

            // Pasos 1 y 2: puntuación proporcional por clase
            foreach (var kv in priors)
            {
                double score = kv.Value;
                foreach (var palabra in palabras)
                    score *= likelihoods[kv.Key][palabra]; // independencia condicional
                scores[kv.Key] = score;
            }

            // Paso 3: normalización
            double total = scores.Values.Sum();
            probs = scores.ToDictionary(kv => kv.Key, kv => kv.Value / total);
        }
    }
}