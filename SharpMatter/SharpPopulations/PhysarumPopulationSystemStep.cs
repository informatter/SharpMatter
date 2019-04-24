using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper;

using Rhino.Geometry;

using SharpMatter.SharpBehavior;
using SharpMatter.SharpField;



namespace SharpMatter.SharpPopulations
{
    public static  class PhysarumPopulationSystemStep
    {

        /// <summary>
        /// This method iterates through all the Physarum Agent Population and calls the Motor and Sensory stage of each Agent
        /// It should be used in any Solve Instance Method,i.e In every update call
        /// </summary>
        /// <param name="physarumAgents"></param>
        /// <param name="ran"></param>
        /// <param name="positions"></param>
        /// <param name="sensorPositions"></param>
        /// <param name="sensorDisplays"></param>
        /// 
      //  [Obsolete("Dont forget to update this method to update Motor and Sensory stage instead of 'RotationByRefTest Method!!'")]
        public static void SystemStep(List<PhysarumAgent> physarumAgents,Random ran, SharpField2D<double> field,out DataTree<GH_Point> positions, out DataTree<GH_Point> sensorPositions, out DataTree<GH_Vector> sensorDisplays)
        {
            DataTree<GH_Point> _positions = new DataTree<GH_Point>();
            DataTree<GH_Point> _sensorPositions = new DataTree<GH_Point>();
            DataTree<GH_Vector> _sensorDisplays = new DataTree<GH_Vector>();

            positions = _positions;
            sensorPositions = _sensorPositions;
            sensorDisplays = _sensorDisplays;

            int pathCounter = -1;// -1 so DataTree will be {0} {1} .....


          //  double num = ran.NextDouble();

         


            //Parallel.ForEach(physarumAgents, item => // BUGS OUT 

            foreach (PhysarumAgent item in physarumAgents)
            {
          
                item.SystemStep(field);
                
                item.CheckBoundary();
           

                pathCounter++;

                GH_Path path = new GH_Path(pathCounter);



                Point3d tempPos = new Point3d(item.Position.X, item.Position.Y, item.Position.Z);
                _positions.Add(new GH_Point(tempPos), path);

                ///////////////////////////////// ADDED FOR DEBUGGING PURPOSES//////////////////////////////////////////////////////////////////

                Point3d tempSensorAPos = new Point3d(item.ForewordSensorA.X, item.ForewordSensorA.Y, item.ForewordSensorA.Z);
                Point3d tempSensorBPos = new Point3d(item.ForewordSensorB.X, item.ForewordSensorB.Y, item.ForewordSensorB.Z);
                Point3d tempRightSensorPos = new Point3d(item.RightSensor.X, item.RightSensor.Y, item.RightSensor.Z);
                Point3d tempLeftSensorPos = new Point3d(item.LeftSensor.X, item.LeftSensor.Y, item.LeftSensor.Z);


                Vector3d tempSensorADisplay = new Vector3d(item.ForewordSensorADisplay.X, item.ForewordSensorADisplay.Y, item.ForewordSensorADisplay.Z);
                Vector3d tempSensorBDisplay = new Vector3d(item.ForewordSensorBDisplay.X, item.ForewordSensorBDisplay.Y, item.ForewordSensorBDisplay.Z);
                Vector3d tempRightSensorDisplay = new Vector3d(item.RightSensorDisplay.X, item.RightSensorDisplay.Y, item.RightSensorDisplay.Z);
                Vector3d tempLeftSensorDisplay = new Vector3d(item.LeftSensorDisplay.X, item.LeftSensorDisplay.Y, item.LeftSensorDisplay.Z);



                _sensorPositions.Add(new GH_Point(tempSensorAPos), path);
                _sensorPositions.Add(new GH_Point(tempSensorBPos), path);
                _sensorPositions.Add(new GH_Point(tempRightSensorPos), path);
                _sensorPositions.Add(new GH_Point(tempLeftSensorPos), path);

                _sensorDisplays.Add(new GH_Vector(tempSensorADisplay), path);
                _sensorDisplays.Add(new GH_Vector(tempSensorBDisplay), path);
                _sensorDisplays.Add(new GH_Vector(tempRightSensorDisplay), path);
                _sensorDisplays.Add(new GH_Vector(tempLeftSensorDisplay), path);

                ///////////////////////////////// ADDED FOR DEBUGGING PURPOSES//////////////////////////////////////////////////////////////////

            }
            //});
        }
    }
}
