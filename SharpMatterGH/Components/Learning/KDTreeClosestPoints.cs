using System;
using System.Collections.Generic;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Rhino.Geometry;
using SharpMatter.SharpLearning;
namespace SharpMatter.SharpMatterGH.Components.Learning
{
    public class KDTreeClosestPoints : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the KDTreeClosestPoints class.
        /// </summary>
        public KDTreeClosestPoints()
          : base("KDTreeClosestPoints", "KDTree Cp's",
              "Description",
              "SharpMatter", "Learning")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("PointCloud", "PointCloud", "Point cloud to search", GH_ParamAccess.tree);
            pManager.AddNumberParameter("SearchPoints", "SearchPoints", "Points  to search from", GH_ParamAccess.tree);
            pManager.AddIntegerParameter("K", "K", "Number of nearest neighbours to find", GH_ParamAccess.item, 5);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Results", "Results", "Results", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<GH_Number> _PointCloud = new GH_Structure<GH_Number>();
            GH_Structure<GH_Number> _testPoints = new GH_Structure<GH_Number>();
            int _num = 0;

            DA.GetDataTree(0, out _PointCloud);
            DA.GetDataTree(1, out _testPoints);
            DA.GetData(2, ref _num);

            DataTree<Point3d> result = SharpKDTree.Knearest(_PointCloud, _testPoints, _num);

            DA.SetDataTree(0, result);
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
            get { return new Guid("5a6c3c49-c6da-404a-8d50-7d5df50c5afa"); }
        }
    }
}