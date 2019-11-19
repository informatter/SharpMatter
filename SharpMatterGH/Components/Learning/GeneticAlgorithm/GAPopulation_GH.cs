using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using SharpMatter.SharpData;
using SharpMatter.SharpLearning.GeneticAlgorithm;


namespace SharpMatter.SharpMatterGH.Components.Learning.GeneticAlgorithm
{
    public class GAPopulation_GH : GH_Component
    {
        private GAPopulation m_population;

        private Random m_ran;

        /// <summary>
        /// Initializes a new instance of the GAPopulation_GH class.
        /// </summary>
        public GAPopulation_GH()
          : base("GAPopulation", "GAPopulation",
              "Description",
              "SharpMatter", "Learning")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //(int size, SharpDomain xDomain, SharpDomain yDomain, double maxSpeed, double mass, int simulationCycles, Curve target, List<Curve> obstacles)

            pManager.AddBooleanParameter("Reset", "Reset", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Size", "Size", "Population size", GH_ParamAccess.item);
            pManager.AddGenericParameter("XBounds", "XBounds", "Environment X Bounds", GH_ParamAccess.item);
            pManager.AddGenericParameter("YBounds", "YBounds", "Environment Y Bounds", GH_ParamAccess.item);
            pManager.AddNumberParameter("MaxSpeed", "MaxSpeed", "Maximum speed of Agents", GH_ParamAccess.item);
            pManager.AddNumberParameter("Mass", "Mass", "Mass of Agents", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Cycle", "Cycle", "Total frames in simulation", GH_ParamAccess.item);
            pManager.AddCurveParameter("Target", "Target", "", GH_ParamAccess.item);
            pManager.AddCurveParameter("Obstacles", "Obstacles", "", GH_ParamAccess.list);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("P", "P", "Population", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool _reset = false;
            int _size = 0;
            SharpDomain _domainX = new SharpDomain();
            SharpDomain _domainY = new SharpDomain();
            double _maxSpeed = 0;
            double _mass = 0;
            int _cycle = 0;
            Curve _target = null;
            List<Curve> _obstacles = new List<Curve>();

            DA.GetData(0, ref _reset);
            DA.GetData(1, ref _size);
            DA.GetData(2, ref _domainX);
            DA.GetData(3, ref _domainY);
            DA.GetData(4, ref _maxSpeed);
            DA.GetData(5, ref _mass);
            DA.GetData(6, ref _cycle);
            DA.GetData(7, ref _target);
            DA.GetDataList(8,  _obstacles);

            if(_reset || m_population==null)
            {
                m_population = new GAPopulation(_size, _domainX, _domainY, _maxSpeed, _mass, _cycle, _target, _obstacles);
            }

            DA.SetData(0, m_population);

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
            get { return new Guid("7e4a9270-451f-4435-ad8b-e28f8aa941b9"); }
        }
    }
}