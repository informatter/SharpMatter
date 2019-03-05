using System;
using System.Collections.Generic;
using SharpMatter.SharpPhysics;
using SharpMatter.SharpGeometry;
namespace SharpMatter.SharpForces
{
    public static class Drag
    {
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="particle"> Current sharp particle</param>
        /// <param name="dragCoefficient"> drag coeficient, must be a negative quantity </param>
        /// <returns></returns>
        public static Vec3 CalculateDragForce(SharpParticle particle, double dragCoefficient)
        {

            //https://en.wikipedia.org/wiki/Drag_equation

            // FD = 0.5*p*u^2*Cd*A

            //FD => is the drag force, which is by definition the force component in the direction of the flow velocity,
            // p => is the mass density of the fluid
            // u => is the flow velocity relative to the object 
            // A => surface area of body
            // Cd => is the drag coefficient – a dimensionless coefficient related to the object's geometry and taking into account both skin friction and form drag. In general, depends on the Reynolds number.

            // for now can be simplified to
            // FD = 0.5 * 1*u^2*Cd *1
           
            double velocityMagSquared = particle.Velocity.SqrMagnitude;

            Vec3 drag = particle.Velocity;
            drag.Normalize();
            drag *= -1; // direction of drag force is opposite to velocity
          
          

           double ForceDrag = 0.5 * (velocityMagSquared * dragCoefficient);

            drag *= ForceDrag;

           
            return drag;

        }
    }
}
