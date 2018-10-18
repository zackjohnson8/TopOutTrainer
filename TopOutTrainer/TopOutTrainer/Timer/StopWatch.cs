using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace TopOutTrainer
{
    class StopWatch
    {

        private const int elapsedMilliSec = 1000;
        private System.Timers.Timer aTimer;
        private bool bStarted;

        private Label mainObjectLabel = null;

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
        public StopWatch(Label defaultObj)
        {
            mainObjectLabel = defaultObj;
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
                aTimer = new System.Timers.Timer(elapsedMilliSec);
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
        
        private void SetText(string myString)
        {
            mainObjectLabel.Text = myString;
        }

        string minuteString;
        string secondString;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
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

            // Build string and set it to object
            if (Second <= 9)
            {
                secondString = String.Concat('0', Second);
            }
            else
            {
                secondString = Second.ToString();
            }

            if (Minute <= 9)
            {
                minuteString = String.Concat('0', Minute);
            }else
            {
                minuteString = Minute.ToString();
            }

            if(mainObjectLabel != null)
            {

                Device.BeginInvokeOnMainThread(() => {
                    SetText(String.Concat(minuteString, ':', secondString));
                });
            }


        }

    }
}




