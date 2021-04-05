using System;
using SharpMatter.Core;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Physics.Constraints
{
    /// <summary>
    /// </summary>
    public class Spring : IForce
    {
        private readonly IRigidBody _rigidBodyA;
        private readonly IRigidBody _rigidBodyB;

        /// <summary>
        /// The maximum length this <see cref="Spring" />
        /// can have.
        /// </summary>
        public double RestLength { get; set; }

        /// <summary>
        /// This <see cref="Spring" />'s constant K.The higher the value the more rigid the spring is,
        /// thus reducing the amount it can stretch under force
        /// </summary>
        public double SpringConstant { get; set; }

        /// <summary>
        /// The first <see cref="IParticle" /> this
        /// <see cref="Spring" /> is attached to.
        /// </summary>
        public SharpParticle SharpParticleA { get; }

        /// <summary>
        /// The second <see cref="IParticle" /> this
        /// <see cref="Spring" /> is attached to.
        /// </summary>
        public SharpParticle SharpParticleB { get; }

        /// <summary>
        /// Construct a <see cref="Spring" />.
        /// </summary>
        /// <param name="sharpParticleA">
        /// The first particle this spring will be connected to.
        /// </param>
        /// <param name="sharpParticleB">
        /// The second particle this spring will be connected to.
        /// </param>
        /// <param name="restLength">
        /// The length of the spring in equilibrium state.
        /// </param>
        /// <param name="springConstant">
        /// The spring constant K. The higher the value the more rigid the spring is,
        /// thus reducing the amount it can stretch under force.
        /// </param>
        public Spring(
            SharpParticle sharpParticleA,
            SharpParticle sharpParticleB,
            double restLength,
            double springConstant)
        {
            this.SharpParticleA = sharpParticleA;

            this.SharpParticleB = sharpParticleB;

            this.RestLength = restLength;

            this.SpringConstant = springConstant;

            _rigidBodyA = this.SharpParticleA.GetComponent<RigidBody>() as RigidBody;

            _rigidBodyB = this.SharpParticleB.GetComponent<RigidBody>() as RigidBody;
        }

        public void Calculate()
        {
            ////Calculate force according to Hooke's Law
            ////F = -k * x
            //// k is springConstant
            //// x is stretch factor --> Difference between current spring length and rest length

            if (_rigidBodyA == null || _rigidBodyB == null)
                throw new ArgumentException("You are attempting to access an object without a rigid body! ");

            // force vector
            Vec3 force = _rigidBodyA.Position - _rigidBodyB.Position;

            //Spring length
            double currentSpringLength = force.Magnitude;

            //Difference between current spring length and rest length => stretch factor
            double x = currentSpringLength - this.RestLength;

            double sf = -this.SpringConstant * x;

            double fx = sf * (force.X / currentSpringLength);

            double fy = sf * (force.Y / currentSpringLength);

            double fz = sf * (force.Z / currentSpringLength);

            var springForce = new Vec3(fx, fy, fz);

            _rigidBodyA.ApplyForce(springForce);

            _rigidBodyB.ApplyForce(-springForce);
        }

        /// <summary>
        /// Gets the current energy of this
        /// <see cref="IForce"/>.
        /// </summary>
        public double GetPotentialEnergy()
        {
            // spring potential energy = 0.5*k*(x^2)
            // k: spring constant => spring stiffness.
            // x: stretch factor.

            Vec3 force = _rigidBodyA.Position - _rigidBodyB.Position;

            //Spring length
            double currentSpringLength = force.Magnitude;

            double x = currentSpringLength - this.RestLength;

            return 0.5 * this.SpringConstant * (x * x);
        }
    }
}