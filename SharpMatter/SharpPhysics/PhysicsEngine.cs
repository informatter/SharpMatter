using System;
using System.Collections.Generic;
using System.Text;
using SharpMatter.SharpGeometry;


namespace SharpMatter.SharpPhysics
{
    public static class PhysicsEngine //: IPhysicsEngine
    {
        public static void UpdatePhysics(ref Vec3 velocity, ref Vec3 acceleration, ref Vec3 position, double maxSpeed, double maxForce)
        {

            velocity += acceleration;
           //  velocity.Normalize();
            velocity *= maxSpeed;
            position += velocity;
            acceleration *= 0.0; //reset forces
        }


        //public static void UpdatePhysics(ref Vec3 velocity, ref Vec3 acceleration, ref Vec3 position, double maxSpeed)
        //{
        //    velocity += acceleration;         
        //    velocity *= maxSpeed;
        //    position += velocity;
        //    acceleration *= 0.0; //reset forces
        //}





        public static void ApplyForces(ref Vec3 force, ref Vec3 acceleration, SharpParticle particle)
        {
           acceleration += force / particle.Mass;
         
        }





    }
}

