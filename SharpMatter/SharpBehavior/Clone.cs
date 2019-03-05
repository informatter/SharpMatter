using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpMatter.SharpGeometry;
using SharpMatter.SharpPhysics;

namespace SharpMatter.SharpBehavior
{
    public static class Clone
    {
        private static void CopyParticles(List<SharpParticle> list, int numberOfDuplicates, Random ran, Vec3 pos, Vec3 acc, double maxSpeed,double maxForce, double mass, double lifeSpan)
        {

            for (int i = 0; i < numberOfDuplicates; i++)
            {
                SharpParticle copy = new SharpParticle(pos,  acc, Vec3.Vector3dRandom(ran), maxSpeed, maxForce, mass, lifeSpan);

                list.Add(copy);
            }


        }


        /// <summary>
        /// This method will a iterate through a population of particles and duplicate a particle given a condition
        /// </summary>
        /// <param name="list"></param>
        /// <param name="numberOfDuplicates"></param>
        /// <param name="ran"></param>
        /// <param name="condition"></param>
        /// <param name="pos"></param>
        /// <param name="acc"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="maxForce"></param>
        /// <param name="mass"></param>
        /// <param name="lifeSpan"></param>
        public static void DuplicateParticles(List<SharpParticle> list, int numberOfDuplicates, Random ran, bool condition, Vec3 pos, Vec3 acc, double maxSpeed, double maxForce, double mass, double lifeSpan)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (condition)
                {
                    CopyParticles(list, numberOfDuplicates, ran,  pos, acc, maxSpeed,  maxForce,  mass, lifeSpan);
                }
            }
        }
    }
}
