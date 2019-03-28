using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpData;
using SharpMatter.SharpMath;
using SharpMatter.SharpUtilities;
using Rhino.Geometry;

namespace SharpMatter.SharpField
{
    public class SharpField2D<T> 

    {
        #region FIELDS

        private int m_columns;
        private int m_rows;
        private double m_resolution;
        private Cell<T>[,] m_Field;
        private Cell<T>[,] m_nextField;
        private T[,] m_fieldValues;

        #endregion

        #region CONSTRUCTORS

        public SharpField2D()
        { }

        public SharpField2D(int columns, int rows, double resolution, List<T> valueA, List<T> valueB)
        {
            m_columns = columns;
            m_rows = rows;
            m_resolution = resolution;

            m_Field = new Cell<T>[m_columns, m_rows];
            m_nextField = new Cell<T>[m_columns, m_rows];
            m_fieldValues = new T[m_columns, m_rows];

            T[] tempValueA = valueA.ToArray();
            T[] tempValueB = valueB.ToArray();
            T[,] valueA2D = Utilities.Make2DArray(tempValueA, m_columns, m_rows);
            T[,] valueB2D = Utilities.Make2DArray(tempValueB, m_columns, m_rows);

            /// Initialize Cell values
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_Field[i, j] = new Cell<T>(valueA2D[i, j], valueB2D[i, j], new Vec3(i*m_resolution,j * m_resolution, 0));
                    m_nextField[i, j] = new Cell<T>(valueA2D[i, j], valueB2D[i, j], new Vec3(i * m_resolution, j * m_resolution, 0));
                }
            }

        }

        #endregion

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
            set
            {
                m_Field = value;
            }
        }

        public T [,] FieldValues
        {
            get { return m_fieldValues; }

            set { m_fieldValues = value; }
        }

        public Point3d [,] Grid
        {
            get { return DisplayField(); }
        }

        public Cell<T>[,] NextField
        {
            get { return m_nextField; }
            set
            {
                m_nextField = value;
            }
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Point3d[,] DisplayField()
        {
            Point3d[,] vecs = new Point3d[m_columns, m_rows];

            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    vecs[i, j] = (Point3d)m_Field[i, j].Position;
                }
            }

            return vecs;
        }

        public void ClearValues(T value)
        {
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_fieldValues[i, j] = value;
                   
                }
            }
        }

     

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


        /// <summary>
        /// 
        /// </summary>
        public void Swap()
        {

            Cell<T>[,] temp = m_Field;

            m_Field = m_nextField;

            m_nextField = temp;

        }

        #endregion






    }
}
