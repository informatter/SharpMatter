using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpForces
{
    public struct  Wind
    {
        public Vec3 wind;

        public Wind(Vec3 force)
        {
            wind = force;
        }
    }
}
