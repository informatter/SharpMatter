using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpMatter.SharpField;
using SharpMatter.SharpMath;
using SharpMatter.SharpUtilities;

using Rhino.Geometry;

namespace SharpMatter.SharpSolvers
{
    /// <summary>
    /// 
    /// </summary>

   
    [Serializable]
    public static class ReactionDiffusion2D
    {

        /// <summary>
        /// This method does all the work and should be used on any solve instance method
        /// </summary>
        /// <param name="scalarField"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="kill"></param>
        /// <param name="feed"></param>
        public static void SolveGreyScottReactionDiffussion(SharpField2D<double> scalarField, List<double> dA, List<double> dB, List<double> kill, List<double> feed, double deltaT)
        {
            double [] tempdA = dA.ToArray();
            double [] tempdB = dB.ToArray();
            double[] tempKill = kill.ToArray();
            double[] tempFeed = feed.ToArray();

            double [,] dA2D = Utilities.Make2DArrayParallel(tempdA, scalarField.Columns, scalarField.Rows);
            double [,] dB2D = Utilities.Make2DArrayParallel(tempdB, scalarField.Columns, scalarField.Rows);
            double [,] kill2D = Utilities.Make2DArrayParallel(tempKill, scalarField.Columns, scalarField.Rows);
            double[,] feed2D = Utilities.Make2DArrayParallel(tempFeed, scalarField.Columns, scalarField.Rows);

            ComputeEquations(scalarField, dA2D, dB2D, kill2D, feed2D, deltaT);

            ComputeField(scalarField);

         



        }


        public static void SolveGreyScottReactionDiffussionB(SharpField2D<double> scalarField, double [,] dA, double[,] dB, double[,] kill, double[,] feed, double deltaT)
        {
           

            ComputeEquations(scalarField, dA, dB, kill, feed, deltaT);

            ComputeField(scalarField);





        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scalarField"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="kill"></param>
        /// <param name="feed"></param>
        private static void ComputeEquations(SharpField2D<double> scalarField, double [,] dA, double[,] dB, double[,] kill, double[,] feed, double deltaT)
        {
            Parallel.For(1, scalarField.Columns - 1, i =>
            // for (int i = 1; i < scalarField.Columns-1; i++)
            {
                for (int j = 1; j < scalarField.Rows - 1; j++)
                {
                    double a = scalarField.Field[i, j].ScalarValueA;
                    double b = scalarField.Field[i, j].ScalarValueB;

                    //kill rate varies along the x axis (from .045 to .07) and the feed rate varies along the y axis (from .01 to .1).

                    scalarField.NextField[i, j].ScalarValueA = a + (dA[i, j] * LaplaceA(i, j, scalarField) - a * b * b + feed[i, j] * (1 - a)) * deltaT;

                    scalarField.NextField[i, j].ScalarValueB = b + (dB[i, j] * LaplaceB(i, j, scalarField) + a * b * b - (kill[i, j] + feed[i, j]) * b) * deltaT;
                }
            //}
            }); // parallel forloop



            scalarField.Swap();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scalarField"></param>
        private static void ComputeField(SharpField2D<double> scalarField)
        {
            Parallel.For(0, scalarField.Columns, i =>
            // for (int i = 0; i < scalarField.Columns; i++)
            {
                for (int j = 0; j < scalarField.Rows; j++)
                {


                    //values[i, j] = Normal(scalarField[i, j].a - scalarField[i, j].b);

                    scalarField.Values[i, j] = Math.Abs(SharpMath.SharpMath.Normalize(scalarField.Field[i, j].ScalarValueA - scalarField.Field[i, j].ScalarValueB));
                    //values[i, j] = SharpMath.LinearInterpolation(0, 1, scalarField[i, j].a - scalarField[i, j].b);

                    //values[i, j] = Math.Abs(scalarField[i, j].b);


                }
                // }
            });// parallel forloop


        }


        /// <summary>
        /// Compute the Laplacian operator for chemical A on a 3X3 convolution matrix
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scalarField"></param>
        /// <returns></returns>
        private static double LaplaceA(int x, int y, SharpField2D<double> scalarField)
        {
            
            double laplaceA = scalarField.Field[x,y].ScalarValueA * -1; 

            laplaceA += scalarField.Field[x + 1, y].ScalarValueA * 0.2;

            laplaceA += scalarField.Field[x - 1, y].ScalarValueA * 0.2;

            laplaceA += scalarField.Field[x, y + 1].ScalarValueA * 0.2;

            laplaceA += scalarField.Field[x, y - 1].ScalarValueA * 0.2;

            laplaceA += scalarField.Field[x - 1, y - 1].ScalarValueA * 0.05;

            laplaceA += scalarField.Field[x + 1, y - 1].ScalarValueA * 0.05;

            laplaceA += scalarField.Field[x - 1, y + 1].ScalarValueA * 0.05;

            laplaceA += scalarField.Field[x + 1, y + 1].ScalarValueA * 0.05;

            return laplaceA;
        }


        /// <summary>
        /// Compute the Laplacian operator for chemical B on a 3X3 convolution matrix
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scalarField"></param>
        /// <returns></returns>
        private static double LaplaceB(int x, int y, SharpField2D<double> scalarField)
        {

            double laplaceB = scalarField.Field[x, y].ScalarValueB * -1;

            laplaceB += scalarField.Field[x + 1, y].ScalarValueB * 0.2f;

            laplaceB += scalarField.Field[x - 1, y].ScalarValueB * 0.2f;

            laplaceB += scalarField.Field[x, y + 1].ScalarValueB * 0.2f;

            laplaceB += scalarField.Field[x, y - 1].ScalarValueB * 0.2f;

            laplaceB += scalarField.Field[x - 1, y - 1].ScalarValueB * 0.05f;

            laplaceB += scalarField.Field[x + 1, y - 1].ScalarValueB * 0.05f;

            laplaceB += scalarField.Field[x - 1, y + 1].ScalarValueB * 0.05f;

            laplaceB += scalarField.Field[x + 1, y + 1].ScalarValueB * 0.05f;

            return laplaceB;
        }


    }




}
