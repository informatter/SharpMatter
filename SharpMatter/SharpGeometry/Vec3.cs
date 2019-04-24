using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;




namespace SharpMatter.SharpGeometry
{
    /// <summary>
    /// 
    /// Represents the three components of a vector in three-dimensional space. 
    /// Vector class written by Nicholas Rawitscher. This library is currently under development 
    /// 
    /// 
    /// </summary>

  
    
    public struct Vec3 : IEquatable<Vec3>
    {
        // Field data
        
        private double m_x;
        private double m_y;
        private double m_z;

        

        public Vec3(double x, double y, double z)
        {
            m_x = x;
            m_y = y;
            m_z = z;

           
        }

       


        #region PROPERTIES

        public double Magnitude
        {
            get { return m_x * m_x + m_y * m_y + m_z * m_z; }

        }


        public double SqrMagnitude
        {
            get { return Math.Pow(Magnitude, 2); }
        }

      


        public double X
        {
            get { return m_x; }

            set { m_x = value; }
        }

        public static Vec3 XAxis

        {

            get { return new Vec3(1.0, 0.0, 0.0); }

        }

        public double Y
        {
            get { return m_y; }

            set { m_y = value; }
        }


        public static Vec3 YAxis

        {

            get { return new Vec3(0.0, 1.0, 0.0); }

        }

        public double Z
        {
            get { return m_z; }

            set { m_z = value; }
        }

        public static Vec3 ZAxis

        {

            get { return new Vec3(0.0, 0.0, 1.0); }

        }


        public static Vec3 Zero

        {

            get { return new Vec3(0.0, 0.0, 0.0); }

        }







        #endregion



        #region IMPLICIT INTERFACE IMPLEMENTATION


        /// <summary>
        /// Compares a Vec3 with another Vec3
        /// </summary>
        /// <param name="other"> other Vector to compare with </param>
        /// <returns></returns>
        public int CompareTo(Vec3 other)

        {

            if (m_x < other.m_x)

                return -1;

            if (m_x> other.m_x)

                return 1;



            if (m_y < other.m_y)

                return -1;

            if (m_y > other.m_y)

                return 1;



            if (m_z < other.m_z)

                return -1;

            if (m_z > other.m_z)

                return 1;



            return 0;

        }


        /// <summary>
        /// true if other has the same coordinates as this vector, else  otherwise false.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns> Bool
        public bool Equals(Vec3 other)
        {
            if (other.m_x == this.m_x && other.m_y == this.m_y && other.m_z == this.m_z)
                return true;

            else
                return false;
        }




        #endregion


        /// <summary>
        /// Set a vector component or get a vector component with a specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]

        {

            get

            {

                if (0 == index)

                    return m_x;

                if (1 == index)

                    return m_y;

                if (2 == index)

                    return m_z;


                else throw new IndexOutOfRangeException();


            }

            set

            {

                if (0 == index)

                   m_x = value;

                else if (1 == index)

                    m_y = value;

                else if (2 == index)

                    m_z = value;

                else throw new IndexOutOfRangeException();




            }

        }



        #region OPERATOR OVERLOADING
        /// <summary>
        /// Cast from Point3d to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Vec3 (Point3d v)
      {
            return new Vec3(v.X, v.Y, v.Z);
      }
        /// <summary>
        /// Cast from Vec3 to Point3d
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Point3d(Vec3 v)
        {
            return new Point3d(v.m_x, v.m_y, v.m_z);
        }

        /// <summary>
        /// Cast from Vector3d to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Vec3(Vector3d v)
        {
            return new Vec3(v.X, v.Y, v.Z);
        }


        /// <summary>
        /// Cast from Vec3 to Vector3d
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Vector3d(Vec3 v)
        {
            return new Vector3d(v.X, v.Y, v.Z);
        }


        /// <summary>
        /// Cast from Vec3 to GH_Vector
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator GH_Vector(Vec3 v)
        {
            return new GH_Vector(new Vector3d(v.X, v.Y, v.Z));
        }


        /// <summary>
        /// Cast from GH_Vector to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Vec3(GH_Vector v)
        {
            return new Vec3(v.Value.X, v.Value.Y, v.Value.Z);
        }

        /// <summary>
        /// Cast from GH_Point to Vec3
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Vec3(GH_Point v)
        {
            return new Vec3(v.Value.X, v.Value.Y, v.Value.Z);
        }


        /// <summary>
        /// Cast from Vec3 to GH_Point
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator GH_Point(Vec3 v)
        {
            return new GH_Point(new Point3d(v.X, v.Y, v.Z));
        }


        public static implicit operator string(Vec3 vector)
        {
            return vector.ToString();
        }




        public static Vec3 operator +(Vec3 vecA, Vec3 vecB)
        {
            vecA.m_x += vecB.m_x;
            vecA.m_y += vecB.m_y;
            vecA.m_z += vecB.m_z;
            return vecA;
        }



        public static Vec3 operator -(Vec3 vecA, Vec3 vecB)
        {
            vecA.m_x -= vecB.m_x;
            vecA.m_y -= vecB.m_y;
            vecA.m_z -= vecB.m_z;
            return vecA;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vec3 operator -(Vec3 vec)
        {
            vec.m_x = -vec.m_x;
            vec.m_y = -vec.m_y;
            vec.m_z = -vec.m_z;
            return vec;
        }


        /// <summary>
        /// Scale a vector by a value
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vec3 operator *(Vec3 vector, double scalar)
        {
            vector.m_x *= scalar;
            vector.m_y *= scalar;
            vector.m_z *= scalar;
            return vector;
        }


        /// <summary>
        /// Scale a vector by a value
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vec3 operator *(double scalar, Vec3 vector)
        {
            vector.m_x *= scalar;
            vector.m_y *= scalar;
            vector.m_z *= scalar;
            return vector;
        }


        /// <summary>
        /// Multiplies two vectors together, returning the dot product 
        /// This value equals vecA.Magnitude * vecB.Magnitude * cos(alpha), where alpha is the angle between vectors.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static double operator *(Vec3 vecA, Vec3 vecB)
        {
            return vecA.m_x * vecB.m_x + vecA.m_y * vecB.m_y + vecA.m_z * vecB.m_z;
        }



        /// <summary>


        /// <summary>
        /// Component-wise division.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static Vec3 operator /(Vec3 vecA, Vec3 vecB)
        {
            vecA.m_x /= vecB.m_x;
            vecA.m_y /= vecB.m_y;
            vecA.m_z /= vecB.m_z;
            return vecA;
        }


        /// <summary>
        /// Divide a vector by a number
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static Vec3 operator /(Vec3 vecA, double a)
        {
            vecA.m_x /= a;
            vecA.m_y /= a;
            vecA.m_z /= a;
            return vecA;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator !=(Vec3 vecA, Vec3 vecB)
        {
            if (vecA.m_x != vecB.m_x && vecA.m_y != vecB.m_y && vecA.m_z != vecB.m_z)
            {
                return true;
            }
            else
                return false;


        }

        public static bool operator ==(Vec3 vecA, Vec3 vecB)
        {
            if (vecA.m_x == vecB.m_x && vecA.m_y == vecB.m_y && vecA.m_z == vecB.m_z)
            {
                return true;
            }
            else
                return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>

        public static bool operator >(Vec3 vecA, Vec3 vecB)
        {
            // a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is larger than b.Z;

            if (vecA.m_x > vecB.m_x || vecA.m_x == vecB.m_x && vecA.m_y > vecB.m_y || vecA.m_x == vecB.m_x && vecA.m_y == vecB.m_y && vecA.m_z > vecB.m_z) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator >=(Vec3 vecA, Vec3 vecB)
        {
            // a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y, or a.X == b.X and a.Y == b.Y and a.Z >= b.Z

            if (vecA.m_x > vecB.m_x || vecA.m_x == vecB.m_x && vecA.m_y > vecB.m_y || vecA.m_x == vecB.m_x && vecA.m_y == vecB.m_y && vecA.m_z >= vecB.m_z) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator <(Vec3 vecA, Vec3 vecB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z

            if (vecA.m_x < vecB.m_x || vecA.m_x == vecB.m_x && vecA.m_y < vecB.m_y || vecA.m_x == vecB.m_x && vecA.m_y == vecB.m_y && vecA.m_z < vecB.m_z) return true;

            else return false;


        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector. 
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator <=(Vec3 vecA, Vec3 vecB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z <= b.Z

            if (vecA.m_x < vecB.m_x || vecA.m_x == vecB.m_x && vecA.m_y < vecB.m_y || vecA.m_x == vecB.m_x && vecA.m_y == vecB.m_y && vecA.m_z <= vecB.m_z) return true;

            else return false;


        }






        #endregion


        #region METHODS



        /// <summary>
        /// computes distance between two vectors
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double DistanceTo(Vec3 other)
        {
            other.m_x -= m_x;
            other.m_y -= m_y;
            other.m_z -= m_z;
            return other.Magnitude;
        }

        /// <summary>
        /// Unitizes the vector in place. A unit vector has length 1 unit. 
        ///An invalid or zero length vector cannot be unitized. in this case will return false
        /// </summary>
        /// <returns></returns>
        public bool Normalize()
        {
            //Jhon Vince, Mathematics for computer graphics
            double length = Magnitude;

            if (length > 0)
            {
                length = 1.0 / Math.Sqrt(length);
                m_x *= length;
                m_y *= length;
                m_z *= length;


                return true;
            }

            else
                return false;


        }

        #endregion

        #region STATIC METHODS
        /// <summary>
        /// Return the cross product of two vectos. The result is a third vector that is at 90 degrees to both
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static Vec3 CrossProduct(Vec3 vecA, Vec3 vecB)
        {
            //Jhon Vince, Mathematics for computer graphics
            // t = VecA X VecB
            // |t| = |VecA| |VecB| Sin(theta)

            // determinant multiplication
            double deltaX = (vecA.m_y * vecB.m_z) - (vecA.m_z * vecB.m_y);
            double deltaY = (vecA.m_z * vecB.m_x) - (vecA.m_x * vecB.m_z);
            double deltaZ = (vecA.m_x * vecB.m_y) - (vecA.m_y * vecB.m_x);
            return new Vec3(deltaX, deltaY, deltaZ);
        }

      

   


        public static Vec3 RandomSphericalDistribution(double x0, double y0, double z0, double radius, Random ran)
        {
            var u = ran.NextDouble();
            var v = ran.NextDouble();
            var theta = 2 * Math.PI * u;
            var phi = Math.Acos(2 * v - 1);
            var x = x0 + (radius * Math.Sin(phi) * Math.Cos(theta));
            var y = y0 + (radius * Math.Sin(phi) * Math.Sin(theta));
            var z = z0 + (radius * Math.Cos(phi));
            return new Vec3(x, y, z);
        }


        ///UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE 
        /// <summary>
        /// Distribute a set of vectors uniformaly in a sphere
        /// UPDATE
        /// </summary>
        /// <param name="numDirections"></param>
        /// <returns></returns>
        /// UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE 

        [Obsolete("This Method needs to be updated to only return one vector.")]
        public static List<Vec3> UniformSphericalDistribution(int numDirections)
        {
            List<Vec3> vecList = new List<Vec3>();

            double inc = Math.PI * (3 - Math.Sqrt(5));
            double off = 2f / numDirections;

            foreach (var k in Enumerable.Range(0, numDirections))
            {
                double y = k * off - 1 + (off / 2);
                double r = Math.Sqrt(1 - y * y);
                double phi = k * inc;
                double x = Math.Cos(phi) * r;
                double z = Math.Sin(phi) * r;
                vecList.Add(new Vec3(x, y, z));

            }

            return vecList;
        }




        /// <summary>
        /// Return a vector at world origin
        /// </summary>
        /// <returns></returns>
        public static Vec3 Up()
        {
            return new Vec3(0, 0, 1);
        }

        /// <summary>
        /// Returns the angle between two vectors. 
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static double VectorAngle(Vec3 vecA, Vec3 vecB, bool radians)
        {
            //Jhon Vince, Mathematics for computer graphics
            if (radians)
            {
                double theta = vecA * vecB / (vecA.Magnitude * vecB.Magnitude);
                double cosTheta = Math.Acos(theta);
                return cosTheta;
            }

            else
            {
                double theta = vecA * vecB / (vecA.Magnitude * vecB.Magnitude);
                double cosTheta = Math.Acos(theta);
                return cosTheta * 180 / Math.PI;
            }
        }





        /// <summary>
        /// Compute the averege of a list of vectors. This will compute the center
        /// point of the vector cloud
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Vec3 VectorialAverege(List<Vec3> data)
        {
            Vec3 averege = Vec3.Zero;

            for (int i = 0; i < data.Count; i++)
            {
                averege = averege + data[i];
            }

            averege = averege / data.Count;

            return averege;
        }



        /// <summary>
        /// Create a random distribution of vectors in a bounding box
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <param name="minZ"></param>
        /// <param name="maxZ"></param>
        /// <returns></returns>
        public static Vec3 VectorRandomDistribution(double minX, double maxX, double minY, double maxY, double minZ, double maxZ, Random ran)
        {
           
            double x = minX + (maxX - minX) * ran.NextDouble();
            double y = minY + (maxY - minY) * ran.NextDouble();
            double z = minZ + (maxZ - minZ) * ran.NextDouble();

            return new Vec3(x, y, z);
        }


        /// <summary>
        /// Crete random 3D vector directions
        /// </summary>
        /// <returns></returns>
        public static Vec3 Vector3dRandom(Random ran)
        {
           
            double phi = 2.0 * Math.PI * ran.NextDouble();
            double theta = Math.Acos(2.0 * ran.NextDouble() - 1.0);

            double x = Math.Sin(theta) * Math.Cos(phi);
            double y = Math.Sin(theta) * Math.Sin(phi);
            double z = Math.Cos(theta);

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Create random 2D vector directions
        /// </summary>
        /// <returns></returns>
        public static Vec3 Vector2dRandom(Random ran)
        {
            
            double angle = 2.0 * Math.PI * ran.NextDouble();

            double x = Math.Cos(angle);
            double y = Math.Sin(angle);

            return new Vec3(x, y, 0.0);
        }






        /// <summary>
        /// Rotate a vector by a specific angle in radians
        /// </summary>
        /// <param name="v"></param>
        /// <param name="radians"></param>
        /// <param name="in2D"></param>
        /// <param name="in3D"></param>
        /// <returns></returns>
        public static Vec3 VectorRotate(Vec3 v, double radians, bool in2D, bool in3D)
        {
            //Jhon Vince, Mathematics for computer graphics
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            if (in3D) return new Vec3(ca * v.m_x - sa * v.m_y, sa * v.m_x + ca * v.m_y, sa * v.m_y + v.m_z * ca);
            else return new Vec3(ca * v.m_x - sa * v.m_y, sa * v.m_x + ca * v.m_y, 0);

        }


        public static Vec3 VectorRotate(ref Vec3 v, double radians, bool in2D, bool in3D)
        {
            //Jhon Vince, Mathematics for computer graphics
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            if (in3D) return new Vec3(ca * v.m_x - sa * v.m_y, sa * v.m_x + ca * v.m_y, sa * v.m_y + v.m_z * ca);
            else return new Vec3(ca * v.m_x - sa * v.m_y, sa * v.m_x + ca * v.m_y, 0);

        }







        #endregion




        #region SYSTEM.OBJECT METHOD OVERRIDING

        public override string ToString()
        {
            //return $"({x}, {y}, {z})";

            var culture = System.Globalization.CultureInfo.InvariantCulture;

            return String.Format("{0},{1},{2}", m_x.ToString(culture), m_y.ToString(culture), m_z.ToString(culture));
        }

        public override bool Equals(object obj)
        {
            Vec3 temp;
            temp = (Vec3)obj;
            if (obj is Vec3 && obj != null)
            {
                if (temp.m_x == this.m_x && temp.m_y == this.m_y && temp.m_z == this.m_z)
                {
                    return true;
                }

                else return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            //return base.GetHashCode();

            return ToString().GetHashCode();
        }

        //bool IEquatable<Vec3>.Equals(Vec3 other)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion


        #region IGH_GOO METHOD OVERRIDING






        //private BoundingBox _bbox = BoundingBox.Unset;









        //public override bool IsValid
        //{
        //    get { return true; }
        //}



        //public override string TypeName
        //{
        //    get { return "Vec3"; }
        //}



        //public override string TypeDescription
        //{
        //    get { return "Vec3"; }
        //}





        //public override IGH_Goo Duplicate()
        //{
        //    return DuplicateGeometry();
        //}



        //public override IGH_GeometricGoo DuplicateGeometry()
        //{
        //    return new Vec3(Value.X, Value.Y, Value.Z);


        //}




        //public override object ScriptVariable()
        //{
        //    return Value;
        //}


        //public override bool CastTo<T>(ref T target)
        //{
        //    if (typeof(T).IsAssignableFrom(typeof(Vec3)))
        //    {
        //        object obj = Value;
        //        target = (T)obj;
        //        return true;
        //    }

        //    if (typeof(T).IsAssignableFrom(typeof(Point3d)))
        //    {
        //        object obj = new GH_Point();
        //        target = (T)obj;
        //        return true;
        //    }

        //    if (typeof(T).IsAssignableFrom(typeof(GH_Point)))
        //    {
        //        object obj = new GH_Point();
        //        target = (T)obj;
        //        return true;
        //    }

        //    if (typeof(T).IsAssignableFrom(typeof(GH_ObjectWrapper)))
        //    {
        //        object obj = new GH_ObjectWrapper(Value);
        //        target = (T)obj;
        //        return true;
        //    }

        //    return false;
        //}



        //public override bool CastFrom(object source)
        //{
        //    if (source is Vec3 vec)
        //    {
        //        Value = vec;
        //        return true;
        //    }

        //    if (source is Point3d m)
        //    {
        //        Value = new Vec3(m.X, m.Y, m.Z);
        //        return true;
        //    }


        //    return false;
        //}





        #endregion

    }
}
