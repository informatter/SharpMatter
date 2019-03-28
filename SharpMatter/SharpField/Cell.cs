using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpField
{
    public class Cell <T>
    {
        
       
        private T m_scalarValueA;
        private T m_scalarValueB;
        private bool m_occupied;
        private Vec3 m_position;


        public Cell(T valueA)
        {
           
            m_scalarValueA = valueA;
          
        }

        public Cell(T valueA, bool occupied)
        {

            m_scalarValueA = valueA;
            m_occupied = occupied;

        }


        public Cell(T valueA, T valueB, Vec3 position)
        {

            m_scalarValueA = valueA;
            m_scalarValueB = valueB;
            m_position = position;

        }

        public bool Occupied
        {
            get { return m_occupied; }

            set
            {
                m_occupied = value;
            }
        }

        public Vec3 Position
        {
            get { return m_position; }
        }

        public T ScalarValueA
        {
            get { return m_scalarValueA; }

            set
            {
                m_scalarValueA = value;
            }
        }


        public T ScalarValueB
        {
            get { return m_scalarValueB; }

            set
            {
                m_scalarValueB = value;
            }
        }


      





    }
}
