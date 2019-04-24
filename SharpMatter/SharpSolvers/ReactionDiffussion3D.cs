using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpField;
using SharpMatter.SharpMath;

namespace SharpMatter.SharpSolvers

{
    /// <summary>
    /// 
    /// </summary>
    
    [Serializable]
    public static class ReactionDiffussion3D
    {

        public static void Solve(int cellCount, int iterations, int timeSteps, SharpField1D<double> chemA, SharpField1D<double> chemB, 
            SharpField1D<double> deltaChemA, SharpField1D<double> deltaChemB, List<double> dB, List<double> dA, List<double> feed, List<double> kill,double deltaTime,bool outPut3DValues, 
            out List<double> chemA3DVal, out List<double> chemB3DVal)


        {
            List<double> chemA3DValTemp = new List<double>();
            List<double> chemB3DValTemp = new List<double>();

            chemA3DVal = chemA3DValTemp;
            chemB3DVal = chemB3DValTemp;


            int multiplier = (int)Math.Sqrt(cellCount);

           // Parallel.For(0, iterations,i =>
           // {
                for (int i = 0; i < iterations; i++)
              {
                for (int j = 0; j < timeSteps; j++)
                {

                    for (int k = 1; k < multiplier - 1; k++)
                    {
                        for (int l = 1; l < multiplier - 1; l++)
                        {

                            int index = k + l * multiplier;

                            double a = chemA.Values[index];
                            double b = chemB.Values[index];

                            deltaChemA.Values[index] = a + (dA[index] * Laplacian(chemA.Values, k, l, multiplier) - a * b * b + feed[index] * (1 - a)) * deltaTime;

                            deltaChemB.Values[index] = b + (dB[index] * Laplacian(chemB.Values, k, l, multiplier) + a * b * b - (kill[index] + feed[index]) * b) * deltaTime;
                        }
                    }


                    for (int m = 0; m < cellCount; m++)
                    {

                        chemA.Values[m] = SharpMath.SharpMath.Normalize(deltaChemA.Values[m]);
                        chemB.Values[m] = SharpMath.SharpMath.Normalize(deltaChemB.Values[m]);

                    }

                    if (outPut3DValues)
                    {
                        if (j == 0)
                        {
                            for (int n = 0; n < cellCount; n++)
                            {
                                chemA3DValTemp.Add(chemA.Values[n]);
                                chemB3DValTemp.Add(chemB.Values[n]);


                            }
                        }
                    }

                }
              }
           // });

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="multiplier"></param>
        /// <returns></returns>

        private static double Laplacian(double[] data, int x, int y, int multiplier) 
        {


            double laplace = 0;
            laplace += data[x + y * multiplier] * -1;

            laplace += data[(x + 1) + y * multiplier] * 0.2;
            laplace += data[(x - 1) + y * multiplier] * 0.2;
            laplace += data[x + (y + 1) * multiplier] * 0.2;

            laplace += data[x + (y - 1) * multiplier] * 0.2;

            laplace += data[(x - 1) + (y - 1) * multiplier] * 0.05;

            laplace += data[(x + 1) + (y - 1) * multiplier] * 0.05;

            laplace += data[(x - 1) + (y + 1) * multiplier] * 0.05;

            laplace += data[(x + 1) + (y + 1) * multiplier] * 0.05;





            return laplace;


        }




    }// END CLASS



}
