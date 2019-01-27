using System;
using System.Collections.Generic;
using System.Text;

namespace SharpMatter.SharpData
{
    /// <summary>
    /// 
    /// Math class written by Nicholas Rawitscher. This library is currently under development 
    /// 
    /// 
    /// </summary>
    public class Domain
    {
        public double min;
        public double max;

        public Domain(double min, double max)
        {
            this.min = min;
            this.max = max;
        }


        /// <summary>
        /// Deconstruct a given domain
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        public static void DeconstructDomain(Domain domain, out double minVal, out double maxVal)
        {
            minVal = domain.min;
            maxVal = domain.max;

        }





    }
}

