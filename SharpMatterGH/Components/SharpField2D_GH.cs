using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using SharpMatter.SharpField;

namespace SharpMatter.SharpMatterGH.Components
{
    public class SharpField2D_GH : GH_Component
    {
        SharpField2D<double> sharpField2D;
        /// <summary>
        /// Initializes a new instance of the SharpField2D_GH class.
        /// </summary>
        public SharpField2D_GH()
          : base("SharpField2Dd", "SharpField2Dd",
              "Description",
              "SharpMatter", "Fields")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

            pManager.AddBooleanParameter("reset", "reset", "reset simulation", GH_ParamAccess.item, false);
            pManager.AddIntegerParameter("columns", "columns", "Field dimenion X-axis", GH_ParamAccess.item, 50);
            pManager.AddIntegerParameter("rows", "rows", "Field dimenion Y-axis", GH_ParamAccess.item, 50);
            pManager.AddNumberParameter("resolution", "resolution", "Field resolution ", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("valueA", "valueA", "Input values", GH_ParamAccess.list);
            pManager.AddNumberParameter("valueB", "valueB", "Input values ", GH_ParamAccess.list);
            //pManager[5].Optional = true; // if no repellers are pluged in, code will still work
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Field", "Field", "Sharp Field 2D", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            bool _reset = false;
            int _columns = 0;
            int _rows = 0;
            double _resolution = 0;
            List<double> _valueA = new List<double>();
            List<double> _valueB = new List<double>();

            DA.GetData(0, ref _reset);
            DA.GetData(1, ref _columns);
            DA.GetData(2, ref _rows);
            DA.GetData(3, ref _resolution);
            DA.GetDataList(4, _valueA);
            DA.GetDataList(5, _valueB);

            if (_reset || sharpField2D == null)
            {
                sharpField2D = new SharpField2D<double>(_columns, _rows, _resolution, _valueA, _valueB);
                sharpField2D.ClearValues(1);
               

            }

          //  sharpField2D = new SharpField2D<double>(_columns, _rows, _resolution, _valueA, _valueB);
            DA.SetData(0, new GH_ObjectWrapper(sharpField2D));


           // DA.SetData(0, sharpField2D);


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
            get { return new Guid("6b81b63f-3f68-4941-92b4-bd02119a7c9d"); }
        }
    }
}