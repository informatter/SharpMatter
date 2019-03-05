using System;

namespace SharpMatter.SharpPhysics
{
    public class SharpBody
    {
        // Field data

        //private Vec3 acceleration;
        //private Vec3 speed;
        //private double maxForce;
        //private double maxSpeed;

        private double m_mass;

        public SharpBody(double mass)
        {
            this.m_mass = mass;
        }

        #region PROPERTIES

        public double Mass
        {
            get { return m_mass; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Mass must be a value larger than 0");
                }

                else m_mass = value;
            }


        }
        #endregion


    }
}

