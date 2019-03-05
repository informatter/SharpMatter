using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SharpMatter.SharpField
{
    public struct Cell <T>
    {
        
       
        public T m_scalarValue;

        public Color m_color;

        public Cell(T value)
        {
           
            m_scalarValue = value;
            m_color = Color.FromArgb(0, 0, 0, 0);
        }





    }
}
