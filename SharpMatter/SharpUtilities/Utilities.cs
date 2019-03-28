using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpUtilities
{
    public static class Utilities
    {
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
    }
}
