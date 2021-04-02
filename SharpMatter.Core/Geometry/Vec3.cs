using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace SharpMatter.Core.Geometry
{
    /// <summary>
    /// Represents the three components of a vector in three-dimensional space.
    /// </summary>
    public readonly struct Vec3 : IEquatable<Vec3>, IComparable<Vec3>
    {
        private const double ZeroTolerance = 0.0001;

        /// <summary>
        /// Gets the vectors X component.
        /// </summary>
        public double X { get;}

        /// <summary>
        /// Gets  the vectors Y component.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Gets the vectors Z component.
        /// </summary>
        public double Z { get;}

        /// <summary>
        /// Constructs an invalid vector
        /// </summary>
        public static Vec3 Invalid => new Vec3(double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Returns de magnitude of the vector
        /// </summary>
        public double Magnitude => System.Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);

        /// <summary>
        /// Returns the squared magnitude of the vector
        /// </summary>
        public double SqrMagnitude => System.Math.Pow(this.Magnitude, 2);


        /// <summary>
        /// Creates a X axis unit vector
        /// </summary>
        public static Vec3 XAxis => new Vec3(1, 0, 0);

        /// <summary>
        /// Creates a Y axis unit vector
        /// </summary>
        public static Vec3 YAxis => new Vec3(0, 1, 0);

        /// <summary>
        /// Creates a Z axis unit vector
        /// </summary>
        public static Vec3 ZAxis => new Vec3(0, 0, 1);

        /// <summary>
        /// Creates a vector at cartisian origin
        /// </summary>
        public static Vec3 Zero => new Vec3(0.0, 0.0, 0.0);

        /// <summary>
        /// Determines weather a vector is valid or not
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (double.IsNaN(this.X) || double.IsNaN(this.Y) || double.IsNaN(this.Z))
                    return false;

                return !double.IsNaN(this.X) || !double.IsNaN(this.Y) || !double.IsNaN(this.Z);
            }
        }



        public Vec3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        

        #region IMPLICIT INTERFACE IMPLEMENTATION

        /// <summary>
        /// Compares a Vec3 with another Vec3
        /// </summary>
        /// <param name="other">
        /// other Vector to compare with
        /// </param>
        /// <returns></returns>
        public int CompareTo(Vec3 other)

        {
            if (this.X < other.X)
                return -1;

            if (this.X > other.X)
                return 1;

            if (this.Y < other.Y)
                return -1;

            if (this.Y > other.Y)
                return 1;

            if (this.Z < other.Z)
                return -1;

            if (this.Z > other.Z)
                return 1;

            return 0;
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
                switch (index)
                {
                    case 0: return this.X;
                    case 1: return this.Y;
                    case 2: return this.Z;
                    default: throw new IndexOutOfRangeException(nameof(index));
                }
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
            return new Point3d(v.X, v.Y, v.Z);
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
            double x = vecA.X + vecB.X;
            double y = vecA.Y + vecB.Y;
            double z = vecA.Z + vecB.Z;

            return new Vec3(x, y, z);
        }

        public static Vec3 operator -(Vec3 vecA, Vec3 vecB)
        {
            double x = vecA.X - vecB.X;
            double y = vecA.Y - vecB.Y;
            double z = vecA.Z - vecB.Z;

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vec3 operator -(Vec3 vec)
        {
            double x = -vec.X;
            double y = -vec.Y;
            double z = -vec.Z;

            return new Vec3(x,y,z);
        }

        /// <summary>
        /// Scale a vector by a value.
        /// </summary>
        public static Vec3 operator *(Vec3 vec, double scalar)
        {
            double x = vec.X * scalar;
            double y = vec.Y * scalar;
            double z = vec.Z * scalar;

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Scale a vector by a value.
        /// </summary>
        public static Vec3 operator *(double scalar, Vec3 vec)
        {
            double x = vec.X * scalar;
            double y = vec.Y * scalar;
            double z = vec.Z * scalar;

            return new Vec3(x, y, z);
        }


        /// <summary>
        /// Multiplies two vectors together, returning the dot product
        /// This value equals vecA.Magnitude * vecB.Magnitude * cos(alpha), where alpha is the angle between
        /// vectors.
        /// </summary>
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

            double x = vecA.X / vecB.X;
            double y = vecA.Y / vecB.Y;
            double z = vecA.Z / vecB.Z;

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Divide a vector by a scalar
        /// </summary>
        public static Vec3 operator /(Vec3 vecA, double scalar)
        {
            double x = vecA.X / scalar;
            double y = vecA.Y / scalar;
            double z = vecA.Z / scalar;

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Determines if the values of <paramref name="vecA"/>
        /// are equal to the values of <paramref name="vecB/>.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator !=(Vec3 vecA, Vec3 vecB)
        {
            return System.Math.Abs(vecA.X - vecB.X) > ZeroTolerance &&
                   System.Math.Abs(vecA.Y - vecB.Y) > ZeroTolerance &&
                   System.Math.Abs(vecA.Z - vecB.Z) > ZeroTolerance;
        }

        public static bool operator ==(Vec3 vecA, Vec3 vecB)
        {
            return System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance &&
                   System.Math.Abs(vecA.Y - vecB.Y) < ZeroTolerance &&
                   System.Math.Abs(vecA.Z - vecB.Z) < ZeroTolerance;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        public static bool operator >(Vec3 vecA, Vec3 vecB)
        {
            // a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is larger than b.Z;

            return vecA.X > vecB.X || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance &&
                vecA.Y > vecB.Y || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance &&
                System.Math.Abs(vecA.Y - vecB.Y) < ZeroTolerance && vecA.Z > vecB.Z;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator >=(Vec3 vecA, Vec3 vecB)
        {
            // a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y, or a.X == b.X and a.Y == b.Y and a.Z >= b.Z

            return vecA.X > vecB.X || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance && 
                vecA.Y > vecB.Y || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance &&
                System.Math.Abs(vecA.Y - vecB.Y) < ZeroTolerance && vecA.Z >= vecB.Z;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator <(Vec3 vecA, Vec3 vecB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z

            return vecA.X < vecB.X || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance && 
                vecA.Y < vecB.Y || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance &&
                System.Math.Abs(vecA.Y - vecB.Y) < ZeroTolerance && vecA.Z < vecB.Z;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the
        /// second vector.
        /// Components evaluation priority is first X, then Y, then Z.
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB"></param>
        /// <returns></returns>
        public static bool operator <=(Vec3 vecA, Vec3 vecB)
        {
            // a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y, or a.X == b.X and a.Y == b.Y and a.Z <= b.Z

            return vecA.X < vecB.X || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance && 
                vecA.Y < vecB.Y || System.Math.Abs(vecA.X - vecB.X) < ZeroTolerance &&
                System.Math.Abs(vecA.Y - vecB.Y) < ZeroTolerance && vecA.Z <= vecB.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec3 vec)
                return this.Equals(vec);

            return false;
        }

        public bool Equals(Vec3 other)
        {
            return this.X.Equals(other.X) && this.Y.Equals(other.Y) && this.Z.Equals(other.Z);
        }

        public override int GetHashCode()
        {
            return (this.X.GetHashCode() * 9) ^ (this.Y.GetHashCode() * 23) ^ (this.Z.GetHashCode() * 51);
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
            double x = other.X - this.X;
            double y = other.Y - this.Y;
            double z = other.Z - this.Z;

            return new Vec3(x, y, z).Magnitude;
        }

        /// <summary>
        /// Computes the Manhattan distance between two vectors
        /// </summary>
        public double ManhattanDistance(Vec3 other)
        {
            double x = other.X - this.X;
            double y = other.Y - this.Y;
            double z = other.Z - this.Z;

            return System.Math.Abs(x) + System.Math.Abs(y) + System.Math.Abs(z);
        }

        /// <summary>
        /// Returns a unitized <see cref="Vec3"/> version of
        /// this <see cref="Vec3"/>. An invalid or zero length vector cannot be unitized
        /// in that case a <see cref="Vec3.Invalid"/> will be returned.
        /// </summary>
        public Vec3 Normalize()
        {
            //Jhon Vince, Mathematics for computer graphics
            double length = this.Magnitude;

            if (length > 0)
            {
                double x = this.Y * 1/length;
                double y = this.Y * 1 / length;
                double z = this.Y * 1 / length;

                return new Vec3(x,y,z);
            }

            return Vec3.Invalid;
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
        public static Vec3 ClosestPoint(Vec3 pointToSearchFrom, List<Vec3> pointCloud, out double minDist)
        {
            if (!pointToSearchFrom.IsValid) throw new ArgumentException(" Point to search from is Invalid!");

            Vec3 closest = Zero;
            minDist = double.MaxValue;

            for (int i = 0; i < pointCloud.Count; i++)
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
                    throw new ArgumentException(" Point cloud at index:" + " " + i + " " + "is invalid");

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

            Vec3 closest = Zero;
            double minDist = double.MaxValue;

            Parallel.For(0, pointCloud.Count, i =>
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
                    throw new ArgumentException(" Point cloud at index:" + " " + i + " " + "is invalid");
            });

            distance = minDist;
            return closest;
        }

        /// <summary>
        /// Convert a collection of colours to a collection of vectors
        /// </summary>
        /// <param name="colours"></param>
        /// <returns></returns>
        public IList<Vec3> ColoursToVectors(IEnumerable<Color> colours)
        {
            var colorvecs = new List<Vec3>();

            foreach (var color in colours)
                colorvecs.Add(new Vec3(color.R, color.G, color.B));

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
            double deltaX = vecA.Y * vecB.Z - vecA.Z * vecB.Y;
            double deltaY = vecA.Z * vecB.X - vecA.X * vecB.Z;
            double deltaZ = vecA.X * vecB.Y - vecA.Y * vecB.X;
            return new Vec3(deltaX, deltaY, deltaZ);
        }

        /// <summary>
        /// Constain a vector to a given domain
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="minTargetDomain"></param>
        /// <param name="maxTargetDomain"></param>
        /// <returns></returns>
        public static Vec3 Constrain(Vec3 vec, double minTargetDomain, double maxTargetDomain)
        {
            double x = vec.X;
            double y = vec.Y;
            double z = vec.Z;

            double newX = SharpMath.SharpMath.Constrain(x, minTargetDomain, maxTargetDomain);
            double newY = SharpMath.SharpMath.Constrain(y, minTargetDomain, maxTargetDomain);
            double newZ = SharpMath.SharpMath.Constrain(z, minTargetDomain, maxTargetDomain);

            return new Vec3(newX, newY, newZ);
        }

        /// <summary>
        /// Constrain a collection of vectors to a given domain
        /// </summary>
        /// <param name="vecs"></param>
        /// <param name="minTargetDomain"></param>
        /// <param name="maxTargetDomain"></param>
        /// <returns></returns>
        public static Vec3[] ConstrainCollectionOfVectors(
            List<Vec3> vecs,
            double minTargetDomain,
            double maxTargetDomain)
        {
            Vec3[] remapedVecs = new Vec3[vecs.Count];

            for (int i = 0; i < vecs.Count; i++)
            {
                double x = vecs[i].X;
                double y = vecs[i].Y;
                double z = vecs[i].Z;

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
            return vecA.X * vecB.X + vecA.Y * vecB.Y + vecA.Z * vecB.Z;
        }

        /// <summary>
        /// Find closest point in a point collection.
        /// </summary>
        /// <param name="vecToSearchFrom"></param>
        /// <param name="vectors"></param>
        /// <param name="maxDist"></param>
        /// <returns></returns>
        public static Vec3 FarthestPoint(Vec3 vecToSearchFrom, List<Vec3> vectors, out double maxDist)
        {
            if (!vecToSearchFrom.IsValid) throw new ArgumentException(" Point to search from is Invalid!");

            Vec3 closest = Zero;
            maxDist = double.MaxValue;

            for (int i = 0; i < vectors.Count; i++)
                if (vectors[i].IsValid)
                {
                    double dist = vecToSearchFrom.DistanceTo(vectors[i]);

                    if (dist > maxDist)
                    {
                        maxDist = dist;
                        closest = vectors[i];
                    }
                }

                else
                    throw new Exception(" Point cloud at index:" + " " + i + " " + "is invalid");

            return closest;
        }

        /// <summary>
        /// Find farthest vector in a vector collection given a search vector.
        /// </summary>
        /// <param name="vecToSearchFrom"></param>
        /// <param name="vectors"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Vec3 FarthestPointParallel(Vec3 vecToSearchFrom, List<Vec3> vectors, out double distance)
        {
            if (!vecToSearchFrom.IsValid) throw new ArgumentException(" Point to search from is Invalid!");

            Vec3 closest = Zero;
            double maxDist = double.MinValue;

            Parallel.For(0, vectors.Count, i =>
            {
                if (vectors[i].IsValid)
                {
                    double dist = vecToSearchFrom.DistanceTo(vectors[i]);

                    if (dist > maxDist)
                    {
                        maxDist = dist;
                        closest = vectors[i];
                    }
                }

                else
                    throw new ArgumentException(" Point cloud at index:" + " " + i + " " + "is invalid");
            });

            distance = maxDist;
            return closest;
        }

        /// <remarks>
        /// also calculated as: DotProduct(vecA, VecB) /  VecB.SqrMagnitude * VecB;
        /// </remarks>
        /// <summary>
        /// Project VecA on to VecB
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="VecB"></param>
        /// <returns></returns>
        public static Vec3 Project(Vec3 vecA, Vec3 VecB)
        {
            return DotProduct(vecA, VecB) / DotProduct(VecB, VecB) * VecB;
        }

        /// <summary>
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="radius"></param>
        /// <param name="ran"></param>
        /// <returns></returns>
        public static Vec3 RandomSphericalDistribution(
            double x0,
            double y0,
            double z0,
            double radius,
            Random ran)
        {
            var u = ran.NextDouble();
            var v = ran.NextDouble();
            var theta = 2 * System.Math.PI * u;
            var phi = System.Math.Acos(2 * v - 1);
            var x = x0 + radius * System.Math.Sin(phi) * System.Math.Cos(theta);
            var y = y0 + radius * System.Math.Sin(phi) * System.Math.Sin(theta);
            var z = z0 + radius * System.Math.Cos(phi);
            return new Vec3(x, y, z);
        }

        /// <summary>
        /// This method sorts the vertices of a 2D polygon in counterclockwise order.
        /// </summary>
        /// <param name="vertices">
        /// A collection of vertices to sort
        /// </param>
        /// <returns>
        /// A sorted collection of vertices
        /// </returns>
        public static Vec3[] SortPolygonVertices(IList<Vec3> vertices)
        {
            Vec3 cntr = Average(vertices);

            double[] _angles = new double[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
                _angles[i] = System.Math.Atan2(vertices[i].X - cntr.X, vertices[i].Y - cntr.Y);

            Vec3[] orderedData = vertices.OrderBy(item => _angles).ToArray();

            return orderedData;
        }

        /// UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE
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

            double inc = System.Math.PI * (3 - System.Math.Sqrt(5));
            double off = 2f / numDirections;

            foreach (var k in Enumerable.Range(0, numDirections))
            {
                double y = k * off - 1 + off / 2;
                double r = System.Math.Sqrt(1 - y * y);
                double phi = k * inc;
                double x = System.Math.Cos(phi) * r;
                double z = System.Math.Sin(phi) * r;
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
                double cosTheta = System.Math.Acos(theta);
                return cosTheta;
            }

            else
            {
                double theta = vecA * vecB / (vecA.Magnitude * vecB.Magnitude);
                double cosTheta = System.Math.Acos(theta);
                return cosTheta * 180 / System.Math.PI;
            }
        }

        /// <summary>
        /// Compute the averege of a list of vectors. This will compute the center
        /// point of the vector cloud
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Vec3 Average(IList<Vec3> vectors)
        {
            Vec3 tempAverege = Zero;

            Vec3 averege = Zero;

            foreach (var vector in vectors)
                averege += vector;

            averege /= vectors.Count;

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
        public static Vec3 VectorRandomDistribution(
            double minX,
            double maxX,
            double minY,
            double maxY,
            double minZ,
            double maxZ,
            Random ran)
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
            double phi = 2.0 * System.Math.PI * ran.NextDouble();
            double theta = System.Math.Acos(2.0 * ran.NextDouble() - 1.0);

            double x = System.Math.Sin(theta) * System.Math.Cos(phi);
            double y = System.Math.Sin(theta) * System.Math.Sin(phi);
            double z = System.Math.Cos(theta);

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Create random 2D vector directions
        /// </summary>
        /// <returns></returns>
        public static Vec3 Vector2dRandom(Random ran)
        {
            double angle = 2.0 * System.Math.PI * ran.NextDouble();

            double x = System.Math.Cos(angle);
            double y = System.Math.Sin(angle);

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
            var ca = System.Math.Cos(radians);
            var sa = System.Math.Sin(radians);
            if (in3D) return new Vec3(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y, sa * v.Y + v.Z * ca);

            return new Vec3(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y, 0);
        }

        /// <summary>
        /// Return a new Vector with its components rounded to the nearest integer
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vec3 VectorRound(Vec3 vec)
        {
            return new Vec3(System.Math.Round(vec.X), System.Math.Round(vec.Y), System.Math.Round(vec.Z));
        }

        /// <summary>
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
        public static Color[] Vec3ToColor(List<Vec3> vectors)
        {
            Color[] col = new Color[vectors.Count];
            for (int i = 0; i < vectors.Count; i++)
                col[i] = Color.FromArgb((int) SharpMath.SharpMath.Constrain(vectors[i].X, 0, 255),
                    (int) SharpMath.SharpMath.Constrain(vectors[i].Y, 0, 255),
                    (int) SharpMath.SharpMath.Constrain(vectors[i].Z, 0, 255));
            return col;
        }

        #endregion

        #region SYSTEM.OBJECT METHOD OVERRIDING

        public override string ToString()
        {
            var culture = CultureInfo.InvariantCulture;
            return $"({this.X.ToString(culture)}, {this.Y.ToString(culture)}, {this.Z.ToString(culture)})";
        }

        #endregion
    }
}