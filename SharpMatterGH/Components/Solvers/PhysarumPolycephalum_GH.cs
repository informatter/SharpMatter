using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Rhino.Geometry;

using SharpMatter.SharpField;
using SharpMatter.SharpMath;
using SharpMatter.SharpPopulations;
using SharpMatter.SharpSolvers;
using SharpMatter.SharpBehavior;
using Grasshopper;

namespace SharpMatter.SharpMatterGH.Components.Solvers
{
    public class PhysarumPolycephalum_GH : GH_Component
    {
        private Random ran = new Random(); // Random Instance used to generate initial random positions and velocities

        private PhysarumPolycephalumPopulation physarumPolycephalumPopulation;

        private CellBoundaryDisplay _cellBoundDisplay = CellBoundaryDisplay.No;



        private PhysarumPolycephalumPopulation.DimensionType _dimenions = PhysarumPolycephalumPopulation.DimensionType._2d;




        public PhysarumPolycephalumPopulation.DimensionType Dimension
        {
            get { return _dimenions; }
            set
            {
                _dimenions = value;
                Message = _dimenions.ToString();
            }
        }

        public CellBoundaryDisplay CellDisplay
        {
            get { return _cellBoundDisplay; }
            set
            {
                _cellBoundDisplay = value;
               
            }
        }






        /// <summary>
        /// Initializes a new instance of the PhysarumPolycephalum_GH class.
        /// </summary>
        public PhysarumPolycephalum_GH()
          : base("Physarum Polycephalum", "Physarum Polycephalum",
              "Simulation of PhysarumPolycephalum better known as 'Slime Mould ' ",
              "SharpMatter", "Solvers")
        {
            Dimension = PhysarumPolycephalumPopulation.DimensionType._2d;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

           // double maxSpeed, double mass, double sensorOffsetDistance, double sensorAngle, double agentRotationAngle,
           //DimensionType dimension, SharpField2D<double> scalarField)

            pManager.AddBooleanParameter("run", "run", "run the simulation", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("reset", "reset", "reset the simulation", GH_ParamAccess.item, false);
            pManager.AddIntegerParameter("Populationnumber", "Populationnumber", "Total population number, this is generally a % of the simulation area. Typical values are between 3-15%",GH_ParamAccess.item,50);
            pManager.AddNumberParameter("maxSpeed", "maxSpeed", "Maximum speed of agent, generally it will equal the resolution of the Scalar field", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("mass", "mass", "mass of agent", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("sensorOffsetDistance", "SO", "The sensor offset distance (SO) from the body of the agent. Typical values range from 3-9 ", GH_ParamAccess.item, 3);
            pManager.AddNumberParameter("sensorAngle", "SA", "The angle between each sensor (SA)  Typical values are 22.5 or 45 degrees", GH_ParamAccess.item, 45);
            pManager.AddNumberParameter("agentRotationAngle", "RA", "The angle between each sensor (RA)  Typical value is  45 degrees", GH_ParamAccess.item, 22.5);
            pManager.AddGenericParameter("xBounds", "xBounds", "X-Dimension boundaries of simulation ", GH_ParamAccess.item);
            pManager.AddGenericParameter("yBounds", "yBounds", "Y-Dimension boundaries of simulation ", GH_ParamAccess.item);
            pManager.AddGenericParameter("field", "field", "sharp field", GH_ParamAccess.item);
            pManager.AddNumberParameter("decayT", "decayT", "Chemoattractant decay factor to damp diffusion values. Smaller values produce less damping of diffusion",GH_ParamAccess.item, 0.1);


        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("PheromoneValues", "PheromoneValues", "Pheromone diffusion values", GH_ParamAccess.list);
            pManager.AddPointParameter("AgentPositions", "AgentPositions", "Physarum Agent positions", GH_ParamAccess.tree);

            ///////////////////////////////// ADDED FOR DEBUGGING PURPOSES//////////////////////////////////////////////////////////////////


            pManager.AddPointParameter("SensorPositions", "SensorPositions", "Sensor positions", GH_ParamAccess.tree);
            pManager.AddVectorParameter("SensorDisplays", "SensorDisplays", "Sensor display", GH_ParamAccess.tree);


            pManager.AddCurveParameter("Grid", "Grid", "Counts total of agents per Cell", GH_ParamAccess.list);

            ///////////////////////////////// ADDED FOR DEBUGGING PURPOSES//////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {


            bool _run = false;
            bool _reset = false;
            int _populationNum = 0;
            double _maxSpeed = 0;
            double _mass = 0;
            double _sensorOffsetDistance = 0;
            double _sensorAngle = 0;
            double _agentRotationAngle = 0;
            SharpDomain _xBounds = new SharpDomain(0, 1);
            SharpDomain _yBounds = new SharpDomain(0, 1);
            SharpField2D<double> _field = new SharpField2D<double>();
            double _decayT = 0;

            DA.GetData(0, ref _run);
            DA.GetData(1, ref _reset);
            DA.GetData(2, ref _populationNum);
            DA.GetData(3, ref _maxSpeed);
            DA.GetData(4, ref _mass);
            DA.GetData(5, ref _sensorOffsetDistance);
            DA.GetData(6, ref _sensorAngle);
            DA.GetData(7, ref _agentRotationAngle);
            DA.GetData(8, ref _xBounds);
            DA.GetData(9, ref _yBounds);
            DA.GetData(10, ref _field);
            DA.GetData(11, ref _decayT);


            DataTree<GH_Point> positions = new DataTree<GH_Point>();
            DataTree<GH_Point> sensorPositions = new DataTree<GH_Point>();
            DataTree<GH_Vector> sensorDisplays = new DataTree<GH_Vector>();

            List<PolylineCurve> cells = new List<PolylineCurve>();



            if (_reset || physarumPolycephalumPopulation == null)
            {
                physarumPolycephalumPopulation = new PhysarumPolycephalumPopulation(_populationNum, _xBounds, _yBounds, _field.Resolution, _maxSpeed, _mass, _sensorOffsetDistance, _sensorAngle, _agentRotationAngle,
                    _dimenions, ran, _xBounds, _yBounds);
            }

            if (_run)
            {

                // System step updates Motorstage and Sensory Stage of each Physarum Agent
                // For testing I have random rotation turned on only
                PhysarumPopulationSystemStep.SystemStep(physarumPolycephalumPopulation.Population, ran, _field, out positions, out sensorPositions, out sensorDisplays);



                //Computes the Cell states and diffusses Chemoattractant levels across the field
                PhysarumField2D.SolvePhysarumField(_field, physarumPolycephalumPopulation.Population, _decayT);

            


                ExpireSolution(true);

            }

            if (_run == false)
            {




                // System step updates Motorstage and Sensory Stage of each Physarum Agent

               PhysarumPopulationSystemStep.SystemStep(physarumPolycephalumPopulation.Population, ran, _field, out positions, out sensorPositions, out sensorDisplays);







            }

            if (_cellBoundDisplay == CellBoundaryDisplay.Yes)
            {
                foreach (SharpCell<double> item in _field.Field)
                {
                    cells.Add(item.BoundingBox);
                }
            }

   





            DA.SetDataList(0, _field.Values);
            DA.SetDataTree(1, positions);
            DA.SetDataTree(2, sensorPositions);
            DA.SetDataTree(3, sensorDisplays);

            DA.SetDataList(4, cells);

            //   DA.SetDataList(4, _field.States);
            //   DA.SetDataList(5, _field.AgentCount);
            //  DA.SetDataList(6, cells);

        }

      


       

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        /// 
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
            get { return new Guid("c505989e-a707-4012-9b8c-c564db4c37b5"); }
        }




        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalComponentMenuItems(menu);

            var sub = Menu_AppendItem(menu, "Simulation dimension");
            Menu_AppendItem(sub.DropDown, "2D", _2DClicked, true, _dimenions == PhysarumPolycephalumPopulation.DimensionType._2d);
            Menu_AppendItem(sub.DropDown, "3D", _3DClicked, true, _dimenions == PhysarumPolycephalumPopulation.DimensionType._3d);

            var sub2 = Menu_AppendItem(menu, "Display Cell Region");

            Menu_AppendItem(sub2.DropDown, "Yes", _yesClicked, true, _cellBoundDisplay == CellBoundaryDisplay.Yes);
            Menu_AppendItem(sub2.DropDown, "No", _noClicked, true, _cellBoundDisplay == CellBoundaryDisplay.No);


        }


        private void _2DClicked(object sender, EventArgs e)
        {
            Dimension = PhysarumPolycephalumPopulation.DimensionType._2d;
            Params.OnParametersChanged();
            ExpireSolution(true);
        }


        private void _3DClicked(object sender, EventArgs e)
        {
            Dimension = PhysarumPolycephalumPopulation.DimensionType._3d;
            Params.OnParametersChanged();
            ExpireSolution(true);
        }



        private void _yesClicked(object sender, EventArgs e)
        {
            CellDisplay = CellBoundaryDisplay.Yes;
            Params.OnParametersChanged();
            ExpireSolution(true);
        }


        private void _noClicked(object sender, EventArgs e)
        {
            CellDisplay = CellBoundaryDisplay.No;
            Params.OnParametersChanged();
            ExpireSolution(true);
        }








    }
}