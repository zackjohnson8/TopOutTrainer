using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TopOutTrainer
{
    class StopWatch
    {

        private static System.Timers.Timer aTimer;

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

        public void Start()
        {

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer();
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }


    }
}




