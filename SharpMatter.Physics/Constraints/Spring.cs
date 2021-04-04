using System;
using SharpMatter.Core;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Physics.Constraints
{
    /// <summary>
    /// </summary>
    public class Spring : IConstraint
    {
        /// <summary>
        /// The maximum length this <see cref="Spring" />
        /// can have.
        /// </summary>
        public double RestLength { get; set; }

        /// <summary>
        /// this <see cref="Spring" />'s material constant.
        /// </summary>
        public double SpringConstant { get; set; }

        public double DampingConstant { get; set; }

        /// <summary>
        /// The first <see cref="IParticle" /> this
        /// <see cref="Spring" /> is attached to.
        /// </summary>
        public IParticle ParticleA { get; }

        /// <summary>
        /// The second <see cref="IParticle" /> this
        /// <see cref="Spring" /> is attached to.
        /// </summary>
        public IParticle ParticleB { get; }

        /// <summary>
        /// Construct a <see cref="Spring" />.
        /// </summary>
        /// <param name="particleA"></param>
        /// <param name="particleB"></param>
        /// <param name="restLength"></param>
        /// <param name="springConstant">
        /// The spring constant
        /// </param>
        public Spring(
            IParticle particleA,
            IParticle particleB,
            double restLength,
            double springConstant/*,*/
           /* double damping*/)
        {
            this.ParticleA = particleA;

            this.ParticleB = particleB;

            this.RestLength = restLength;

            this.SpringConstant = springConstant;

            //this.DampingConstant = damping;
        }

        public void Calculate()
        {
            // force vector
            Vec3 force = this.ParticleA.Position - this.ParticleB.Position;

            //Spring length
            double currentSpringLength = force.Magnitude;

            //Difference between current spring length and rest length => stretch factor
            double x = currentSpringLength - this.RestLength;

            //Calculate force according to Hooke's Law
            //F = -k * x
            // k is springConstant
            // x is stretch factor --> Difference between current spring length and rest length
            force.Normalize();

            force *= (-1 * this.SpringConstant * x);

            this.ParticleA.AddForce(force);

            this.ParticleB.AddForce(-force);

            //Vec3 force = Vec3.Zero;

            //double dx = this.ParticleA.Position.X - this.ParticleB.Position.X;

            //double dy = this.ParticleA.Position.Y - this.ParticleB.Position.Y;

            //double dz = this.ParticleA.Position.Z - this.ParticleB.Position.Z;

            //double currentSpringLength = Math.Sqrt(dx * dx + dy * dy + dz * dz);


            //double fx = this.SpringConstant * (currentSpringLength - this.RestLength);

            //fx += this.DampingConstant * (this.ParticleA.Velocity.X - this.ParticleB.Velocity.X) * dx /
            //      currentSpringLength;

            //fx *= -dx / currentSpringLength;


            //double fy = this.SpringConstant * (currentSpringLength - this.RestLength);

            //fy += this.DampingConstant * (this.ParticleA.Velocity.Y - this.ParticleB.Velocity.Y) * dy /
            //      currentSpringLength;

            //fy *= -dy / currentSpringLength;

            //double fz = this.SpringConstant * (currentSpringLength - this.RestLength);

            //fz += this.DampingConstant * (this.ParticleA.Velocity.Z - this.ParticleB.Velocity.Z) * dz /
            //      currentSpringLength;

            //fz *= -dz / currentSpringLength;

            //var force = new Vec3(fx, fy, fz);

            //var normalizedForce = force.Normalize();

            //if (!this.ParticleA.Fixed)
            //{

            //    this.ParticleA.Force += normalizedForce;
            //}

            //if (!this.ParticleB.Fixed)
            //{
            //    this.ParticleB.Force += normalizedForce * -1;
            //}
        }
    }
}