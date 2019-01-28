using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpForces
{
    public class ForcesController
    {
        Wind wind = new Wind();
        Friction friction = new Friction();
        Springs springs = new Springs();
        FluidResistance fluidResistance = new FluidResistance();
        Gravity gravity = new Gravity();
    }
}

