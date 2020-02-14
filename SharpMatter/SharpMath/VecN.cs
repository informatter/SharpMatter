using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpMath
{

    /// <summary>
    /// This class is intended to build an N dimensional vector 
    /// </summary>
    public struct  VecN
    {
        #region FIELDS

        private double[] m_vecN;

        #endregion


        #region CONSTRUCTORS
        /// <summary>
        /// Construct an VecN from an array of values
        /// </summary>
        /// <param name="values"></param>
        public VecN(double[] values)

        {
            m_vecN = values;
        }


        /// <summary>
        /// Construct a VecN from an initial dimension
        /// </summary>
        /// <param name="dimension"></param>
        public VecN(int dimension)
        {

            m_vecN = new double[dimension];
        }

        #endregion







        #region OPERATOR OVERLOADING


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        public static implicit operator string(VecN vector)
        {
            return vector.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NVecA"></param>
        /// <param name="NVecB"></param>
        /// <returns></returns>
        public static VecN operator +(VecN NVecA, VecN NVecB)
        {

            if (NVecA.NVec.Length != NVecB.NVec.Length) throw new ArgumentException("Vectors have to be the same dimensions!");

 
                for (int i = 0; i < NVecA.NVec.Length; i++)
                {
                    NVecA.NVec[i] += NVecB.NVec[i];
                }

         
            return NVecA;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="NVecA"></param>
        /// <param name="NVecB"></param>
        /// <returns></returns>
        public static VecN operator -(VecN NVecA, VecN NVecB)
        {

            if (NVecA.NVec.Length != NVecB.NVec.Length) throw new ArgumentException("Vectors have to be the same dimensions!");

                for (int i = 0; i < NVecA.NVec.Length; i++)
                {
                    NVecA.NVec[i] -= NVecB.NVec[i];
                }

         
            return NVecA;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static VecN operator -(VecN vec)
        {
            for (int i = 0; i < vec.NVec.Length; i++)
            {
                vec.NVec[i] -= vec.NVec[i];
            }
            return vec;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static VecN operator *(VecN vec, double scalar)
        {
            for (int i = 0; i < vec.NVec.Length; i++)
            {
                vec.NVec[i] *= scalar;
            }
            return vec;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static VecN operator *(double scalar, VecN vec)
        {
            for (int i = 0; i < vec.NVec.Length; i++)
            {
                vec.NVec[i] *= scalar;
            }
            return vec;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NVecA"></param>
        /// <param name="NVecB"></param>
        /// <returns></returns>
        public static VecN operator /(VecN NVecA, VecN NVecB)
        {
            if (NVecA.NVec.Length != NVecB.NVec.Length) throw new ArgumentException("Vectors have to be the same dimensions!");

                for (int i = 0; i < NVecA.NVec.Length; i++)
                {
                    NVecA.NVec[i] /= NVecB.NVec[i];
                }

            
            return NVecA;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NVecA"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static VecN operator /(VecN NVecA, double scalar)
        {
            for (int i = 0; i < NVecA.NVec.Length; i++)
            {
                NVecA.NVec[i] /= scalar;
            }

            return NVecA;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NVecA"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static VecN operator /(VecN NVecA, int scalar)
        {
            for (int i = 0; i < NVecA.NVec.Length; i++)
            {
                NVecA.NVec[i] /= scalar;
            }

            return NVecA;
        }


        #endregion



        #region PROPERTIES

        /// <summary>
        /// Compute the Magnitude of an VecN.
        /// Calculates a pythaorean theorem in N dimensions
        /// </summary>
        public double Magnitude
        {
            get { return System.Math.Sqrt(Mag()); }

        }

        /// <summary>
        /// 
        /// </summary>
        public double[] NVec
        {
            get { return m_vecN; }
            set { m_vecN = value; }
        }

        #endregion


        #region METHODS



        /// <summary>
        /// Compute a vector - matrix multiplication. In essence this is similar to a 
        /// vector dot product
        /// </summary>
        /// <returns></returns>
        public VecN MatrixProduct(Matrix  matrix) 
        {
            VecN newVec = new VecN(matrix.Columns);
         
            // if the number of cols in the matrix does not match the vector dim
            if (matrix.Values[0].Length != m_vecN.Length) throw new ArgumentException("Matrix has to have same number of columns as vector!");


            else
            {

                for (int i = 0; i < matrix.Values.Length; i++)
                {
                   
                    List<double> partialR = new List<double>();
                    for (int j = 0; j < matrix.Values[i].Length; j++)
                    {

                        partialR.Add(this.m_vecN[j] * matrix.Values[i][j]);

                    }

                    double r = partialR.Sum();
                    newVec.m_vecN[i] = r;

                }


            }

            return newVec;
        }


        /// <summary>
        /// Compute the Dot Product of a VecN
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double DotProduct(VecN other)
        {

            double sum = 0;

            for (int i = 0; i < other.NVec.Length; i++)
            {
                sum+= other.m_vecN[i] *this.m_vecN[i];
            }

           

            return sum;
        }

        /// <summary>
        /// Compute the distance between 2 N-dimensional vectors
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double DistanceTo(VecN other)
        {

            for (int i = 0; i < other.NVec.Length; i++)
            {

                other.m_vecN[i] -= this.m_vecN[i];

            }

            return other.Magnitude;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private double Mag()
        {
            double r = 0;
            for (int i = 0; i < m_vecN.Length; i++)
            {
                r += m_vecN[i] * m_vecN[i];
            }
            return r;
        }


        /// <summary>
        /// Create a zero vector of the specified dimensions
        /// </summary>
        /// <param name="dimensions"></param>
        /// <returns></returns>
        public static VecN Zero(int dimensions)
        {
            VecN zeroVec = new VecN(dimensions);
            for (int i = 0; i < zeroVec.NVec.Length; i++)
            {
                zeroVec.NVec[i] = 0;
            }

            return zeroVec;
        }


        #endregion  
    }
}
