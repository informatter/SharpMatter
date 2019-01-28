using System;
using System.Collections.Generic;
using System.Text;

using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpPhysics
{
    public class SharpParticle : SharpBody

    {
        //Field data
        public Vec3 position;
        public Vec3 acceleration;
        public Vec3 velocity;
        private double initMaxSpeed;
        private double initMaxForce;
        private double lifeSpan;




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

        public double LifeSpan
        {
            get { return lifeSpan; }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Life span must be a value larger than 0");
                }

                else lifeSpan = value;
            }
        }


        #endregion


        public SharpParticle(Vec3 position, Vec3 acceleration, Vec3 velocity, double maxSpeed, double maxForce, double mass,double lifeSpan) : base(mass)
        {
            this.position = position;
            this.acceleration = acceleration;
            this.velocity = velocity;
            this.initMaxSpeed = maxSpeed;
            this.initMaxForce = maxForce;
            base.Mass = mass;
            this.lifeSpan = lifeSpan;

        }


        public void Update()
        {
           
            physicsEngine.UpdatePhysics(velocity, acceleration, position, initMaxSpeed, initMaxForce);
        }

        public void AddForce(Vec3 force)
        {
            physicsEngine.ApplyForces(force, acceleration, this);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="particle"></param>
        public void Die(List<SharpParticle> particle)
        {
            for (int i = particle.Count-1; i >=0; i--)
            {
                if(particle[i].lifeSpan<=0)
                {
                    particle.Remove(particle[i]);
                }
            }
        }




    }
}

