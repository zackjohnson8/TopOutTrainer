using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public class StopWatch
    {

        public enum CountDirection
        {
            COUNTUP,
            COUNTDOWN
        }

        private const int elapsedMilliSec = 10;
        private System.Timers.Timer aTimer = new System.Timers.Timer(elapsedMilliSec);
        private bool eventRunning = false;
        private CountDirection countChoice = CountDirection.COUNTUP;
        private int timerDuration;

        private string minuteString;
        private string secondString;

        private bool StartComplete { get; set; } = false;

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

        public StopWatch(Label defaultObj, CountDirection choice, int duration)
        {
            mainObjectLabel = defaultObj;
            countChoice = choice;
            timerDuration = duration;

            if(choice == CountDirection.COUNTDOWN)
            {
                Minute = duration / 60;
                Second = duration % 60;

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
                }
                else
                {
                    minuteString = Minute.ToString();
                }

                if (mainObjectLabel != null)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText(String.Concat(minuteString, ':', secondString));
                    });
                }

            }



        }

        public void AddLabelToDraw(Label defaultObj)
        {
            mainObjectLabel = defaultObj;
        }

        public void Stop()
        {
            aTimer.Enabled = false;
        }

        public void Pause()
        {
            aTimer.Enabled = false;
        }

        public void Resume()
        {
            aTimer.Enabled = true;
        }

        public void Start()
        {
            // Hook up the Elapsed event for the timer. 
            if (!eventRunning)
            {
                aTimer.Elapsed += OnTimedEvent;
                eventRunning = true;
            }
            aTimer.AutoReset = true;

            Timer.TimerLock.UnlockTotalTimeLocker();
            while (!Timer.TimerLock.ReadyCheck()) { }
            aTimer.Enabled = true;
        }

        public void Reset()
        {
            //aTimer.Enabled = false;
            MilliSecond = 0;


            Minute = timerDuration / 60;
            Second = timerDuration % 60;

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
            }
            else
            {
                minuteString = Minute.ToString();
            }

            if (mainObjectLabel != null)
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    SetText(String.Concat(minuteString, ':', secondString));
                });
            }

            //aTimer.Enabled = true;
        }
        
        private void SetText(string myString)
        {
            mainObjectLabel.Text = myString;
        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (countChoice == CountDirection.COUNTUP)
            {
                MilliSecond += elapsedMilliSec;

                if (MilliSecond >= 1000)
                {
                    MilliSecond = 0;
                    Second += 1;
                }

                if (Second >= 60)
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
                }
                else
                {
                    minuteString = Minute.ToString();
                }

                if (mainObjectLabel != null)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText(String.Concat(minuteString, ':', secondString));
                    });
                }
            }else
            {
                MilliSecond += elapsedMilliSec;

                if (MilliSecond >= 1000)
                {
                    MilliSecond = 0;
                    Second -= 1;
                }else
                {
                    return;
                }

                if (Second < 0)
                {
                    Second = 59;
                    Minute -= 1;
                }

                if (Minute < 0)
                {

                    if (mainObjectLabel != null)
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText("00:00");
                        });
                    }
                    this.Stop();
                    return;
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
                }
                else
                {
                    minuteString = Minute.ToString();
                }

                if (mainObjectLabel != null)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText(String.Concat(minuteString, ':', secondString));
                    });
                }
            }

        }

    }
}




