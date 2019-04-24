using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using SharpMatter.SharpField;

namespace SharpMatter.SharpMatterGH.Components.Field
{
    public class SharpField2D_GH : GH_Component
    {

        private SharpField2DType _SharpField2DType = SharpField2DType.Physarium;

        SharpField2D<double> sharpField2D;

        private Param_Number _reactionDiffusion = new Param_Number()
        {
            Name = "valuesB",
            NickName = "valuesB",
            Description = "Input values",
            Access = GH_ParamAccess.list,

        };


        private Param_Boolean _Physarium = new Param_Boolean()
        {
            Name = "occupationStates",
            NickName = "occupationStates",
            Description = "Initial Cell occupation states",
            Access = GH_ParamAccess.list,

        };

        public SharpField2DType FieldType
        {
            get { return _SharpField2DType; }
            set
            {
                _SharpField2DType = value;
                Message = _SharpField2DType.ToString();
            }
        }


    

      
        /// <summary>
        /// Initializes a new instance of the SharpField2D_GH class.
        /// </summary>
        public SharpField2D_GH()
          : base("SharpField2D", "SharpField2D",
              "Create a 2D Scalar Field",
              "SharpMatter", "Fields")
        {
            FieldType = SharpField2DType.Physarium;
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
            pManager.AddNumberParameter("values", "values", "Input values", GH_ParamAccess.list);
            
            pManager.AddParameter(_reactionDiffusion);

            //if (FieldType == SharpField2DType.ReactionDiffusion)
            //{
            //    pManager.AddNumberParameter("valueA", "valueA", "Input values", GH_ParamAccess.list);
            //    pManager.AddNumberParameter("valueB", "valueB", "Input values ", GH_ParamAccess.list);
            //}

            //if(FieldType == SharpField2DType.Physarium)
            //{
            //    pManager.AddNumberParameter("valueA", "valueA", "Input values", GH_ParamAccess.list);
            //    pManager.AddBooleanParameter("occupied", "valueB", "Input values ", GH_ParamAccess.list);
            //}
            //pManager[5].Optional = true; // if no data pluged in, code will still work
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

            //List<double> _valueA = new List<double>();
            //List<double> _valueB = new List<double>();

            DA.GetData(0, ref _reset);
            DA.GetData(1, ref _columns);
            DA.GetData(2, ref _rows);
            DA.GetData(3, ref _resolution);
            //DA.GetDataList(4, _valueA);
            //DA.GetDataList(5, _valueB);

            //if (_reset || sharpField2D == null)
            //{
            //    sharpField2D = new SharpField2D<double>(_columns, _rows, _resolution, _valueA, _valueB);
            //    sharpField2D.ClearValues(1);


            //}


            //DA.SetData(0, new GH_ObjectWrapper(sharpField2D));


            switch (_SharpField2DType)
            {
                case SharpField2DType.ReactionDiffusion:
                    {
                        List<double> _valueA = new List<double>();
                        List<double> _valueB = new List<double>();
                        DA.GetDataList(4, _valueA);
                        DA.GetDataList(5, _valueB);

                        if (_reset || sharpField2D == null)
                        {
                            sharpField2D = new SharpField2D<double>(_columns, _rows, _resolution, _valueA, _valueB);
                            sharpField2D.ClearValues(1);


                        }

                        break;
                    }
                case SharpField2DType.Physarium:
                    {
                        List<double> _valueA = new List<double>();
                        List<bool> _valueB = new List<bool>();
                        DA.GetDataList(4, _valueA);
                        DA.GetDataList(5, _valueB);

                        if (_reset || sharpField2D == null)
                        {
                            sharpField2D = new SharpField2D<double>(_columns, _rows, _resolution, _valueA, _valueB);
                            sharpField2D.ClearValues(0);
                            sharpField2D.ClearOccupiedStates();


                        }
                        break;
                    }


                default:
                    {
                        //throw new ArgumentException("The specified field type is not supported.");
                        List<double> _valueA = new List<double>();
                        List<bool> _valueB = new List<bool>();
                        DA.GetDataList(4, _valueA);
                        DA.GetDataList(5, _valueB);

                        if (_reset || sharpField2D == null)
                        {
                            sharpField2D = new SharpField2D<double>(_columns, _rows, _resolution, _valueA, _valueB);
                            sharpField2D.ClearValues(0);
                            sharpField2D.ClearOccupiedStates();


                        }

                        break;
                    }
            }


            DA.SetData(0, new GH_ObjectWrapper(sharpField2D));



        }






        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalComponentMenuItems(menu);

            var sub = Menu_AppendItem(menu, "Type");
            Menu_AppendItem(sub.DropDown, "Reaction Diffusion", ReactionDiffusionClicked, true, _SharpField2DType == SharpField2DType.ReactionDiffusion);
            Menu_AppendItem(sub.DropDown, "Physarium Field", PhysariumClicked, true, _SharpField2DType == SharpField2DType.Physarium);
          //  Menu_AppendItem(sub.DropDown, "Generic Field", GenericClicked, true, _type == FieldType.Vector);


        }

        private void ReactionDiffusionClicked(object sender, EventArgs e)
        {
            FieldType = SharpField2DType.ReactionDiffusion;
            Params.Input[5] = _reactionDiffusion;
            Params.OnParametersChanged();
            ExpireSolution(true);
        }

        private void PhysariumClicked(object sender, EventArgs e)
        {
            FieldType = SharpField2DType.Physarium;
            Params.Input[5] = _Physarium;
            Params.OnParametersChanged();
            ExpireSolution(true);
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


        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes.CustomAttributes(this);
        }
    }
}