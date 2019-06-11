using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using System.Timers;
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

        SKPaint primaryColorFillPaint;
        SKPaint primaryLinePaint = new SKPaint
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

            SizeChanged += BitmapCountDown_SizeChanged;

        }

        SKSurface surface;
        SKCanvas canvas;
        int width = 0;
        int height = 0;
        void Handle_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            if (!clearBitMap) 
            {
                surface = e.Surface;
                canvas = surface.Canvas;
                width = e.Info.Width;
                height = e.Info.Height;
                canvas.Clear(StaticFiles.ColorSettings.mainGrayColor.ToSKColor());
                primaryLinePaint.Color = primaryColor.ToSKColor();

                canvas.Translate(0, height / 2);
                //canvas.Scale(width / 200f);
                primaryLinePaint.StrokeWidth = (int)(height * .15);

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
                return;
            }
            
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


        private bool onGetReady = false;
        private bool onStart = false;
        private bool onRepBreak = false;
        private bool onSetBreak = false;
        //private bool onEnd = false;
        private int repCountIndex = 0;
        private int setCountIndex = 0;
        public void Start()
        {
            if (StartCheck())
            {

                if (!eventRunning)
                {
                    aTimer.Elapsed += OnTimedEvent;
                    eventRunning = true;
                }
                aTimer.AutoReset = true;
                aTimer.Enabled = true;

                //startTimer = true;
                //Device.StartTimer(TimeSpan.FromSeconds(1f / 60), () =>
                //{
                //    currentTime += 1f / 60;
                //    if (currentTime >= totalTime)
                //    {
                //        NextTimer();
                //    }
                //    this.InvalidateSurface();
                //    return startTimer;
                //});
            }
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
                return true;
            }
            else
              if (StaticFiles.TimerPageUISettings.startTime > 0)
            {
                onStart = true;
                totalTime = StaticFiles.TimerPageUISettings.startTime + 1;
                primaryColor = StaticFiles.ColorSettings.startColor;
                return true;
            }
            else
              if (StaticFiles.TimerPageUISettings.reps > 0)
            {
                onRepBreak = true;
                totalTime = StaticFiles.TimerPageUISettings.repsRestTime + 1;
                primaryColor = StaticFiles.ColorSettings.repBreakColor;
                return true;
            }
            else
              if (StaticFiles.TimerPageUISettings.sets > 0)
            {
                onSetBreak = true;
                totalTime = StaticFiles.TimerPageUISettings.setsRestTime + 1;
                primaryColor = StaticFiles.ColorSettings.setBreakColor;
                return true;
            }
            else
            {
                //onEnd = true;
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
                }

            }else
            if(onStart)
            {
                onStart = false;
                repCountIndex++;

                if(repCountIndex <= StaticFiles.TimerPageUISettings.reps)
                {
                    totalTime = StaticFiles.TimerPageUISettings.repsRestTime;
                    primaryColor = StaticFiles.ColorSettings.repBreakColor;
                    onRepBreak = true;
                }else // Set break
                {
                    setCountIndex++;

                    if (setCountIndex < StaticFiles.TimerPageUISettings.sets)
                    {
                        repCountIndex = 0;
                        totalTime = StaticFiles.TimerPageUISettings.setsRestTime;
                        primaryColor = StaticFiles.ColorSettings.setBreakColor;
                        onSetBreak = true;
                    }else
                    {
                        this.Stop();
                        setCountIndex = 0;
                    }
                }
            }
            else
            if (onRepBreak)
            {
                onRepBreak = false;
                onGetReady = true;
                totalTime = StaticFiles.TimerPageUISettings.getReadyTime;
                primaryColor = StaticFiles.ColorSettings.getReadyColor;
            }
            else
            if (onSetBreak)
            {
                onSetBreak = false;
                onGetReady = true;
                totalTime = StaticFiles.TimerPageUISettings.getReadyTime;
                primaryColor = StaticFiles.ColorSettings.getReadyColor;
            }

        }

        public void Stop()
        {
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

    }
}

// 448
