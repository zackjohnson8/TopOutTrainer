using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public static class StopWatch
    {

        private const int elapsedMilliSec = 1000;
        private static System.Timers.Timer aTimer = new System.Timers.Timer(elapsedMilliSec);
        private static bool eventRunning = false;


        private static Label mainObjectLabel = null;

        public static int Minute
        {
            private set;
            get;
        } = 0;

        public static int Second
        {
            private set;
            get;
        } = 0;

        public static int MilliSecond
        {
            private set;
            get;
        } = 0;

        public static void AddLabelToDraw(Label defaultObj)
        {
            mainObjectLabel = defaultObj;
        }

        public static void Stop()
        {
            aTimer.Enabled = false;
        }

        public static void Start()
        {
            // Hook up the Elapsed event for the timer. 
            if (!eventRunning)
            {
                aTimer.Elapsed += OnTimedEvent;
                eventRunning = true;
            }
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static void Reset()
        {
            MilliSecond = 0;
            Second = 0;
            Minute = 0;

            if (mainObjectLabel != null)
            {

                Device.BeginInvokeOnMainThread(() => {
                    SetText(String.Concat("00", ':', "00"));
                });
            }

        }
        
        private static void SetText(string myString)
        {
            mainObjectLabel.Text = myString;
        }

        private static string minuteString;
        private static string secondString;
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
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




