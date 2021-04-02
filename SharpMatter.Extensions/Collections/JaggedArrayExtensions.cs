using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel.Data;

namespace SharpMatter.Extensions
{
    public static class JaggedArrayExtensions
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
            var dataTree = new DataTree<T>();

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                    dataTree.Add(data[i][j], new GH_Path(i));
                
            }
            return dataTree;
        }



        /// <summary>
        /// Write Jagged Array contents to a .txt file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Jagger Array to manipulate</param>
        /// <param name="folderPath">Traget folder path </param>
        /// <param name="fileName">file name </param>
        public static void JaggedArrayToTxtFile<T>(this T[][] data, string folderPath, string fileName)
        {

            string arrayString = "";
            string fullPath = Path.Combine(folderPath, fileName);
            for (int i = 0; i < data.Length; i++)
            {

                arrayString += "Element" + $"{"{" + i + "}"}" + " ";

                for (int j = 0; j < data[i].Length; j++)
                {
                    arrayString += $"{data[i][j]}{(j == (data[i].Length - 1) ? "" : " ")}";
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
        /// Convert from Jagged Array to Multidimensional Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jaggedArray"></param>
        /// <param name="numOfColumns"></param>
        /// <param name="numOfRows"></param>
        /// <returns></returns>
        public static T[,] To2DArrayParallel<T>(this T[][] jaggedArray, int numOfColumns, int numOfRows)
        {
            T[,] temp2DArray = new T[numOfColumns, numOfRows];

            // for (int c = 0; c < numOfColumns; c++)
            Parallel.For(0, numOfColumns, c =>
            {
                for (int r = 0; r < numOfRows; r++)
                {
                    temp2DArray[c, r] = jaggedArray[c][r];
                }

            });

            return temp2DArray;
        }


        /// <summary>
        /// Convert from Jagged Array to Multidimensional Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jaggedArray"></param>
        /// <param name="numOfColumns"></param>
        /// <param name="numOfRows"></param>
        /// <returns></returns>
        public static T[,] To2DArray<T>(this T[][] jaggedArray, int numOfColumns, int numOfRows)
        {
            T[,] temp2DArray = new T[numOfColumns, numOfRows];

            for (int c = 0; c < numOfColumns; c++)

            {
                for (int r = 0; r < numOfRows; r++)
                {
                    temp2DArray[c, r] = jaggedArray[c][r];
                }

            }

            return temp2DArray;
        }
    }
}
