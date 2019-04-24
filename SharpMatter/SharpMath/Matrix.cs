using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpMatter.SharpMath
{

    /// <summary>
    /// Matrix class. Currently under development
    /// </summary>
    /// <typeparam name="T"></typeparam>
   
        [Serializable]
        public class Matrix<T> where T: struct
        {
     
            private int m_columns;
            private int m_rows;
            public T[,] values;


            public Matrix(int columns, int rows)
            {
                this.m_columns = columns;
                this.m_rows = rows;

                values = new T[m_columns, m_rows];


            }


            public int Columns
            {
                get { return m_columns; }

                set
                {
                    if (value <= 0)
                    {
                    
                        throw new Exception("value has to be larger than 0!");
                       
                    }

                    else m_columns = value;

                }
            }


            public int Rows
            {
                get { return m_rows; }

                set
                {
                    if (value <= 0)
                    {
                        throw new Exception("value has to be larger than 0!");
                    }

                    else m_rows = value;

                }
            }

            #region STATIC METHODS

         public  void DisplayToTextFile()
        {
            string path = @"C:\Users\nicol\source\repos\SharpMatter";
            string name = "\test." + "txt";
            string fullPath = System.IO.Path.Combine(path, name);

            StreamWriter sr = new StreamWriter(fullPath);

            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {

                    string OutPut = values[i,j].ToString();


                    sr.WriteLine(OutPut);

                }
            }
        }


        public void DisplayToConsoleWindow()
        {
            for (int i = 0; i < m_columns; i++)
            {

                for (int j = 0; j < m_rows; j++)
                {

                    Console.Write(values[i, j] + "\t" );
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }



            public  void InitializeValues( T[,] data)
            {

            if (data.GetLength(0) != m_columns && data.GetLength(1) != m_rows)
            {
                throw new ArgumentException("Input 2D array has to have the same dimensions as the current matrix");
            }

            else
            {

                for (int i = 0; i < m_columns; i++)
                {
                    for (int j = 0; j < m_rows; j++)
                    {
                        values[i, j] = data[i, j];
                    }
                }
            }

            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static Matrix<T> Rotate(Matrix<T> a, Matrix<T> b)
            {
                return null;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static Matrix<T> Scale(Matrix<T> a, Matrix<T> b)
            {
                return null;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static Matrix<T> Translate(Matrix<T> a, Matrix<T> b)
            {
                return null;
            }

            #endregion


        
    }
}
