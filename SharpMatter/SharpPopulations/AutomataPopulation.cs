using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpBehavior;
using SharpMatter.SharpUtilities;
using SharpMatter.SharpGeometry;

namespace SharpMatter.SharpPopulations
{
    public class AutomataPopulation
    {
        private Automata[,] m_population;

        private int m_columns;
        private int m_rows;
        private double m_resolution;


        public AutomataPopulation(List<int> states, int rows, int columns, double resolution)
        {
            m_rows = rows;
            m_columns = columns;
            m_resolution = resolution;


            int [] tempstates = states.ToArray();


            int [,] states2D = Utilities.Make2DArray(tempstates, m_columns, m_rows);


            ///// Initialize Cell values
            for (int i = 0; i < m_columns; i++)
            {
                for (int j = 0; j < m_rows; j++)
                {
                   // m_population[i,j] = new Automata(new Vec3(i * m_resolution, j * m_resolution, 0), states2D[i,j]);
                }
            }
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


        public Automata [,] Population
        {
            get { return m_population; }
         
        }


        public int Rows
        {
            get { return m_rows; }
            set
            {
                if (m_rows <= 0) throw new ArgumentException("Number of columns must be greater than zero!");
                else m_rows = value;
            }
        }


        public double Resolution
        {
            get { return m_resolution; }
            set
            {
                if (m_resolution <= 0) throw new ArgumentException("Resolution must be greater than zero!");
                else m_resolution = value;
            }
        }

    }
}
