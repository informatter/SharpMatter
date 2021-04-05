using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Core
{
    public interface IRigidBody
    {
        /// <summary>
        /// Determines if this <see cref="IRigidBody"/> is active or not.
        /// If its not active, it will not have effect on the
        /// <see cref="ISharpObject"/> is is attached to.
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// All the <see cref="IConstraint"/>'s applied to this
        /// <see cref="IRigidBody"/>.
        /// </summary>
        IList<IConstraint> Constraints { get; }

        double Collider { get; }


        /// <summary>
        /// Determines if this <see cref="IRigidBody"/>
        /// will be effected by gravity or not.
        /// </summary>
        bool UseGravity { get; set; }

        /// <summary>
        /// The mass of this <see cref="IRigidBody"/>. Larger values will have
        /// smaller effects on all the forces acting on this <see cref="IRigidBody"/>,
        /// while smaller values will have greater effects.
        /// </summary>
        double Mass { get; }

        /// <summary>
        /// The position in cartesian coordinates of this <see cref="IRigidBody"/>.
        /// </summary>
        Vec3 Position { get; set; }

        /// <summary>
        /// The velocity of this <see cref="IRigidBody"/>.
        /// </summary>
        Vec3 Velocity { get; set; }

        /// <summary>
        /// Updates the position, velocity and acceleration
        /// of this <see cref="IRigidBody"/>.
        /// </summary>
        void Update(double damping);

        void ApplyForce(Vec3 force);
    }
}
