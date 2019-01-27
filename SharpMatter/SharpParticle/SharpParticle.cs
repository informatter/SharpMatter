using System;
using System.Collections.Generic;
using System.Text;

using SharpMatter.SharpPhysics;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpParticle
{
    public class SharpParticle : SharpBody

    {
        //Field data
        public Vec3 position;
        public Vec3 acceleration;
        public Vec3 velocity;
        private double initMaxSpeed;
        private double initMaxForce;




        // Polymorphic relationships
        private PhysicsEngine physicsEngine = new PhysicsEngine();



        #region PROPERTIES

        public double MaxSpeed
        {
            get { return initMaxSpeed; }
            set
            {
                //mass = sb.Mass;
                if (value <= 0)
                {
                    throw new ArgumentException("MaxSpeed must be a value larger than 0");
                }

                else initMaxSpeed = value;
            }


        }


        public double MaxForce
        {
            get { return initMaxForce; }
            set
            {

                if (value <= 0)
                {
                    throw new ArgumentException("MaxForce must be a value larger than 0");
                }

                else initMaxForce = value;
            }


        }


        #endregion


        public SharpParticle(Vec3 position, Vec3 acceleration, Vec3 velocity, double maxSpeed, double maxForce, double mass) : base(mass)
        {
            this.position = position;
            this.acceleration = acceleration;
            this.velocity = velocity;
            this.initMaxSpeed = maxSpeed;
            this.initMaxForce = maxForce;
            base.Mass = mass;

        }


        public void Update(Vec3 force)
        {
            physicsEngine.ApplyForces(force, acceleration, base.Mass);
            physicsEngine.UpdatePhysics(velocity, acceleration, position, initMaxSpeed, initMaxForce);
        }





    }
}

