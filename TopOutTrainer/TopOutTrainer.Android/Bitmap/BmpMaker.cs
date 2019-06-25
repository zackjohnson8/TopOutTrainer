using System;
using System.IO;
using System.Text;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;

namespace TopOutTrainer
{

    public class BmpMaker
    {
        const int headerSize = 54;
        readonly byte[] buffer;

        private Color mainColor;
        private Color secondaryColor;
        
        public BmpMaker(int width, int height)
        {
            Width = width;
            Height = height;

            int numPixels = Width * Height;
            int numPixelBytes = 4 * numPixels;
            int fileSize = headerSize + numPixelBytes;
            buffer = new byte[fileSize];

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

        public void DrawLine(Color fillColor, Color emptyColor, double currentTime, double totalTime)
        {
            // Create a bitmap of the size asked. No need to do calculations
            // for the exact size of the box asked. Why would you... 
            // Needed items:
            // Fill (Color) and Empty space (White) for current app
            for(int column = 0; column < Width; column++)
            {
                for(int row = 0; row < Height; row++)
                {
                    SetPixel(column, row, mainColor);
                }
            }

        }

        public void DrawCircle(int row, int col, int thickness, int hangTime, int restTime, int hangIntervals)
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
            // Inside of outercircle
            for(int x = 0; x < row; x++)
            {
                for(int y = 0; y < col; y++)
                {
                    
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

             //Outside of outer circle
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {

                    // Each cordinate (x,y) has a position inside or outside a circle. Equation of a circle is (x – h)2 + (y – k)2 = r2 where the center (h,k)
                    xSqrd = (x - centerX) * (x - centerX);
                    ySqrd = (y - centerY) * (y - centerY);
                    xySum = xSqrd + ySqrd;
                    if (xySum > rSqrd)
                    {

                        SetPixel(x, y, Color.FromHex("#303030"));
                    }

                }

            }

            // Draw inner circle
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
                        SetPixel(x, y, Color.FromHex("#303030"));
                    }

                }

            }

            // Draw the inner workings of the timer onto the bitmap
            // A circle is broken into 360 degrees
            // Variables are rest time, hang time, and number of hangIntervals
            // Hang for x seconds, rest for y seconds
            // I need the length of each hang and rest compared to the degree
            // Example: 3 hangs, hang for 7 seconds, and rest for 30 seconds
            // TODO: Well thats a problem, if we do this based on a percentage worth
            // there will be a problem with disproportionate amount of bar == to rest
            // hhrrrrrrrrrrrrrrrrrrrrrrrrrrrrhhrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrhh

            // Theoretically I could have a round bar (currently have) which 
            // switches between each hang and a long flat bar to show whats left
            // or split everything evenly and just move some slower than others...
            // Lets say we split everything evenly and only use the circle.
            // Using this method we'd take...

            // Example: 360/(hangIntervals * 2) **hang and rest intervals**
            // Ex: Each hang and rest will have a percentage of the 360
            // Ex: 3 hangs, hang for 7 seconds, and rest for 30 seconds
            // Ex: 360/3*2 = 60 degrees each. Starting from 270 degrees or bottom
            // most part of the circle, we'd need to color the circle hangColor 270 to 210
            // degrees, restColor 210 to 150, hangColor 150 to 90, restColor 90 to 30,
            // hangColor 30 to 330, restColor 330 to 270

            // Using sin and cos there shouldnt be a problem just subtracting 60 from 270
            // even if it goes negative its still an equivalent degree that should translate
            // accordingly

            // So basically everything between the 60 degree angle and between the outer and
            // inner circle should paint correctly... technically... -.-
            //FillCircleAnimation(row, col, thickness, hangTime, restTime, hangIntervals);


        }

        private void FillCircleAnimation(int row, int col, int thickness, int hangTime, int restTime, int hangIntervals)
        {
            int diameter = row;
            int radiusOuter = diameter / 2 - 10;
            int radiusInnter = (diameter - thickness) / 2 - 10;

            int centerX = row / 2;
            int centerY = col / 2;
            int xSqrd;
            int ySqrd;
            int rSqrdOuter = radiusOuter * radiusOuter;
            int rSqrdInner = radiusInnter * radiusInnter;
            int xySum;
            // Inside of outercircle
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {

                    

                    // Each cordinate (x,y) has a position inside or outside a circle. Equation of a circle is (x – h)2 + (y – k)2 = r2 where the center (h,k)
                    xSqrd = (x - centerX) * (x - centerX);
                    ySqrd = (y - centerY) * (y - centerY);
                    xySum = xSqrd + ySqrd; // add together then just use a if statement to determine if within circle
                    float angle;
                    if (xySum <= rSqrdOuter)
                    {
                        if(xySum >= rSqrdInner)
                        {

                            // Now seperate each section into the appropriate color
                            // Was using the draw a line but it misses pixels. Gonna use the equation of a circle instead
                            //x = center + (radius - index0) * Math.Cos((index) * Math.PI / 180);
                            //y = center + (radius - index0) * Math.Sin((index) * Math.PI / 180);
                            //if(CheckPoint(radiusOuter, x, y, centerX, centerY, 12, 90))
                            //{
                            //    SetPixel(x, y, Color.Blue);
                            //}
                            angle = GetAngleOfPoint(x, y, centerX, centerY);
                            if (angle > 0 && angle < 60)
                            {
                                SetPixel(x, y, Color.Blue);
                            }
                            else
                            if (angle > 60 && angle < 120)
                            {
                                SetPixel(x, y, Color.Orange);
                            }
                            else
                            if (angle > 120 && angle < 180)
                            {
                                SetPixel(x, y, Color.Blue);
                            }
                            else
                            if (angle > 180 && angle < 240)
                            {
                                SetPixel(x, y, Color.Orange);
                            }
                            else
                            if (angle > 240 && angle < 300)
                            {
                                SetPixel(x, y, Color.Blue);
                            }
                            else
                            if (angle > 300 && angle < 360)
                            {
                                SetPixel(x, y, Color.Orange);
                            }

                            // Alright I've got x, y and centerx, centery.
                            // The angle of xy is tan(y/x)^-1 or 1/(tan(y/x))

                        }
                    }

                }

            }
        }

        private float GetAngleOfPoint(int x, int y, int centerX, int centerY)
        {


            float dy = (y - centerY);
            float dx = (x - centerX);
            float angle = (float)(180 / Math.PI) * (float)Math.Atan2(dy, dx);

            //if (angle < 0)
            //{
            //    angle += 180;
            //}

            return angle + 180;
        }

        private Boolean CheckPoint(int radius, int x, int y, int centerX, int centerY,
                    float percent, float startAngle)
        {

            // calculate endAngle 
            float endAngle = (360 * percent / 100) + startAngle;

            // Calculate polar co-ordinates 
            float polarradius = (float)Math.Sqrt(x * x + y * y);

            float Angle = (float)Math.Atan(y / x);

            // Check whether polarradius is less then  
            // radius of circle or not and Angle is  
            // between startAngle and endAngle or not 
            if (Angle >= startAngle && Angle <= endAngle
                                && polarradius < radius)
                return true;
            else
                return false;
        }


    }
}

/* BITMAKER
 * Using the bitmaker create a circle to display time. The pixels in the inner circle should
 * trigger an event to begin and stop timer. Ignore reset, could use a pop up menu to handle
 * any changes that need to be made.
 * 
 * 
 * 
 * 
 */