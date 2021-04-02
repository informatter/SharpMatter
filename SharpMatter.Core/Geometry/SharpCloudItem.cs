using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using System.Drawing;

namespace SharpMatter.SharpGeometry
{
    public struct  SharpCloudItem
    {
       public  Point3f m_location;
        public Color m_color;
        public Vector3f m_normal;

        public SharpCloudItem(Point3f location, Color color, Vector3f normal)
        {
            m_location = location;
            m_color = color;
            m_normal = normal;

        }

        public SharpCloudItem(Point3f location)
        {
            m_location = location;
            m_color = Color.White;
            m_normal = Vector3f.Zero;

        }


    }
}
