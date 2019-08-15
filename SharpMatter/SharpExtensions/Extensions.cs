using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace SharpMatter.SharpExtensions
{
    public static class Extensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="li"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<T> ListSlice<T>(this List<T> li, int start, int end)
        {
            end += 1;
            if (start < 0)    // support negative indexing
            {
                start = li.Count + start;
            }
            if (end < 0)    // support negative indexing
            {
                end = li.Count + end;
            }
            if (start > li.Count)    // if the start value is too high
            {
                start = li.Count;
            }
            if (end > li.Count)    // if the end value is too high
            {
                end = li.Count;
            }
            var count = end - start;             // calculate count (number of elements)
            return li.GetRange(start, count);    // return a shallow copy of li of count elements
        }


        /// <summary>
        /// Function will reorder elements in list randomly
        /// </summary>
        /// <typeparam name="T"></typeparam> generic type
        /// <param name="list"></param> input list

        public static void ListJitter<T>(this List<T> list)
        {
            Random ran = new Random();

            list.Sort((x, y) => ran.Next(-1, 1));

        }




        /// <summary>
        /// Convert from a multidimensional array to an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_2DArray"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this T[,] _2DArray)
        {
            ParallelOptions paraOpts = new ParallelOptions();
            paraOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            int count = 0;

            T[] data = new T[_2DArray.GetLength(0) * _2DArray.GetLength(1)];

            Parallel.For(0, _2DArray.GetLength(0), paraOpts, i =>

            {
                for (int j = 0; j < _2DArray.GetLength(1); j++)
                {
                    data[count++] = _2DArray[i, j];
                }


            });


            return data;
        }


        public static bool IsNull<T>(this T[,] _2DArray)
        {
            int nullCount = 0;
            bool result = false;
            for (int i = 0; i < _2DArray.GetLength(0); i++)
            {
                for (int j = 0; j < _2DArray.GetLength(1); j++)
                {
                    if (_2DArray[i, j] == null) nullCount++;
                }
            }

            if (nullCount == _2DArray.Length) result = true;
            else result = false;
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T[] collection)
        {

            int nullCount = 0;
            bool result = false;
            for (int i = 0; i < collection.Length; i++)
            {
                if (collection[i] == null) nullCount++;

            }

            if (nullCount == collection.Length) result = true;
            else result = false;
            return result;

        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static T[,] Make2DArray<T>(this T[] input, int height, int width)
        {
            // if(input.GetType().IsArray)
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }


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



        public static void DataTreeToTxtFile<T>(this DataTree<T> data, string folderPath, string fileName)
        {

            string arrayString = "";
            string fullPath = Path.Combine(folderPath, fileName);
            for (int i = 0; i < data.BranchCount; i++)
            {

                arrayString += string.Format("Element" + "{0}" + " ", "{" + i + "}");

                List<T> d = data.Branches[i];

                for (int j = 0; j < d.Count; j++)
                {
                    arrayString += string.Format("{0}{1}", d[j], j == (data.Branches[i].Count - 1) ? "" : " ");

                }
                arrayString += System.Environment.NewLine;
            }


            System.IO.File.WriteAllText(fullPath, arrayString);

        }


        /// <summary>
        /// Convert a list of Point3d to a Data Tree
        /// </summary>
        /// <param name="data"></param>
        public static void ToDataTree(this List<Point3d> data)
        {
            DataTree<double> dataTree = new DataTree<double>();

            int totalBranches = data.Count;

            // Parallel.For(0, data.Length, i =>
            for (int i = 0; i < data.Count; i++)
            {
                GH_Path path = new GH_Path(i);
                double[] structure = new double[] { data[i].X, data[i].Y, data[i].Z };
                for (int j = 0; j < structure.Length; j++)
                {
                    dataTree.Add(structure[j], path);
                }
            }
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











        public static T[] RemoveFromArray<T>(this T[] original, int itemToRemove)
        {


            List<T> tmp = new List<T>(original);
            tmp.RemoveAt(itemToRemove);
            return tmp.ToArray();


        }







        /// <summary>
        /// Remove a an item at a specified index of a 1D Array without resizing it. This will 
        /// be achieved by adding the Objects default value at the removed index instead.
        /// Please be aware that this method is not computationally performative
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            List<T> temp = null;
            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                {
                    temp = array.ToList();
                    temp.RemoveAt(index);
                    temp.Insert(index, default(T));


                }
            }

            return temp.ToArray();

        }



        /// <summary>
        /// Remove an item at the specified index. This method will resize the input array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        public static void RemoveAtAndResize<T>(this T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);
        }


        /// <summary>
        /// Remove a an item at a specified index of a 1D Array without resizing it. This will 
        /// be achieved by adding a null value at the removed index instead.
        /// Please be aware that this method is not computationally performative 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] RemoveAt_null<T>(this T[] array, int index) where T : class
        {
            List<T> temp = null;
            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                {
                    temp = array.ToList();
                    temp.RemoveAt(index);
                    temp.Insert(index, null);

                }
            }

            return temp.ToArray();

        }

        public static T[] RemoveAtParallel<T>(this T[] array, int index)
        {
            ParallelOptions paraOpts = new ParallelOptions();
            paraOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;



            List<T> temp = null;
            Parallel.For(0, array.Length, paraOpts, i =>
            {
                if (i == index)
                {
                    temp = array.ToList();
                    temp.RemoveAt(index);
                    temp.Insert(index, default(T));

                }
            });

            return temp.ToArray();

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


        public static T[][] ToJaggedArray<T>(this List<T> data, DataTree<T> dataTree)
        {
            // Get total elements on each row
            //dataTree.DataCount = total elements in data structure
            int totalElementsPerArray = dataTree.DataCount / dataTree.BranchCount;

            // dataTree.BranchCount = number of elemets/rows
            int numElements = dataTree.BranchCount;

            T[][] outPut = new T[numElements][];

            for (int i = 0; i < outPut.Length; i++)
            {
                T[] temp = new T[totalElementsPerArray];


                for (int j = 0; j < temp.Length; j++)
                {
                    int index = j + temp.Length * i;
                    temp[j] = data[index];
                }

                outPut[i] = temp;
            }

            return outPut;
        }


        /// <summary>
        /// Convert from Multidimensional Array to Jagged Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="multiArray"></param>
        /// <param name="numOfColumns"></param>
        /// <param name="numOfRows"></param>
        /// <returns></returns>
        public static T[][] ToJaggedArrayParallel<T>(this T[,] multiArray, int numOfColumns, int numOfRows)
        {
            T[][] jaggedArray = new T[numOfColumns][];

            //for (int c = 0; c < numOfColumns; c++)
            Parallel.For(0, numOfColumns, c =>
            {
                jaggedArray[c] = new T[numOfRows];
                for (int r = 0; r < numOfRows; r++)
                {
                    jaggedArray[c][r] = multiArray[c, r];
                }
            });

            return jaggedArray;
        }



        /// <summary>
        /// Convert from Multidimensional Array to Jagged Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="multiArray"></param>
        /// <param name="numOfColumns"></param>
        /// <param name="numOfRows"></param>
        /// <returns></returns>
        public static T[][] ToJaggedArray<T>(this T[,] multiArray, int numOfColumns, int numOfRows)
        {
            T[][] jaggedArray = new T[numOfColumns][];

            for (int c = 0; c < numOfColumns; c++)

            {
                jaggedArray[c] = new T[numOfRows];
                for (int r = 0; r < numOfRows; r++)
                {
                    jaggedArray[c][r] = multiArray[c, r];
                }
            }

            return jaggedArray;
        }


        public static GH_Number[][] GH_StructureToJaggedArray(this GH_Structure<GH_Number> data)
        {
            // formula for getting items in branches = total number of elements / branch count
            int totalElementsPerArray = data.DataCount / data.PathCount;
            int numElements = data.PathCount;

            GH_Number[][] observations = new GH_Number[numElements][];

            IGH_StructureEnumerator temp = data.AllData(true);

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


