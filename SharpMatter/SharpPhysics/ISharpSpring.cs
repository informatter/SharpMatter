using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using SharpMatter.SharpPhysics;
namespace SharpMatter.SharpPhysics
{
    interface ISharpSpring
    {
        double Stiffness
        {
            get;
            set;
        }

        double RestLength
        {
            get;
            set;
        }

      
        SharpParticle ParticleA
        {
            get;
            set;
        }

        SharpParticle ParticleB
        {
            get;
            set;
        }

        void Calculate();
        Line Display();
    }
}
