using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using SharpMatter.SharpGeometry;
using System.Drawing;
using Grasshopper.Kernel.Types;
namespace SharpMatter.SharpColor
{

    /// <summary>
    /// 
    /// Represents the three components of a color in three-dimensional space and its treated mathematically like a Vector.
    /// Color class written by Nicholas Rawitscher. This library is currently under development 
    /// 
    /// 
    /// </summary>


    public struct SharpColor : IEquatable<SharpColor>
    {
        //public static readonly SharpColor Empty = new SharpColor();

        private double m_r;
        private double m_g;
        private double m_b;

        #region CONSTRUCTORS

        /// <summary>
        /// Constrcut a SharpColor from RGB values
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public SharpColor(int R, int G, int B)
        {
            this.m_r = (R > 255) ? 255 : ((R < 0) ? 0 : R);
            this.m_g = (G > 255) ? 255 : ((G < 0) ? 0 : G);
            this.m_b = (B > 255) ? 255 : ((B < 0) ? 0 : B);
        }



        public SharpColor(double R, double G, double B)
        {
            this.m_r = (R > 255) ? 255 : ((R < 0) ? 0 : R);
            this.m_g = (G > 255) ? 255 : ((G < 0) ? 0 : G);
            this.m_b = (B > 255) ? 255 : ((B < 0) ? 0 : B);
        }

        /// <summary>
        /// Constrcut a SharpColor from a Vec3
        /// </summary>
        /// <param name="vec"></param>
        public SharpColor(Vec3 vec)
        {
            this.m_r = ((int)vec.X > 255) ? 255 : (((int)vec.X < 0) ? 0 : (int)vec.X);
            this.m_g = ((int)vec.Y > 255) ? 255 : (((int)vec.Y < 0) ? 0 : (int)vec.Y);
            this.m_b = ((int)vec.Z > 255) ? 255 : (((int)vec.Z < 0) ? 0 : (int)vec.Z);
        }


        #endregion

        #region OPERATOR OVERLOADING




        /// <summary>
        /// Cast from Point3d to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator SharpColor(Point3d v)
        {
            return new SharpColor((int)v.X, (int)v.Y, (int)v.Z);
        }
        /// <summary>
        /// Cast from Vec3 to Point3d
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Point3d(SharpColor v)
        {
            return new Point3d(v.m_r, v.m_g, v.m_b);
        }

        /// <summary>
        /// Cast from Vector3d to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator SharpColor(Vector3d v)
        {
            return new SharpColor((int)v.X, (int)v.Y, (int)v.Z);
        }



        /// <summary>
        /// Cast from Color to SharpColor
        /// </summary>
        /// <param name="c"></param>
        public static explicit operator SharpColor(Color c)
        {
            return new SharpColor(c.R, c.G, c.B);
        }


        public static explicit operator Color(SharpColor c)
        {
            return Color.FromArgb((int)c.R, (int)c.G, (int)c.B);


        }




        public static explicit operator SharpColor(GH_Colour c)
        {
            return new SharpColor(c.Value.R, c.Value.G, c.Value.B);
        }



        /// <summary>
        /// Cast from Vec3 to Vector3d
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Vector3d(SharpColor v)
        {
            return new Vector3d(v.R, v.G, v.B);
        }


        /// <summary>
        /// Cast from Vec3 to GH_Vector
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator GH_Colour(SharpColor v)
        {
            return new GH_Colour(Color.FromArgb((int)v.R, (int)v.G, (int)v.B));
        }


        /// <summary>
        /// Cast from GH_Vector to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator SharpColor(GH_Vector v)
        {
            return new SharpColor((int)v.Value.X, (int)v.Value.Y, (int)v.Value.Z);
        }

        /// <summary>
        /// Cast from GH_Point to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator SharpColor(GH_Point v)
        {
            return new SharpColor((int)v.Value.X, (int)v.Value.Y, (int)v.Value.Z);
        }


        /// <summary>
        /// Cast from Vec3 to GH_Point
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator GH_Point(SharpColor v)
        {
            return new GH_Point(new Point3d(v.R, v.G, v.B));
        }


        public static implicit operator string(SharpColor color)
        {
            return color.ToString();
        }





        public static bool operator ==(SharpColor vecA, SharpColor vecB)
        {
            if (vecA.m_r == vecB.m_r && vecA.m_g == vecB.m_g && vecA.m_b == vecB.m_b)
            {
                return true;
            }
            else
                return false;


        }



        public static bool operator !=(SharpColor item1, SharpColor item2)
        {
            //return (
            //    item1.m_r != item2.m_r
            //    || item1.m_g != item2.m_g
            //    || item1.m_b != item2.m_b
            //    );


            if (item1.m_r != item2.m_r && item1.m_g != item2.m_g && item1.m_b != item2.m_b)
            {
                return true;
            }
            else
                return false;
        }






        public static SharpColor operator +(SharpColor colorA, SharpColor colorB)
        {
            colorA.m_r += colorB.m_r;
            colorA.m_g += colorB.m_g;
            colorA.m_b += colorB.m_b;
            return colorA;
        }



        public static SharpColor operator -(SharpColor colorA, SharpColor colorB)
        {
            colorA.m_r -= colorB.m_r;
            colorA.m_g -= colorB.m_g;
            colorA.m_b -= colorB.m_b;
            return colorA;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Color"></param>
        /// <returns></returns>
        public static SharpColor operator -(SharpColor Color)
        {
            Color.m_r = -Color.m_r;
            Color.m_g = -Color.m_g;
            Color.m_b = -Color.m_b;
            return Color;
        }


        /// <summary>
        /// Scale a vector by a value
        /// </summary>
        /// <param name="color"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static SharpColor operator *(SharpColor color, int scalar)
        {
            color.m_r *= scalar;
            color.m_g *= scalar;
            color.m_b *= scalar;
            return color;
        }


        public static SharpColor operator *(SharpColor color, double scalar)
        {
            color.m_r *= scalar;
            color.m_g *= scalar;
            color.m_b *= scalar;
            return color;
        }


        /// <summary>
        /// Scale a vector by a value
        /// </summary>
        /// <param name="color"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static SharpColor operator *(int scalar, SharpColor color)
        {
            color.m_r *= scalar;
            color.m_g *= scalar;
            color.m_b *= scalar;
            return color;
        }


        public static SharpColor operator *(double scalar, SharpColor color)
        {
            color.m_r *= scalar;
            color.m_g *= scalar;
            color.m_b *= scalar;
            return color;
        }



        /// <summary>
        /// Multiplies two colors together, returning the dot product 
        /// This value equals vecA.Magnitude * vecB.Magnitude * cos(alpha), where alpha is the angle between vectors.
        /// </summary>
        /// <param name="colorA"></param>
        /// <param name="colorB"></param>
        /// <returns></returns>
        public static double operator *(SharpColor colorA, SharpColor colorB)
        {
            return colorA.m_r * colorB.m_r + colorA.m_g * colorB.m_g + colorA.m_b * colorB.m_b;
        }





        /// <summary>


        /// <summary>
        /// Component-wise division.
        /// </summary>
        /// <param name="colorA"></param>
        /// <param name="ColorA"></param>
        /// <returns></returns>
        public static SharpColor operator /(SharpColor colorA, SharpColor ColorA)
        {
            colorA.m_r /= ColorA.m_r;
            colorA.m_g /= ColorA.m_g;
            colorA.m_b /= ColorA.m_b;
            return colorA;
        }


        /// <summary>
        /// Divide a vector by a number
        /// </summary>
        /// <param name="color"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static SharpColor operator /(SharpColor color, int a)
        {
            color.m_r /= a;
            color.m_g /= a;
            color.m_b /= a;
            return color;
        }





        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="colorA"></param>
        /// <param name="colorB"></param>
        /// <returns></returns>

        public static bool operator >(SharpColor colorA, SharpColor colorB)
        {
            // a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is larger than b.Z;

            if (colorA.m_r > colorB.m_r || colorA.m_r == colorB.m_r && colorA.m_g > colorB.m_g ||
                colorA.m_r == colorB.m_r && colorA.m_g == colorB.m_g && colorA.m_b > colorB.m_b) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="colorA"></param>
        /// <param name="colorB"></param>
        /// <returns></returns>
        public static bool operator >=(SharpColor colorA, SharpColor colorB)
        {
            // a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y, or a.X == b.X and a.Y == b.Y and a.Z >= b.Z

            if (colorA.m_r > colorB.m_r || colorA.m_r == colorB.m_r && colorA.m_g > colorB.m_g ||
                colorA.m_r == colorB.m_r && colorA.m_g == colorB.m_g && colorA.m_b >= colorB.m_b) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="colorA"></param>
        /// <param name="colorB"></param>
        /// <returns></returns>
        public static bool operator <(SharpColor colorA, SharpColor colorB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z

            if (colorA.m_r < colorB.m_r || colorA.m_r == colorB.m_r && colorA.m_g < colorB.m_g ||
                colorA.m_r == colorB.m_r && colorA.m_g == colorB.m_g && colorA.m_b < colorB.m_b) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="colorA"></param>
        /// <param name="colorB"></param>
        /// <returns></returns>
        public static bool operator <=(SharpColor colorA, SharpColor colorB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z <= b.Z

            if (colorA.m_r < colorB.m_r || colorA.m_r == colorB.m_r && colorA.m_g < colorB.m_g ||
                colorA.m_r == colorB.m_r && colorA.m_g == colorB.m_g && colorA.m_b <= colorB.m_b) return true;

            else return false;


        }








        #endregion

        #region IMPLICIT INTERFACE IMPLEMENTATION

        public bool Equals(SharpColor other)
        {
            if (other.m_r == this.m_r && other.m_g == this.m_g && other.m_b == this.m_b)
                return true;

            else
                return false;
        }

        #endregion



        #region PROPERTIES
        /// <summary>
        /// Gets or sets red value.
        /// </summary>
        public double R
        {
            get
            {
                return m_r;
            }
            set
            {
                m_r = (value > 255) ? 255 : ((value < 0) ? 0 : value);
            }
        }

        /// <summary>
        /// Gets or sets red value.
        /// </summary>
        public double G
        {
            get
            {
                return m_g;
            }
            set
            {
                m_g = (value > 255) ? 255 : ((value < 0) ? 0 : value);
            }
        }

        /// <summary>
        /// Gets or sets red value.
        /// </summary>
        public double B
        {
            get
            {
                return m_b;
            }
            set
            {
                m_b = (value > 255) ? 255 : ((value < 0) ? 0 : value);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public double Magnitude
        {
            get { return Math.Sqrt(m_r * m_r + m_g * m_g + m_b * m_b); }

        }


        public double SqrMagnitude
        {
            get { return Math.Pow(Magnitude, 2); }
        }




        #endregion

        #region STATIC METHODS

        public static SharpColor Zero()
        {

            return new SharpColor(0, 0, 0);
        }


        #endregion

        #region METHODS


        /// <summary>
        /// Find the closest color in a color collection.
        /// </summary>
        /// <param name="colorToSearchFrom"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        public static SharpColor ClosestColor(SharpColor colorToSearchFrom, List<SharpColor> colors)

        {

            List<double> distanceList = new List<double>();

            if (colorToSearchFrom != null)

            {

                for (int i = 0; i < colors.Count; i++)

                {

                    if (colors[i] != null)

                    {
                        double distance = colorToSearchFrom.DistanceTo(colors[i]);

                        distanceList.Add(distance);

                    }

                }

            }

            int smallestIndex = distanceList.IndexOf(distanceList.Min());

            return colors[smallestIndex];

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="colorToSearchFrom"></param>
        /// <param name="colors"></param>
        /// <param name="index"></param>

        public static void ClosestColor(SharpColor colorToSearchFrom, List<SharpColor> colors, out int index)

        {

            List<double> distanceList = new List<double>();

            if (colorToSearchFrom != null)

            {

                for (int i = 0; i < colors.Count; i++)

                {

                    if (colors[i] != null)

                    {
                        double distance = colorToSearchFrom.DistanceTo(colors[i]);

                        distanceList.Add(distance);

                    }

                }

            }

            int smallestIndex = distanceList.IndexOf(distanceList.Min());

            index = smallestIndex;


        }



        /// <summary>
        /// computes distance between two vectors
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double DistanceTo(SharpColor other)
        {
            other.m_r -= m_r;
            other.m_g -= m_g;
            other.m_b -= m_b;
            return other.Magnitude;
        }


        public static SharpColor Round(SharpColor color)
        {

            return new SharpColor(Math.Round(color.R), Math.Round(color.G), Math.Round(color.B));

        }




        public double SqrDistanceTo(SharpColor other)
        {
            other.m_r -= m_r;
            other.m_g -= m_g;
            other.m_b -= m_b;
           return  other.SqrMagnitude;
        }

       

        #endregion


        #region SYSTEM.OBJECT METHOD OVERRIDING

        public override string ToString()
        {
            //return $"({x}, {y}, {z})";

            var culture = System.Globalization.CultureInfo.InvariantCulture;

            return String.Format("{0},{1},{2}", m_r.ToString(culture), m_g.ToString(culture), m_b.ToString(culture));
        }


        public override bool Equals(object obj)
        {
            SharpColor temp;
            temp = (SharpColor)obj;
            if (obj is SharpColor && obj != null)
            {
                if (temp.m_r == this.m_r && temp.m_g == this.m_g && temp.m_b == this.m_b)
                {
                    return true;
                }

                else return false;
            }

            return false;
        }


        public override int GetHashCode()
        {
            return m_r.GetHashCode() ^ m_g.GetHashCode() ^ m_b.GetHashCode();
        }



        #endregion

    }
}
