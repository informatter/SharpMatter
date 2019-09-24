using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpField;
using SharpMatter.SharpGeometry;
using System.Drawing;

using Rhino.Geometry;

namespace SharpMatter.SharpBehavior
{

    public enum States { Alive, Dead};
    public  class Automata : SharpCell<double>
        
       
                  
    {
        private double m_previousState;
        private double m_state;
        private double m_value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="state"></param>
        /// <param name="value"></param>
        public Automata(Vec3 position, double state, double value) : base(position,state, value)
        {
            m_state = state;
            m_previousState = m_state;

        }


        /// <summary>
        /// 
        /// </summary>
        public double PreviousState
        {
            get { return m_previousState; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double State
        {
            get { return m_state; }

         
        }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool Display(out Vec3 pos)
        {
            Vec3 temp= Vec3.Zero;
            pos = temp;

            if (m_state == 1)
            {
                temp = base.Position;
               
                pos = temp;

                return true;

            }


          else  return false;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Color DisplayColor()
        {
            Color outP;
            if (m_state== 1) outP = Color.FromArgb(255, 0, 0, 0);


            else outP = Color.FromArgb(255, 255, 255, 255);


            return outP;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newState"></param>
        public void SaveNewState(double newState)
        {
            m_state = newState;
        }


        /// <summary>
        /// 
        /// </summary>
        public void SavePreviousState()
        {
            m_previousState = m_state;
        }


      

    }
}
