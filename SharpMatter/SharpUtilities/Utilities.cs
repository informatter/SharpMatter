using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace SharpMatter.SharpUtilities
{
    public static class Utilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static T[,] Make2DArray<T>(T[] input, int height, int width)
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static T[,] Make2DArrayParallel<T>(T[] input, int height, int width)
        {
            // if(input.GetType().IsArray)
            T[,] output = new T[height, width];
            //  for (int i = 0; i < height; i++)
            Parallel.For(0, height, i =>
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            });
            return output;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static List<T> Array2DToList<T>(T[,] input, int height, int width)
        {

            List<T> list = new List<T>(width * height);

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                    list.Add(input[i, j]);
            }

            return list;
        }









    }
}
