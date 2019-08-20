using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Grasshopper.Kernel.Data;
using Grasshopper;
using Grasshopper.Kernel.Types;
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





        public static double[][] ConvertGH_NumberToDouble(GH_Number[][] data)
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
