﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using SharpMatter.SharpGeometry;
using SharpMatter.SharpForces;
using SharpMatter.SharpBehavior;

namespace SharpMatter.SharpPhysics
{
    public class SharpParticle : SharpBody//,ICloneable

    {
        #region FIELD DATA
        private Vec3 m_position;
        private Vec3 m_acceleration;
        private Vec3 m_velocity;
        private Vec3 m_force;
        private double m_initMaxSpeed;
        private double m_initMaxForce;
        private double m_lifeSpan;
        private string m_tag;
        private int m_state;

        #endregion

        #region POLYMORPHIC RELATIONSHIPS
        // private Polymorphic relationships
        
        // public Polymorphic relationships
        public BehaviorController behaviorController = new BehaviorController();

        #endregion


       
        


        #region PROPERTIES

        public Vec3 Acceleration
        {
            get { return m_acceleration; }
            set { m_acceleration = value; }
        }


        public Vec3 Force
        {
            get { return m_force; }
            set { m_force = value; }
        }

        public double LifeSpan
        {
            get { return m_lifeSpan; }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Life span must be a value larger than 0");
                }

                else m_lifeSpan = value;
            }
        }

       

        public double MaxSpeed
        {
            get { return m_initMaxSpeed; }
            set
            {
                
                if (value <= 0)
                {
                    throw new ArgumentException("MaxSpeed must be a value larger than 0");
                }

                else m_initMaxSpeed = value;
            }


        }


        public double MaxForce
        {
            get { return m_initMaxForce; }
            set
            {

                if (value <= 0)
                {
                    throw new ArgumentException("MaxForce must be a value larger than 0");
                }

                else m_initMaxForce = value;
            }


        }

        public Vec3 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public int State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        public string Tag
        {
            get { return m_tag; }
            set { m_tag = value; }
        }

        public Vec3 Velocity
        {
            get { return m_velocity; }
            set { m_velocity = value; }
        }



        #endregion


        public SharpParticle(Vec3 position, Vec3 acceleration, Vec3 velocity, double maxSpeed, double maxForce, double mass,double lifeSpan) : base(mass)
        {
            this.m_position = position;
            this.m_acceleration = acceleration;
            this.m_velocity = velocity;
            this.m_initMaxSpeed = maxSpeed;
            this.m_initMaxForce = maxForce;
            base.Mass = mass;
            this.m_lifeSpan = lifeSpan;
            m_force = Vec3.Zero;

           
        }


        public SharpParticle(Vec3 position, Vec3 acceleration, Vec3 velocity, double maxSpeed, double mass) : base(mass)
        {
            this.m_position = position;
            this.m_acceleration = acceleration;
            this.m_velocity = velocity;
            this.m_initMaxSpeed = maxSpeed;
            base.Mass = mass;
            m_force = Vec3.Zero;


        }

        public SharpParticle(Vec3 position, Vec3 velocity, double maxSpeed, double mass) : base(mass)
        {
            this.m_position = position;
            this.m_acceleration = velocity;
            this.m_velocity = velocity;
            this.m_initMaxSpeed = maxSpeed;
            base.Mass = mass;
            m_force = Vec3.Zero;


        }


        public SharpParticle(Vec3 position, double mass =1) : base(mass)
        {
            this.m_position = position;      
            base.Mass = mass;
           


        }



        public SharpParticle() { }



        public void AddForce(Vec3 force)
        {

            PhysicsEngine.ApplyForces(ref force, ref m_acceleration, this);



        }




        /// <summary>
        /// Method duplicates the current particle uppon any given condition generated by the user.
        /// Please note that the forloop that will iterate the list of particle objects has to be from end to beginning, otheriwse the compiler
        /// will generate an enumerator exception
        /// </summary>
        /// <param name="list">List to store particle duplicate</param>
        /// <param name="numberOfDuplicates">number of particles to duplicate </param>
        /// <param name="ran"> random class instance</param>
        private void CopyParticles(List<SharpParticle> list, int numberOfDuplicates, Random ran)
        {

            for (int i = 0; i < numberOfDuplicates; i++)
            {
                SharpParticle copy = new SharpParticle(this.m_position, this.m_acceleration, Vec3.Vector3dRandom(ran), this.m_initMaxSpeed, this.m_initMaxForce, this.Mass, this.m_lifeSpan);

                list.Add(copy);
            }


        }




        public void SetPositionX(double x)
        {
            m_position.X = x;
        }
        public void SetPositionY(double y)
        {
            m_position.Y = y;
        }

        public void SetPositionZ(double z)
        {
            m_position.Z = z;
        }


        public void SetVelocityX(double x)
        {
            m_velocity.X = x;
        }

        public void SetVelocityY(double y)
        {
            m_velocity.Y = y;
        }

        public void SetVelocityZ(double z)
        {
            m_velocity.Z = z;
        }

        public virtual void Update()
        {
            
           
            PhysicsEngine.UpdatePhysics(ref m_velocity, ref m_acceleration, ref m_position,  m_initMaxSpeed,  m_initMaxForce);
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
                if(particle[i].m_lifeSpan<= lifeThreshold && particle.Count!=0)
                {
                    particle.Remove(particle[i]);
                }
            }
        }






        public  void Test(List<SharpParticle> list, int numberOfDuplicates, Random ran, bool condition)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
               if(condition)
                {
                    CopyParticles(list, numberOfDuplicates, ran);
                }
            }
        }

        





        /// <summary>
        /// Calculate life span of particle
        /// </summary>
        /// <param name="particle"></param> current particle
        /// <param name="decay"></param> decay value
        public void Life(SharpParticle particle, double decay)
        {
            
            particle.m_lifeSpan -= decay;
          
        }




    }
}

