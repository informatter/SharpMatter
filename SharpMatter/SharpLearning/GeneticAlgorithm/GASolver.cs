using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpBehavior;

namespace SharpMatter.SharpLearning.GeneticAlgorithm
{

    /// <summary>
    /// 
    /// </summary>
    public static class GASolver
    {

        public static void CalculatePopulationFitness(GAPopulation population)
        {
            for (int i = 0; i < population.Size; i++)
            {
                population.SmartAgentPopulation[i].CalculateFitness(population.Target);
            }
        }


        private static double GetMaxPopulationFitness(GAPopulation population)
        {
            double record = double.MinValue;

            for (int i = 0; i < population.Size; i++)

            {
                if (population.SmartAgentPopulation[i].Fitness > record)
                {
                    record = population.SmartAgentPopulation[i].Fitness;

                }


            }

            return record;
        }


        public static void Reproduction(GAPopulation population, Random ran, double mutationRate)
        {
            for (int i = 0; i < population.Size; i++)

            {

                int indexA = ran.Next(0, population.MatingArray.Count);
                int indexB = ran.Next(0, population.MatingArray.Count);

                // pick parents
                SmartAgent parentA = population.MatingArray[indexA];

                SmartAgent parentB = population.MatingArray[indexB];

                // get parent genes
                DNA parentAGenes = parentA.DNA;
                DNA parentBGenes = parentB.DNA;

                //string[] outpp = new string[population.SimulationCycle];
                //for (int j = 0; j < parentAGenes.Genes.Length; j++)
                //{
                //    outpp[j] = parentAGenes.Genes[j].ToString();
                //}

                //string fName = "parentAGenes" + i.ToString() + " " + "SimulationCycle" + population.SimulationCycle.ToString() + ".txt";
                //string fPath = @"C:\Users\nicho\source\repos\SharpMatter\";
                //string c = System.IO.Path.Combine(fPath, fName);
                //System.IO.File.WriteAllLines(c, outpp);

                // produce child
                DNA child = parentBGenes.CrossOver(parentAGenes, population.SimulationCycle);

               

                // gene mutation
                child.Mutation(mutationRate);

             

                // create new population with next generation

                population.SmartAgentPopulation[i] = new SmartAgent(child, Vec3.Zero, Vec3.Zero, Vec3.Zero, population.MaxSpeed,
                    population.Mass, population.SimulationCycle, population.DomainX, population.DomainY, population.Obstacles);

            }

            //string[] outp = new string[population.SimulationCycle];
            //for (int i = 0; i < population.Size; i++)
            //{
            //    var genes = population.SmartAgentPopulation[i].DNA.Genes.Length;

            //    for (int j = 0; j < genes; j++)
            //    {
            //        outp[j] = population.SmartAgentPopulation[i].DNA.Genes[j].ToString();
            //    }

            //    string fName = "GenesAgent" + i.ToString() + " " + "SimulationCycle" + population.SimulationCycle.ToString() + ".txt";
            //    string fPath = @"C:\Users\nicho\source\repos\SharpMatter\";
            //    string c = System.IO.Path.Combine(fPath, fName);
            //    System.IO.File.WriteAllLines(c, outp);
            //}

           
        }



        /// <summary>
        /// 
        /// </summary>
        public static void Selection(GAPopulation population)
        {

            population.MatingArray.Clear();
            double maxFitness = GetMaxPopulationFitness(population);

            // multiply all fitness values by 100 (just a value) to make the probability
            // higher in adding the agents with higher fitness values to the mating pool

            for (int i = 0; i < population.Size; i++)

            {

                // Normalize fitness between 0-1
                double fitnessNormal = SharpMath.SharpMath.Remap(0, maxFitness, 0,1, population.SmartAgentPopulation[i].Fitness);

                // multiply fitness by arbitrary large number
                // this will increaste the number of fittest agents to be added to the mating array.
                // agents with lower fitness will be added in less numbers to the mating array
                int nTimes = (int)(fitnessNormal * 50);

                // Add N agents
                for (int j = 0; j < nTimes; j++)
                {
                    population.MatingArray.Add(population.SmartAgentPopulation[i]);  
                }
            }

        }


    


        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        public static void UpdateSystem(GAPopulation population, int cycleCount)
        {

            for (int i = 0; i < population.Size; i++)
            {
                population.SmartAgentPopulation[i].Update(cycleCount);
                population.SmartAgentPopulation[i].CalculateFinish(population.Target, 0.1);
                population.SmartAgentPopulation[i].CheckCollision();
                population.SmartAgentPopulation[i].CheckBoundaries();
            }
        }





    }
}
