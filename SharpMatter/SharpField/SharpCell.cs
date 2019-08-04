using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpMatter.SharpData;
using Rhino.Geometry;
using SharpMatter.SharpField;
using SharpMatter.SharpGeometry;

using SharpMatter.SharpBehavior;
using SharpMatter.SharpPhysics;

namespace SharpMatter.SharpField
{
    /// <summary>
    /// This class implements the typical behaviour of a Cell object within a Scalar Field
    /// </summary>
    /// <typeparam name="T"></typeparam>
  
    public class SharpCell <T> 
        //where T: struct
    {

        #region FIELDS

        private PolylineCurve m_cellBoundingBox;
        private T m_scalarValueA;
        private T m_scalarValueB;
        private bool m_occupied;
        private Vec3 m_position;
        

        private readonly int m_columns;
        private readonly int m_rows;

        private Vec3[] m_vertices;
      
        private double m_resolution;

        private SharpDomain m_domainX;
        private SharpDomain m_domainY;

        private int m_numAgentsInCell;

        ParallelOptions options = new ParallelOptions();

       

        //private bool m_flag=true;

        #endregion

        #region CONSTRUCTORS

        public SharpCell(T valueA)
        {
           
            m_scalarValueA = valueA;
          
        }

        public SharpCell(T valueA, Vec3 position, bool occupied)
        {

            m_scalarValueA = valueA;
            m_occupied = occupied;

        }

        /// <summary>
        /// This constructor is typically used for Physarum models
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="position"></param>
        /// <param name="occupied"></param>
        /// <param name="resolution"></param>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public SharpCell(T valueA, Vec3 position, bool occupied, double resolution,int columns, int rows )
        {

            m_scalarValueA = valueA;
            m_position = position;
            m_occupied = occupied;
            m_resolution = resolution;
            m_columns = columns;
            m_rows = rows;
            m_vertices = new Vec3[4];

            m_cellBoundingBox = new PolylineCurve();

            m_domainX = new SharpDomain();
            m_domainY = new SharpDomain();

            DetermineBBox();

        }


        public SharpCell(T valueA, T valueB, Vec3 position)
        {

            m_scalarValueA = valueA;
            m_scalarValueB = valueB;
            m_position = position;

        }

        #endregion

        #region PROPERTIES

        public int AgentsInCell
        {
            get { return m_numAgentsInCell; }
            set
            {
                m_numAgentsInCell = value;
            }
        }

     

        public PolylineCurve BoundingBox
        {
            get { return m_cellBoundingBox ; } 
        }

        public bool Occupied
        {
            get { return m_occupied; }

            set
            {
                m_occupied = value;
            }
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

      

        public Vec3 Position
        {
            get { return m_position; }
        }

        public T ScalarValueA
        {
            get { return m_scalarValueA; }

            set
            {
                m_scalarValueA = value;
            }
        }


        public T ScalarValueB
        {
            get { return m_scalarValueB; }

            set
            {
                m_scalarValueB = value;
            }
        }

        #endregion

        #region METHODS


     



        /// <summary>
        /// "Draws" imaginary boundary around a cell
        /// </summary>
        private void DetermineBBox()
        {
            List<double> xCoordinates = new List<double>();
            List<double> yCoordinates = new List<double>();
            Vec3 vertexA;  // Lower right corner
            Vec3 vertexB; // Lower left corner
            Vec3 vertexC; // Upper left corner
            Vec3 vertexD; // Upper right corner


            vertexA = new Vec3(m_position.X + m_resolution / 2, m_position.Y + m_resolution / 2 * -1, 0);
            vertexB = new Vec3(m_position.X + m_resolution / 2 * -1, m_position.Y + m_resolution / 2 * -1, 0);
            vertexC = new Vec3(m_position.X + m_resolution / 2 * -1, m_position.Y + m_resolution / 2, 0);
            vertexD = new Vec3(m_position.X + m_resolution / 2, m_position.Y + m_resolution / 2, 0);

            xCoordinates.Add(vertexA.X);
            xCoordinates.Add(vertexB.X);
            xCoordinates.Add(vertexC.X);
            xCoordinates.Add(vertexD.X);

            yCoordinates.Add(vertexA.Y);
            yCoordinates.Add(vertexB.Y);
            yCoordinates.Add(vertexC.Y);
            yCoordinates.Add(vertexD.Y);

            xCoordinates.Sort();
            yCoordinates.Sort();
            m_domainX.Min = xCoordinates[0];
            m_domainX.Max = xCoordinates[xCoordinates.Count - 1];

            m_domainY.Min = yCoordinates[0];
            m_domainY.Max = yCoordinates[xCoordinates.Count - 1];

          
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="particles">Population of particles to process</param>
        //public void DetermineOccupationState<U>(List<U> particles) where U: SharpParticle
        public void Contains(List<PhysarumAgent> particles)
        {
            
           

            int ptsCount = 0;
            foreach (var particle in particles)
            {

                if (particle.ForewordSensorB.X >= m_domainX.Min && particle.ForewordSensorB.X <= m_domainX.Max &&
                    particle.ForewordSensorB.Y >= m_domainY.Min && particle.ForewordSensorB.Y <= m_domainY.Max)
                {

                    ptsCount++;


                }


            }



            ///////////////// FOR SOME RESON IT DECREASES PERFORMANCE BY 8 MS. IT IS FASTER TO HAVE PARALLEL NESTED FORLOOP IN PHYSARUM FIELD 2D /////////////////////////////////
            //ParallelOptions options = new ParallelOptions();

            //options.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            //Parallel.ForEach(particles, options, particle =>
            //{
            //    if (particle.ForewordSensorB.X >= m_domainX.Min && particle.ForewordSensorB.X <= m_domainX.Max &&
            //        particle.ForewordSensorB.Y >= m_domainY.Min && particle.ForewordSensorB.Y <= m_domainY.Max)
            //    {

            //        ptsCount++;


            //    }


            //});
            ///////////////// FOR SOME RESON IT DECREASES PERFORMANCE BY 8 MS. IT IS FASTER TO HAVE PARALLEL NESTED FORLOOP IN PHYSARUM FIELD 2D /////////////////////////////////

            m_numAgentsInCell = ptsCount;
            if (ptsCount > 1) m_occupied = true;
            else m_occupied = false;








        }


        //***********************
        //
        //THIS METHOD CRASHES, NO MATTER IF USED WITHIN THE NESTED PARALLEL FORLOOPS OF PHYSUARUM FIELD 2D OR NOT!!!
        // MAYBE ITS DUE BECAUSE OF THE CAST FROM VEC3 TO POINT3D???
        //***********************

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PhysarumAgentPopulation"></param>
        public void ContainsSpherical(List<PhysarumAgent> PhysarumAgentPopulation)
        {
            RTree rTree = new RTree();
            List<PhysarumAgent> neighbours = new List<PhysarumAgent>();
            int ptsCount = 0;
            for (int i = 0; i < PhysarumAgentPopulation.Count; i++)
                rTree.Insert((Point3d)PhysarumAgentPopulation[i].Position, i);

         

            foreach (PhysarumAgent agent in PhysarumAgentPopulation)
            {


                EventHandler<RTreeEventArgs> rTreeCallback =
                (object sender, RTreeEventArgs args) =>
                {
                    if (PhysarumAgentPopulation[args.Id] != agent)
                        neighbours.Add(PhysarumAgentPopulation[args.Id]);
                };

                rTree.Search(new Sphere((Point3d)agent.Position, 1), rTreeCallback);


            }

            for (int i = 0; i < neighbours.Count; i++)
            {
                ptsCount++;
            }

            m_numAgentsInCell = ptsCount;
            if (ptsCount > 1) m_occupied = true;
            else m_occupied = false;
        }



      





        private PolylineCurve DrawCell()
        {




            List<Point3d> vertices = new List<Point3d>();


            Point3d vertexA;  // Lower right corner
            Point3d vertexB; // Lower left corner
            Point3d vertexC; // Upper left corner
            Point3d vertexD; // Upper right corner

            vertexA = new Point3d(m_position.X + m_resolution / 2, m_position.Y + m_resolution / 2 * -1, 0);
            vertexB = new Point3d(m_position.X + m_resolution / 2 * -1, m_position.Y + m_resolution / 2 * -1, 0);
            vertexC = new Point3d(m_position.X + m_resolution / 2 * -1, m_position.Y + m_resolution / 2, 0);
            vertexD = new Point3d(m_position.X + m_resolution / 2, m_position.Y + m_resolution / 2, 0);

            vertices.Add(vertexA);
            vertices.Add(vertexB);
            vertices.Add(vertexC);
            vertices.Add(vertexD);
            vertices.Add(vertexA);



           

            PolylineCurve line = new PolylineCurve(vertices);

            return m_cellBoundingBox = line;







        }

            #endregion


        }// END CLASS

}//END NAMESPACE
