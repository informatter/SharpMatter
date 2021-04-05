using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.Core;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Physics.Constraints
{
    public class PlaneBoundary: IConstraint
    {
        private readonly Vec3 _planeOrigin;

        private readonly Vec3 _planeNormal;

        private  Vec3 _force = Vec3.Zero;

        /// <summary>
        /// The <see cref="IRigidBody"/> this
        /// <see cref="IConstraint"/> si attached to.
        /// </summary>
        public IRigidBody RigidBody { get; set; }

        public PlaneBoundary(Vec3 planeOrigin, Vec3 planeNormal, IRigidBody rigidBody)
        {
            _planeOrigin = Vec3.Zero;

            _planeNormal = Vec3.ZAxis;

            this.RigidBody = rigidBody;
        }

        /// <summary>
        /// Calculates the force of this <see cref="IConstraint"/>.
        /// </summary>
        public Vec3 Calculate()
        {
            double distanceToPlane = (this.RigidBody.Position.Z - _planeOrigin.Z);

            if (distanceToPlane == this.RigidBody.Collider)
            {

                this.RigidBody.Position = new Vec3(this.RigidBody.Position.X, this.RigidBody.Position.Y, this.RigidBody.Collider);

                _force += Vec3.Reflect(this.RigidBody.Velocity, _planeNormal) * 10;

               var force_= _force.Normalize();

               force_ *= 0.9;

               // var reflect = Vec3.Reflect(this.RigidBody.Velocity, _planeNormal)*2;


                return force_;
            }

            return Vec3.Zero;
        }
    }
}
