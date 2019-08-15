using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.Collections;
using Accord;
using Accord.Math;
using Grasshopper;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using SharpMatter.SharpExtensions;

namespace SharpMatter.SharpLearning
{
    public static class SharpKDTree
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PointCloud"></param>
        /// <param name="testPoint"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static List<Point3d> Knearest (GH_Structure<GH_Number> PointCloud, double [] testPoint, int num )
        {

            



            GH_Number[][] observationsTemp = PointCloud.GH_StructureToJaggedArray();

            double [][] pCloud = ConvertGH_NumberToDouble(observationsTemp);

            KDTree<int> tree = KDTree.FromData<int>(pCloud);


            KDTreeNodeCollection <KDTreeNode<int>> neighbours   =  tree.Nearest(testPoint, num);

            List<double[]> r = new List<double []>();
            for (int i = 0; i < neighbours.Count; i++)
            {
               

                KDTreeNode<int> node = neighbours[i].Node;
                r.Add(node.Position);
           

            }

            List<Point3d> result = new List<Point3d>();

            foreach (var item in r)
            {
                double[] d = item;
           
                result.Add(new Point3d(d[0], d[1], d[2]));
               
            }

           
            return result;
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
