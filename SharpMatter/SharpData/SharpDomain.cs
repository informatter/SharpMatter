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
    public class SharpDomain
    {
        private double m_min;
        private double m_max;

        public SharpDomain(double min, double max)
        {
            this.m_min = min;
            this.m_max = max;
        }

        public SharpDomain()
        {
            //this.m_min = 0;
            //this.m_max = 1;
        }


        ///// <summary>
        ///// Deconstruct a given domain
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <param name="minVal"></param>
        ///// <param name="maxVal"></param>
        //public static void DeconstructDomain(Domain domain, out double minVal, out double maxVal)
        //{
        //    minVal = domain.m_min;
        //    maxVal = domain.m_max;

        //}


        public double Min
        {
            get { return m_min; }
            set { m_min = value; }
        }

        public double Max
        {
            get { return m_max; }
            set { m_max = value; }
        }


        public override string ToString()
        {
            return $"SharpDomain {m_min} To {m_max} ";
        }




    }
}

