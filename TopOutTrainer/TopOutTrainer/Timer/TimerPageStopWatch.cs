using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public class TimerPageStopWatch
    {


        private const int elapsedMilliSec = 1000;
        private System.Timers.Timer aTimer = new System.Timers.Timer(elapsedMilliSec);
        private bool eventRunning = false;

        private string minuteString;
        private string secondString;

        private int getReadyTime = StaticFiles.TimerPageUISettings.getReadyTime; // Get Ready : default time of 7 seconds to get ready
        private int startTime = StaticFiles.TimerPageUISettings.startTime; // Start : give them 15 seconds to finish the set
        private int repBreakTime = StaticFiles.TimerPageUISettings.repsRestTime; // Rep Break : Take from rep time option
        private int setBreakTime = StaticFiles.TimerPageUISettings.setsRestTime; // Set Break : Take from set time option
        private int repCount = StaticFiles.TimerPageUISettings.reps; // Rep Count : Take from rep count option
        private int setCount = StaticFiles.TimerPageUISettings.sets; // Set Count : Take from set count option

        private Label timeLabel = null;
        private Label descriptionLabel = null;

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

        public TimerPageStopWatch(Label timeLabelP, Label descriptionLabelP)
        {
            // The user presses start ( waits 7 seconds while they get ready )
            // The app then begin ( waits 15 seconds as they do their workout )

            // Set label to change for timer
            timeLabel = timeLabelP;
            descriptionLabel = descriptionLabelP;


            Minute = getReadyTime / 60;
            Second = getReadyTime % 60;

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

            if (timeLabel != null)
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    SetText(String.Concat(minuteString, ':', secondString), "Get Ready");
                });
            }

        }


        public void AddLabelToDraw(Label timeLabelP, Label descriptionLabelP)
        {
            timeLabel = timeLabelP;
            descriptionLabel = descriptionLabelP;
        }

        public void Stop()
        {
            aTimer.Enabled = false;
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
            aTimer.Enabled = true;
        }

        public void Reset()
        {
            MilliSecond = 0;
            Second = 0;
            Minute = 0;

            if (timeLabel != null)
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    SetText(String.Concat("00", ':', "00"));
                });
            }

        }

        private void SetText(string timeText, string descriptionText = null)
        {
            timeLabel.Text = timeText;
            if (descriptionText != null)
            {
                descriptionLabel.Text = descriptionText;
            }
        }


        private bool onGetReady = true;
        private bool onStart = false;
        private bool onRepBreak = false;
        private bool onSetBreak = false;
        private bool onEnd = false;
        private int repCountIndex = 0;
        private int setBreakIndex = 0;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MilliSecond += elapsedMilliSec;

            if (MilliSecond >= 1000)
            {
                MilliSecond = 0;
                Second -= 1;
            }

            if (Second < 0)
            {
                Second = 59;
                Minute -= 1;
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

            if (onGetReady)
            {
                // Done with get ready time
                if( Minute < 0 )
                {
                    // Move to next phase
                    onGetReady = false;
                    onStart = true;
                    // Prep start
                    Minute = startTime / 60;
                    Second = startTime % 60;
                    MilliSecond = 0;
                    if (timeLabel != null)
                    {

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

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText(String.Concat(minuteString, ':', secondString), "Start");
                        });
                    }

                }
            }

            if(onStart)
            {

                // Done with get ready start rep or set break
                if ( Minute < 0)
                {
                    onStart = false;
                    repCountIndex++;

                    // Determine if rep break or set break
                    if(repCountIndex <= repCount - 1)
                    {
                        Minute = repBreakTime / 60;
                        Second = repBreakTime % 60;
                        onRepBreak = true;

                        if (timeLabel != null)
                        {

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

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                SetText(String.Concat(minuteString, ':', secondString), "Rep Break");
                            });

                        }
                    }
                    else // Set break
                    {

                        setBreakIndex++;
                        if(setBreakIndex < setCount)
                        {
                            Minute = setBreakTime / 60;
                            Second = setBreakTime % 60;
                            onSetBreak = true;
                            repCountIndex = 0;
                        }else
                        {
                            onEnd = true;
                        }

                        if (timeLabel != null)
                        {

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

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                SetText(String.Concat(minuteString, ':', secondString), "Set Break");
                            });

                        }
                    }

                }

            }

            if (onRepBreak)
            {
                if ( Minute < 0 )
                {
                    // Breaks done so get ready again
                    onRepBreak = false;
                    onGetReady = true;
                    Minute = getReadyTime / 60;
                    Second = getReadyTime % 60;


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

                    if (timeLabel != null)
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText(String.Concat(minuteString, ':', secondString), "Get Ready");
                        });
                    }

                }
            }

            if (onSetBreak)
            {
                if (Minute < 0)
                {
                    // Breaks done so get ready again
                    onSetBreak = false;
                    onGetReady = true;
                    Minute = getReadyTime / 60;
                    Second = getReadyTime % 60;


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

                    if (timeLabel != null)
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText(String.Concat(minuteString, ':', secondString), "Get Ready");
                        });
                    }

                }
            }

            if(onEnd)
            {
                // Breaks done so get ready again
                setBreakIndex = 0;

                if (timeLabel != null)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText("Finished!", "");
                    });
                }

                this.Stop();
                return;
            }


            Device.BeginInvokeOnMainThread(() =>
            {
                SetText(String.Concat(minuteString, ':', secondString));
            });
            

        }
    }
}




