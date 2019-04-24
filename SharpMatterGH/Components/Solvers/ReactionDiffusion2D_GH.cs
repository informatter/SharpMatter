using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using SharpMatter.SharpField;
using SharpMatter.SharpSolvers;
using SharpMatter.SharpExtensions;

namespace SharpMatter.SharpMatterGH.Components.Solvers
{
    public class ReactionDiffusion2D_GH: GH_Component
    {



        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public ReactionDiffusion2D_GH()
          : base("Reaction Diffusion 2D", "Reaction Diffusion 2D",
              "Solve Grey Scott's reactiong diffusion equations",
              "SharpMatter", "Solvers")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {


            pManager.AddBooleanParameter("run", "run", "run the simulation", GH_ParamAccess.item, false);
            pManager.AddGenericParameter("field", "field", "sharp fiueld", GH_ParamAccess.item);
            pManager.AddNumberParameter("Da", "Da", "Input values diffusion rate for chemical A", GH_ParamAccess.list);
            pManager.AddNumberParameter("Db", "Db", "Input values diffusion rate for chemical B", GH_ParamAccess.list);
            pManager.AddNumberParameter("kill", "kill", "Input values for kill rate", GH_ParamAccess.list);
            pManager.AddNumberParameter("feed", "feed", "Input values for feed rate", GH_ParamAccess.list);
            pManager.AddNumberParameter("deltaT", "deltaT", "Delta time", GH_ParamAccess.item);



        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Values", "Values", "Reaction Diffusion values", GH_ParamAccess.list);
      

            /////////////////////////////////// debugging////////////////////////////////////////////////////////////
            ///
       

            /////////////////////////////////// debugging////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {






            bool _run = false;
            SharpField2D<double> _field = new SharpField2D<double>();
            List<double> _Da = new List<double>();
            List<double> _Db = new List<double>();
            List<double> _kill = new List<double>();
            List<double> _feed = new List<double>();
            double _deltaT = 0;

            DA.GetData(0, ref _run);
            DA.GetData(1, ref _field);
            DA.GetDataList(2, _Da);
            DA.GetDataList(3, _Db);
            DA.GetDataList(4, _kill);
            DA.GetDataList(5, _feed);

            DA.GetData(6, ref _deltaT);






            double[] dA = _Da.ToArray();
            double[] dB = _Db.ToArray();
            double[] kill = _kill.ToArray();
            double[] feed = _feed.ToArray();

            double[,] a = dA.Make2DArray(_field.Columns, _field.Rows);
            double[,] b = dB.Make2DArray(_field.Columns, _field.Rows);
            double[,] c = kill.Make2DArray(_field.Columns, _field.Rows);
            double[,] d = feed.Make2DArray(_field.Columns, _field.Rows);


            if (_run)
             {

                   
               ReactionDiffusion2D.SolveGreyScottReactionDiffussionB(_field, a, b, c, d, _deltaT);
              //  ReactionDiffusion2D.SolveGreyScottReactionDiffussion(_field, _Da, _Db, _kill, _feed, _deltaT);

                ExpireSolution(true);
             }
            


        

            DA.SetDataList(0, _field.Values);



        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c03c45c0-772f-4e42-b93d-a53baad2640d"); }
        }


    }
}
