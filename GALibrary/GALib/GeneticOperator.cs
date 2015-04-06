using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
    static public class GeneticOperator<T> where T : IGenProvide
    {
        static Random rng = new Random();
        ///<summary>
        /// A simple one-point crossover operator 
        /// returns one child
        /// </summary>
        static public Chromosome<T> Crossover(Chromosome<T> chr1, Chromosome<T> chr2)
        {
            Chromosome<T> childchr = new Chromosome<T>(chr1);
            int point = rng.Next(1, childchr.Length * childchr.GenLength);
            string child = string.Concat(chr1.ToString().Substring(0, point), chr2.ToString().Substring(point, chr2.Length * chr2.GenLength - point));
            for (int i = 0; i < childchr.Length; i++)
                childchr.Gens[i].Change(child.Substring(i * childchr.GenLength, childchr.GenLength));
            return childchr;
        }
        ///<summary>
        /// Simple mutation operator
        /// Swaps two random symbols in the chromosome
        ///</summary>
        static public Chromosome<T> Mutation(Chromosome<T> chr)
        {
            Chromosome<T> mutationchr = new Chromosome<T>(chr);
            int point1 = rng.Next(0, mutationchr.Length * mutationchr.GenLength);
            int point2 = rng.Next(0, mutationchr.Length * mutationchr.GenLength);
            StringBuilder mutation = new StringBuilder(mutationchr.ToString());
            char temp = mutation[point1];
            mutation[point1] = mutation[point2];
            mutation[point2] = temp;
            for (int i = 0; i < mutationchr.Length; i++)
                mutationchr.Gens[i].Change(mutation.ToString().Substring(i * mutationchr.GenLength, mutationchr.GenLength));
            return mutationchr;
        }
        
        static public Population<T> SelectionElite(Population<T> currentGeneration, int count)
        {
            Population<T> intermediateGeneration = new Population<T>(count);
            currentGeneration.FitnessCalculate();
            currentGeneration.chromosomes.Sort();

            for (int i = 0; i < count; i++)
            {
                intermediateGeneration.chromosomes.Add(new Chromosome<T>(currentGeneration.chromosomes[currentGeneration.Count - 1 - i]));

            }
            return intermediateGeneration;
        }
    }
}
