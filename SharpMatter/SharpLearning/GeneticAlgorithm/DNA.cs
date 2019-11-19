using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpLearning.GeneticAlgorithm
{
    /// <summary>
    /// DNA Class, in the future it will be generic so it can manipulate any kind of data
    /// </summary>
    public class DNA //where T: struct ** in the future it will be generic so it can have any data of type struct
    {
        #region FIELDS

        private Vec3[] m_geneArray;
        private int m_simulationCycle; // Maybe I dont need to store this variable here
       private readonly Random m_ran;

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Initialize  evolved DNA
        /// </summary>
        /// <param name="newGenes"></param>
        /// <param name="simulationCycle"></param>
        public DNA(Vec3[] newGenes, int simulationCycle, int randomSeed)
        {
            //m_simulationCycle = 1000;
            m_geneArray = new Vec3[simulationCycle];
            m_ran = new Random(randomSeed);

            for (int i = 0; i < simulationCycle; i++)
            {
                m_geneArray[i] = newGenes[i];

            }


            //string[] outpp = new string[simulationCycle];
            //for (int j = 0; j < m_geneArray.Length; j++)
            //{
            //    outpp[j] = m_geneArray[j].ToString();
            //}

            //string fName = "GeneArrayBeforCrossOverChild" + "GeneArrayLength_" + m_geneArray.Length +".txt";
            //string fPath = @"C:\Users\nicho\source\repos\SharpMatter\";
            //string c = System.IO.Path.Combine(fPath, fName);
            //System.IO.File.WriteAllLines(c, outpp);

        }


        /// <summary>
        /// Initialize Random DNA
        /// </summary>
        /// <param name="simulationCycle"></param>
        public DNA(int simulationCycle, int randomSeed)
        {
            
            // Each agent needs a DNA value during each iteration of the simualtion cycle

            m_simulationCycle = simulationCycle;
            m_geneArray = new Vec3[simulationCycle];
            m_ran = new Random(randomSeed);

            for (int i = 0; i < m_simulationCycle; i++)
             {
                
                m_geneArray[i] = Vec3.Vector2dRandom(m_ran);

             }


        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 
        /// </summary>
        public Vec3 [] Genes
        {
            get { return m_geneArray; }
            set { m_geneArray = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SimulationCycle
        {
            get { return m_simulationCycle; }
            set { m_simulationCycle = value; }
        }
        #endregion


        #region METHODS
        //public void CreateRandomDNA(ref T [] geneArray, )
        //{
        //    for (int i = 0; i < geneArray.Length; i++)
        //    {

        //        geneArray[i] = Vec3.Vector2dRandom(ran);


        //    }
        //}



        /// <summary>
        /// Cross over genes from this DNA and another DNA
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        public DNA CrossOver(DNA partner, int simulationCycle)
        {
            // create array to store childrens Genes
            Vec3[] child = new Vec3 [simulationCycle];

            // choose a random splitting value
            int midValue = m_ran.Next(0, simulationCycle);

            // take half of the genes from both parents
            for (int i = 0; i < simulationCycle; i++) 
            {
                if (i > midValue)
                {
                    child[i] = m_geneArray[i];
         
                }

                else
                {
                    child[i] = partner.m_geneArray[i];

                }
            }




            int seed = m_ran.Next(0, 10);

            DNA newGenes = new DNA(child,seed, simulationCycle);

            if (newGenes.Genes.Length != simulationCycle) throw new ArgumentException("CrossOver not returning full genes!");



            return newGenes;
        }

      
        /// <summary>
        /// Pick a new random value based on a given Mutation rate
        /// </summary>
        /// <param name="mutationRate"></param>
        public void Mutation(double mutationRate)
        {
            for (int i = 0; i < m_geneArray.Length; i++)
            {
                // Pick random value vec based on a probability
                if (m_ran.NextDouble() < mutationRate) 
                {
                    m_geneArray[i] = Vec3.Vector2dRandom(m_ran);
                   // m_geneArray[i] *= (ran.NextDouble() * maxForce);
                }
            }

        }

        #endregion

    }
}
