using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using SharpMatter.SharpData;

namespace SharpMatter.SharpMatterGH.Components.Types
{
    public class SharpDomain_GH : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SharpDomain_GH class.
        /// </summary>
        public SharpDomain_GH()
          : base("SharpDomain", "SharpDomain",
              "Represents a 2D Domain",
              "SharpMatter", "Math")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("min", "min", "Start domain", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("max", "max", "End domain", GH_ParamAccess.item, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Domain", "Domain", "Sharp Domain", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            double  _min = 0;
            double _max = 0;
            DA.GetData(0, ref _min);
            DA.GetData(1, ref _max);

            SharpDomain d = new SharpDomain(_min, _max);

            DA.SetData(0, d);


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
            get { return new Guid("b2639cf4-9172-4020-a8ae-26e0446287e5"); }
        }

        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes.CustomAttributes(this);
        }
    }
}