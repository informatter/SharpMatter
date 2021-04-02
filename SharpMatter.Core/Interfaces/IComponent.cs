﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.Core
{
    public interface IComponent
    {
        /// <summary>
        /// The <see cref="ISharpObject"/> this <see cref="IComponent"/>
        /// is attached to.
        /// </summary>
        ISharpObject Owner { get; }
    }
}
