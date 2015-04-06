using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
       public class Chromosome<T> : IComparable<Chromosome<T>> where T : IGenProvide
    {
        public int id;
        private short length;
        private short genLength;
        public List<T> Gens;
        public double? Fitness = null;
        private Func<List<double>, double> fitnessFunction;
        public Func<List<double>, double> FitnessFunction
        {
            get { return fitnessFunction; }
            set { fitnessFunction = value; }
        }
        public short Length
        {
            get { return length; }
            set { length = value; }
        }
        public short GenLength
        {
            get { return genLength; }
            set { genLength = value; }
        }
        public Chromosome(short length, short genLength, Func<List<double>, double> fitnessFunction, int id = -1)
        {
            this.id = id;
            this.Length = length;
            this.GenLength = genLength;
            this.FitnessFunction = fitnessFunction;
            Gens = new List<T>(Length);
        }
        public Chromosome(short length, short genLength, List<T> gens, Func<List<double>, double> fitnessFunction, int id = -1)
        {
            this.id = id;
            this.Length = length;
            this.GenLength = genLength;
            this.FitnessFunction = fitnessFunction;
            Gens = new List<T>(gens);
        }

        public Chromosome(Chromosome<T> chromosome)
        {
            this.id = chromosome.id;
            this.Length = chromosome.Length;
            this.GenLength = chromosome.GenLength;
            this.FitnessFunction = chromosome.FitnessFunction;
            this.Gens = new List<T>(chromosome.Length);
            foreach (T gen in chromosome.Gens)
                this.Gens.Add((T)gen.Clone());

        }
        public double FitnessOfChromosome()
        {
            List<double> ValuesOfGen = new List<double>(Gens.Count);
            foreach (T gen in Gens)
                ValuesOfGen.Add(gen.GetDigitalForm());
            double result = FitnessFunction(ValuesOfGen); ;
            Fitness = result;
            return result;
        }
        public double FitnessOfChromosome(Func<List<double>, double> fitnessFunction)
        {
            List<double> ValuesOfGen = new List<double>(Gens.Count);
            foreach (T gen in Gens)
                ValuesOfGen.Add(gen.GetDigitalForm());
            double result = fitnessFunction(ValuesOfGen);
            Fitness = result;
            return result;
        }
        public int CompareTo(Chromosome<T> other)
        {
            if (other == null) return 1;
            return this.FitnessOfChromosome().CompareTo(other.FitnessOfChromosome());
        }
        static public List<Chromosome<T>> Create(int count, short length, short genLength, List<List<T>> gens, Func<List<double>, double> fitnessFunction, int id = -1)
        {
            List<Chromosome<T>> chromosomes = new List<Chromosome<T>>(count);
            for (int i = 0; i < count; i++)
            {
                chromosomes.Add(new Chromosome<T>(length, genLength, fitnessFunction, id));
                chromosomes[i].Gens = new List<T>(length);
                for (int j = 0; j < length; j++)
                    chromosomes[i].Gens.Add(gens[j][i]);
            }
            return chromosomes;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Length; i++)
                result.Append(Gens[i].GetTextForm());
            return result.ToString();
        }

    }    

}
