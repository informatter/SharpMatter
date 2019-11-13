using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using Rhino.Geometry;

namespace SharpMatter.SharpPhysics
{
    public class SharpSpring: ISharpSpring
    {
        double m_restLength;
        double m_k;//Spring constant
        SharpParticle m_particleA;
        SharpParticle m_particleB;

        public SharpSpring(SharpParticle particleA, SharpParticle particleB, double restLength, double constant)
        {
            m_particleA = particleA;
            m_particleB = particleB;
            m_restLength = restLength;
            m_k = constant;
        }

        /// <summary>
        /// Represents the a constant value of the springs material stiffnes
        /// </summary>
        public double Stiffness
        {
            get { return m_k; }

            set { m_k = value; }
        }

        /// <summary>
        /// //Rest length
        /// </summary>
        public double RestLength
        {
            get { return m_restLength; }

            set { m_restLength = value; }
        }

        public SharpParticle ParticleA
        {
            get { return m_particleA; }

            set { m_particleA = value; }
        }

        public SharpParticle ParticleB
        {
            get { return m_particleB; }

            set { m_particleB = value; }
        }
       

        /// <summary>
        /// 
        /// </summary>
        public void Calculate()
        {
            // force vector
            Vec3 force = m_particleA.Position - m_particleB.Position;

            //Spring length
            double currentSpringLength = force.Magnitude;

            //Difference between current spring length and rest length
            double x = currentSpringLength - m_restLength;

            //Calculate force according to Hooke's Law
            //F = -k * x
            // k is stifness
            // x is stretch factor --> Difference between current spring length and rest length
            force.Normalize();
            force *= (-1 * m_k * x);
            m_particleA.AddForce(force);

            //double CKF = 0.9;
            //double n = 1.0;

            //double magnitudeKinneticFriction = CKF * n;

            //Vec3 friction = force;
            //friction *= -1;
            //// friction.Normalize();
            //Vec3 Friction = friction * magnitudeKinneticFriction;

            // 
            m_particleB.AddForce(-force); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Line Display()
        {
            return new Line((Point3d)m_particleA.Position, (Point3d)m_particleB.Position);
        }
    }
}
