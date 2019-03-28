using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using SharpMatter.SharpField;
using SharpMatter.SharpUtilities;
namespace SharpMatter.SharpField
{
    public enum imageFormat{ jpg, png}
    public static class SharpFieldIO
    {


        /// <summary>
        /// nunuunnunununununununu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="counter"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>                                                                 imageFormat format,
        public static void SaveImageSecuence<T>(SharpField2D<T> field, string path, string name,int counter, imageFormat format, List<Color> colors )
        {
            Bitmap bmp = new Bitmap(field.Columns, field.Rows);
            Color[] tempColors = colors.ToArray();
            Color[,] colors2D = Utilities.Make2DArray(tempColors, field.Columns, field.Rows);

           // string Name = name + counter.ToString() + ".jpg";
              if (imageFormat.jpg == format)
              {
                string Name = name + counter.ToString() + ".jpg";
                //Parallel.For(0, field.Columns, i =>
                for (int i = 0; i < field.Columns; i++)
                {

                    for (int j = 0; j < field.Rows; j++)
                    {
                        bmp.SetPixel(i, j, colors2D[i, j]);
                    }
                 }

                   // });//End Parallel forloop

                bmp.Save(Path.Combine(path,Name), ImageFormat.Jpeg);
              }


            if (imageFormat.png == format)
            {
                string Name = name + counter.ToString() + ".png";
                for (int i = 0; i < field.Columns; i++)
                {

                    for (int j = 0; j < field.Rows; j++)
                    {
                        bmp.SetPixel(i, j, colors2D[i, j]);
                    }
                }

               // });//End Parallel forloop

                bmp.Save(Path.Combine(path, Name), ImageFormat.Png);
            }

        }
    }
}
