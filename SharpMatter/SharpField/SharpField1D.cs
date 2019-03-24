using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpField
{
    public class SharpField1D<T>
    {
        private T [] values = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="data"></param>
       public SharpField1D(int length, List<T> data)
        {
            if (length != data.Count) throw new ArgumentException("length of field and number of data items must be equal!");
            values = new T [length];

            for (int i = 0; i < data.Count; i++)
            {
                values[i] = data[i];


            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        public SharpField1D(int length)
        {
           
            values = new T[length];

        }



        //PROPERTIES
        public T[] Values
        {
            get { return values; }
            
        }


    }
}
