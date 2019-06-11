using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using System.Timers;
using System.Diagnostics;

namespace TopOutTrainer.Bitmap
{
    public enum ShapeType
    {

    }

    public enum CountSetting
    {
        SETBREAK,
        REPBREAK,
        GETREADY,
        START,
        END
    }

    public class BitmapCountDown : SKCanvasView
    {

        private const int elapsedMilliSec = 10;
        private System.Timers.Timer aTimer = new System.Timers.Timer(elapsedMilliSec);
        private bool eventRunning = false;

        private Color primaryColor;
        private Color secondaryColor;
        private float totalTime = 5;
        private float currentTime = 0;
        private bool startTimer;
        private bool clearBitMap = false;
        private CountSetting BitSetting = CountSetting.GETREADY;

        SKSurface surface;
        SKCanvas canvas;
        int width = 0;
        int height = 0;

        private bool onGetReady = false;
        private bool onStart = false;
        private bool onRepBreak = false;
        private bool onSetBreak = false;
        //private bool onEnd = false;
        private int repCountIndex = 0;
        private int setCountIndex = 0;

        SKPaint primaryColorFillPaint;
        SKPaint primaryLinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,
        };

        SKPaint secondaryColorFillPaint;
        SKPaint secondaryLinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,

        };

        public BitmapCountDown(float totalTimeP, Color primaryColorP, Color secondaryColorP)
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

            secondaryColorFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = secondaryColor.ToSKColor()
            };
            secondaryLinePaint.Color = secondaryColorP.ToSKColor();

            StartCheck();

            SizeChanged += BitmapCountDown_SizeChanged;

        }

        void BitmapCountDown_SizeChanged(object sender, EventArgs e)
        {
            this.InvalidateSurface();
        }

        public void UpdatePrimaryColor(Color colorP)
        {
            primaryColor = colorP;
            primaryLinePaint.Color = primaryColor.ToSKColor();
        }

        public void UpdateTotalTime(float totalTimeP)
        {
            totalTime = totalTimeP;
        }

        public void UpdateCurrentTime(float currentTimeP)
        {
            currentTime = currentTimeP;
        }

        public void Start()
        {

            if (!eventRunning)
            {
                aTimer.Elapsed += OnTimedEvent;
                eventRunning = true;
            }
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }

        //private int MilliSecond = 0;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            currentTime += elapsedMilliSec / 1000f;

            if (currentTime >= totalTime)
            {
                NextTimer();
            }
            this.InvalidateSurface();
        }


        private bool StartCheck()
        {
            if (StaticFiles.TimerPageUISettings.getReadyTime > 0)
            {
                onGetReady = true;
                totalTime = StaticFiles.TimerPageUISettings.getReadyTime + 1;
                primaryColor = StaticFiles.ColorSettings.getReadyColor;
                primaryLinePaint.Color = StaticFiles.ColorSettings.getReadyColor.ToSKColor();
                return true;
            }
            else
            if (StaticFiles.TimerPageUISettings.startTime > 0)
            {
                onStart = true;
                totalTime = StaticFiles.TimerPageUISettings.startTime + 1;
                primaryColor = StaticFiles.ColorSettings.startColor;
                primaryLinePaint.Color = StaticFiles.ColorSettings.startColor.ToSKColor();
                return true;
            }
            else
            if (StaticFiles.TimerPageUISettings.reps > 0)
            {
                onRepBreak = true;
                totalTime = StaticFiles.TimerPageUISettings.repsRestTime + 1;
                primaryColor = StaticFiles.ColorSettings.repBreakColor;
                primaryLinePaint.Color = StaticFiles.ColorSettings.repBreakColor.ToSKColor();
                return true;
            }
            else
            if (StaticFiles.TimerPageUISettings.sets > 0)
            {
                onSetBreak = true;
                totalTime = StaticFiles.TimerPageUISettings.setsRestTime + 1;
                primaryColor = StaticFiles.ColorSettings.setBreakColor;
                primaryLinePaint.Color = StaticFiles.ColorSettings.setBreakColor.ToSKColor();
                return true;
            }
            else
            {
                //onEnd = true;
                this.Stop();
                return false;
            }
        }


        private void NextTimer()
        {
            currentTime = 0;

            if(onGetReady)
            {
                onGetReady = false;
                onStart = true;
                if(StaticFiles.TimerPageUISettings.startTime > 0)
                {
                    totalTime = StaticFiles.TimerPageUISettings.startTime;
                    primaryColor = StaticFiles.ColorSettings.startColor;
                    primaryLinePaint.Color = StaticFiles.ColorSettings.startColor.ToSKColor();
                }

            }else
            if(onStart)
            {
                onStart = false;
                repCountIndex++;
                if(repCountIndex < StaticFiles.TimerPageUISettings.reps)
                {
                    totalTime = StaticFiles.TimerPageUISettings.repsRestTime;
                    primaryColor = StaticFiles.ColorSettings.repBreakColor;
                    primaryLinePaint.Color = StaticFiles.ColorSettings.repBreakColor.ToSKColor();
                    onRepBreak = true;
                }else // Set break
                {
                    setCountIndex++;
                    if (setCountIndex < StaticFiles.TimerPageUISettings.sets)
                    {
                        repCountIndex = 0;
                        totalTime = StaticFiles.TimerPageUISettings.setsRestTime;
                        primaryColor = StaticFiles.ColorSettings.setBreakColor;
                        primaryLinePaint.Color = StaticFiles.ColorSettings.setBreakColor.ToSKColor();
                        onSetBreak = true;
                    }else
                    {
                        this.Stop();
                        setCountIndex = 0;
                    }
                }
            }
            else
            if (onRepBreak || onSetBreak)
            {
                onSetBreak = false;
                onRepBreak = false;
                onGetReady = true;
                totalTime = StaticFiles.TimerPageUISettings.getReadyTime;
                primaryColor = StaticFiles.ColorSettings.getReadyColor;
                primaryLinePaint.Color = StaticFiles.ColorSettings.getReadyColor.ToSKColor();
            }

        }

        public void Stop()
        {
            primaryLinePaint.Color = StaticFiles.ColorSettings.mainGrayColor.ToSKColor();
            secondaryLinePaint.Color = StaticFiles.ColorSettings.mainGrayColor.ToSKColor();
            this.InvalidateSurface();
            aTimer.Enabled = false;
        }

        public void Clear()
        {
            clearBitMap = true;
            this.InvalidateSurface();
        }

        public void SetTimerSetting(CountSetting selection)
        {

            this.Stop();
            BitSetting = selection;

            switch(BitSetting)
            {
                case Bitmap.CountSetting.GETREADY:
                    this.primaryColor = StaticFiles.ColorSettings.getReadyColor;
                    totalTime = StaticFiles.TimerPageUISettings.getReadyTime;
                    currentTime = 0;
                    this.Start();
                    break;
                case Bitmap.CountSetting.START:
                    this.primaryColor = StaticFiles.ColorSettings.startColor;
                    totalTime = StaticFiles.TimerPageUISettings.startTime;
                    currentTime = 0;
                    this.Start();
                    break;
                case Bitmap.CountSetting.REPBREAK:
                    this.primaryColor = StaticFiles.ColorSettings.repBreakColor;
                    totalTime = StaticFiles.TimerPageUISettings.repsRestTime;
                    currentTime = 0;
                    this.Start();
                    break;
                case Bitmap.CountSetting.SETBREAK:
                    this.primaryColor = StaticFiles.ColorSettings.setBreakColor;
                    totalTime = StaticFiles.TimerPageUISettings.setsRestTime;
                    currentTime = 0;
                    this.Start();
                    break;
                case Bitmap.CountSetting.END:
                    this.Clear();
                    break;
                default:
                    break;
            }
        }

        void Handle_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            surface = e.Surface;
            canvas = surface.Canvas;
            width = e.Info.Width;
            height = e.Info.Height;
            canvas.Save();
            canvas.Clear(StaticFiles.ColorSettings.mainGrayColor.ToSKColor());
            canvas.Translate(0, height / 2);
            //canvas.Scale(width / 200f);
            primaryLinePaint.StrokeWidth = (int)(height * .15);
            secondaryLinePaint.StrokeWidth = (int)(height * .15);

            float completedTime = ((float)currentTime / (float)totalTime);
            int leftPoint = (int)(width * .25); // 100
            int rightPoint = (int)(width * .75); // 1080
            int totalPixelLeftToRight = rightPoint - leftPoint; // 980
            float totalReduction = totalPixelLeftToRight * completedTime;
            rightPoint = rightPoint - (int)totalReduction;

            //int rightPoint = (int)(width - (width / 1.05));
            //canvas.DrawCircle(new SKPoint(0, 0), 10, primaryColorFillPaint);
            canvas.DrawLine(new SKPoint(leftPoint, 0), new SKPoint((int)(width * .75), 0), secondaryLinePaint);
            canvas.DrawLine(new SKPoint(leftPoint, 0), new SKPoint(rightPoint, 0), primaryLinePaint);
            //canvas.Clear(primaryColor.ToSKColor());
            canvas.Restore();
            return;
        }

    }
}

// 448
