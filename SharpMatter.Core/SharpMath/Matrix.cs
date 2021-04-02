using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SharpMatter.SharpExtensions;
namespace SharpMatter.SharpMath
{

    /// <summary>
    /// Matrix class. Currently under development
    /// </summary>
    /// <typeparam name="T"></typeparam>
   
        [Serializable]
        public class Matrix 
        {
     
            private int m_columns;
            private int m_rows;
            private double [][] m_values;


            /// <summary>
            /// Initialize a N X N Matrix with default values set to zero
            /// </summary>
            /// <param name="columns"></param>
            /// <param name="rows"></param>
            public Matrix(int columns, int rows)
            {
                this.m_columns = columns;
                this.m_rows = rows;

                m_values = new double [m_rows][];

                double[,] temp = new double [m_columns,m_rows];

                for (int i = 0; i < rows; i++)
                {

                    for (int j = 0; j< columns; j++)
                    {
                        temp[i, j] = 0;
                    }
    
                }

                m_values = temp.ToJaggedArray(m_columns, rows);


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


            public double[][] Values
            {
                get { return m_values; }

            }


        #region METHODS


            public void Create()
            {

            }

        #endregion

        #region STATIC METHODS

            public void DisplayToTextFile(string path, string name)
            {
                m_values.JaggedArrayToTxtFile(path, name);
            }


            public void DisplayToConsoleWindow()
            {
                m_values.JaggedArrayToConsoleWindow();
            }



            public  void InitializeValues(double [][] data)
            {

                

                if (data.Length!= m_rows && data[0].Length != m_columns)
                {
                    throw new ArgumentException("Input  has to have the same dimensions as the current matrix");
                }

                else
                {

                    for (int i = 0; i < m_values.Length; i++)
                    {
                        for (int j = 0; j < m_values[i].Length; j++)
                        {
                            m_values[i][j] = data[i][j];
                        }
                    }
                     
                }

            }

      
        #endregion



    }
}
