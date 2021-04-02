using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SharpMatter.Core;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Physics
{
    public struct SharpParticle: IParticle
    {
        private Vec3 _forceSum;
        private Vec3 _deltaSum;
        private double _weightSum;
        private double _mass;
        private double _inverseMass;

        /// <summary>
        /// The position of this <see cref="IParticle"/>.
        /// </summary>
        public Vec3 Position { get; set; }

        /// <summary>
        /// The velocity of this <see cref="IParticle"/>.
        /// </summary>
        public Vec3 Velocity { get; set; }

        /// <summary>
        /// The mass of this <see cref="IParticle"/>.
        /// </summary>
        public double Mass
        {
            get => _mass;
            set
            {
                if (value < 0)
                    throw new ArgumentException("should not be a negative value!");

                _mass = value;
                _inverseMass = 1.0 / value;
            }
        }

        /// <summary>
        /// Determines if this <see cref="IParticle"/>
        /// is active or not. If its not active
        /// no forces will act upon it.
        /// </summary>
        public bool Fixed { get; }

        /// <summary>
        /// Construct a <see cref="SharpParticle"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="mass"></param>
        /// <param name="pinned"></param>
        public SharpParticle(Vec3 position, Vec3 velocity, double mass, bool pinned)
        {
            _inverseMass = 0;

            _mass = 0;

            _forceSum = Vec3.Zero;

            _deltaSum = Vec3.Zero;

            _weightSum = 0;

            this.Position = position;

            this.Velocity = velocity;

            this.Fixed = pinned;

            this.Mass = mass;
        }


        /// <summary>
        /// Adds a <paramref name="force"/>
        /// to this <see cref="IParticle"/>.
        /// </summary>
        /// <param name="force"></param>
        public void AddForce(Vec3 force)
        {
            _forceSum += force;
        }

        /// <summary>
        /// Adds a weighted constraint force
        /// to this <see cref="IParticle"/>
        /// </summary>
        public void AddConstraintForce(Vec3 delta, double weight)
        {
            _deltaSum += delta * weight;

            _weightSum += weight;
        }

        /// <summary>
        /// Updates the position of this
        /// <see cref="IParticle"/>
        /// </summary>
        public void Update(double timeStep, double damping)
        {
            var deltaVelocity = this.Velocity * (1.0 - damping) + _forceSum * (timeStep * _inverseMass);

            if (_weightSum > 0.0)
                deltaVelocity += _deltaSum * (timeStep * _inverseMass / _weightSum);

            var deltaPosition = deltaVelocity * timeStep;

            this.Position += deltaPosition;

            this.Velocity += deltaVelocity;

            this.Reset();
        }

        /// <summary>
        /// Resets all the forces of this
        /// <see cref="IParticle"/>
        /// </summary>
        public void Reset()
        {
            _forceSum = _deltaSum = Vec3.Zero;
            _weightSum = 0.0;
        }

     



    }
}

