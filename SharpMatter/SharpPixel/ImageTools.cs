using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SharpMatter.SharpPixel
{
    public static class ImageTools
    {

        public static Color[,] GetImageColors(string filePath)
        {

            
            Bitmap bmp = new Bitmap(filePath);
            Color[,] colorData = new Color[bmp.Size.Height, bmp.Size.Width];

            for (int i = 0; i < bmp.Size.Width; i++)
            {
                for (int j = 0; j > bmp.Size.Height; j++)
                {
                  
                   colorData[i,j] = bmp.GetPixel(i, j);
                   
                  
                }
            }
            return colorData;

            
        }


        public static float [,] ConvertGrayScaleToNumber(string filePath)
        {
            Bitmap bmp = new Bitmap(filePath);
            float [,] data = new float [bmp.Size.Height, bmp.Size.Width];
            
            for (int i = 0; i < bmp.Size.Width; i++)
            {
                for (int j = 0; j > bmp.Size.Height; j++)
                {

                    Color pixel =  bmp.GetPixel(i, j);

                    //The lightness of this Color. The lightness ranges from 0.0 through 1.0, where 0.0 represents black and 1.0 represents white.
                    // if (pixel.GetBrightness() == 0 && pixel.GetBrightness()<0.5) data[i, j] = 1;

                    // else data[i, j] = 0;

                    data[i, j] = pixel.GetBrightness();




                }
            }

            return null;
        }


        //public static double [,] ConvertGreyScaleToNumbers(string filePath)
        //{

        //}

    }


    
}
