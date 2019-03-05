using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpPhysics;
using SharpMatter.SharpGeometry;


using Rhino.Geometry;


namespace SharpMatter.SharpBehavior
{
    public class FlockBehavior
    {





        /// <summary>
        /// This method creates an allignment force between the neighbouring particles
        /// Each agent will have the averege velocity of its neighbourhood
        /// </summary>
        /// <param name="neighbours"></param> current neighbours
        /// <param name="currentParticle"></param > current particle 
        /// <param name="allignmentStrength"></param> allignment strength
        private void Allignment(List<SharpParticle> neighbours, SharpParticle currentParticle, double allignmentStrength)
        {
            Vec3 alignment = Vec3.Zero;

            foreach (SharpParticle p in neighbours)
                alignment += p.Velocity;

            //  divide by the number of neighbours to get average velocity
            alignment /= neighbours.Count;
            Vec3 alignmentForce = allignmentStrength * alignment;
            currentParticle.AddForce(alignmentForce);

        }

        /// <summary>
        /// This method creates a cohesion force between the neighbouring particles. 
        /// Each agent will be attracted towards the center of mass of its neighbourhood
        /// </summary>
        /// <param name="neighbours"></param> current neighbours
        /// <param name="currentParticle"></param> current particle 
        /// <param name="cohesionStrength"></param> cohesionStrength strength
        private void Cohesion(List<SharpParticle> neighbours, SharpParticle currentParticle, double cohesionStrength)
        {

            Vec3 centre = Vec3.Zero;

            foreach (SharpParticle p in neighbours)
                centre += p.Position;

            //  divide by the number of neighbours to actually get  centre of mass
            centre /= neighbours.Count;

            Vec3 cohesion = centre - currentParticle.Position;


            currentParticle.AddForce(cohesionStrength * cohesion);


        }

        /// <summary>
        /// This method will find the neihgbourhood of the current particle within a certain radius. 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="currentParticle"></param>
        /// <param name="visionRadius"></param>
        /// <returns></returns>
        public List<SharpParticle> FindNeighbours(List<SharpParticle> population, SharpParticle currentParticle, double visionRadius)
        {
            List<SharpParticle> neighbours = new List<SharpParticle>();

            foreach (SharpParticle p in population)
            {
                if (p != currentParticle &&
                    p.Position.DistanceTo(currentParticle.Position) < visionRadius)

                    neighbours.Add(p);


            }

            return neighbours;

        }


    



        /// <summary>
        /// This method will create the overall flocking behavior
        /// </summary>
        /// <param name="neighbours"></param>
        /// <param name="currentParticle"></param>
        /// <param name="cohesionStrength"></param>
        /// <param name="allignmentStrength"></param>
        /// <param name="separationDistance"></param>
        public void Flock(List<SharpParticle> neighbours, SharpParticle currentParticle, double cohesionStrength, double allignmentStrength, double separationDistance)
        {


            if (neighbours.Count == 0)

            {
                currentParticle.Velocity += Vec3.Zero;


            }

            else

            {



                Separation(neighbours, currentParticle, separationDistance);
                Allignment(neighbours, currentParticle, allignmentStrength);
                Cohesion(neighbours, currentParticle, cohesionStrength);



            }
        }



        /// <summary>
        /// This method creates a flocking behavior using all the cores available on your computer
        /// </summary>
        /// <param name="population"></param>
        /// <param name="currentParticle"></param>
        /// <param name="cohesionStrength"></param>
        /// <param name="allignmentStrength"></param>
        /// <param name="separationDistance"></param>
        /// <param name="visionRadius"></param>
        public void ParallelFlock(List<SharpParticle> population, SharpParticle currentParticle, double cohesionStrength, double allignmentStrength, double separationDistance, double visionRadius)
        {
            Parallel.ForEach(population, sharpParticle =>
            {
                List<SharpParticle> neighbours = FindNeighbours(population, sharpParticle, visionRadius);
                Flock(neighbours, currentParticle, cohesionStrength, allignmentStrength, separationDistance);
            });
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="currentParticle"></param>
        /// <param name="cohesionStrength"></param>
        /// <param name="allignmentStrength"></param>
        /// <param name="separationDistance"></param>
        /// <param name="visionRadius"></param>
        public void RTreeFlock(List<SharpParticle> population, SharpParticle currentParticle, double cohesionStrength, double allignmentStrength, double separationDistance, double visionRadius)
        {

            RTree rTree = new RTree();

            // Insert elements
            for (int i = 0; i < population.Count; i++)
                rTree.Insert((Point3d)population[i].Position, i);

            //Find neighbours inside RTree data structure
            foreach (SharpParticle agent in population)
            {
                List<SharpParticle> neighbours = new List<SharpParticle>();

                EventHandler<RTreeEventArgs> rTreeCallback =
                (object sender, RTreeEventArgs args) =>
                {
                    if (population[args.Id] != agent)
                        neighbours.Add(population[args.Id]);
                };

                rTree.Search(new Sphere((Point3d)agent.Position, visionRadius), rTreeCallback);

                Flock(neighbours, currentParticle, cohesionStrength, allignmentStrength, separationDistance);
            }



        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="neighbours"></param>
        /// <param name="currentParticle"></param>
        /// <param name="separationDistance"></param>
        private void Separation(List<SharpParticle> neighbours, SharpParticle currentParticle, double separationDistance)
        {
            Vec3 separation = Vec3.Zero;

            foreach (SharpParticle p in neighbours)
            {
                double distanceToNeighbour = currentParticle.Position.DistanceTo(p.Position);

                if (distanceToNeighbour < separationDistance)
                {
                    Vec3 getAway = currentParticle.Position - p.Position;
                    //  scale the getAway vector by inverse of distanceToNeighbour to make the getAway vector bigger as the agent gets closer to its neighbour

                    separation += getAway / (getAway.Magnitude * distanceToNeighbour);
                }



                Vec3 separationForce = separationDistance * separation;

                currentParticle.AddForce(separationForce);


            }


        }













    }
}
