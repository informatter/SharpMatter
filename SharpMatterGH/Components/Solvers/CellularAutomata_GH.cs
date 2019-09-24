using System;
using System.Collections.Generic;
using SharpMatter.SharpField;
using SharpMatter.SharpSolvers;
using System.Drawing;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SharpMatter.SharpMatterGH.Components.Solvers
{
    public class CellularAutomata_GH : GH_Component
    {
        

        int m_iterations = 0;
        /// <summary>
        /// Initializes a new instance of the CellularAutomata_GH class.
        /// </summary>
        public CellularAutomata_GH()
          : base("Cellular Automata", "Cellular Automata",
              "Description",
              "SharpMatter", "Solvers")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("reset", "reset", "reset the simulation", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("run", "run", "run the simulation", GH_ParamAccess.item, false);
            pManager.AddGenericParameter("field", "field", "sharp fiueld", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddColourParameter("States", "States", "", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Iterations", "Iterations", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool _reset = false;
            bool _run = false;
            SharpField2D<double> _field = new SharpField2D<double>();

            DA.GetData(0, ref _reset);
            DA.GetData(1, ref _run);
            DA.GetData(2, ref _field);

            if(_reset)
            {
               
                m_iterations = 0;
            }

            if(_run)
            {
                CellularAutomata.Solve(_field);
                m_iterations++;
                ExpireSolution(true);
            }

            if (!_run)
            {
                CellularAutomata.Initdisplay(_field);
            }



            DA.SetDataList(0, _field.FieldColors);
            DA.SetData(1, m_iterations);

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
            get { return new Guid("5c4dce8f-6892-4173-a9a9-cff3d74f972a"); }
        }
    }
}