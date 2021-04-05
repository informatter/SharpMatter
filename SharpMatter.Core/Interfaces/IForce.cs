using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.Core
{
    /// <summary>
    /// A contract for all <see cref="IForce"/>'s.
    /// </summary>
    public interface IForce
    {
        /// <summary>
        /// Calculates all the forces of this <see cref="IForce"/>.
        /// </summary>
        void Calculate();

        /// <summary>
        /// Gets the current energy of this
        /// <see cref="IForce"/>.
        /// </summary>
        double GetPotentialEnergy();
    }
}
