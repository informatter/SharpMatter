using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpData;
using SharpMatter.SharpMath;

namespace SharpMatter.SharpField
{
    public class Field2D<T> where T: struct

    {
        #region FIELDS

        private int m_columns;
        private int m_rows;
        private double m_resolution;
        private Cell<T>[,] m_Field;
        private Cell<T>[,] m_nextField;

        #endregion


        public Field2D(int columns, int rows, double resolution)
        {
            m_columns = columns;
            m_rows = rows;

            m_Field = new Cell<T>[columns, rows];

        }


        #region PROPERTIES

        public int Columns
        {
            get { return m_columns; }
            set
            {
                if (m_columns <= 0) throw new ArgumentException("Number of columns must be greater than zero!");
                else m_columns = value;
            }
        }

      


        public Cell<T>[,] Field
        {
            get { return m_Field; }
        }


        public double Resolution
        {
            get { return m_resolution; }
            set
            {
                if (m_resolution <= 0) throw new ArgumentException("Resolution be greater than zero!");
                else m_resolution = value;
            }
        }

        public int Rows
        {
            get { return m_rows; }
            set
            {
                if (m_rows <= 0) throw new ArgumentException("Number of rows be greater than zero!");
                else m_rows = value;
            }
        }


        #endregion

        #region METHODS
        public Vec3[,] DisplayField()
        {
            Vec3[,] vecs = new Vec3[m_columns, m_rows];
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    vecs[i, j] = new Vec3(i*m_resolution, j*m_resolution, 0);
                }
            }

            return vecs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T [,] DisplayData()
        {
             T[,] data = new T [m_columns, m_rows];
           
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    data[i, j] = m_Field[i, j].m_scalarValue;

                   
                }
            }
            return data;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <typeparam name="U"></typeparam>
       /// <param name="data"></param>
        public void InitializeFieldExistingData(T [,] data)
        {
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_Field[i, j].m_scalarValue = data[i,j];
                }
            }

        }


        public void InitializeFieldRandomData(T[,] randomdata) 
        {
          
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    
                    m_Field[i, j].m_scalarValue = randomdata[i,j];

               
                }
            }

        }


        #endregion





        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookup"></param>
        /// <returns></returns>
        public Cell<T> LookupValue(Vec3 lookup)
        {

           
            int column = (int)(SharpMath.SharpMath.Constrain(lookup.X / m_resolution, 0, m_columns - 1));
            int row = (int)(SharpMath.SharpMath.Constrain(lookup.Z / m_resolution, 0, m_rows - 1));

            return m_Field[column, row];
        }








    }
}
