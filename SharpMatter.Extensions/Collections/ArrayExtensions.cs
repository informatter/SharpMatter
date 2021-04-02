using System;
using System.Collections.Generic;

namespace SharpMatter.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Makes a 2D array from a 1D array.
        /// </summary>
        public static T[,] Make2DArray<T>(this T[] input, int height, int width)
        {
            
            var output = new T[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    output[i, j] = input[i * width + j];
            }

            return output;
        }

        /// <summary>
        /// Removes an element from this <paramref name="array" />
        /// given an <paramref name="index" />. This method creates a
        /// shallow copy to a list ans returns a new array.
        /// </summary>
        /// <returns></returns>
        public static T[] RemoveFromArray<T>(this T[] array, int index)
        {
            var tmp = new List<T>(array);

            tmp.RemoveAt(index);

            return tmp.ToArray();
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
            Array.Resize(ref arr, arr.Length - 1);
        }
    }
}