using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;

namespace SharpMatter.Extensions
{
    /// <summary>
    /// An extensions class for all concrete implementations of
    /// <see cref="List{T}"/>.
    /// </summary>
    public static class ListExtensions
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



        public static T[][] ToJaggedArray<T>(this IList<T> data, DataTree<T> dataTree)
        {
            // Get total elements on each row
            //dataTree.DataCount = total elements in data structure
            int totalElementsPerArray = dataTree.DataCount / dataTree.BranchCount;

            // dataTree.BranchCount = number of elemets/rows
            int numElements = dataTree.BranchCount;

            var outPut = new T[numElements][];

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



    }
}
