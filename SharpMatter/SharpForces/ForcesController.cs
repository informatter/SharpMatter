using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpForces
{
    public class ForcesController
    {
       public  Wind wind = new Wind();
       public Friction friction = new Friction();
        public Springs springs = new Springs();
        public FluidResistance fluidResistance = new FluidResistance();
        public Gravity gravity = new Gravity();
        public Attraction attraction = new Attraction();
    }
}

