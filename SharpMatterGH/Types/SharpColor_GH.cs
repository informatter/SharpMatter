using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using SharpMatter;
using SharpMatter.SharpColor;

using SharpMatter.SharpMatterGH.Components.Parameters;
using SharpMatter.SharpGeometry;
namespace SharpMatter.SharpMatterGH.Types
{
    public class SharpColor_GH : GH_GeometricGoo<SharpColor.SharpColor>
    {

        public override SharpColor.SharpColor Value

        {
            get { return base.Value; }
            set { base.Value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec3"></param>
        public static implicit operator SharpColor.SharpColor(SharpColor_GH vec3)
        {
            return vec3.Value;


        }

        public static explicit operator Point3d(SharpColor_GH v)
        {
            return new Point3d(v.m_value.R, v.m_value.G, v.m_value.B);
        }

        public static explicit operator Vector3d(SharpColor_GH v)
        {
            return new Vector3d(v.m_value.R, v.m_value.G, v.m_value.B);
        }



        public static explicit operator Color(SharpColor_GH v)
        {
            return Color.FromArgb((int)v.m_value.R, (int)v.m_value.G, (int)v.m_value.B);
        }














        private BoundingBox _bbox = BoundingBox.Unset;


        /// <summary>
        /// 
        /// </summary>
        public SharpColor_GH()
        {
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public SharpColor_GH(SharpColor.SharpColor value)
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
            get { return "SharpColor"; }
        }


        /// <inheritdoc />
        public override string TypeDescription
        {
            get { return "SharpColor"; }
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
            return new SharpColor_GH(new SharpColor.SharpColor(Value.R, Value.G, Value.B));
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
            if (typeof(T).IsAssignableFrom(typeof(SharpColor.SharpColor)))
            {
                object obj = Value;
                target = (T)obj;
                return true;
            }

            if (typeof(T).IsAssignableFrom(typeof(Vector3d)))
            {
                object obj = Value;
                target = (T)obj;
                return true;
            }

            if (typeof(T).IsAssignableFrom(typeof(Vec3)))
            {
                object obj = Value;
                target = (T)obj;
                return true;
            }

            //if (typeof(T).IsAssignableFrom(typeof(Point3d)))
            //{
            //    object obj = Value;
            //    target = (T)obj;
            //    return true;
            //}

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
            if (source is SharpColor.SharpColor color)
            {
                Value = color;
                return true;
            }


            if (source is Point3d p)
            {
                Value = (SharpColor.SharpColor)p;
                return true;
            }

            if (source is Vector3d vec)
            {
                Value = (SharpColor.SharpColor)vec;
                return true;
            }

            if (source is GH_Colour vecgh)
            {
                Value = (SharpColor.SharpColor)vecgh;
                return true;
            }

            if (source is GH_Point ghP)
            {
                Value = (SharpColor.SharpColor)ghP;
                return true;
            }


            if (source is Color col)
            {
                Value = (SharpColor.SharpColor)col;
                return true;
            }

            if (source is GH_Colour colgh)
            {
                Value = (SharpColor.SharpColor)colgh;
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
            if (m_value != null)
            {
                var v = new SharpColor.SharpColor(m_value.R, m_value.G, m_value.B);

                Point3d p = new Point3d(v.R, v.G, v.B);
                p.Transform(xform);

                v.R = (int)p.X;
                v.G = (int)p.Y;
                v.B = (int)p.Z;
                return new SharpColor_GH(v);
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
                var v = new SharpColor.SharpColor(m_value.R, m_value.G, m_value.B);
                Point3d p = new Point3d(v.R, v.G, v.B);
                p = xmorph.MorphPoint(p);
                v.R = (int)p.X;
                v.G = (int)p.Y;
                v.B = (int)p.Z;

                return new SharpColor_GH(v);
            }

            else
            {
                return null;
            }
        }


    }// END CLASS





}




