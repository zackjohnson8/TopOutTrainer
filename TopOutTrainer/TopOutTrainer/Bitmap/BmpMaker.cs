using System;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public class BmpMaker
    {
        const int headerSize = 54;
        readonly byte[] buffer;

        public BmpMaker(int width, int height)
        {
            Width = width;
            Height = height;

            int numPixels = Width * Height;
            int numPixelBytes = 4 * numPixels;
            int fileSize = headerSize + numPixelBytes;
            buffer = new byte[fileSize];

            //try
            //{
            //    using (MemoryStream ms = new MemoryStream(buffer))
            //    {
            //        sw = new StreamWriter(ms);

            //        sw.WriteLine("data");
            //        sw.WriteLine("data 2");
            //        ms.Position = 0;

            //    }
            //}
            //finally
            //{
            //    if (sw != null) sw.Dispose();
            //}

            // Write headers in MemoryStream and hence the buffer
            MemoryStream memoryStream = new MemoryStream(buffer);
            BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8);

            // Construct BMP header (14 bytes).
            writer.Write(new char[] { 'B', 'M' });  // Signature
            writer.Write(fileSize);                 // File size
            writer.Write((short)0);                 // Reserved
            writer.Write((short)0);                 // Reserved
            writer.Write(headerSize);               // Offset to pixels

            // Construct BitmapInfoHeader (40 bytes).
            writer.Write(40);                       // Header size
            writer.Write(Width);                    // Pixel width
            writer.Write(Height);                   // Pixel height
            writer.Write((short)1);                 // Planes
            writer.Write((short)32);                // Bits per pixel
            writer.Write(0);                        // Compression
            writer.Write(numPixelBytes);            // Image size in bytes
            writer.Write(0);                        // X pixels per meter
            writer.Write(0);                        // Y pixels per meter
            writer.Write(0);                        // Number colors in color table
            writer.Write(0);                        // Important color count
            writer.Flush();
            memoryStream.Position = 0;


                    
                
            
        }

        public int Width
        {
            private set;
            get;
        }

        public int Height
        {
            private set;
            get;
        }

        public void SetPixel(int row, int col, Color color)
        {
            SetPixel(row, col, (int)(255 * color.R),
                               (int)(255 * color.G),
                               (int)(255 * color.B),
                               (int)(255 * color.A));
        }

        public void SetPixel(int row, int col, int r, int g, int b, int a = 255)
        {
            int index = (row * Width + col) * 4 + headerSize;
            buffer[index + 0] = (byte)b;
            buffer[index + 1] = (byte)g;
            buffer[index + 2] = (byte)r;
            buffer[index + 3] = (byte)a;
        }

        public void DrawCircle(int row, int col, int thickness)
        {

            int diameter = row;
            int radius = diameter / 2 - 10;
            int center = row / 2; // division takes the lower number even with odd numbers
            int radiusSqrd = radius * radius;

            int centerX = row / 2;
            int centerY = col / 2;

            int xSqrd;
            int ySqrd;
            int rSqrd = radius * radius;
            int xySum;
            for(int x = 0; x < row; x++)
            {
                for(int y = 0; y < col; y++)
                {

                    // Was using the draw a line but it misses pixels. Gonna use the equation of a circle instead
                    //x = center + (radius - index0) * Math.Cos((index) * Math.PI / 180);
                    //y = center + (radius - index0) * Math.Sin((index) * Math.PI / 180);
                    
                    // Each cordinate (x,y) has a position inside or outside a circle. Equation of a circle is (x – h)2 + (y – k)2 = r2 where the center (h,k)
                    xSqrd = (x - centerX) * (x - centerX);
                    ySqrd = (y - centerY) * (y - centerY);
                    xySum = xSqrd + ySqrd; // add together then just use a if statement to determine if within circle
                    if (xySum < rSqrd)
                    {

                        SetPixel(x, y, Color.White);
                    }

                }

            }


            radius = (diameter - thickness) / 2 - 10;
            rSqrd = radius * radius;
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {

                    // Was using the draw a line but it misses pixels. Gonna use the equation of a circle instead
                    //x = center + (radius - index0) * Math.Cos((index) * Math.PI / 180);
                    //y = center + (radius - index0) * Math.Sin((index) * Math.PI / 180);

                    // Each cordinate (x,y) has a position inside or outside a circle. Equation of a circle is (x – h)2 + (y – k)2 = r2 where the center (h,k)
                    xSqrd = (x - centerX) * (x - centerX);
                    ySqrd = (y - centerY) * (y - centerY);
                    xySum = xSqrd + ySqrd; // add together then just use a if statement to determine if within circle
                    if (xySum < rSqrd)
                    {

                        SetPixel(x, y, ContentViewHandler.timerPage_backgroundColor);
                    }

                }

            }

        }

        public ImageSource Generate()
        {
            // Create MemoryStream from buffer with bitmap.
            MemoryStream memoryStream = new MemoryStream(buffer);

            // Convert to StreamImageSource.
            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                return memoryStream;
            });
            return imageSource;
        }
    }
}