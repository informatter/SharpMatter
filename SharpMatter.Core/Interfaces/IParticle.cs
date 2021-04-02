using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel.Geometry;

namespace SharpMatter.Core
{
    public interface IParticle
    {
        /// <summary>
        /// The position of this <see cref="IParticle"/>.
        /// </summary>
        Vec3 Position { get; }

        /// <summary>
        /// The velocity of this <see cref="IParticle"/>.
        /// </summary>
        Vec3 Velocity { get; }

        /// <summary>
        /// The mass of this <see cref="IParticle"/>.
        /// </summary>
        double Mass { get; set; }


    }
}
