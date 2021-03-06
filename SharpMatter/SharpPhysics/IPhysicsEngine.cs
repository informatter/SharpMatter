﻿using System;
using System.Collections.Generic;
using System.Text;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpPhysics
{
    interface IPhysicsEngine
    {
        void UpdatePhysics(ref Vec3  velocity, ref Vec3 acceleration, ref Vec3 position, double maxSpeed, double maxForce);
        void ApplyForces( ref Vec3 force, ref Vec3 acceleration, SharpParticle particle);
       
    }
}

