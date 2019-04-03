using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

namespace SharpMatter.SharpExtensions
{
    public static class Extensions
    {




        /// <summary>
        /// Convert Jagged Array to a Data Tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Jagged Array to manipulate </param>
        /// <returns> A Grasshopper Data Tree matching the same data structure as the input 
        /// Jagged Array </returns>
        public static DataTree<T> ToDataTree<T>(this T[][] data)
        {
            DataTree<T> dataTree = new DataTree<T>();

            int totalBranches = data.Length;

           // Parallel.For(0, data.Length, i =>
               for (int i = 0; i < data.Length; i++)
            {
                GH_Path path = new GH_Path(i);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataTree.Add(data[i][j], path);
                }
             }
           // });


            return dataTree;
        }





        /// <summary>
        /// Write Jagged Array contents to a .txt file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Jagger Array to manipulate</param>
        /// <param name="folderPath">Traget folder path </param>
        /// <param name="fileName">file name </param>
        public static void JaggedArrayToTxtFile<T>(this T[][] data,string folderPath, string fileName )
        {
       
            string arrayString = "";
            string fullPath = Path.Combine(folderPath, fileName);
            for (int i = 0; i < data.Length; i++)
            {

                arrayString += string.Format("Element" + "{0}" + " ", "{" + i + "}");

                for (int j = 0; j < data[i].Length; j++)
                {
                    arrayString += string.Format("{0}{1}", data[i][j], j == (data[i].Length - 1) ? "" : " ");
                }
                arrayString += System.Environment.NewLine;
            }


            System.IO.File.WriteAllText(fullPath, arrayString);

        }


        /// <summary>
        /// Display a Jagged Array to a console window in a c# console application 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"> Jagged array to display </param>
        public static void JaggedArrayToConsoleWindow<T>(this T[][] data)

        {

          
            for (int i = 0; i < data.Length; i++)
            {
                System.Console.Write("Element({0}): ", i);

                for (int j = 0; j < data[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", data[i][j], j == (data[i].Length - 1) ? "" : " ");
                }
                System.Console.WriteLine();
            }
            // Keep the console window open in debug mode.
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
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

            T[][] observations = new T[numElements][];

            List<T> temp = data.AllData();
            Parallel.For(0, observations.Length, i =>
            //for (int i = 0; i < observations.Length; i++)
            {
                T[] sub = new T[totalElementsPerArray];
                List<T> itemsInBranch = data.Branch(i);
                for (int j = 0; j < totalElementsPerArray; j++)
                {

                    sub[j] = itemsInBranch[j];
                }
                observations[i] = sub;
                // }
            });

            return observations;
        }



        public static GH_Number [][] GH_StructureToJaggedArray(this GH_Structure<GH_Number> data)
        {
            // formula for getting items in branches = total number of elements / branch count
            int totalElementsPerArray = data.DataCount / data.PathCount;
            int numElements = data.PathCount;

            GH_Number[][] observations = new GH_Number[numElements][];

            IGH_StructureEnumerator  temp = data.AllData(true);

            Parallel.For(0, observations.Length, i =>
            // for (int i = 0; i < observations.Length; i++)
            {
                GH_Number[] sub = new GH_Number[totalElementsPerArray];

                IList<GH_Number> itemsInBranch = data.get_Branch(i) as IList<GH_Number>;


                for (int j = 0; j < totalElementsPerArray; j++)
                {

                    sub[j] = itemsInBranch[j];
                }
                observations[i] = sub;
            });
           // }

            return observations;
        }




     





    }
}


