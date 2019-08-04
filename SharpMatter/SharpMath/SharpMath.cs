using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpData;


namespace SharpMatter.SharpMath
{
    /// <summary>
    /// 
    /// Math class written by Nicholas Rawitscher. This library is currently under development 
    /// 
    /// 
    /// </summary>

    public static class SharpMath
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double ArithmeticAverege(List<double> data)
        {
            double totalSum = data.Sum();
            return totalSum / data.Count;
        }





            /// <summary>
            /// Return the minimum and maximum value of a list of numbers
            /// </summary>
            /// <param name="numbers"></param>
            /// <returns></returns>
            public static SharpDomain Bounds(List<double> numbers)
            {
                numbers.Sort();

                return new SharpDomain(numbers[0], numbers[numbers.Count - 1]);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static byte ByteAverege(byte a, byte b)
            {
                return (byte)((a + b) / 2);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            public static double Constrain(double x, double min, double max)
            {
                return Math.Max(min, (Math.Min(x, max)));
            }



            /// <summary>
            /// lerp two values by a factor of t
            /// </summary>
            /// <param name="t0"></param>
            /// <param name="t1"></param>
            /// <param name="t"></param>
            /// <returns></returns>
            public static double Lerp(double t0, double t1, double t)
            {
                //Spatial Slur, Dave Reeves
                return t0 + (t1 - t0) * t;
            }


            /// <summary>
            /// Normalize a number between a minimum value and a maximum value
            /// </summary>
            /// <param name="t0"></param>
            /// <param name="t1"></param>
            /// <param name="t"></param>
            /// <returns></returns>
            public static double Normalize(double t, double t0, double t1)
            {
                //Spatial Slur, Dave Reeves
                return (t - t0) / (t1 - t0);
            }


            /// <summary>
            /// Normalizes a number between 0 and 1
            /// </summary>
            /// <param name="t"></param>
            /// <returns></returns> 
            public static double Normalize(double t)
            {
                return Math.Min(Math.Max(t, 0), 1);
            }




            /// <summary>
            /// Remap a number from its given domain to a target domain
            /// </summary>
            /// <param name="minSourceDomain">Source domain min</param>
            /// <param name="maxSourceDomain">Source domain max </param>
            /// <param name="minTargetDomain">Target domain min</param>
            /// <param name="maxTargetDomain">Target domain max</param>
            /// <param name="valueToRemap"> value to remap</param>
            /// <returns></returns>
            public static double Remap(double minSourceDomain, double maxSourceDomain, double minTargetDomain, double maxTargetDomain, double valueToRemap)
            {
                return minTargetDomain + (maxTargetDomain - minTargetDomain) * ((valueToRemap - minSourceDomain) / (maxSourceDomain - minSourceDomain));
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="numberOfSteps"></param>
            /// <returns></returns>
            public static List<double> Range(double numberOfSteps)
            {
                double steps = 1.0f / numberOfSteps;
                double count = 0;
                List<double> values = new List<double>();
                for (int i = 0; i < numberOfSteps; i++)
                {
                    values.Add(count += steps);
                }


                return values;

            }


            /// <summary>
            /// Compare to numbers for similarity within a given threshold
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="threshHold"></param>
            /// <returns></returns>
            public static bool Similar(double a, double b, double threshHold)
            {

                return Math.Abs((a - b)) <= threshHold;
            }


            /// <summary>
            /// Convert an agnle in degrees to radians
            /// </summary>
            /// <param name="degrees"></param>
            /// <returns></returns>
            public static double ToRadians(double degrees)
            {
                return degrees * Math.PI / 180;
            }


            public static double ToDegrees(double radians)
            {
                return radians * 180 / Math.PI;
            }



        }
    }

