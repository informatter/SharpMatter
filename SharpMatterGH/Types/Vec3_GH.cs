using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

using SharpMatter.SharpMatterGH.Components.Parameters;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpMatterGH.Types
{
    public class Vec3_GH : GH_GeometricGoo<Vec3>
    {
        #region Static


        public override Vec3 Value

        {
            get { return base.Value; }
            set { base.Value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec3"></param>
        public static implicit operator Vec3(Vec3_GH vec3)
        {
            return vec3.Value;

            
        }

        public static explicit operator Point3d(Vec3_GH v)
        {
            return new Point3d(v.m_value.X, v.m_value.Y, v.m_value.Z);
        }
        #endregion


        private BoundingBox _bbox = BoundingBox.Unset;


        /// <summary>
        /// 
        /// </summary>
        public Vec3_GH()
        {
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Vec3_GH(Vec3 value)
        {
            Value = value;
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        //public Vec3_GH(Vec3_GH other)
        //{
        //    Value = other.Value;
        //}


        // <inheritdoc />
        public override bool IsValid
        {
            get { return true; }
        }


        /// <inheritdoc />
        public override string TypeName
        {
            get { return "Vec3"; }
        }


        /// <inheritdoc />
        public override string TypeDescription
        {
            get { return "Vec3"; }
        }


        /// <inheritdoc />
        public override BoundingBox Boundingbox
        {
            get
            { 
                if (m_value != null && !_bbox.IsValid)
                    _bbox = new BoundingBox();

                return _bbox;
            }
        }

      

        // <inheritdoc />
        public override IGH_Goo Duplicate()
        {
            return DuplicateGeometry();
        }


        // <inheritdoc />
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            return new Vec3_GH(new Vec3(Value.X, Value.Y, Value.Z));
        }


       // / <inheritdoc />
        public override string ToString()
        {
            return Value.ToString();
        }


        /// <inheritdoc />
        public override object ScriptVariable()
        {
            return Value;
        }


       // / <inheritdoc />
        public override bool CastTo<T>(ref T target)
        {
            if (typeof(T).IsAssignableFrom(typeof(Vec3)))
            {
                object obj = Value;
                target = (T)obj;
                return true;
            }

            if (typeof(T).IsAssignableFrom(typeof(GH_ObjectWrapper)))
            {
                object obj = new GH_ObjectWrapper(Value);
                target = (T)obj;
                return true;
            }

            return false;
        }


        /// <inheritdoc />
        public override bool CastFrom(object source)
        {
            if (source is Vec3 vec3)
            {
                Value = vec3;
                return true;
            }


            if (source is Point3d p)
            {
                Value = (Vec3)p;
                return true;
            }

            if (source is Vector3d vec)
            {
                Value = (Vec3)vec;
                return true;
            }

            if (source is GH_Point ghP)
            {
                Value = (Vec3)ghP;
                return true;
            }

            return false;
        }


        /// <inheritdoc />
        public override BoundingBox GetBoundingBox(Transform xform)
        {
            var b = Boundingbox;
            b.Transform(xform);
            return b;
        }


        /// / <inheritdoc />
        public override IGH_GeometricGoo Transform(Transform xform)
        {
            if(m_value!=null)
            {
                var v = new Vec3(m_value.X, m_value.Y, m_value.Z);

                Point3d p = new Point3d(v.X, v.Y, v.Z);
                p.Transform(xform);

                v.X = p.X;
                v.Y = p.Y;
                v.Z = p.Z;
                return new Vec3_GH(v);
            }
            else
            {
                return null;
            }
            
        }


        /// <inheritdoc />
        public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
        {
            if (m_value != null)
            {
                var v = new Vec3(m_value.X, m_value.Y, m_value.Z);
                Point3d p = new Point3d(v.X, v.Y, v.Z);
                p = xmorph.MorphPoint(p);
                v.X = (float)p.X;
                v.Y = (float)p.Y;
                v.Z = (float)p.Z;

                return new Vec3_GH(v);
            }

            else
            {
                return null;
            }
        }

      
    }
}
