using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
    public struct GAparametr
    {
        public double CrossoverProbality;
        public double MutationProbality;
        public double GenerationNumber;
    }
    public class GAClassic<T> where T : IGenProvide
    {
        private double crossoverProbality;
        private double mutationProbality;
        private double generationNumber;
        public Population<T> population;
        static Random rng = new Random();

        public double CrossoverProbality
        {
            get { return crossoverProbality; }
            set { crossoverProbality = value; }
        }
        public double MutationProbality
        {
            get { return mutationProbality; }
            set { mutationProbality = value; }
        }
        public double GenerationNumber
        {
            get { return generationNumber; }
            set { generationNumber = value; }
        }
        public GAClassic(Population<T> population, GAparametr parametrs)
        {

            this.population = population;
            CrossoverProbality = parametrs.CrossoverProbality;
            MutationProbality = parametrs.MutationProbality;
            GenerationNumber = parametrs.GenerationNumber;
        }
        public Population<T> Run()
        {
            Population<T> intermediateGeneration = new Population<T>(population.Count);
            List<Chromosome<T>> chromosomes = new List<Chromosome<T>>(population.Count);
            for (int i = 0; i < GenerationNumber; i++)
            {
                population.FitnessCalculate();
                intermediateGeneration = GeneticOperator<T>.SelectionElite(population, population.Count);
                chromosomes.Clear();
                for (int j = 0; j < population.Count - 1; j += 2)
                {
                    if (rng.NextDouble() < CrossoverProbality)
                    {
                        chromosomes.Add(GeneticOperator<T>.Crossover(intermediateGeneration.chromosomes[j], intermediateGeneration.chromosomes[j + 1]));
                        chromosomes.Add(GeneticOperator<T>.Crossover(intermediateGeneration.chromosomes[j + 1], intermediateGeneration.chromosomes[j]));
                    }
                    else
                    {
                        chromosomes.Add(intermediateGeneration.chromosomes[j]);
                        chromosomes.Add(intermediateGeneration.chromosomes[j + 1]);
                    }
                }
                for (int j = 0; j < population.Count; j++)
                    if (rng.NextDouble() < MutationProbality)
                        chromosomes[j] = GeneticOperator<T>.Mutation(chromosomes[j]);

                population = new Population<T>(chromosomes);
            }
            return population;
        }
    }
}
