using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.Core;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Physics.Constraints
{
    public struct Drag: IConstraint
    {
        /// <summary>
        /// The <see cref="IRigidBody"/> this
        /// <see cref="IConstraint"/> si attached to.
        /// </summary>
        public IRigidBody RigidBody { get; set; }

        public double Coefficient { get; set; }

        public Drag( double coefficient, IRigidBody rigidBody)
        {
             this.Coefficient = coefficient;

             this.RigidBody = rigidBody;
        }


        /// <summary>
        /// Calculates the force of this <see cref="IConstraint"/>.
        /// </summary>
        public Vec3 Calculate()
        {
            // Fd = -0.5p*v^2*A*Cd*V^

            var velocity = this.RigidBody.Velocity;

            double speed = velocity.Magnitude;

            double dragMagnitude = this.Coefficient * (speed * speed);

            
            velocity *= -1;

            Vec3 normDragForce = velocity.Normalize();

            normDragForce *= dragMagnitude;

            return normDragForce;
        }
    }
}
