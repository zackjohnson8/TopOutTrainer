using System;
using System.Diagnostics;
using System.Threading;
using Xamarin.Forms;

namespace TopOutTrainer.Timer
{
    public class TimerHandler
    {
        StopWatch totalTime;
        TimerPageStopWatch phaseTime;
        Bitmap.BitmapCountDown bitmapTime;

        Thread totalTimeThread;
        Thread phaseTimeThread;
        Thread bitmapTimeThread;

        private bool isRunning = false; 

        public TimerHandler(StopWatch stopWatchP, TimerPageStopWatch timerPageStopWatchP, Bitmap.BitmapCountDown bitmapCountDownP)
        {
            totalTime = stopWatchP;
            phaseTime = timerPageStopWatchP;
            bitmapTime = bitmapCountDownP;

            totalTimeThread = new Thread(totalTime.Start);
            bitmapTimeThread = new Thread(bitmapTime.Start);
            phaseTimeThread = new Thread(phaseTime.Start);
        }

        public void Start()
        {
            isRunning = true;
            totalTimeThread.Start();
            bitmapTimeThread.Start();
            phaseTimeThread.Start();
        }

        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                //totalTimeThread.Abort();
                //bitmapTimeThread.Abort();
                //phaseTimeThread.Abort();
                totalTime.Stop();
                phaseTime.Stop();
                bitmapTime.Stop();
            }
            
        }

        public void Pause()
        {
            //totalTimeThread.Interrupt();
            //bitmapTimeThread.Interrupt();
            //phaseTimeThread.Interrupt();
            totalTime.Pause();
            bitmapTime.Pause();
            phaseTime.Pause();
        }

        public void Resume()
        {
            totalTime.Resume();
            bitmapTime.Resume();
            phaseTime.Resume();
        }

        public bool TimerComplete()
        {
            return phaseTime.IsComplete;
        }

        public void Reset()
        {
            totalTime.Reset();
            bitmapTime.Reset();
            phaseTime.Reset();

            if(TimerComplete())
            {
                //this.Start();
                totalTime.Start();
                bitmapTime.Start();
                phaseTime.Start();
                phaseTime.IsComplete = false;
            }
        }
    }
}
