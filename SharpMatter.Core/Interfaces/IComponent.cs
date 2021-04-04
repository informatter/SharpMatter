using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.Core
{
    public interface IConstraint
    {
        /// <summary>
        /// Calculates this <see cref="IConstraint"/>.
        /// </summary>
        void Calculate();
    }
}
