using System;
using System.Threading.Tasks;

namespace SharpMatter.Extensions
{
    public static class MultiArrayExtensions
    {
        /// <summary>
        /// Convert from a multidimensional array to an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_2DArray"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this T[,] _2DArray)
        {
            var paraOpts = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            int count = 0;

            T[] data = new T[_2DArray.GetLength(0) * _2DArray.GetLength(1)];

            Parallel.For(0, _2DArray.GetLength(0), paraOpts, i =>

            {
                for (int j = 0; j < _2DArray.GetLength(1); j++) data[count++] = _2DArray[i, j];
            });

            return data;
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
                for (int r = 0; r < numOfRows; r++) jaggedArray[c][r] = multiArray[c, r];
            }

            return jaggedArray;
        }
    }
}