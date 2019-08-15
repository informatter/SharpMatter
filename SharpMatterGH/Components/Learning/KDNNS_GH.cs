using System;
using System.Collections.Generic;

using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using SharpMatter.SharpLearning;
using Rhino.Geometry;
using SharpMatter.SharpExtensions;

namespace SharpMatter.SharpMatterGH.Components.Learning
{
    public class KDNNS_GH : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the KDNNS class.
        /// </summary>
        public KDNNS_GH()
          : base("KDTreeNearestNeighbourSearch", "KDNNS",
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
            pManager.AddNumberParameter("SearchPoint", "SearchPoint", "Point  to search from", GH_ParamAccess.list);
            pManager.AddIntegerParameter("K", "K", "Number of nearest neighbours to find", GH_ParamAccess.item,5);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Results", "Results", "Results", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<GH_Number> _PointCloud = new GH_Structure<GH_Number>();
            List<double> _testPoint = new List<double>();
            int _num = 0;

            DA.GetDataTree(0, out _PointCloud);
            DA.GetDataList(1,  _testPoint);
            DA.GetData(2, ref _num);


            List<Point3d> result =SharpKDTree.Knearest(_PointCloud, _testPoint.ToArray(), _num);

           
            DA.SetDataList(0, result);
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
            get { return new Guid("93f8d06f-5f0c-4535-ac9d-064f9d703be8"); }
        }
    }
}