using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpPhysics;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpBehavior
{
    public static class Spawner
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="objectList"></param>
        /// <param name="position"></param>
        /// <param name="acceleration"></param>
        /// <param name="velocity"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="maxForce"></param>
        /// <param name="mass"></param>
        /// <param name="lifeSpan"></param>
        public static void Spawn(int number, List<SharpParticle> objectList, Vec3 position, Vec3 acceleration, Vec3 velocity, double maxSpeed, double maxForce, double mass, double lifeSpan)
        {
            for (int i = 0; i < number; i++)
            {
                SharpParticle p = new SharpParticle(position, acceleration, velocity, maxSpeed, maxForce, mass, lifeSpan);
                objectList.Add(p);
            }

            
        }

        /// <summary>
        /// Spawn particles randomly on a unit sphere
        /// </summary>
        /// <param name="number"></param>
        /// <param name="objectList"></param>
        /// <param name="position"></param>
        /// <param name="acceleration"></param>
        /// <param name="velocity"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="maxForce"></param>
        /// <param name="mass"></param>
        /// <param name="lifeSpan"></param>

        public static void SpawnRandomSpherical(int number,List<SharpParticle> objectList,Vec3 origin ,double radius,Vec3 acceleration, Vec3 velocity, 
            double maxSpeed, double maxForce, double mass, double lifeSpan, Random ran)
        {
           
            for (int i = 0; i < number; i++)
            {
                SharpParticle p = new SharpParticle(Vec3.RandomSphericalDistribution(origin.X,origin.Y,origin.Z,radius,ran), acceleration, velocity, maxSpeed, maxForce, mass, lifeSpan);
                objectList.Add(p);
            }
            
        }
    }
}
