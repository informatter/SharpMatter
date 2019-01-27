using System;
using System.Collections.Generic;
using System.Text;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpPhysics
{
    public class SharpBody
    {
        // Field data

        //private Vec3 acceleration;
        //private Vec3 speed;
        //private double maxForce;
        //private double maxSpeed;

        private double mass;

        public SharpBody(double mass)
        {
            this.mass = mass;
        }

        #region PROPERTIES

        public double Mass
        {
            get { return mass; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Mass must be a value larger than 0");
                }

                else mass = value;
            }


        }
        #endregion


    }
}

