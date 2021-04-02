namespace SharpMatter.Core.Math
{
    /// <summary>
    /// </summary>
    public struct SharpDomain
    {
        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public SharpDomain(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        #endregion

        #region PROPERTIES

        public double Min { get; set; }

        public double Max { get; set; }

        #endregion

        #region OPERATOR OVERLOADING

        public static SharpDomain operator +(SharpDomain domainA, SharpDomain domainB)
        {
            domainA.Min += domainB.Min;
            domainA.Max += domainB.Max;

            return domainA;
        }

        public static SharpDomain operator -(SharpDomain domainA, SharpDomain domainB)
        {
            domainA.Min -= domainB.Min;
            domainA.Max -= domainB.Max;

            return domainA;
        }

        public static SharpDomain operator *(SharpDomain domain, double scalar)
        {
            domain.Min *= scalar;
            domain.Max *= scalar;
            return domain;
        }

        public static SharpDomain operator *(SharpDomain domainA, SharpDomain domainB)
        {
            domainA.Min *= domainB.Min;
            domainA.Max *= domainB.Max;
            return domainA;
        }

        /// <summary>
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator !=(SharpDomain domainA, SharpDomain domainB)
        {
            if (domainA.Min != domainB.Min && domainA.Max != domainB.Max)
                return true;

            return false;
        }

        public static bool operator ==(SharpDomain domainA, SharpDomain domainB)
        {
            if (domainA.Min == domainB.Min && domainA.Max == domainB.Max)
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator >(SharpDomain domainA, SharpDomain domainB)
        {
            if (domainA.Min > domainB.Min || domainA.Min == domainB.Min && domainA.Max > domainB.Max ||
                domainA.Min == domainB.Min && domainA.Max == domainB.Max) return true;

            return false;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator >=(SharpDomain domainA, SharpDomain domainB)
        {
            if (domainA.Min > domainB.Min || domainA.Min == domainB.Min && domainA.Max > domainB.Max ||
                domainA.Min == domainB.Min && domainA.Max == domainB.Max) return true;

            return false;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator <(SharpDomain domainA, SharpDomain domainB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z

            if (domainA.Min < domainB.Min || domainA.Min == domainB.Min && domainA.Max < domainB.Max ||
                domainA.Min == domainB.Min && domainA.Max == domainB.Max) return true;

            return false;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="domainA"></param>
        /// <param name="domainB"></param>
        /// <returns></returns>
        public static bool operator <=(SharpDomain domainA, SharpDomain domainB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z <= b.Z

            if (domainA.Min < domainB.Min || domainA.Min == domainB.Min && domainA.Max < domainB.Max ||
                domainA.Min == domainB.Min && domainA.Max == domainB.Max) return true;

            return false;
        }

        #endregion

        #region STATIC METHODS

        /// <summary>
        /// Tests whether a number is inside a domain
        /// </summary>
        /// <param name="minVal">
        /// Minimum value
        /// </param>
        /// <param name="maxVal">
        /// Maximum value
        /// </param>
        /// <param name="numToTest">
        /// Number to test
        /// </param>
        /// <returns>
        /// True if in domain, false otherwise
        /// </returns>
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

            return false;
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
            return $"SharpDomain {this.Min} To {this.Max} ";
        }

        public override bool Equals(object obj)
        {
            SharpDomain temp;
            temp = (SharpDomain) obj;
            if (obj is SharpDomain && obj != null)
            {
                if (temp.Min == this.Min && temp.Max == this.Max)
                    return true;

                return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        #endregion
    }
}