using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Core
{
    /// <summary>
    /// A contract for all <see cref="IConstraint"/> implementations.
    /// <see cref="IConstraint"/> must be attached on <see cref="IRigidBody"/>'s.
    /// </summary>
    public interface IConstraint
    {
        /// <summary>
        /// The <see cref="IRigidBody"/> this
        /// <see cref="IConstraint"/> si attached to.
        /// </summary>
        IRigidBody RigidBody { get; set; }

        /// <summary>
        /// Calculates the force of this <see cref="IConstraint"/>.
        /// </summary>
        Vec3 Calculate();
    }
}
