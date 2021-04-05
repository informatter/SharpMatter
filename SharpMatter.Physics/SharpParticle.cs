using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SharpMatter.Core;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Physics
{
    public class SharpParticle: IParticle, ISharpObject
    {
       
        //private double _mass;
        //private double _inverseMass;
        //private Vec3 _acceleration;

        /// <summary>
        /// The collection of all <see cref="IComponent"/>'s
        /// attached to this <see cref="ISharpObject"/>.
        /// </summary>
        public IList<IComponent> Components { get; }

        ///// <summary>
        ///// The position of this <see cref="IParticle"/>.
        ///// </summary>
        //public Vec3 Position { get; set; }

        ///// <summary>
        ///// The velocity of this <see cref="IParticle"/>.
        ///// </summary>
        //public Vec3 Velocity { get; set; }

        ///// <summary>
        ///// The mass of this <see cref="IParticle"/>.
        ///// </summary>
        //public double Mass
        //{
        //    get => _mass;
        //    set
        //    {
        //        if (value < 0)
        //            throw new ArgumentException("should not be a negative value!");

        //        _mass = value;
        //        _inverseMass = 1.0 / value;
        //    }
        //}

        /// <summary>
        /// Determines if this <see cref="IParticle"/>
        /// is active or not. If its not active
        /// no forces will act upon it.
        /// </summary>
        //public bool Fixed { get; set; }

        public int LifeSpan { get; set; }

        /// <summary>
        /// Construct a <see cref="SharpParticle"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="mass"></param>
        /// <param name="pinned"></param>
        public SharpParticle(/*Vec3 position, Vec3 velocity, double mass, bool pinned*/ int lifeSpan = 10000)
        {
            //_acceleration = Vec3.Zero;
            //_inverseMass = 0;

            //_mass = 0;

            //this.Position = position;

            //this.Velocity = velocity;

            //this.Fixed = pinned;

            //this.Mass = mass;

            this.Components = new List<IComponent>(10);

            this.LifeSpan = lifeSpan;
        }

        ///// <summary>
        ///// Adds a <paramref name="force"/>
        ///// to this <see cref="IParticle"/>.
        ///// </summary>
        ///// <param name="force"></param>
        //public void AddForce(Vec3 force)
        //{
        //    _acceleration += force / this.Mass;
        //}

        ///// <summary>
        ///// Updates the position of this
        ///// <see cref="IParticle"/>
        ///// </summary>
        //public void Update(double damping)
        //{
        //    if(this.Fixed) return;

        //    this.Velocity += _acceleration;

        //    this.Velocity *= damping;

        //    this.Position += this.Velocity;

        //    this.Reset();
        //}

        ///// <summary>
        ///// Resets all the forces of this
        ///// <see cref="IParticle"/>
        ///// </summary>
        //public void Reset()
        //{
        //    _acceleration *= 0;
        //}


        /// <summary>
        /// Adds the <paramref name="component"/>
        /// to this <see cref="ISharpObject"/>
        /// </summary>
        /// <param name="component"></param>
        public void AttachComponent(IComponent component)
        {
            this.Components.Add(component);
        }

        /// <summary>
        /// Gets the specified component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public IComponent GetComponent<T>()
        {
           return this.Components.FirstOrDefault(c => c.GetType() == typeof(T));
        }


        public void UpdateLifeSpan()
        {
            throw new NotImplementedException();
        }
    }

   
}

