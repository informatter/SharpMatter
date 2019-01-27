using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;


namespace SharpMatter.SharpGeometry
{
    /// <summary>
    /// 
    /// Vector class written by Nicholas Rawitscher. This library is currently under development 
    /// 
    /// 
    /// </summary>

    //[Serializable]
    public class Vec3
    {
        // Field data
        private double X;
        private double Y;
        private double Z;

        private Random ran = new Random();

        public Vec3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vec3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

       public void test()
        {
            Point3d b = new Point3d();
            Vec3 temp;

            temp = (Vec3)b;
        }


        #region PROPERTIES

        public double Magnitude
        {
            get { return X * X + Y * Y + Z * Z; }

        }




        #endregion


        #region OPERATOR OVERLOADING

     public static explicit operator Vec3 (Point3d v)
      {
            return (Vec3)v;
      }


        public static implicit operator string(Vec3 vector)
        {
            return vector.ToString();
        }




        public static Vec3 operator +(Vec3 vecA, Vec3 vecB)
        {
            vecA.X += vecB.X;
            vecA.Y += vecB.Y;
            vecA.Z += vecB.Z;
            return vecA;
        }



        public static Vec3 operator -(Vec3 vecA, Vec3 vecB)
        {
            vecA.X -= vecB.X;
            vecA.Y -= vecB.Y;
            vecA.Z -= vecB.Z;
            return vecA;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vec3 operator -(Vec3 vec)
        {
            vec.X = -vec.X;
            vec.Y = -vec.Y;
            vec.Z = -vec.Z;
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
            vector.X *= scalar;
            vector.Y *= scalar;
            vector.Z *= scalar;
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
            vector.X *= scalar;
            vector.Y *= scalar;
            vector.Z *= scalar;
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
            return vecA.X * vecB.X + vecA.Y * vecB.Y + vecA.Z * vecB.Z;
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
            vecA.X /= vecB.X;
            vecA.Y /= vecB.Y;
            vecA.Z /= vecB.Z;
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
            vecA.X /= a;
            vecA.Y /= a;
            vecA.Z /= a;
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
            if (vecA.X != vecB.X && vecA.Y != vecB.Y && vecA.Z != vecB.Z)
            {
                return true;
            }
            else
                return false;


        }

        public static bool operator ==(Vec3 vecA, Vec3 vecB)
        {
            if (vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Z == vecB.Z)
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

            if (vecA.X > vecB.X || vecA.X == vecB.X && vecA.Y > vecB.Y || vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Z > vecB.Z) return true;

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

            if (vecA.X > vecB.X || vecA.X == vecB.X && vecA.Y > vecB.Y || vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Z >= vecB.Z) return true;

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

            if (vecA.X < vecB.X || vecA.X == vecB.X && vecA.Y < vecB.Y || vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Z < vecB.Z) return true;

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

            if (vecA.X < vecB.X || vecA.X == vecB.X && vecA.Y < vecB.Y || vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Z <= vecB.Z) return true;

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
            other.X -= X;
            other.Y -= Y;
            other.Z -= Z;
            return other.Magnitude;
        }

        /// <summary>
        /// Unitizes the vector in place. A unit vector has length 1 unit. 
        ///An invalid or zero length vector cannot be unitized. in this case will return false
        /// </summary>
        /// <returns></returns>
        public bool Normalize()
        {
            double length = Magnitude;

            if (length > 0)
            {
                length = 1.0 / Math.Sqrt(length);
                X *= length;
                Y *= length;
                Z *= length;
                return true;
            }

            else
                return false;


        }

        #endregion

        #region STATIC METHODS
        /// <summary>
        /// Return the cross product of two vectos. The result is a third vector that is at right angles to both
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static Vec3 CrossProduct(Vec3 vecA, Vec3 vecB)
        {
            double deltaX = (vecA.Y * vecB.Z) - (vecA.Z * vecB.Y);
            double deltaY = (vecA.Z * vecB.X) - (vecA.X * vecB.Z);
            double deltaZ = (vecA.X * vecB.Y) - (vecA.Y * vecB.X);
            return new Vec3(deltaX, deltaY, deltaZ);
        }


        /// <summary>
        /// Distribute a set of vectors uniformaly in a sphere
        /// </summary>
        /// <param name="numDirections"></param>
        /// <returns></returns>
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
            Vec3 averege = Vec3.Zero();

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
        public static Vec3 VectorRandomDistribution(double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            Random ran = new Random();
            double x = minX + (maxX - minX) * ran.NextDouble();
            double y = minY + (maxY - minY) * ran.NextDouble();
            double z = minZ + (maxZ - minZ) * ran.NextDouble();

            return new Vec3(x, y, z);
        }


        /// <summary>
        /// Crete random 3D vector directions
        /// </summary>
        /// <returns></returns>
        public static Vec3 Vector3dRandom()
        {
            Random ran = new Random();
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
        public static Vec3 Vector2dRandom()
        {
            Random ran = new Random();
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
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            if (in3D) return new Vec3(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y, sa * v.Y + v.Z * ca);
            else return new Vec3(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y, 0);

        }

        /// <summary>
        /// Return a vector at world origin
        /// </summary>
        /// <returns></returns>
        public static Vec3 Zero()
        {
            return new Vec3(0, 0, 0);
        }


        #endregion




        #region METHOD OVERRIDING

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public override bool Equals(object obj)
        {
            Vec3 temp;
            temp = (Vec3)obj;
            if (obj is Vec3 && obj != null)
            {
                if (temp.X == this.X && temp.Y == this.Y && temp.Z == this.Z)
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
        #endregion

    }
}
