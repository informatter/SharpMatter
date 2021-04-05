using System;

namespace SharpMatter.Core.Math
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

        public double[][] Values { get; }

        /// <summary>
        /// Initialize a N X N Matrix with default values set to zero
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public Matrix(int columns, int rows)
        {
            m_columns = columns;
            m_rows = rows;

            this.Values = new double [m_rows][];

            double[,] temp = new double [m_columns, m_rows];

            for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                temp[i, j] = 0;

            throw new NotImplementedException();

            //this.Values = temp.ToJaggedArray(m_columns, rows);
        }

        public int Columns
        {
            get => m_columns;

            set
            {
                if (value <= 0)
                    throw new Exception("value has to be larger than 0!");

                m_columns = value;
            }
        }

        public int Rows
        {
            get => m_rows;

            set
            {
                if (value <= 0)
                    throw new Exception("value has to be larger than 0!");

                m_rows = value;
            }
        }


        #region METHODS

        public void Create() { }

        #endregion

        #region STATIC METHODS

        public void DisplayToTextFile(string path, string name)
        {
            //this.Values.JaggedArrayToTxtFile(path, name);

            throw new NotImplementedException();
        }

        public void DisplayToConsoleWindow()
        {
            //this.Values.JaggedArrayToConsoleWindow();

            throw new NotImplementedException();
        }

        public void InitializeValues(double[][] data)
        {
            if (data.Length != m_rows && data[0].Length != m_columns)
                throw new ArgumentException("Input  has to have the same dimensions as the current matrix");

            for (int i = 0; i < this.Values.Length; i++)
            for (int j = 0; j < this.Values[i].Length; j++)
                this.Values[i][j] = data[i][j];
        }

        #endregion
    }
}