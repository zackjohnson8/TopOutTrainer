using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TopOutTrainer
{
    class StopWatch
    {

        private const int elapsedMilliSec = 1000;
        private System.Timers.Timer aTimer;
        private bool bStarted;

        public int Minute
        {
            private set;
            get;
        } = 0;

        public int Second
        {
            private set;
            get;
        } = 0;

        public int MilliSecond
        {
            private set;
            get;
        } = 0;

        // What does a stopwatch do? start, stop, reset, time, minute, second, millisecond
        public StopWatch()
        {
            
        }

        public void Stop()
        {
            aTimer.Enabled = false;
        }

        public void Start()
        {

            if (!bStarted)
            {
                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer();
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
                bStarted = true;
            }
            else
            {
                aTimer.Enabled = true;
            }
        }

        public void Reset()
        {

            MilliSecond = 0;
            Second = 0;
            Minute = 0;

            aTimer.Enabled = false;

        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            MilliSecond += elapsedMilliSec;

            if(MilliSecond >= 1000)
            {
                MilliSecond = 0;
                Second += 1;
            }

            if(Second >= 60)
            {
                Second = 0;
                Minute += 1;
            }

        }

    }
}




