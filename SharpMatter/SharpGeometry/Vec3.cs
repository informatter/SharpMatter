using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using SharpMatter.SharpField;
using System.Collections;

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

        /// <summary>
        /// Determines weather a vector is valid or not
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (m_x == double.NaN || m_y == double.NaN || m_z == double.NaN)
                    return false;

                else if (m_x == double.NaN && m_y == double.NaN && m_z == double.NaN)
                    return false;

                else
                    return true;
            }
        }


        /// <summary>
        /// Returns de magnitude of the vector
        /// </summary>
        public double Magnitude
        {
            get { return Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z); }

        }

        /// <summary>
        /// Returns the squared magnitude of the vector
        /// </summary>
        public double SqrMagnitude
        {
            get { return Math.Pow(Magnitude, 2); }
        }



        /// <summary>
        /// 
        /// </summary>
        public double X  { get => m_x; set => m_x = value; }



        

        public static Vec3 XAxis

        {

            get { return new Vec3(1.0, 0.0, 0.0); }

        }

        public double Y { get => m_y; set => m_y = value; }



        public static Vec3 YAxis

        {

            get { return new Vec3(0.0, 1.0, 0.0); }

        }

        public double Z { get => m_z; set => m_z = value; }


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

            if (m_x > other.m_x)

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
        public static explicit operator Vec3(Point3d v)
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
        /// Cast from Color to Vec3
        /// </summary>
        /// <param name="c"></param>
        public static explicit operator Vec3(Color c)
        {
            return new Vec3(c.R, c.G, c.B);
        }



        public static explicit operator Vec3(GH_Colour c)
        {
            return new Vec3(c.Value.R, c.Value.G, c.Value.B);
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
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double ManhattanDistance(Vec3 other)
        {
            double a = other.m_x -= m_x;
            double b = other.m_y -= m_y;
            double c =  other.m_z -= m_z;

            return Math.Abs(a) + Math.Abs(b) + Math.Abs(c);
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
                m_x *= 1.0 / length;
                m_y *= 1.0 / length;
                m_z *= 1.0 / length;

                return true;
            }

            else
                return false;


        }

        #endregion

        #region STATIC METHODS



        /// <summary>
        /// Find closest point in a point collection.
        /// </summary>
        /// <param name="pointToSearchFrom"></param>
        /// <param name="pointCloud"></param>
        /// <param name="minDist"></param>
        /// <returns></returns>
        public static Vec3 ClosestPoint(Vec3 pointToSearchFrom, List<Vec3> pointCloud, out double minDist )
        {

            if (!pointToSearchFrom.IsValid) throw new ArgumentException(" Point to search from is Invalid!");
            Vec3 closest = Vec3.Zero;     
            minDist = double.MaxValue;

            for (int i = 0; i < pointCloud.Count; i++)
            {
                if (pointCloud[i].IsValid)
                {
                   double dist =  pointToSearchFrom.DistanceTo(pointCloud[i]);

                    if(dist<minDist)
                    {
                        minDist = dist;
                        closest = pointCloud[i];
                        
                    }
                }

                else
                {
                    throw new ArgumentException(" Point cloud at index:" + " " + i.ToString() + " " + "is invalid");
                }
            }


            return closest;
        }


        /// <summary>
        /// Find closest point in a point collection.
        /// </summary>
        /// <param name="pointToSearchFrom"></param>
        /// <param name="pointCloud"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Vec3 ClosestPointParallel(Vec3 pointToSearchFrom, List<Vec3> pointCloud, out double distance)
        {
            if (!pointToSearchFrom.IsValid) throw new ArgumentException(" Point to search from is Invalid!");

            Vec3 closest = Vec3.Zero;
            double minDist = double.MaxValue;

            System.Threading.Tasks.Parallel.For (0, pointCloud.Count, i =>
            {
                if (pointCloud[i].IsValid)
                {
                    double dist = pointToSearchFrom.DistanceTo(pointCloud[i]);

                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = pointCloud[i];
                    }

                }

                else
                {
                    throw new ArgumentException(" Point cloud at index:" + " " + i.ToString() + " " + "is invalid");
                }
            });

            distance = minDist;
            return closest;
        }





        /// <summary>
        /// Convert a collection of colours to a collection of vectors
        /// </summary>
        /// <param name="colours"></param>
        /// <returns></returns>
        public List<Vec3> ColoursToVectors(List<Color> colours)
        {

            List<Vec3> colorvecs = new List<Vec3>();

            for (int i = 0; i < colours.Count; i++)
            {
                colorvecs.Add(new Vec3(colours[i].R, colours[i].G, colours[i].B));
            }


            return colorvecs;

        }

        public static Vec3 ColourToVector(Color color)
        {
            return new Vec3(color.R, color.G, color.B);
        }
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="minTargetDomain"></param>
        /// <param name="maxTargetDomain"></param>
        /// <returns></returns>
        public static Vec3 Constrain(Vec3 vec, double minTargetDomain, double maxTargetDomain)
        {
            double x = vec.m_x;
            double y = vec.m_y;
            double z = vec.m_z;

            double newX = SharpMath.SharpMath.Constrain(x, minTargetDomain, maxTargetDomain);
            double newY = SharpMath.SharpMath.Constrain(y, minTargetDomain, maxTargetDomain);
            double newZ = SharpMath.SharpMath.Constrain(z, minTargetDomain, maxTargetDomain);

            return new Vec3(newX, newY, newZ);

        }


        public static Vec3[] ConstrainCollectionOfVectors(List<Vec3> vecs, double minTargetDomain, double maxTargetDomain)
        {
            Vec3[] remapedVecs = new Vec3[vecs.Count];

            for (int i = 0; i < vecs.Count; i++)
            {
                double x = vecs[i].m_x;
                double y = vecs[i].m_y;
                double z = vecs[i].m_z;

                double newX = SharpMath.SharpMath.Constrain(x, minTargetDomain, maxTargetDomain);
                double newY = SharpMath.SharpMath.Constrain(y, minTargetDomain, maxTargetDomain);
                double newZ = SharpMath.SharpMath.Constrain(z, minTargetDomain, maxTargetDomain);

                remapedVecs[i] = new Vec3(newX, newY, newZ);

            }

            return remapedVecs;

        }



       
        /// <summary>
        /// Return the dot product of two vectors
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static double DotProduct(Vec3 vecA, Vec3 vecB)
        {

            return vecA.X + vecB.X * vecA.Y + vecB.Y * vecA.Z + vecB.Z;

        }


        /// <summary>
        /// Project VecA on to VecB
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="VecB"></param>
        /// <returns></returns>
        public static Vec3 Project(Vec3 vecA, Vec3 VecB)
        {
           
            return DotProduct(vecA, VecB) / DotProduct(VecB, VecB) * VecB;

            // also calculated as: DotProduct(vecA, VecB) /  VecB.SqrMagnitude * VecB;
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="radius"></param>
        /// <param name="ran"></param>
        /// <returns></returns>
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
        public static Vec3 VectorialAverege(List<Vec3> data, bool round)
        {
            Vec3 tempAverege = Vec3.Zero;
            Vec3 averege = Vec3.Zero;

            for (int i = 0; i < data.Count; i++)
            {
                tempAverege += data[i];
            }

            tempAverege /= data.Count;
            if (round)
            {
                averege = VectorRound(tempAverege);
            }

            if (round == false)
            {
                averege = tempAverege;
            }


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



        /// <summary>
        /// 
        /// Return a new Vector with its components rounded to the nearest integer
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vec3 VectorRound(Vec3 vec)
        {
            return new Vec3(Math.Round(vec.X), Math.Round(vec.Y), Math.Round(vec.Z));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
        public static Color [] Vec3ToColor(List<Vec3> vectors)
        {
            Color[] col = new Color[vectors.Count];
            for (int i = 0; i < vectors.Count; i++)
            {
                col[i] = Color.FromArgb((int)SharpMath.SharpMath.Constrain(vectors[i].X, 0, 255),
                    (int)SharpMath.SharpMath.Constrain(vectors[i].Y, 0, 255), (int)SharpMath.SharpMath.Constrain(vectors[i].Z, 0, 255));

            }
            return col;
        }





        #endregion




        #region SYSTEM.OBJECT METHOD OVERRIDING

        public override string ToString()
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            return $"({m_x.ToString(culture)}, {m_y.ToString(culture)}, {m_z.ToString(culture)})";



            //return String.Format("{0},{1},{2}", m_x.ToString(culture), m_y.ToString(culture), m_z.ToString(culture));
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




    }
}
