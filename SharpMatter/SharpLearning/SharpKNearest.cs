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
using SharpMatter.SharpUtilities;
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

            double [][] pCloud = Utilities.ConvertGH_NumberToDouble(observationsTemp);

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




        /// <summary>
        /// Find K-nearest points from a collection of points to a cloud of points to search
        /// </summary>
        /// <param name="PointCloud"></param>
        /// <param name="testPoints"></param>
        /// <param name="num"></param>
        /// <returns>For every testPoint it will return K-neighbours </returns>
        ///     
        public static DataTree<Point3d> Knearest(GH_Structure<GH_Number> PointCloud, GH_Structure<GH_Number> testPoints, int num)
        {

            DataTree<Point3d> output = new DataTree<Point3d>();

            // Conversions between data structures 
            GH_Number[][] pointCloudTemp = PointCloud.GH_StructureToJaggedArray();

            double[][] pCloud = Utilities.ConvertGH_NumberToDouble(pointCloudTemp);

            GH_Number[][] testPointsTemp = testPoints.GH_StructureToJaggedArray();

            double[][] tPoints = Utilities.ConvertGH_NumberToDouble(testPointsTemp);

            KDTree<int> tree = KDTree.FromData<int>(pCloud);



            for (int i = 0; i < tPoints.Length; i++)
            {


                    GH_Path path = new GH_Path(i);
           
                    // Actually use KDTree's Nearest Neighbour Search
                    KDTreeNodeCollection<KDTreeNode<int>> neighbours = tree.Nearest(tPoints[i], num);

                    List<double[]> neighbourNodes = new List<double[]>();
                    for (int k = 0; k < neighbours.Count; k++)
                    {


                        KDTreeNode<int> node = neighbours[k].Node;
                        neighbourNodes.Add(node.Position);


                    }

    
                    foreach (var item in neighbourNodes)
                    {
                        double[] d = item;


                        output.Add(new Point3d(d[0], d[1], d[2]), path);

                    }

             

            }

            

            return output;
        }

    }
}
