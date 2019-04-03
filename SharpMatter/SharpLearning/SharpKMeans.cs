using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Grasshopper;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using SharpMatter.SharpExtensions;
namespace SharpMatter.SharpLearning
{
    public static class SharpKMeans
    {

        /// <summary>
        /// Method to be used when scripting within GH C# component. Compute data in to specific clusters using the KMeans Machine Learning algorithm
        /// k-means clustering aims to partition n observations into k clusters in which each observation belongs to the cluster with the nearest mean
        /// </summary>
        /// <param name="clusterNum"></param>
        /// <param name="input"></param>
        /// <param name="centroids"></param>
        /// <param name="results"></param>
        public static void KMeansClustering(int clusterNum, DataTree<double> input, out DataTree<double> centroids, out int[] results)
        {
            Accord.Math.Random.Generator.Seed = 0;

            KMeans k = new KMeans(clusterNum);

            double[][] observations = input.ToJaggedArray();

            KMeansClusterCollection clusters = k.Learn(observations);


            int[] labels = clusters.Decide(observations);
            centroids = k.Centroids.ToDataTree();

            results = labels;

        }


        /// <summary>
        /// Method to be used to integrate with GH Plugin. Compute data in to specific clusters using the KMeans Machine Learning algorithm
        /// k-means clustering aims to partition n observations into k clusters in which each observation belongs to the cluster with the nearest mean
        /// </summary>
        /// <param name="clusterNum"></param>
        /// <param name="input"></param>
        /// <param name="centroids"></param>
        /// <param name="results"></param>
        public static void KMeansClustering(int clusterNum, GH_Structure<GH_Number> input, out DataTree<double> centroids, out int[] results)
        {
            Accord.Math.Random.Generator.Seed = 0;

            KMeans k = new KMeans(clusterNum);

            GH_Number[][] observationsTemp = input.GH_StructureToJaggedArray();

            double[][] observations = ConvertGH_NumberToDouble(observationsTemp);

            KMeansClusterCollection clusters = k.Learn(observations);


            int[] labels = clusters.Decide(observations);
            centroids = k.Centroids.ToDataTree();

          

            results = labels;

        }

        private static double[][] ConvertGH_NumberToDouble(GH_Number[][] data)
        {
            
            double[][] output = new double[data.Length][];

            Parallel.For(0, data.Length, i =>
            //for (int i = 0; i < data.Length; i++)
            {
                double[] temp = new double[data[i].Length]; // create  array 

                List<GH_Number> itemsInElementTemp = data[i].ToList();

                List<double> itemsInElement = new List<double>();
                foreach (var item in itemsInElementTemp)
                {
                    object a = new GH_Number(item);
                    GH_Number b = (GH_Number)a;
                    double c = b.Value;

                    itemsInElement.Add(c);

                }
                for (int j = 0; j < data[i].Length; j++)
                {
                    temp[j] = itemsInElement[j];
                }

                output[i] = temp;

                //}
            });
            return output;
        }



    }


}

