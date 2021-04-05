using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace SharpMatter.Extensions
{
    /// <summary>
    /// An extensions class for grasshopper's
    /// <see cref="GH_Structure{T}"/> and <see cref="DataTree{T}"/>
    /// </summary>
    public static class GhStructureExtensions
    {

        public static GH_Number[][] ToJaggedArray(this GH_Structure<GH_Number> data)
        {
            // formula for getting items in branches = total number of elements / branch count
            int totalElementsPerArray = data.DataCount / data.PathCount;

            int numElements = data.PathCount;

            var observations = new GH_Number[numElements][];

            var temp = data.AllData(true);

             for (int i = 0; i < observations.Length; i++)
          
            {
                var sub = new GH_Number[totalElementsPerArray];

                var itemsInBranch = data.get_Branch(i) as IList<GH_Number>;

                for (int j = 0; j < totalElementsPerArray; j++)
                    sub[j] = itemsInBranch[j];
                
                observations[i] = sub;
            }
             
            return observations;
        }

        /// <summary>
        /// Convert Data Tree to Jagged Array data structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Data tree to convert</param>
        /// <returns></returns>
        public static T[][] ToJaggedArray<T>(this DataTree<T> data)
        {
            // formula for getting items in branches = total number of elements / branch count
            int totalElementsPerArray = data.DataCount / data.BranchCount;
            int numElements = data.BranchCount;

           var observations = new T[numElements][];


            for (int i = 0; i < observations.Length; i++)
            {
                var sub = new T[totalElementsPerArray];

                for (int j = 0; j < totalElementsPerArray; j++)
                    sub[j] = data.Branch(i)[j];
                
                observations[i] = sub;
            }


            return observations;
        }

        /// <summary>
        /// Convert a list of Point3d to a Data Tree
        /// </summary>
        /// <param name="data"></param>
        public static DataTree<double> ToDataTree(this IEnumerable<Point3d> points)
        {
            var dataTree = new DataTree<double>();

            int pathCount = -1;
            foreach (var point in points)
            {
                pathCount++;

                double[] structure = { point.X, point.Y, point.Z };

                foreach (double value in structure)
                {
                    dataTree.Add(value, new GH_Path(pathCount));
                }
            }

            return dataTree;
        }


        public static void DataTreeToTxtFile<T>(this DataTree<T> data, string folderPath, string fileName)
        {

            string arrayString = "";
            string fullPath = Path.Combine(folderPath, fileName);
            for (int i = 0; i < data.BranchCount; i++)
            {

                arrayString += "Element" + $"{"{" + i + "}"}" + " ";

                var d = data.Branches[i];

                for (int j = 0; j < d.Count; j++)
                {
                    arrayString += $"{d[j]}{(j == (data.Branches[i].Count - 1) ? "" : " ")}";

                }
                arrayString += System.Environment.NewLine;
            }


            System.IO.File.WriteAllText(fullPath, arrayString);

        }



    }
}
