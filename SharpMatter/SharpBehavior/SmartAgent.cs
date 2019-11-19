using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpPhysics;
using SharpMatter.SharpLearning.GeneticAlgorithm;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpData;

using SharpMatter.SharpBehavior.Interfaces;
using Rhino.Geometry;

namespace SharpMatter.SharpBehavior
{
    public class SmartAgent : SharpParticle, ISmartAgent
    {

        private DNA m_dna;
        private double m_fitness;
        private SharpDomain m_domainX;
        private SharpDomain m_domainY;
        private bool m_stuck;
        private bool m_arrived;
        private double m_recordDistance;
        private int m_geneCounter;
        private List<Curve> m_obstacles = new List<Curve>();

        public SmartAgent()
        { }

        public SmartAgent(Vec3 position,int randomSeed, Vec3 acceleration, Vec3 velocity, double maxSpeed, double mass, int simulationCycle, SharpDomain xDomain, SharpDomain yDomain, List<Curve> obstacles)
            : base(position, acceleration, velocity, maxSpeed, mass)
        {
            m_dna = new DNA(simulationCycle, randomSeed);
            m_domainX = xDomain;
            m_domainY = yDomain;
            m_geneCounter = -1; // to start at 0
            m_obstacles = obstacles;
            m_recordDistance = double.MaxValue;
        }


        public SmartAgent(DNA newDna, Vec3 position, Vec3 acceleration, Vec3 velocity, double maxSpeed, double mass, int simulationCycle, SharpDomain xDomain, SharpDomain yDomain, List<Curve> obstacles)
            : base(position, acceleration, velocity, maxSpeed, mass)
        {
            m_dna = newDna;
            m_domainX = xDomain;
            m_domainY = yDomain;
            m_geneCounter = -1; // to start at 0
            m_obstacles = obstacles;
            m_recordDistance = double.MaxValue;
        }

        public DNA DNA
        {
            get { return m_dna; }
            set { m_dna = value; }

        }

        public double Fitness
        {
            get { return m_fitness; }
            set { m_fitness = value; }

        }

        
        /// <summary>
        /// Calculate fitness based on a criteria
        /// </summary>
        public void CalculateFitness(Curve  target ) 
        {
           
            m_fitness = 1 / m_recordDistance;

            m_fitness = Math.Pow(m_fitness, 3);



            if (m_stuck) m_fitness *= 0.1;
            if (!m_stuck && !m_arrived) m_fitness *= 0.7;
            if (m_arrived) m_fitness *= 4;
        }


        public void CalculateFinish(Curve target, double epsilon)
        {
            target.TryGetPlane(out Plane plane);

            double distanceToTarget = Position.DistanceTo((Vec3)plane.Origin);

            if (distanceToTarget < m_recordDistance) m_recordDistance = distanceToTarget;

            if (distanceToTarget <= epsilon) m_arrived = true;
        }

        public void CheckBoundaries()
        {
            if (Position.X > m_domainX.Max || Position.X < m_domainX.Min)
            {
                m_stuck = true;
            }

            if (Position.Y > m_domainY.Max || Position.Y < m_domainY.Min)
            {
                m_stuck = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CheckCollision()
        {
            for (int i = 0; i < m_obstacles.Count; i++)
            {
                PointContainment contains = m_obstacles[i].Contains((Point3d)Position,Plane.WorldXY,0.001);

                if(contains == PointContainment.Inside || contains == PointContainment.Coincident)
                {
                    m_stuck = true;
                    break;
                }
               
            }

           
        }

        public void Update(int cycleCount)
        {
            m_geneCounter++;
            if (!m_stuck && !m_arrived)
            {
                if (m_dna.Genes.Length != 0)
                {
                    //m_geneCounter++;
                    if (m_geneCounter > m_dna.Genes.Length) throw new ArgumentException("Gene counter is larger than Gene List.Count");

                    Vec3 currentGene = m_dna.Genes[m_geneCounter];
                    //m_geneCounter++;
                    // m_geneCounter = (m_geneCounter + 1) % m_dna.Genes.Length;
                    base.AddForce(currentGene);
                    base.Update();
                   

                }

                //else
                //    throw new ArgumentException("Genes list has no elements!");

                
            }
        }

    }
    
}
