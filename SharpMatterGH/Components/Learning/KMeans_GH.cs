using System;
using System.Collections.Generic;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using SharpMatter.SharpLearning;
namespace SharpMatter.SharpMatterGH.Components.Learning
{
    public class KMeans_GH : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the KMeans class.
        /// </summary>
        public KMeans_GH()
          : base("K-Means", "K-Means",
              "Cluster a set of data based on their similarity",
              "SharpMatter", "Learning")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
          
          
            pManager.AddNumberParameter("input", "input", "Input data to cluster", GH_ParamAccess.tree);
            pManager.AddIntegerParameter("clusters", "clusters", "Specify the number of clusters", GH_ParamAccess.item, 5);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Results", "Results", "Clustered data", GH_ParamAccess.list);
            pManager.AddNumberParameter("Centroids", "Centroids", "Cluster centroids", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // DataTree< GH_Number> _input = new DataTree<GH_Number>();

            GH_Structure<GH_Number> _input = new GH_Structure<GH_Number>();
            int _clusters = 0;
          
            DA.GetDataTree(0, out _input);
            DA.GetData(1, ref _clusters);



          
            DataTree<double> centroids = new DataTree<double>();
            int[] results;

            SharpKMeans.KMeansClustering(_clusters, _input, out centroids, out results);

            DA.SetDataList(0, results);
            DA.SetDataTree(1, centroids);
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
            get { return new Guid("972e82d8-56a4-4e6f-80aa-dbc3c390935e"); }
        }
    }
}