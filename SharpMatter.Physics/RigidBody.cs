using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.Core;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Physics
{
    public class RigidBody : IRigidBody, IComponent
    {

        private Vec3 _acceleration;

        private double _mass;

        /// <summary>
        /// All the <see cref="IConstraint"/>'s applied to this
        /// <see cref="IRigidBody"/>.
        /// </summary>
        public IList<IConstraint> Constraints { get; }

        /// <summary>
        /// The <see cref="ISharpObject"/> this
        /// <see cref="IComponent"/> is attached to.
        /// </summary>
        public ISharpObject SharpObject { get; set; }


        public double Collider { get; }

        /// <summary>
        /// Determines if this <see cref="IRigidBody"/>
        /// will be effected by gravity or not.
        /// </summary>
        public bool UseGravity { get; set; }

        /// <summary>
        /// The mass of this <see cref="RigidBody"/>.
        /// </summary>
        public double Mass
        {
            get => _mass;
            set
            {
                if (value < 0)
                    throw new ArgumentException("should not be a negative value!");

                _mass = value;
            }
        }

        /// <summary>
        /// The position in cartesian coordinates of this <see cref="RigidBody"/>.
        /// </summary>
        public Vec3 Position { get;  set; }

        /// <summary>
        /// The velocity of this <see cref="RigidBody"/>
        /// </summary>
        public Vec3 Velocity { get; set; }

       

        /// <summary>
        /// Determines if this <see cref="RigidBody"/> is active or not.
        /// If its not active, it will not have effect on the
        /// <see cref="ISharpObject"/> is is attached to.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Constructs a <see cref="RigidBody"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="useGravity"></param>
        /// <param name="active"></param>
        /// <param name="mass"></param>
        public RigidBody(Vec3 position, bool useGravity, bool active = true, double mass = 1)
        {
            this.Constraints = new List<IConstraint>();

            this.Position = position;

            this.Velocity = Vec3.Zero;

            this.Active = active;

            this.UseGravity = useGravity;

            this.Collider = 1;

            _acceleration = Vec3.Zero;

            _mass = mass;
        }

        public void ApplyForce(Vec3 force)
        {
            _acceleration += force / this.Mass;
        }

        private void EulerIntegration(double damping)
        {
            this.Velocity += _acceleration;

            this.Velocity *= damping;

            this.Position += this.Velocity;

            _acceleration *= 0;
        }

        public void Update(double damping)
        {
            if (!this.Active) return;

            foreach (var constraint in this.Constraints)
            {
                var f = constraint.Calculate();

                this.ApplyForce(f);
            }

            this.EulerIntegration(damping);
        }

    }
}
