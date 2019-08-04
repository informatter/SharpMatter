using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMatter.SharpGeometry;
using SharpMatter.SharpField;
using SharpMatter.SharpPhysics;

namespace SharpMatter.SharpField.Interfaces
{
    /// <summary>
    /// Determines the typical behavior of a 2DField class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IField2D<T>
        //where T: struct
    {
        /// <summary>
        /// Returns/sets the number of columns in the field. X-dimension
        /// </summary>
        int Columns { get; set; }

        /// <summary>
        /// Returns/sets the number of rows in the field. Y-dimension
        /// </summary>
        int Rows { get; set; }

        /// <summary>
        /// Returns/sets the resolution of the field
        /// </summary>
        double Resolution { get; set; }

        /// <summary>
        /// Returns/sets the values of the field
        /// </summary>
        T [,] Values { get; set; }

        /// <summary>
        /// Gets the current cell at a specified position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        SharpCell<T> LookUpCell(Vec3 position);

    }
}
