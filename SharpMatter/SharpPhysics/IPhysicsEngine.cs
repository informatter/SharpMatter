using System;
using System.Collections.Generic;
using System.Text;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpPhysics
{
    interface IPhysicsEngine
    {
        void UpdatePhysics(Vec3 velocity, Vec3 acceleration, Vec3 position, double maxSpeed, double maxForce);
        void ApplyForces(Vec3 force, Vec3 acceleration, double mass);
        void ResetForces(Vec3 acceleration);
    }
}

