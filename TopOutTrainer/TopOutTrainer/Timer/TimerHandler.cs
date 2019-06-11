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

        public TimerHandler(StopWatch stopWatchP, TimerPageStopWatch timerPageStopWatchP, Bitmap.BitmapCountDown bitmapCountDownP)
        {
            totalTime = stopWatchP;
            phaseTime = timerPageStopWatchP;
            bitmapTime = bitmapCountDownP;
        }

        public void Start()
        {
            totalTimeThread = new Thread(totalTime.Start);
            bitmapTimeThread = new Thread(bitmapTime.Start);
            phaseTimeThread = new Thread(phaseTime.Start);

            totalTimeThread.Start();
            bitmapTimeThread.Start();
            phaseTimeThread.Start();

        }

        public void Stop()
        {
            try
            {
                totalTimeThread.Abort();
                bitmapTimeThread.Abort();
                phaseTimeThread.Abort();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("Error: Reseting all");
            }
        }
    }
}
