using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpPhysics;
namespace SharpMatter.SharpForces
{
    public  class Attraction
    {
        public Attraction() { }
        public  void AttractionForce(List<Vec3> attractors, SharpParticle particle, double scaleForce, double affectedArea=100)
        {

            Vec3 force = Vec3.Zero;
            for (int i = 0; i < attractors.Count; i++)
            {
                Vec3 dir = attractors[i] - particle.Position;
                if (dir * dir < affectedArea) return;


                double forceStrength = 0.001 * (0.000004 * particle.Mass) / dir.Magnitude * dir.Magnitude;
                Vec3 resultantForce = dir * forceStrength;

                dir.Normalize();
                resultantForce.Normalize();

                force += dir * scaleForce / (dir * dir);

               // particle.Force += dir * 0.9 / (dir * dir); 
            }

            particle.AddForce(force);

            //particle.AddForce(particle.Force);


        }


        public  void RepulsionForce(List<Vec3> repulsors, SharpParticle particle, double scaleForce,double affectedArea = 3000)
        {
            Vec3 force = Vec3.Zero;
            for (int i = 0; i < repulsors.Count; i++)
            {
                Vec3 dir = particle.Position - repulsors[i];

                if (dir * dir < 0.00001) return; //0.00001

                if (dir * dir > affectedArea) return;

                dir.Normalize();

                 force += dir * scaleForce / (dir * dir);

                //particle.Force += dir * 0.9 / (dir * dir);

            }

            particle.AddForce( force);
        }


        public  void MomentumForce( SharpParticle particle, double forceScale = 2)
        {
           Vec3 force = Vec3.Zero;
            Vec3 vel = particle.Velocity;
            vel.Normalize();

             force += vel * forceScale;

            particle.Force += vel * forceScale;
            particle.AddForce(force);

            //particle.AddForce(particle.Force);

        }


    }
}



