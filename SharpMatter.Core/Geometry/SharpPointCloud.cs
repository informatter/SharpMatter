using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace SharpMatter.SharpGeometry
{
    public class SharpPointCloud: PointCloud
    {
        SharpCloudItem [] m_cloud;
        public SharpPointCloud(Point3d [] points):base(points)
        {
            m_cloud = new SharpCloudItem[points.Count()];

            for (int i = 0; i < m_cloud.Length; i++)
            {
                m_cloud[i] = new SharpCloudItem(new Point3f((float)points[i].X, (float) points[i].Y, (float) points[i].Z));
            }
        }
    }
}
