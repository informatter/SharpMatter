using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpMatter.SharpMath
{
   
        public class Matrix
        {
            private int columns;
            private int rows;
            public double[,] values;


            public Matrix(int columns, int rows)
            {
                this.columns = columns;
                this.rows = rows;

            }


            public int Columns
            {
                get { return columns; }

                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("value has to be larger than 0!");
                    }

                    else columns = value;

                }
            }


            public int Rows
            {
                get { return rows; }

                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("value has to be larger than 0!");
                    }

                    else rows = value;

                }
            }

            #region STATIC METHODS

         public static void Display(Matrix a)
        {
            string path = @"C:\Users\nicol\source\repos\SharpMatter";
            string name = "\test." + "txt";
            string fullPath = System.IO.Path.Combine(path, name);

            StreamWriter sr = new StreamWriter(fullPath);

            for (int i = 0; i < a.columns; i++)
            {
                for (int j = 0; j < a.rows; j++)
                {

                    string OutPut = a.values[i,j].ToString();


                    sr.WriteLine(OutPut);

                }
            }
        }



            public static void InitializeValues(Matrix a, double[,] data)
            {
         

            for (int i = 0; i < a.columns; i++)
                {
                    for (int j = 0; j < a.rows; j++)
                    {
                        a.values[i, j] = data[i, j];
                    }
                }

            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static Matrix Rotate(Matrix a, Matrix b)
            {
                return null;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static Matrix Scale(Matrix a, Matrix b)
            {
                return null;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static Matrix Translate(Matrix a, Matrix b)
            {
                return null;
            }

            #endregion


        
    }
}
