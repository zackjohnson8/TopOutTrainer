using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
namespace TopOutTrainer.Bitmap
{
    public enum ShapeType
    {

    }

    public class BitmapCountDown : SKCanvasView
    {


        private Color primaryColor;
        private Color secondaryColor;
        private int totalTime = 5;
        private int currentTime = 0;

        SKPaint primaryColorFillPaint;
        SKPaint primaryLinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,
        };

        public BitmapCountDown(int totalTimeP, Color primaryColorP, Color secondaryColorP)
        {// , int width, int height,
            totalTime = totalTimeP;
            currentTime = 0;
            primaryColor = primaryColorP;
            secondaryColor = secondaryColorP;
            this.PaintSurface += Handle_PaintSurface;

            primaryColorFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = primaryColor.ToSKColor()
            };
            primaryLinePaint.Color = primaryColorP.ToSKColor();

            SizeChanged += BitmapCountDown_SizeChanged;

        }

        SKSurface surface;
        SKCanvas canvas;
        int width = 0;
        int height = 0;
        void Handle_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            surface = e.Surface;
            canvas = surface.Canvas;
            width = e.Info.Width;
            height = e.Info.Height;
            canvas.Clear(StaticFiles.ColorSettings.mainGrayColor.ToSKColor());

            canvas.Translate(0, height / 2);
            //canvas.Scale(width / 200f);

            primaryLinePaint.StrokeWidth = (int)(height * .15);

            // startTime = 7, finish = 0, currentTime = 5 default(0)
            // 1080 (5/7 ( 71% complete) ) 
            float completedTime = ((float)currentTime / (float)totalTime);
            int leftPoint = (int)(width * .25); // 100
            int rightPoint = (int)(width * .75); // 1080
            int totalPixelLeftToRight = rightPoint - leftPoint; // 980
            float totalReduction = totalPixelLeftToRight * completedTime;
            rightPoint = rightPoint - (int)totalReduction;

            //int rightPoint = (int)(width - (width / 1.05));
            //canvas.DrawCircle(new SKPoint(0, 0), 10, primaryColorFillPaint);
            canvas.DrawLine(new SKPoint(leftPoint, 0), new SKPoint(rightPoint, 0), primaryLinePaint);

            //canvas.Clear(primaryColor.ToSKColor());
        }

        void BitmapCountDown_SizeChanged(object sender, EventArgs e)
        {

            //this.Handle_PaintSurface(sender,);
            //width = (int)this.Width;
            //height = (int)this.Height;

            this.InvalidateSurface();

        }

        public void UpdatePrimaryColor(Color colorP)
        {
            primaryColor = colorP;
            primaryLinePaint.Color = primaryColor.ToSKColor();
        }

        public void UpdateTotalTime(int totalTimeP)
        {
            totalTime = totalTimeP;
        }

        public void UpdateCurrentTime(int currentTimeP)
        {
            currentTime = currentTimeP;
        }


    }
}

// 448
