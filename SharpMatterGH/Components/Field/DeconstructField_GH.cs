using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace SharpMatter.SharpMatterGH.Components.Field
{
    public class DeconstructField_GH : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructField_GH class.
        /// </summary>
        public DeconstructField_GH()
          : base("DeconstructField", "DeconstructField",
              "Description",
              "SharpMatter", "Fields")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("field", "field", "sharp field", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

            pManager.AddNumberParameter("Values", "Values", "Sharp Field 2D", GH_ParamAccess.list);
            pManager.AddBooleanParameter("States", "States", "Sharp Field 2D", GH_ParamAccess.list);
            pManager.AddPointParameter("Grid", "Grid", "Sharp Field 2D", GH_ParamAccess.list);
            pManager.AddLineParameter("Cells", "Cells", "Sharp Field 2D", GH_ParamAccess.list);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            SharpField.SharpField2D<double> _field = new SharpField.SharpField2D<double>();

            DA.GetData(0, ref _field);

           // List<double> values = new List<double>();
            //List<bool> states = new List<bool>();
            List<Point3d> grid = new List<Point3d>();
            List<Polyline> cells = new List<Polyline>();

            foreach (var item in _field.Field)
            {
                //states.Add(item.Occupied);
                grid.Add(new Point3d(item.Position.X, item.Position.Y, item.Position.Z));
                //cells.Add(item.DisplayCell);
            }

            DA.SetData(0, _field.Values);
            DA.SetDataList(1, _field.States);
            DA.SetDataList(2, grid);
           // DA.SetDataList(3, cells);



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
            get { return new Guid("015c9372-186a-4821-8e1f-1ca55adc9dd0"); }
        }
    }
}