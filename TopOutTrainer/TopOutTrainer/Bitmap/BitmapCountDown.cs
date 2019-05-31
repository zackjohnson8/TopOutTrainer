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
        private int totalTime;

        SKPaint primaryColorFillPaint;
        SKPaint primaryLinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,
        };

        public BitmapCountDown(int totalTime_p, Color primaryColor_p, Color secondaryColor_p)
        {// , int width, int height,
            totalTime = totalTime_p;
            primaryColor = primaryColor_p;
            secondaryColor = secondaryColor_p;
            this.PaintSurface += Handle_PaintSurface;

            primaryColorFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = primaryColor.ToSKColor()
            };
            primaryLinePaint.Color = primaryColor_p.ToSKColor();

            SizeChanged += BitmapCountDown_SizeChanged;

        }

        SKSurface surface;
        SKCanvas canvas;
        void Handle_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            surface = e.Surface;
            canvas = surface.Canvas;

            int width = e.Info.Width;
            int height = e.Info.Height;

            canvas.Translate(width / 2, height / 2);
            canvas.Scale(width / 200f);

            primaryLinePaint.StrokeWidth = height / 60;

            //canvas.DrawCircle(new SKPoint(0, 0), 10, primaryColorFillPaint);
            canvas.DrawLine(new SKPoint(-width/25, 0), new SKPoint(width/25, 0), primaryLinePaint);

            //canvas.Clear(primaryColor.ToSKColor());
        }

        void BitmapCountDown_SizeChanged(object sender, EventArgs e)
        {
            //this.Handle_PaintSurface(sender,);
        }

    }
}

// 448
