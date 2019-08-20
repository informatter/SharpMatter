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
using SharpMatter.SharpUtilities;





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

            double[][] observations = Utilities.ConvertGH_NumberToDouble(observationsTemp);

            KMeansClusterCollection clusters = k.Learn(observations);
        

            int[] labels = clusters.Decide(observations);
            centroids = k.Centroids.ToDataTree();
            results = labels;

        }

       



    }


}

