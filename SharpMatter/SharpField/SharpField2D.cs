﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpData;
using SharpMatter.SharpMath;
using SharpMatter.SharpUtilities;
using Rhino.Geometry;

using SharpMatter.SharpPhysics;

using SharpMatter.SharpField.Interfaces;

namespace SharpMatter.SharpField
{
    public enum SharpField2DType { ReactionDiffusion, Physarium, Generic }
    public enum CellBoundaryDisplay { Yes, No}
    public enum BoundaryType { Wrap, Rebound, SoftRebound}
    public class SharpField2D<T> : IField2D<T>
        where T: struct
       


    {
        #region FIELDS

        private int m_columns;
        private int m_rows;
        private double m_resolution;
        private Cell<T>[,] m_Field;
        private Cell<T>[,] m_nextField;
        private T[,] m_fieldValues;

        private bool[,] m_fieldStates;
        private int [,] m_agentCountPerCell;

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
            m_agentCountPerCell = new int[m_columns, m_rows];
            m_fieldStates = new bool[m_columns, m_rows]; 


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


        public SharpField2D(int columns, int rows, double resolution, List<T> valueA, List<bool> occupied)
        {
            m_columns = columns;
            m_rows = rows;
            m_resolution = resolution;

            m_Field = new Cell<T>[m_columns, m_rows];
            m_nextField = new Cell<T>[m_columns, m_rows];
            m_fieldValues = new T[m_columns, m_rows];
            m_fieldStates = new bool[m_columns, m_rows];
            m_agentCountPerCell = new int[m_columns, m_rows];

            T[] tempValueA = valueA.ToArray();
            bool[] occupiedTemp = occupied.ToArray();
          
            T[,] valueA2D = Utilities.Make2DArray(tempValueA, m_columns, m_rows);
            bool [,] occupied2D = Utilities.Make2DArray(occupiedTemp, m_columns, m_rows);


            /// Initialize Cell values
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_Field[i, j] = new Cell<T>(valueA2D[i, j], new Vec3(i * m_resolution, j * m_resolution, 0), occupied2D[i, j], m_resolution, m_columns, m_rows);
                    m_nextField[i, j] = new Cell<T>(valueA2D[i, j], new Vec3(i * m_resolution, j * m_resolution, 0), occupied2D[i, j], m_resolution, m_columns, m_rows);
                }
            }

        }




        public SharpField2D(int columns, int rows, double resolution, List<T> valueA)
        {
            m_columns = columns;
            m_rows = rows;
            m_resolution = resolution;

            m_Field = new Cell<T>[m_columns, m_rows];
            m_nextField = new Cell<T>[m_columns, m_rows];
            m_fieldValues = new T[m_columns, m_rows];
            m_fieldStates = new bool[m_columns, m_rows];
            m_agentCountPerCell = new int[m_columns, m_rows];
           

            T[] tempValueA = valueA.ToArray();
        

            T[,] valueA2D = Utilities.Make2DArray(tempValueA, m_columns, m_rows);
      


            /// Initialize Cell values
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_Field[i, j] = new Cell<T>(valueA2D[i, j], new Vec3(i * m_resolution, j * m_resolution, 0), false, m_resolution, m_columns, m_rows);
                    m_nextField[i, j] = new Cell<T>(valueA2D[i, j], new Vec3(i * m_resolution, j * m_resolution, 0), false, m_resolution, m_columns, m_rows);
                }
            }

        }



        #endregion

        #region PROPERTIES

        /// <summary>
        /// Array that cointains total number of agents per cell
        /// should be changed for a more generic name.
        /// </summary>
        public int [,] AgentCount
        {
            get { return m_agentCountPerCell; }

            set { m_agentCountPerCell = value; }
        }

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


        public bool[,] States
        {
            get { return m_fieldStates; }
            set
            {
                m_fieldStates = value;
            }
        }


        public T[,] Values
        {
            get { return m_fieldValues; }

            set { m_fieldValues = value; }
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


        public void ClearOccupiedStates()
        {
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                    m_Field[i, j].Occupied = false;

                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public Cell<T>[] GetKernelNeighbours(int indexX, int indexY)
        {
            Cell<T>[] neighbours = new Cell<T>[8];
   
            neighbours[0] = m_Field[indexX + 1, indexY];
            neighbours[1] = m_Field[indexX - 1, indexY];
            neighbours[2] = m_Field[indexX, indexY +1];
            neighbours[3] = m_Field[indexX, indexY -1];
            neighbours[4] = m_Field[indexX - 1, indexY -1];
            neighbours[5] = m_Field[indexX + 1, indexY-1];
            neighbours[6] = m_Field[indexX - 1, indexY +1];
            neighbours[7] = m_Field[indexX + 1, indexY +1];
            


            return null;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"> Particle position</param>
        /// <returns></returns>
        public Cell<T> LookUpCell(Vec3 position)
        {

           
            int column = (int)(SharpMath.SharpMath.Constrain(position.X / m_resolution, 0, m_columns - 1));
            int row = (int)(SharpMath.SharpMath.Constrain(position.Y / m_resolution, 0, m_rows - 1));

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



        public override string ToString()
        {
            return $"SharpField2D<{typeof(T).Name}>({m_columns},{m_rows})";
        }


    }
}
