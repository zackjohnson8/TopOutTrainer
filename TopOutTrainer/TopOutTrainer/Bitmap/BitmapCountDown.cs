using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using System.Timers;
using UIKit;

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
        private CountSetting BitSetting = CountSetting.GETREADY;

        SKSurface surface;
        SKCanvas canvas;
        int width = 0;
        int height = 0;

        private struct TimerType
        {
            public bool active; // does the user have this set larger than 0
            public bool focus; // currently being used
            public Color color;
            public int totalTime;

        };

        private TimerType readyTimerType;
        private TimerType startTimerType;
        private TimerType repTimerType;
        private TimerType setTimerType;
        private TimerType endTimerType;

        //private bool onEnd = false;
        private int repCountIndex = 0;
        private int setCountIndex = 0;

        SKPaint primaryLinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,
        };

        SKPaint secondaryLinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,

        };

        public BitmapCountDown()
        {
            // Repaint event
            this.PaintSurface += Handle_PaintSurface;

            // Set values needed for BitmapCountDown to work
            TimerTypeFactory();
            StartCheck();

            SizeChanged += BitmapCountDown_SizeChanged;

        }

        public void Start()
        {

            if (!eventRunning)
            {
                aTimer.Elapsed += OnTimedEvent;
                eventRunning = true;
            }
            aTimer.AutoReset = true;

            Timer.TimerLock.UnlockBitMapLocker();
            while (!Timer.TimerLock.ReadyCheck()) { }
            aTimer.Enabled = true;

        }

        public void Pause()
        {
            aTimer.Enabled = false;
        }

        public void Resume()
        {
            aTimer.Enabled = true;
        }

        public void Reset()
        {
            currentTime = 0;
            repCountIndex = 0;
            setCountIndex = 0;
            readyTimerType.focus = false;
            startTimerType.focus = false;
            repTimerType.focus = false;
            setTimerType.focus = false;

            StartCheck();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.InvalidateSurface();
            });
        }

        public void Stop()
        {
            primaryLinePaint.Color = StaticFiles.ColorSettings.mainGrayColor.ToSKColor();
            secondaryLinePaint.Color = StaticFiles.ColorSettings.mainGrayColor.ToSKColor();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.InvalidateSurface();
            });
            aTimer.Enabled = false;
        }

        public void Clear()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.InvalidateSurface();
            });
        }

        private void TimerTypeFactory()
        {
            if (StaticFiles.TimerPageUISettings.getReadyTime > 0)
            {
                readyTimerType.color = StaticFiles.ColorSettings.getReadyColor;
                readyTimerType.active = true;
                readyTimerType.focus = false;
                readyTimerType.totalTime = StaticFiles.TimerPageUISettings.getReadyTime;
            }
            else
            {
                readyTimerType.color = StaticFiles.ColorSettings.getReadyColor;
                readyTimerType.active = false;
                readyTimerType.focus = false;
                readyTimerType.totalTime = StaticFiles.TimerPageUISettings.getReadyTime;
            }

            if (StaticFiles.TimerPageUISettings.startTime > 0)
            {
                startTimerType.color = StaticFiles.ColorSettings.startColor;
                startTimerType.active = true;
                startTimerType.focus = false;
                startTimerType.totalTime = StaticFiles.TimerPageUISettings.startTime;
            }
            else
            {
                startTimerType.color = StaticFiles.ColorSettings.startColor;
                startTimerType.active = false;
                startTimerType.focus = false;
                startTimerType.totalTime = StaticFiles.TimerPageUISettings.startTime;
            }

            if (StaticFiles.TimerPageUISettings.reps > 0)
            {
                repTimerType.color = StaticFiles.ColorSettings.repBreakColor;
                repTimerType.active = true;
                repTimerType.focus = false;
                repTimerType.totalTime = StaticFiles.TimerPageUISettings.repsRestTime;
            }
            else
            {
                repTimerType.color = StaticFiles.ColorSettings.repBreakColor;
                repTimerType.active = false;
                repTimerType.focus = false;
                repTimerType.totalTime = StaticFiles.TimerPageUISettings.repsRestTime;
            }

            if (StaticFiles.TimerPageUISettings.sets > 0)
            {
                setTimerType.color = StaticFiles.ColorSettings.setBreakColor;
                setTimerType.active = true;
                setTimerType.focus = false;
                setTimerType.totalTime = StaticFiles.TimerPageUISettings.setsRestTime;
            }
            else
            {
                setTimerType.color = StaticFiles.ColorSettings.setBreakColor;
                setTimerType.active = false;
                setTimerType.focus = false;
                setTimerType.totalTime = StaticFiles.TimerPageUISettings.setsRestTime;
            }

            endTimerType.color = StaticFiles.ColorSettings.mainGrayColor;
            endTimerType.active = true;
            endTimerType.focus = false;
        }

        private bool StartCheck()
        {
            secondaryLinePaint.Color = StaticFiles.ColorSettings.darkGrayColor.ToSKColor();

            if (StaticFiles.TimerPageUISettings.getReadyTime > 0)
            {
                readyTimerType.focus = true;
                totalTime = readyTimerType.totalTime;
                primaryLinePaint.Color = readyTimerType.color.ToSKColor();
                return true;
            }
            else
            if (StaticFiles.TimerPageUISettings.startTime > 0)
            {
                startTimerType.focus = true;
                totalTime = StaticFiles.TimerPageUISettings.startTime;
                primaryLinePaint.Color = StaticFiles.ColorSettings.startColor.ToSKColor();
                return true;
            }
            else
            if (StaticFiles.TimerPageUISettings.reps > 0 && ((repCountIndex + 1) < StaticFiles.TimerPageUISettings.reps))
            {
                repCountIndex++;
                repTimerType.focus = true;
                totalTime = StaticFiles.TimerPageUISettings.repsRestTime;
                primaryLinePaint.Color = StaticFiles.ColorSettings.repBreakColor.ToSKColor();
                return true;
            }
            else
            if (StaticFiles.TimerPageUISettings.sets > 0 && ((setCountIndex + 1) < StaticFiles.TimerPageUISettings.sets))
            {
                setCountIndex++;
                setTimerType.focus = true;
                totalTime = StaticFiles.TimerPageUISettings.setsRestTime;
                primaryLinePaint.Color = StaticFiles.ColorSettings.setBreakColor.ToSKColor();
                return true;
            }
            else
            {
                this.Stop();
                return false;
            }
        }


        private bool NextTimer()
        {
            aTimer.Enabled = false;
            currentTime = 0;

            if(readyTimerType.focus)
            {
                if(startTimerType.active)
                {
                    readyTimerType.focus = false;
                    startTimerType.focus = true;
                    totalTime = startTimerType.totalTime;
                    primaryLinePaint.Color = StaticFiles.ColorSettings.startColor.ToSKColor();
                    aTimer.Enabled = true;
                    return true;
                }

                readyTimerType.focus = false;
                startTimerType.focus = true;
                return NextTimer();

            }else
            if(startTimerType.focus)
            {
                startTimerType.focus = false;
                repCountIndex++;
                if(repCountIndex < StaticFiles.TimerPageUISettings.reps)
                {
                    if(repTimerType.active)
                    {
                        repTimerType.focus = true;
                        totalTime = repTimerType.totalTime;
                        primaryLinePaint.Color = repTimerType.color.ToSKColor();
                        aTimer.Enabled = true;
                        return true;
                    }

                    repTimerType.focus = true;
                    return NextTimer();

                }else // Set break
                {
                    setCountIndex++;
                    if (setCountIndex < StaticFiles.TimerPageUISettings.sets)
                    {
                        if (setTimerType.active)
                        {
                            repCountIndex = 0;
                            setTimerType.focus = true;
                            totalTime = setTimerType.totalTime;
                            primaryLinePaint.Color = setTimerType.color.ToSKColor();
                            aTimer.Enabled = true;
                            return true;
                        }

                        setTimerType.focus = true;
                        return NextTimer();

                    }else
                    {
                        this.Stop();
                        setCountIndex = 0;
                    }
                }
            }
            else
            if (repTimerType.focus)
            {
                if (readyTimerType.active)
                {
                    repTimerType.focus = false;
                    readyTimerType.focus = true;
                    totalTime = readyTimerType.totalTime;
                    primaryLinePaint.Color = readyTimerType.color.ToSKColor();
                    aTimer.Enabled = true;
                    return true;
                }

                repTimerType.focus = false;
                readyTimerType.focus = true;
                return NextTimer();

            }else
            if (setTimerType.focus)
            {

                if (readyTimerType.active)
                {
                    setTimerType.focus = false;
                    readyTimerType.focus = true;
                    totalTime = readyTimerType.totalTime;
                    primaryLinePaint.Color = readyTimerType.color.ToSKColor();
                    aTimer.Enabled = true;
                    return true;
                }

                setTimerType.focus = false;
                readyTimerType.focus = true;
                return NextTimer();

            }

            return false;

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

        // EVENTS //

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
            primaryLinePaint.StrokeWidth = (int)(height * .25);
            secondaryLinePaint.StrokeWidth = (int)(height * .25);

            float completedTime = ((float)currentTime / (float)totalTime);
            int leftPoint = (int)(width * .25); // 100
            int rightPoint = (int)(width * .75); // 1080
            int totalPixelLeftToRight = rightPoint - leftPoint; // 980
            float totalReduction = totalPixelLeftToRight * completedTime;
            rightPoint = rightPoint - (int)totalReduction;

            //int rightPoint = (int)(width - (width / 1.05));
            canvas.DrawLine(new SKPoint(leftPoint, 0), new SKPoint((int)(width * .75), 0), secondaryLinePaint);
            canvas.DrawLine(new SKPoint(leftPoint, 0), new SKPoint(rightPoint, 0), primaryLinePaint);
            canvas.Restore();
            return;
        }

        //private int MilliSecond = 0;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            currentTime += elapsedMilliSec / 1000f;

            if (currentTime >= totalTime)
            {
                NextTimer();
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                this.InvalidateSurface();
            });
            //this.InvalidateSurface();
        }

        void BitmapCountDown_SizeChanged(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.InvalidateSurface();
            });
        }

    }
}

// 448
