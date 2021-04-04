using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Core
{
    public class PhysicsEngine
    {

        public IList<IParticle> Particles { get; }

        public IList<IConstraint> Springs { get; }


        public double Drag { get; }

        public double TimeStep { get; }


        private Vec3 _gravity;

        /// <summary>
        /// Construct a <see cref="PhysicsEngine"/>.
        /// </summary>
        /// <param name="particles"></param>
        /// <param name="timeStep"></param>
        /// <param name="damping"></param>
        public PhysicsEngine(
            IList<IParticle> particles,
            IList<IConstraint> springs, 
            Vec3 gravity, double timeStep, double drag = 1)
        {
            this.Particles = particles;

            this.Springs = springs;

            this.Drag = drag;

            _gravity = gravity;

            this.TimeStep = timeStep;
        }


        public void Run()
        {
            foreach (var particle in this.Particles)
            {
                // particle.Update(this.TimeStep,this.Damping);

                particle.Force = Vec3.Zero;

                if(particle.Fixed) continue;

                particle.Force += _gravity * particle.Mass;

               particle.Force -= this.Drag * particle.Velocity;

            }

            foreach (var spring in this.Springs)
                spring.Calculate();

           var derivatives = this.CalculateDerivatives();

           for (int i = 0; i < this.Particles.Count; i++)
           {
               var particle = Particles[i];

               particle.Position += derivatives[i].DpDt*this.TimeStep;
               particle.Velocity += derivatives[i].DvDt* this.TimeStep;
           }
        }


        private IList<Derivative> CalculateDerivatives()
        {
            var derivatives = new List<Derivative>();

            foreach (var particle in this.Particles)
            {
                var derivative = new Derivative();

                double DpDtX = particle.Velocity.X;
                double DpDtY = particle.Velocity.Y;
                double DpDtZ = particle.Velocity.Z;

                double DvDtX = particle.Force.X / particle.Mass;
                double DvDtY = particle.Force.Y / particle.Mass;
                double DvDtZ = particle.Force.Z / particle.Mass;

                derivative.DpDt = new Vec3(DpDtX, DpDtY, DpDtZ);

                derivative.DvDt = new Vec3(DvDtX, DvDtY, DvDtZ);

                derivatives.Add(derivative);

            }

            return derivatives;
        }

        private struct Derivative
        {
            /// <summary>
            /// the change of rate of position over time
            /// </summary>
            internal Vec3 DpDt { get; set; }

            /// <summary>
            /// the change of rate of velocity
            /// over time.
            /// </summary>
            internal Vec3 DvDt { get; set; }
        }
    }
}
