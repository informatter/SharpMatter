using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using SharpMatter.SharpLearning.GeneticAlgorithm;

namespace SharpMatter.SharpMatterGH.Components.Learning.GeneticAlgorithm
{
    /// <summary>
    /// GA Solver Component. Wrapper class
    /// </summary>
    public class GASolver_GH : GH_Component
    {
        private GAPopulation m_population;
        private int m_generations;
        private int m_CycleCount;
        private Random ran;
        /// <summary>
        /// Initializes a new instance of the GASolver_GH class.
        /// </summary>
        public GASolver_GH()
          : base("GASolver", "GASolver",
              "Description",
              "SharpMatter", "Learning")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Run", "Run", "", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Reset", "Reset", "", GH_ParamAccess.item);
            pManager.AddGenericParameter("Population", "Population", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("MutationR", "MutationRate", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Generations", "Generations", "", GH_ParamAccess.item);
            pManager.AddPointParameter("Pos", "Pos", "", GH_ParamAccess.list);
            pManager.AddVectorParameter("Vel", "Vel", "", GH_ParamAccess.list);
            pManager.AddTextParameter("info", "info", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            

          
            bool _run = false;
            bool _reset = false;
            GAPopulation _pop = new GAPopulation();
            double _mR = 0;

            DA.GetData(0, ref _run);
            DA.GetData(1, ref _reset);
            DA.GetData(2, ref _pop);
            DA.GetData(3, ref _mR);

           // int simulationCycles = 0;
            if (_reset || m_population == null)
            {
                m_population = _pop;
                m_generations = 0;
                m_CycleCount = 0;
                ran = new Random();

                //simulationCycles = m_population.SimulationCycle;
            }

            List<Point3d> pos = new List<Point3d>();
            List<Vector3d> vel = new List<Vector3d>();

            if(_run)
            {
              

                if (m_CycleCount < m_population.SimulationCycle)
                {
                    GASolver.UpdateSystem(m_population, m_CycleCount);

                    for (int i = 0; i < m_population.Size; i++)
                    {

                        pos.Add((Point3d) m_population.SmartAgentPopulation[i].DiplayPosition());
                        vel.Add((Vector3d) m_population.SmartAgentPopulation[i].DiplayVelocity());
                    }

                    m_CycleCount++;
                }

                else
                {
                    m_CycleCount = 0;
                    m_generations++;

                    GASolver.CalculatePopulationFitness(m_population);
                    GASolver.Selection(m_population);
                    GASolver.Reproduction(m_population, ran, _mR);
                    

                }


                ExpireSolution(true);
            }

            DA.SetData(0, m_generations);
            DA.SetDataList(1, pos);
            DA.SetDataList(2, vel);
            DA.SetData(3, m_population.SimulationCycle.ToString()) ;
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e720c922-3bc3-4e1e-b3cc-1a4c93743843"); }
        }
    }
}