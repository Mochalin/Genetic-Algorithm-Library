using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GALib;

namespace GA_using
{
    class Program
    {
        /// <summary>
        /// Simpl test function for optimization: -(x-2)^2+4
        /// Globax maximum x=2 y=4
        /// </summary>
        public static double testFunction(List<double> variable)
        {
            return 4-Math.Pow(variable[0]-2,2);
        }

        static void Main(string[] args)
        {

            Func<List<double>, double> fitnessFunction = testFunction;
            List<List<GenTextBinEncoding>> Gens;
            List<Chromosome<GenTextBinEncoding>> chromosomes;
            Population<GenTextBinEncoding> p;
            //set parametrs
            GAparametr parametrs = new GAparametr();
            parametrs.CrossoverProbality = 0.8;
            parametrs.MutationProbality = 0.15;
            parametrs.GenerationNumber = 100;


            GAClassic<GenTextBinEncoding> algorithm;
            double x;

            Gens = GenTextBinEncoding.Create(1, 60, new List<short> {6}, new List<double> {-10 }, new List<double> { 10 }, 0);
            chromosomes = Chromosome<GenTextBinEncoding>.Create(60, 1, 6, Gens, fitnessFunction, 0);
            p = new Population<GenTextBinEncoding>(chromosomes);
            algorithm = new GAClassic<GenTextBinEncoding>(p, parametrs);
            // Run classic genetic algorithm
            p = algorithm.Run();           
            p.chromosomes.Sort();
            x = algorithm.population.chromosomes[59].Gens[0].GetDigitalForm();
            Console.WriteLine("Exact solution x = 2  y = 4 ");
            Console.WriteLine("Solution obtained by genetic algorithm x={0}, y={1}", x, testFunction(new List<double> { x }));
            Console.ReadLine();
        }
    }
}
