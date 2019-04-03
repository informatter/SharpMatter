using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using SharpMatter;
using SharpMatter.SharpField;
using SharpMatter.SharpSolvers;



// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace SharpMatter.SharpMatterGH.Components.Solvers
{
    public class ReactionDiffusion2D_GH : GH_Component
    {

        SharpField2D<double> sharpField2D;

       


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
            pManager.AddBooleanParameter("reset", "reset", "reset simulation", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("run", "run", "run the simulation", GH_ParamAccess.item, false);
            pManager.AddIntegerParameter("columns", "columns", "Field dimenion X-axis", GH_ParamAccess.item, 50);
            pManager.AddIntegerParameter("rows", "rows", "Field dimenion Y-axis", GH_ParamAccess.item, 50);
            pManager.AddNumberParameter("resolution", "resolution", "Field resolution ", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("ChemA", "ChemA", "Input values for chemical A", GH_ParamAccess.list);
            pManager.AddNumberParameter("ChemB", "ChemB", "Input values for chemical B", GH_ParamAccess.list);
            pManager.AddNumberParameter("Da", "Da", "Input values diffusion rate for chemical A", GH_ParamAccess.list);
            pManager.AddNumberParameter("Db", "Db", "Input values diffusion rate for chemical B", GH_ParamAccess.list);
            pManager.AddNumberParameter("kill", "kill", "Input values for kill rate", GH_ParamAccess.list);
            pManager.AddNumberParameter("feed", "feed", "Input values for feed rate", GH_ParamAccess.list);
            pManager.AddNumberParameter("deltaT", "deltaT", "Delta time", GH_ParamAccess.item);



            //pManager.AddBooleanParameter("run", "run", "run the simulation", GH_ParamAccess.item, false);
            //pManager.AddGenericParameter("field", "field", "sharp fiueld", GH_ParamAccess.item);
            //pManager.AddNumberParameter("Da", "Da", "Input values diffusion rate for chemical A", GH_ParamAccess.list);
            //pManager.AddNumberParameter("Db", "Db", "Input values diffusion rate for chemical B", GH_ParamAccess.list);
            //pManager.AddNumberParameter("kill", "kill", "Input values for kill rate", GH_ParamAccess.list);
            //pManager.AddNumberParameter("feed", "feed", "Input values for feed rate", GH_ParamAccess.list);
            //pManager.AddNumberParameter("deltaT", "deltaT", "Delta time", GH_ParamAccess.item);



        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Values", "Values", "Reaction Diffusion values", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Iterations", "Iterations", "Total number of iterations during simulation", GH_ParamAccess.item);

            /////////////////////////////////// debugging////////////////////////////////////////////////////////////
            ///
            pManager.AddTextParameter("data", "data", "data", GH_ParamAccess.list);

            /////////////////////////////////// debugging////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            bool _reset = false;
            bool _run = false;
            int _columns = 0;
            int _rows = 0;
            double _resolution = 0;
            List<double> _chemA = new List<double>();
            List<double> _chemB = new List<double>();
            List<double> _Da = new List<double>();
            List<double> _Db = new List<double>();
            List<double> _kill = new List<double>();
            List<double> _feed = new List<double>();
            double _deltaT = 0;

            ///////// FIELD PARAMETERS/////
            DA.GetData(0, ref _reset);
            DA.GetData(1, ref _run);
            DA.GetData(2, ref _columns);
            DA.GetData(3, ref _rows);
            DA.GetData(4, ref _resolution);
            DA.GetDataList(5, _chemA);
            DA.GetDataList(6, _chemB);
            ///////// FIELD PARAMETERS/////

            ///////// REACTION DIFFUSION PARAMETERS/////
            DA.GetDataList(7, _Da);
            DA.GetDataList(8, _Db);
            DA.GetDataList(9, _kill);
            DA.GetDataList(10, _feed);
            DA.GetData(11, ref _deltaT);
            ///////// REACTION DIFFUSION PARAMETERS/////




            //bool _run = false;
            //SharpField2D<double> _field = new SharpField2D<double>();
            //List<double> _Da = new List<double>();
            //List<double> _Db = new List<double>();
            //List<double> _kill = new List<double>();
            //List<double> _feed = new List<double>();
            //double _deltaT = 0;

            /////////// FIELD PARAMETERS/////



            /////////// FIELD PARAMETERS/////

            /////////// REACTION DIFFUSION PARAMETERS/////
            //DA.GetData(0, ref _run);
            //DA.GetData(1, ref _field);
            //DA.GetDataList(2, _Da);
            //DA.GetDataList(3, _Db);
            //DA.GetDataList(4, _kill);
            //DA.GetDataList(5, _feed);

            //DA.GetData(6, ref _deltaT);
            /////////// REACTION DIFFUSION PARAMETERS/////









            if (_reset || sharpField2D == null)
            {
                sharpField2D = new SharpField2D<double>(_columns, _rows, _resolution, _chemA, _chemB);
                sharpField2D.ClearValues(1);
     
            }



            else
             {

                if (_run)
                {
                    ReactionDiffusion2D.SolveGreyScottReactionDiffussion(sharpField2D, _Da, _Db, _kill, _feed, _deltaT);
              
                    ExpireSolution(true);
                }
            }

         
            List<string> data = new List<string>();


            data.Add("resolution:" + "" + sharpField2D.Resolution.ToString());
            data.Add("rows:" + "" + sharpField2D.Rows.ToString());
            data.Add("columns:" + "" + sharpField2D.Columns.ToString());

            DA.SetDataList(0, sharpField2D.FieldValues);

            //data.Add("resolution:" + "" + _field.Resolution.ToString());
            //data.Add("rows:" + "" + _field.Rows.ToString());
            //data.Add("columns:" + "" + _field.Columns.ToString());

            //DA.SetDataList(0, _field.FieldValues);


            DA.SetDataList(2, data);
           
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
            get { return new Guid("721c50d6-79a8-4e8f-bdcb-ff8cc559093f"); }
        }
    }
}
