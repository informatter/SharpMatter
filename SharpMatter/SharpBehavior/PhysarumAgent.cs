using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpPhysics;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpMath;
using SharpMatter.SharpField;
using SharpMatter.SharpData;

namespace SharpMatter.SharpBehavior
{
    /// <summary>
    /// This class contains the Behaviours of a Physarum particle defined by Jeff Jhones in "Characteristics of pattern formation and evolution
    /// http://dx.doi.org/10.1162/artl.2010.16.2.16202
    ///in approximations of physarum transport networks" 
    /// </summary>
    public class PhysarumAgent : SharpParticle
    {

        #region FIELD DATA

     
        private double m_agentRotationAngle; // RA
        private double m_sensorAngle;// SA

        private double m_sensorOffsetDistance; // SO

        private Vec3 m_frontSensorAPos;
        private Vec3 m_frontSensorBPos;
        private Vec3 m_rightSensorPos;
        private Vec3 m_leftSensorPos;

        private Vec3 m_frontSensorAVecDisplay;
        private Vec3 m_frontSensorBVecDisplay;
        private Vec3 m_rightSensorVecDisplay;
        private Vec3 m_leftSensorVecDisplay;

        private double m_resolution;

        private Random random;

        private SharpDomain m_xBounds;
        private SharpDomain m_yBounds;

        

        #endregion



        #region CONSTRUCTORS
        /// <summary>
        /// Slime Agent constructor, inherits from Sharp Particle
        /// </summary>
        /// <param name="position"> agent position </param>
        /// <param name="acceleration"> agent acceleration</param>
        /// <param name="velocity">agent velocity </param>
        /// <param name="maxSpeed"> maximum speed</param>
        /// <param name="mass"> mass </param>
        /// <param name="sensorOffsetDistance">Sensor offset distance </param>
        /// <param name="sensorAngle">Sensor angle </param>
        public PhysarumAgent(Vec3 position, Vec3 velocity, double fieldResolution, double maxSpeed, double mass, double sensorOffsetDistance, double sensorAngle,
            double agentRotationAngle, int randomSeed, SharpDomain xBounds, SharpDomain yBounds)

            : base(position, velocity, maxSpeed, mass)
        {
            random = new Random(randomSeed);
            m_xBounds = new SharpDomain();
            m_yBounds = new SharpDomain();
            base.Position = position;
         
            base.Acceleration = Vec3.Zero;//velocity;
            base.Velocity = velocity;
            base.MaxSpeed = maxSpeed;//fieldResolution;
            base.Mass = mass;

            this.m_sensorOffsetDistance = sensorOffsetDistance;
            this.m_sensorAngle = sensorAngle;
            this.m_agentRotationAngle = agentRotationAngle;

            this.m_resolution = fieldResolution;

            this.m_xBounds = xBounds;
            this.m_yBounds = yBounds;


            m_frontSensorAPos = Vec3.Zero;
            m_frontSensorBPos = Vec3.Zero;
            m_rightSensorPos = Vec3.Zero;
            m_leftSensorPos = Vec3.Zero;

          


           // UpdateSensorPositions();

        }



        /// <summary>
        /// Slime Agent constructor, inherits from Sharp Particle
        /// This construcctor should be used when other forces will be applied
        /// </summary>
        /// <param name="position"></param>
        /// <param name="acceleration"></param>
        /// <param name="velocity"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="mass"></param>
        /// <param name="sensorOffsetDistance"></param>
        /// <param name="sensorAngle"></param>
        /// <param name="agentRotationAngle"></param>
        public PhysarumAgent(Vec3 position, Vec3 acceleration, Vec3 velocity, double fieldResolution, double maxSpeed, double mass, double sensorOffsetDistance, double sensorAngle,
            double agentRotationAngle, int randomSeed, SharpDomain xBounds, SharpDomain yBounds )
            :base(position,acceleration,velocity,maxSpeed,mass)
        {

            random = new Random(randomSeed);
            m_xBounds = new SharpDomain();
            m_yBounds = new SharpDomain();

            base.Position = position;
            base.Acceleration = acceleration;
            base.Velocity = velocity;
            base.MaxSpeed = maxSpeed; //fieldResolution;
            base.Mass = mass;

            this.m_sensorOffsetDistance = sensorOffsetDistance;
            this.m_sensorAngle = sensorAngle;
            this.m_agentRotationAngle = agentRotationAngle;
            this.m_resolution = fieldResolution;


            m_frontSensorAPos = Vec3.Zero;
            m_frontSensorBPos = Vec3.Zero;
            m_rightSensorPos = Vec3.Zero;
            m_leftSensorPos = Vec3.Zero;

            this.m_xBounds = xBounds;
            this.m_yBounds = yBounds;


          //  UpdateSensorPositions();


        }

   







        public PhysarumAgent() { }


        #endregion



        #region PROPERTIES

  

        /// <summary>
        /// Agent Rotation Angle (RA) defined by Jeff Jhones, typical values are 45 or 22.5 degrees
        /// </summary>
        public double AgentRotationAngle //RA
        {
            get { return m_agentRotationAngle; }

            set
            {

                if (value <=0)
                {
                    throw new Exception("Rotation angle must be larger than zero ");
                }

                else
                    m_agentRotationAngle = value;

            }
        }

        /// <summary>
        /// Foreword sensor to sample chemical values of cells in Scalar Field
        /// </summary>
        public Vec3 ForewordSensorA
        {
            
            get { return m_frontSensorAPos; }
        }

        /// <summary>
        /// Foreword sensor to sample occupation states of cells in Scalar Field. The lenght of the sensor is equal to the resolution of the field
        /// </summary>
        public Vec3 ForewordSensorB
        {

            get { return m_frontSensorBPos; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vec3 ForewordSensorADisplay
        {

            get { return m_frontSensorAVecDisplay; }
        }

        public Vec3 ForewordSensorBDisplay
        {

            get { return m_frontSensorBVecDisplay; }
        }

        public Vec3 LeftSensor
        {
            get { return m_leftSensorPos; }
        }

        public Vec3 LeftSensorDisplay
        {
            get { return m_leftSensorVecDisplay; }
        }

        public Vec3 RightSensor
        {
            get { return m_rightSensorPos; }
        }

        public Vec3 RightSensorDisplay
        {
            get { return m_rightSensorVecDisplay; }
        }


        /// <summary>
        /// Sensor rotation angles (SA) defined by Jeff Jhones. Typical values are 45 or 22.5 degrees
        /// </summary>
        public double SensorAngle
        {
            get { return m_sensorAngle; }

            set
            {
                if (value != 22.5 || value != 45.0)
                {
                    throw new Exception("Sensor angle must equal 22.5 or 45.0 degrees");
                }

                else
                    m_sensorAngle = value;

            }
        }


        /// <summary>
        /// Sensor offset defined by Jeff Jhones. Typical values are between 3 and 38
        /// </summary>
        public double SensorOffsetDistance
        {
            get { return m_sensorOffsetDistance; }

            set
            {
                if (value < 3 || value > 38.0)
                {
                    throw new Exception("Offset distance must be between 3.0 and 38.0");
                }

                else
                    m_sensorOffsetDistance = value;

            }
        }

        #endregion

        #region METHODS



        /// <summary>
        /// Wraps around borders
        /// </summary>
        public void CheckBoundary()

        {


            if (this.Position.X < m_xBounds.Min) base.SetPositionX(m_xBounds.Max);
            else if (Position.X > m_xBounds.Max) base.SetPositionX(m_xBounds.Min);

            if (Position.Y < m_yBounds.Min) base.SetPositionY(m_yBounds.Max);
            else if (Position.Y > m_yBounds.Max) base.SetPositionY(m_yBounds.Min);



            //if (Position.X < m_xBounds.Min || Position.X > m_xBounds.Max)
            //{
            //    double tempVx = Velocity.X;
            //    base.SetVelocityX(tempVx *= -1);
            //}
            //if (Position.Y < m_yBounds.Min || Position.Y > m_yBounds.Max)
            //{
            //    double tempVy = Velocity.Y;
            //    base.SetVelocityY(tempVy *= -1);
            //}



        }


        /// <summary>
        /// Define the posiion of front sensor A
        /// </summary>
        private void DefineFrontSensorA()
        {
            Vec3 m_tempPos = Velocity;
            m_tempPos.Normalize();
            m_tempPos *= m_sensorOffsetDistance;


            m_frontSensorAPos = Position + m_tempPos;

            m_frontSensorAVecDisplay = m_frontSensorAPos - Position;


        }

        /// <summary>
        /// Define the posiion of front sensor B. This sensor samples the cell that is inmediatelly in front of the agent 
        /// to determine its occupation state. Refer to diagrams on pg 39 of the book "From Pattern Formation to Material Computation. Multi-agent Modelling of Physarum Polycephalum"   
        /// </summary>
        private void DefineFrontSensorB()
        {
            Vec3 m_tempPos = Velocity;
            m_tempPos.Normalize();
            m_tempPos *= m_resolution;


            m_frontSensorBPos = Position + m_tempPos;

            m_frontSensorBVecDisplay = m_frontSensorBPos - Position;


        }

        /// <summary>
        /// 
        /// </summary>
        private void DefineLeftSensor()
        {
            Vec3 m_tempPos = Velocity;
            m_tempPos.Normalize();
            m_tempPos *= m_sensorOffsetDistance;

            Vec3 rotVec = Vec3.VectorRotate(m_tempPos, SharpMath.SharpMath.ToRadians(m_sensorAngle), true, false);
            m_leftSensorPos = Position + rotVec;

            m_leftSensorVecDisplay = m_leftSensorPos- Position;


        }

        /// <summary>
        /// 
        /// </summary>
        private void DefineRightSensor()
        {
            Vec3 m_tempPos = Velocity;
            m_tempPos.Normalize();
            m_tempPos *= m_sensorOffsetDistance;

            Vec3 rotVec = Vec3.VectorRotate(m_tempPos, SharpMath.SharpMath.ToRadians(m_sensorAngle*-1), true, false);
            m_rightSensorPos = Position + rotVec;

            m_rightSensorVecDisplay = m_rightSensorPos - Position;

        }



        /// <summary>
        /// Motor stage of the Slime Agent. The Slime Agent will only deposit a Chemical value to its current cell if it does not have any other agents
        /// If the current Cell has more than one agent a random direction for the agent will be chosen
        /// </summary>
        /// <param name="field">Scalar Field</param>
        /// <param name="ran">Random class instance</param>
        private void MotorStage(SharpField2D<double> field)
        {
          

            //Foreword sensor B is the one that samples the field to determine if its inmediatelly front cell is occupied or not
            //Refer to diagrams on pg 39 of the book "From Pattern Formation to Material Computation. Multi-agent Modelling of Physarum Polycephalum"   

            Cell<double> cell =  field.LookUpCell(m_frontSensorBPos);
            Vec3 vel = Vec3.Zero;

            bool occupied = cell.Occupied;

            if (occupied == false)
            {
                // Move to next Cell and deposit chemoattractant
                UpdateInternal();

              // cell.Occupied = true;

                cell.ScalarValueA += 5.0;

              //cell.Occupied = false;
                
            }

            if (occupied == true)
            {
                // Stay in current Cell, dont deposit chemoattractant and choose random new orientration
               // cell.Occupied = true;
               
                Vec3 newVel = Vec3.VectorRotate(Velocity, SharpMath.SharpMath.ToRadians(random.Next(0, 360)), true, false);
                // Velocity = newVel;
                vel = newVel;
                Velocity = vel;

            }

          

        }


        /// <summary>
        /// Each sensor will retrieves its current chemoattractant value from the scalar field 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="frontSensorVal"></param>
        private void SampleField(SharpField2D<double> field, out double frontSensorVal, out double leftSensorVal, out double rightSensorVal)
        {


            Cell<double> currentCell = field.LookUpCell(m_frontSensorAPos);
            Cell<double> currentCellRight = field.LookUpCell(m_rightSensorPos);
            Cell<double> currentCellLeft = field.LookUpCell(m_leftSensorPos);

            frontSensorVal = currentCell.ScalarValueA;
            leftSensorVal = currentCellLeft.ScalarValueA;
            rightSensorVal = currentCellRight.ScalarValueA;



        }

        /// <summary>
        /// Sensory stage of the Slime Agent. Each sensor retrieves its current Chemo-attractant value from the scalar field
        /// Each value will determine which direction the agent will face next. The agent will always be more attracted to
        /// higher value of the chemical 
        /// </summary>
        /// <param name="field"></param>
        private void SensoryStage(SharpField2D<double> field)
        {
            double F; //F
            double FL; //FL
            double FR; //FR

            // Each sensor retrieves its current chemoattractant value from the scalar field
            SampleField(field, out F, out FL, out FR);

            Vec3 vel= Vec3.Zero;

            if (F > FL && F > FR)
            {


                // continue facing same direction

                vel = Velocity;

            }

            else if (F < FL && F < FR)
            {
                //double num = random.NextDouble();
                //if (num > 0.5)
                //{
                //    //rotate left
                //   // Vec3 temp = Velocity;
                //  //  Vec3 newVel = Vec3.VectorRotate(temp, SharpMath.SharpMath.ToRadians(m_agentRotationAngle), true, false);
                //    //Velocity = newVel;

                //    vel = Vec3.VectorRotate(Velocity, SharpMath.SharpMath.ToRadians(m_agentRotationAngle), true, false); 



                //}

                //// or
                //else
                //{
                //    // rotate right
                //   // Vec3 temp = Velocity;
                //    //  Vec3 newVel = Vec3.VectorRotate(temp, SharpMath.SharpMath.ToRadians(m_agentRotationAngle * -1), true, false);
                //    //Velocity = newVel;
                //    vel = Vec3.VectorRotate(Velocity, SharpMath.SharpMath.ToRadians(m_agentRotationAngle*-1), true, false);
                //}




                /////////MOVE TOWARDS HIGHER CONCENTRATION///////////
                if (FL > FR)
                {
                    //rotate left
             
                    Vec3 newVel = Vec3.VectorRotate(Velocity, SharpMath.SharpMath.ToRadians(m_agentRotationAngle), true, false);
             
                    vel = newVel;
                }

                if (FL < FR)
                {
                    //rotate right
                    Vec3 newVel = Vec3.VectorRotate(Velocity, SharpMath.SharpMath.ToRadians(m_agentRotationAngle * -1), true, false);  
                    vel = newVel;
                }


            }

            else if(FL< FR)
            {
                // rotate right
  
                vel = Vec3.VectorRotate(Velocity, SharpMath.SharpMath.ToRadians(m_agentRotationAngle*-1), true, false);

            }

            else if (FR < FL)
            {
                // rotate left

                vel = Vec3.VectorRotate(Velocity, SharpMath.SharpMath.ToRadians(m_agentRotationAngle), true, false);
            }

            else
            {
                // continue facing same direction

                vel = Velocity;


            }

            Velocity = vel;
       


        }

     
        /// <summary>
        /// 
        /// </summary>
        public override void Update()
        {
            Velocity += Acceleration;

            //////////////////////////////////////

           // If this line of code is enabled, for some reason every updtate call velocity will get smaller and smaller

            // Velocity*= MaxSpeed;

            //////////////////////////////////////
            Position += Velocity*MaxSpeed;

          
       
        }


        private void UpdateInternal()
        {
           Velocity += Acceleration;

            //////////////////////////////////////

            // If this line of code is enabled, for some reason every updtate call velocity will get smaller and smaller

            // Velocity*= MaxSpeed;

            //////////////////////////////////////
            Position += Velocity* MaxSpeed;

          

        }

        /// <summary>
        /// Written for debugging purposes
        /// </summary>
        /// <param name="ran"></param>

     


        [Obsolete("This method was written for debugging purposes only")]
        public void RotationByRefTest()
        {
            //CheckBoundary();

            double num = random.NextDouble();

            if (num > 0.98)
            {
                Vec3 temp = Velocity;


                Vec3 newVel = Vec3.VectorRotate(temp, SharpMath.SharpMath.ToRadians(random.Next(0, 360)), true, false);


                Velocity = newVel;
            }



        }

        /// <summary>
        /// Update Motor and Sensory stage of Agent
        /// </summary>
        /// <param name="field"></param>
        /// <param name="ran"></param>
        public void SystemStep(SharpField2D<double> field)
        {
            this.MotorStage(field);
            this.SensoryStage(field);
          

           UpdateSensorPositions();// has to be all the time
        }


        private void UpdateSensorPositions()
        {

            DefineFrontSensorA();
            DefineFrontSensorB();
            DefineLeftSensor();
            DefineRightSensor();
        }


       
    
   
      
        #endregion

       









    }/// END SLIME MOULD CLASS
}
