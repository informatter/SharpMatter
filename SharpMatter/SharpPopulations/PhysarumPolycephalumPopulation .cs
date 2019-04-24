using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpMatter.SharpBehavior;
using SharpMatter.SharpField;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpData;

namespace SharpMatter.SharpPopulations
{
    public class PhysarumPolycephalumPopulation
    {
        /// <summary>
        /// Specifies dimension type of popualtion
        /// </summary>
        public enum DimensionType { _2d, _3d,}

        private List<PhysarumAgent> m_population;


      

        public PhysarumPolycephalumPopulation(int number, SharpDomain xDimension, SharpDomain yDimension,double fieldResolution ,double maxSpeed, double mass, double sensorOffsetDistance, double sensorAngle, double agentRotationAngle,
            DimensionType dimension, Random ran, SharpDomain xBounds, SharpDomain YBounds)
        {
            m_population = new List<PhysarumAgent>();

           if(dimension == DimensionType._2d)
            {
                for (int i = 0; i < number; i++)
                {
                 
                    m_population.Add(new PhysarumAgent(Vec3.VectorRandomDistribution(xDimension.Min+5, xDimension.Max-5,yDimension.Min+5, yDimension.Max-5,0,0, ran), 
                        Vec3.Vector2dRandom(ran), fieldResolution, maxSpeed, mass, sensorOffsetDistance, sensorAngle, agentRotationAngle,i, xBounds, YBounds));
                }
            }

            if (dimension == DimensionType._3d) throw new ArgumentException("Parameter is still not implemented!");

        }



        /// <summary>
        /// 
        /// </summary>
       public  List<PhysarumAgent> Population
        {
            get { return m_population; }

            
        }




      







    }
}
