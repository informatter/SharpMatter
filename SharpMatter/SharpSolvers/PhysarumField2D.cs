using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpMatter.SharpField;
using SharpMatter.SharpBehavior;

namespace SharpMatter.SharpSolvers
{
    public static class PhysarumField2D
    {



        /// <summary>
        /// This method does all the work and should be used on any solve instance method
        /// <para> scalarField => Scalar Field to Compute </para>
        /// <para> PhysarumAgentPopulation => Physarum Agent Population to compute </para>
        ///<para> decayT => Chemoattractant decay factor to damp diffusion values. Smaller values produce less damping of diffusion </para>
        /// </summary>
        /// <param name="scalarField">Scalar Field to Compute</param>
        /// <param name="PhysarumAgentPopulation"> Physarum Agent Population to compute </param>
        /// <param name="decayT">Chemoattractant decay factor to damp diffusion values. Smaller values produce less damping of diffusion</param>
        public static void SolvePhysarumField(SharpField2D<double> scalarField, List<PhysarumAgent> PhysarumAgentPopulation, double decayT=0.1)
        {

          
            ComputeDiffusionEquation(scalarField, decayT); 
            ComputeField(scalarField, PhysarumAgentPopulation);

        }





        /// <summary>
        ///<para> Compute the Difussion equation for the Chemoattractant in the Scalar Field </para>
        /// <para> Jeff Jhones states the equation in pg. 42 in his book "From Pattern Formation to Material Computation. Multi-agent Modelling of Physarum Polycephalum </para>
        /// </summary>
        /// <param name="scalarField"></param>
        /// <param name="decayT"> Chemoattractant decay factor to damp diffusion values. Smaller values produce less damping of diffusion </param> 
        private static void ComputeDiffusionEquation(SharpField2D<double> scalarField, double decayT)
        {


            ParallelOptions paraOpts = new ParallelOptions();
            paraOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            // the diffusion equation takes form of:
            // Du/Dt = Laplacian *(1-decayT) 

            // Jeff Jhones states the equation in pg. 42 in his book "From Pattern Formation to Material Computation. Multi-agent Modelling of Physarum Polycephalum

            // Laplacian = 3X3 Kernel mean filter => The idea of mean filtering is simply to replace each pixel value in an image with the mean(`average') value of its neighbors, including itself.

            Parallel.For(1, scalarField.Columns - 1, paraOpts, i =>

            {



                for (int j = 1; j < scalarField.Rows - 1; j++)
                {
                    scalarField.NextField[i, j].ScalarValueA = (1 - decayT) * Laplacian(i, j, scalarField); 
                }



            }); // parallel forloop

            scalarField.Swap();





        }


        


        private static void ComputeField(SharpField2D<double> scalarField, List<PhysarumAgent> PhysarumAgentPopulation)
        {


            ParallelOptions paraOpts = new ParallelOptions();
            paraOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            Parallel.For(0, scalarField.Columns, paraOpts, i =>

            {


                //for (int j = 0; j < scalarField.Rows; j++)
                //{

                //    scalarField.Values[i, j] = Math.Abs((SharpMath.SharpMath.Normalize(scalarField.Field[i, j].ScalarValueA)));
                //    // scalarField.Field[i, j].Contains(PhysarumAgentPopulation);
                //    scalarField.Field[i, j].ContainsSpherical(PhysarumAgentPopulation);
                //    scalarField.States[i, j] = scalarField.Field[i, j].Occupied;
                //    scalarField.AgentCount[i, j] = scalarField.Field[i, j].AgentsInCell;

                //}

                Parallel.For(0, scalarField.Rows, paraOpts, j =>
                  {
                     // scalarField.Values[i, j] = Math.Abs((SharpMath.SharpMath.Normalize(scalarField.Field[i, j].ScalarValueA)));

                      scalarField.Values[i, j] = scalarField.Field[i, j].ScalarValueA;


                      /// CALCULATE CELL CONTAINMENT
                      scalarField.Field[i, j].Contains(PhysarumAgentPopulation);

                      scalarField.States[i, j] = scalarField.Field[i, j].Occupied;
                    //  scalarField.AgentCount[i, j] = scalarField.Field[i, j].AgentsInCell;

                  });



            });// parallel forloop


            //for (int i = 0; i < scalarField.Columns; i++)
            //{
            //    for (int j = 0; j < scalarField.Rows; j++)
            //    {

            //        scalarField.Values[i, j] = Math.Abs((SharpMath.SharpMath.Normalize(scalarField.Field[i, j].ScalarValueA)));
            //        // scalarField.Field[i, j].Contains(PhysarumAgentPopulation);
            //        scalarField.Field[i, j].ContainsSpherical(PhysarumAgentPopulation);
            //        scalarField.States[i, j] = scalarField.Field[i, j].Occupied;
            //        scalarField.AgentCount[i, j] = scalarField.Field[i, j].AgentsInCell;

            //    }
            //}


        }


     



        /// <summary>
        /// <para> Laplacian takes form 3X3 Kernel mean filter => </para>
        /// <para> The idea of mean filtering is simply to replace each pixel value in an image with the mean(`average') value of its neighbors, including itself.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scalarField"></param>
        /// <returns></returns>
        private static double Laplacian(int x, int y, SharpField2D<double> scalarField)
        {

            // double meanFilter = scalarField.Field[x, y].ScalarValueA;
            double meanFilter = 0;

            meanFilter += scalarField.Field[x + 1, y].ScalarValueA;

            meanFilter += scalarField.Field[x - 1, y].ScalarValueA;

            meanFilter += scalarField.Field[x, y + 1].ScalarValueA;

            meanFilter += scalarField.Field[x, y - 1].ScalarValueA;

            meanFilter += scalarField.Field[x - 1, y - 1].ScalarValueA;

            meanFilter += scalarField.Field[x + 1, y - 1].ScalarValueA;

            meanFilter += scalarField.Field[x - 1, y + 1].ScalarValueA;

            meanFilter += scalarField.Field[x + 1, y + 1].ScalarValueA;

            return meanFilter/8;

            
        }



    
    }// END CLASS
}
