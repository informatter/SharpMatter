using System;
using System.Collections.Generic;
using System.Text;

namespace SharpMatter.SharpMath
{
    /// <summary>
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public struct SharpDomain
    {
        #region FIELDS
        private double m_min;
        private double m_max;
        #endregion

        #region CONSTRUCTORS
        public SharpDomain(double min, double max)
        {
            this.m_min = min;
            this.m_max = max;
        }

        #endregion


        #region PROPERTIES
        public double Min { get => m_min; set => m_min = value; }
      

        public double Max { get => m_max; set => m_max = value; }


        #endregion

        #region OPERATOR OVERLOADING


        public static SharpDomain operator +(SharpDomain domainA, SharpDomain domainB)
        {
            domainA.m_min += domainB.m_min;
            domainA.m_max += domainB.m_max;
      
            return domainA;
        }



        public static SharpDomain operator -(SharpDomain domainA, SharpDomain domainB)
        {
            domainA.m_min -= domainB.m_min;
            domainA.m_max -= domainB.m_max;
   
            return domainA;
        }


        public static SharpDomain operator *(SharpDomain domain, double scalar)
        {
            domain.m_min *= scalar;
            domain.m_max *= scalar;
            return domain;
        }

        public static SharpDomain operator *(SharpDomain domainA,  SharpDomain domainB)
        {
            domainA.m_min *= domainB.m_min;
            domainA.m_max *= domainB.m_max; 
            return domainA;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator !=(SharpDomain domainA, SharpDomain domainB)
        {
            if (domainA.m_min != domainB.m_min && domainA.m_max != domainB.m_max )
            {
                return true;
            }
            else
                return false;


        }

        public static bool operator ==(SharpDomain domainA, SharpDomain domainB)
        {
            if (domainA.m_min == domainB.m_min && domainA.m_max == domainB.m_max )
            {
                return true;
            }
            else
                return false;


        }


        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>

        public static bool operator >(SharpDomain domainA, SharpDomain domainB)
        {
           

            if (domainA.m_min > domainB.m_min || domainA.m_min == domainB.m_min && domainA.m_max > domainB.m_max || domainA.m_min == domainB.m_min && domainA.m_max == domainB.m_max ) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator >=(SharpDomain domainA, SharpDomain domainB)
        {
      

            if (domainA.m_min > domainB.m_min || domainA.m_min == domainB.m_min && domainA.m_max > domainB.m_max || domainA.m_min == domainB.m_min && domainA.m_max == domainB.m_max) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator <(SharpDomain domainA, SharpDomain domainB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z

            if (domainA.m_min < domainB.m_min || domainA.m_min == domainB.m_min && domainA.m_max < domainB.m_max || domainA.m_min == domainB.m_min && domainA.m_max == domainB.m_max) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator <=(SharpDomain domainA, SharpDomain domainB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z <= b.Z

            if (domainA.m_min < domainB.m_min || domainA.m_min == domainB.m_min && domainA.m_max < domainB.m_max || domainA.m_min == domainB.m_min && domainA.m_max == domainB.m_max ) return true;

            else return false;


        }


        #endregion



        #region STATIC METHODS

        /// <summary>
        /// Tests whether a number is inside a domain
        /// </summary>
        /// <param name="minVal">Minimum value</param>
        /// <param name="maxVal">Maximum value</param>
        /// <param name="numToTest">Number to test </param>
        /// <returns>True if in domain, false otherwise </returns>
        public static bool InDomain(double minVal, double maxVal, double numToTest)
        {
            double min = 0;
            double max = 0;
            if (minVal > maxVal)
            {
                min = maxVal;
                max = minVal;
            }

            if (minVal < maxVal)
            {
                min = minVal;
                max = maxVal;
            }


            if (numToTest >= min && numToTest <= max) return true;

            else return false;

        }

        #endregion

        //public bool Equals(SharpDomain other)
        //{
        //    if (other.m_min == this.m_min && other.m_max == this.m_max )
        //        return true;

        //    else
        //        return false;
        //}


        #region METHODS OVERRIDE

        public override string ToString()
        {
            return $"SharpDomain {m_min} To {m_max} ";
        }

        public override bool Equals(object obj)
        {
            SharpDomain temp;
            temp = (SharpDomain)obj;
            if (obj is SharpDomain && obj != null)
            {
                if (temp.m_min == this.m_min && temp.m_max == this.m_max)
                {
                    return true;
                }

                else return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            

            return ToString().GetHashCode();
        }

        #endregion



    }
}

