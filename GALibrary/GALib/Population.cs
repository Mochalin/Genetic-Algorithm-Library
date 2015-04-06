using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{   
    public class Population<T> where T : IGenProvide
    {
        private int count;
        public List<Chromosome<T>> chromosomes;
        public double? TotalFitness = null;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public Population(int count)
        {
            this.count = count;
            chromosomes = new List<Chromosome<T>>(count);
        }
        public Population(List<Chromosome<T>> chromosomes)
        {
            this.count = chromosomes.Count;
            this.chromosomes = new List<Chromosome<T>>(chromosomes);
        }
        public void FitnessCalculate()
        {
            double result = 0;
            foreach (Chromosome<T> chr in chromosomes)
                result += chr.FitnessOfChromosome();
            TotalFitness = result;
        }
        public void Print()
        {
            for (int i = 0; i < chromosomes.Count; i++)
                Console.WriteLine(chromosomes[i]);
        }
        public static Population<T> operator +(Population<T> popul1, Population<T> popul2)
        {
            Population<T> result = new Population<T>(popul1.Count + popul2.Count);
            for (int i = 0; i < popul1.count; i++)
                result.chromosomes.Add(new Chromosome<T>(popul1.chromosomes[i]));
            for (int i = 0; i < popul2.count; i++)
            {
                result.chromosomes.Add(new Chromosome<T>(popul2.chromosomes[i]));
            }
            return result;
        }
    }
}
