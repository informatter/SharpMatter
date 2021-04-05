using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.Core.Geometry;

namespace SharpMatter.Core
{
    public interface IParticle
    {
        ///// <summary>
        ///// The position of this <see cref="IParticle"/>.
        ///// </summary>
        //Vec3 Position { get; set; }

        ///// <summary>
        ///// The velocity of this <see cref="IParticle"/>.
        ///// </summary>
        //Vec3 Velocity { get; set; }


        ///// <summary>
        ///// The mass of this <see cref="IParticle"/>.
        ///// </summary>
        //double Mass { get; }

        ///// <summary>
        ///// Determines if this <see cref="IParticle"/>
        ///// is active or not. If its not active
        ///// no forces will act upon it.
        ///// </summary>
        //bool Fixed { get; set; }

        ///// <summary>
        ///// Adds a <paramref name="force"/>
        ///// to this <see cref="IParticle"/>.
        ///// </summary>
        ///// <param name="force"></param>
        //void AddForce(Vec3 force);

        ///// <summary>
        ///// Updates the position of this
        ///// <see cref="IParticle"/>
        ///// </summary>
        ///// <param name="damping">
        ///// A scalar value which will make this <see cref="IParticle"/>
        ///// reach equilibrium over time. 
        ///// </param>
        //void Update(double damping);

        ///// <summary>
        ///// Resets all the forces of this
        ///// <see cref="IParticle"/>
        ///// </summary>
        //void Reset();

        int LifeSpan { get; set; }
        void UpdateLifeSpan();




    }
}
