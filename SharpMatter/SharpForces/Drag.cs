using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpPhysics;
using SharpMatter.SharpGeometry;
namespace SharpMatter.SharpForces
{
    public class Drag
    {
        private Vec3 drag;
        public Drag(SharpParticle sharpParticle)
        {
            
            drag = sharpParticle.velocity;
            drag.Normalize();
            drag *= sharpParticle.MaxForce;


        }

        public Vec3 DragForce
        {
            get { return drag; }

            set
            {
                drag = value;
            }
        }
    }
}
