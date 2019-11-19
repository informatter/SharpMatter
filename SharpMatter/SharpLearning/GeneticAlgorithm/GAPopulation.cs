using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using SharpMatter.SharpBehavior;
using SharpMatter.SharpData;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpLearning.GeneticAlgorithm
{

    /// <summary>
    /// 
    /// </summary>
    public class GAPopulation
    {
        #region FIELDS
        private readonly int m_populationSize;
        private readonly SmartAgent[] m_populationArray;
        private SharpDomain m_domainX;
        private SharpDomain m_domainY;
        private List<SmartAgent> m_matingArray;
        private double m_maxSpeed;
        private double m_mass;
        private int m_simulationCycle;
        private List<Curve> m_obstacles;
        private Curve m_target;
        #endregion


        #region CONSTRCUTORS


        /// <summary>
        /// 
        /// </summary>
        public GAPopulation() { }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="mutationRate"></param>
        /// <param name="xDomain"></param>
        /// <param name="yDomain"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="mass"></param>
        /// <param name="simulationCycles"></param>
        /// <param name="obstacles"></param>
        public GAPopulation(int size,SharpDomain xDomain, SharpDomain yDomain, double maxSpeed, double mass, int simulationCycles,Curve target, List<Curve> obstacles)
        {
            m_domainX = xDomain;
            m_domainY = yDomain;
            m_populationSize = size;
            m_populationArray = new SmartAgent[size];
            m_matingArray = new List<SmartAgent>();
            m_maxSpeed = maxSpeed;
            m_mass = mass;
            m_simulationCycle = simulationCycles;
            m_obstacles = obstacles;
            m_target = target;


            Initialize(maxSpeed, mass, simulationCycles, obstacles);
        }

        #endregion

        #region PROPERTIES

        public double Mass
        {
            get { return m_mass; }
            set { m_mass = value; }
        }

        public double MaxSpeed
        {
            get { return m_maxSpeed; }
            set { m_maxSpeed = value; }
        }

        public List<SmartAgent> MatingArray
        {
            get { return m_matingArray; }
            set {  m_matingArray = value; }
        }

        public List<Curve> Obstacles
        {
            get { return m_obstacles; }
            set { m_obstacles = value; }
        }
        public int Size
        {
            get { return m_populationSize; }
        }

        public int SimulationCycle
        {
            get { return m_simulationCycle; }
            set { m_simulationCycle = value; }
        }
        public SmartAgent [] SmartAgentPopulation
        {
            get { return m_populationArray; }
           

        }

        public Curve Target
        {
            get { return m_target; }
            set { m_target = value; }
        }

        public SharpDomain DomainX
        {
            get { return m_domainX; }
            set { m_domainX = value; }
        }

        public SharpDomain DomainY
        {
            get { return m_domainY; }
            set { m_domainY = value; }
        }



        #endregion


        #region METHODS
        private void Initialize(double maxSpeed, double mass, int simulationCycles, List<Curve> obstacles)
        {
            for (int i = 0; i < m_populationSize; i++)
            {
                m_populationArray[i] = new SmartAgent(Vec3.Zero,i, Vec3.Zero, Vec3.Zero, maxSpeed, mass, simulationCycles, m_domainX, m_domainY, obstacles);
            }
        }

        #endregion
    }
}
