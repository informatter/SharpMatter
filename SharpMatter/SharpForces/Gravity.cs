using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpForces
{
    public struct Gravity
    {
        public Vec3 gravity;

        public Gravity (Vec3 force)
        {
            this.gravity = force;
        }

    }

   
}
