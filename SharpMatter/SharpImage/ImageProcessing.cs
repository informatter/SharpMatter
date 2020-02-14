using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMatter.SharpImage
{
    public static class ImageProcessing
    {
        public static List<double> ProcessUsingLockbits(Bitmap image, double scale, out List<Color> colors)
        {
            // pixel format can be Format24bppRgb or Format32bppArgb
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
              ImageLockMode.ReadWrite, image.PixelFormat);

            // basically divides 24bytes/8 and itr will be 3 bytes per pixel RGB
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = Math.Abs(bitmapData.Stride) * image.Height;

            // Get height and width
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            List<Color> col = new List<Color>();
            List<double> heights = new List<double>();

            unsafe
            {
                byte* ptrFirstPxl = (byte*)bitmapData.Scan0;


                for (int i = 0; i < heightInPixels; i++)
                {
                    byte* currentLine = ptrFirstPxl + (i * bitmapData.Stride);
                    for (int j = 0; j < widthInBytes; j += bytesPerPixel)
                    {

                        int b = currentLine[j];
                        int g = currentLine[j + 1];
                        int r = currentLine[j + 2];

                        
                        Color c = Color.FromArgb(r, g, b);
                        
                        col.Add(c);
                        heights.Add(c.GetBrightness() * scale);
 


                    }
                }
            }


            image.UnlockBits(bitmapData);
            colors = col;
            return heights;
        }
    }
}
