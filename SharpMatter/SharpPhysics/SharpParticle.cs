using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using SharpMatter.SharpGeometry;
using SharpMatter.SharpForces;
using SharpMatter.SharpBehavior;

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
        private string tag;
        private int state;




        // private Polymorphic relationships
        private PhysicsEngine physicsEngine = new PhysicsEngine();
        // public Polymorphic relationships
        public BehaviorController behaviorController = new BehaviorController();





        #region PROPERTIES

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

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
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


        public SharpParticle(Vec3 position, double mass =1) : base(mass)
        {
            this.position = position;      
            base.Mass = mass;
           


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
       /// Death rate of the given particle, given a life threashold
       /// </summary>
       /// <param name="particle"></param> target Sharp Particle
       /// <param name="lifeThreshold"></param> life threshold which the particle will die
        public static  void Die(List<SharpParticle> particle, double lifeThreshold)
        {
            for (int i = particle.Count-1; i >=0; i--)
            {
                if(particle[i].lifeSpan<= lifeThreshold && particle.Count!=0)
                {
                    particle.Remove(particle[i]);
                }
            }
        }


        /// <summary>
        /// particle to duplicate
        /// </summary>
        /// <param name="particle"></param>
        /// <param name="numOfDuplicates"></param>
        public void Duplicate( List<SharpParticle> particle, int numOfDuplicates)
        {
            for (int i = 0; i < numOfDuplicates; i++)
            {
                particle.Add(new SharpParticle(position, acceleration, velocity, MaxSpeed, MaxForce, Mass, LifeSpan));
            }
            
        }

        

        /// <summary>
        /// Calculate life span of particle
        /// </summary>
        /// <param name="particle"></param> current particle
        /// <param name="decay"></param> decay value
        public void Life(SharpParticle particle, double decay)
        {
            
            particle.lifeSpan -= decay;
          
        }




    }
}

