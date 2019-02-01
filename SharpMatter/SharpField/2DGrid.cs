using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpData;

namespace SharpMatter.SharpField
{
    public class _2DGrid
    {
        private int columns;
        private int rows;
        private Cell[,] data;

        public _2DGrid(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;

            data = new Cell[columns, rows];

        }


        public Cell[,] Data
        {
            get { return data; }
        }


        public Vec3[,] Display()
        {
            Vec3[,] vecs = new Vec3[columns, rows];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    vecs[i, j] = new Vec3(i, j, 0);
                }
            }

            return vecs;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <typeparam name="U"></typeparam>
       /// <param name="info"></param>
        public void InitializeFieldExistingData(double [,] info)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    data[i, j].dataItem = info[i,j];
                }
            }

        }


        public void InitializeFieldRandom(Random ran,Domain domain ) 
        {
          
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    data[i, j].dataItem = ran.Next((int)domain.min,(int)domain.max);

               
                }
            }

        }









    }
}
