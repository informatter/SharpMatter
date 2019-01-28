using System;
using System.Collections.Generic;
using System.Text;
using SharpMatter.SharpGeometry;


namespace SharpMatter.SharpPhysics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        public void UpdatePhysics(Vec3 velocity, Vec3 acceleration, Vec3 position, double maxSpeed, double maxForce)
        {

            velocity += acceleration;
            velocity.Normalize();
            velocity *= maxSpeed;

            position += velocity;



            ResetForces(acceleration);
        }

        private void ResetForces(Vec3 acceleration)
        {
            acceleration *= 0.0;
        }

     

        public void ApplyForces(Vec3 force, Vec3 acceleration, SharpParticle particle)
        {
            acceleration += force / particle.Mass;
        }
    }
}

