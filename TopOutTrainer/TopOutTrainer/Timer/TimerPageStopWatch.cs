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
        private Label totalTimeLabel = null;
        private int totalTime;
        private int totalTimeMin;
        private int totalTimeSec;

        private Bitmap.BitmapCountDown bitmapAnimation;

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

        private bool onGetReady = false;
        private bool onStart = false;
        private bool onRepBreak = false;
        private bool onSetBreak = false;
        private bool onEnd = false;
        public TimerPageStopWatch(Label timeLabelP, Label totalTimeLabelP, Label descriptionLabelP, Bitmap.BitmapCountDown bitmapP)
        {
            // The user presses start ( waits 7 seconds while they get ready )
            // The app then begin ( waits 15 seconds as they do their workout )

            // Set label to change for timer

            timeLabel = timeLabelP;
            descriptionLabel = descriptionLabelP;
            totalTimeLabel = totalTimeLabelP;

            bitmapAnimation = bitmapP;

            // totalTime in seconds
            //int getReadyAndStart = StaticFiles.TimerPageUISettings.reps * StaticFiles.TimerPageUISettings.sets * (StaticFiles.TimerPageUISettings.getReadyTime + StaticFiles.TimerPageUISettings.startTime);
            //int breakReps = StaticFiles.TimerPageUISettings.reps * (StaticFiles.TimerPageUISettings.reps - 1);
            //int breakSets = StaticFiles.TimerPageUISettings.sets;
            //totalTime = getReadyAndStart + (breakReps * StaticFiles.TimerPageUISettings.repsRestTime) + (breakSets * StaticFiles.TimerPageUISettings.setsRestTime);

            if (StaticFiles.TimerPageUISettings.getReadyTime > 0)
            {
                onGetReady = true;
                Minute = StaticFiles.TimerPageUISettings.getReadyTime / 60;
                Second = StaticFiles.TimerPageUISettings.getReadyTime % 60;
                addedString = "Get Ready";

                bitmapP.UpdatePrimaryColor(StaticFiles.ColorSettings.getReadyColor);
                bitmapP.UpdateTotalTime((Minute * 60) + Second);
                bitmapP.UpdateCurrentTime(0);
                bitmapP.InvalidateSurface();
            }
            else
            if(StaticFiles.TimerPageUISettings.startTime > 0)
            {
                onStart = true;
                Minute = StaticFiles.TimerPageUISettings.startTime / 60;
                Second = StaticFiles.TimerPageUISettings.startTime % 60;
                addedString = "Start";
            }
            else
            if (StaticFiles.TimerPageUISettings.reps > 0)
            {
                onRepBreak = true;
                Minute = StaticFiles.TimerPageUISettings.repsRestTime / 60;
                Second = StaticFiles.TimerPageUISettings.repsRestTime % 60;
                addedString = "Rep Break";
            }
            else
            if (StaticFiles.TimerPageUISettings.sets > 0)
            {
                onSetBreak = true;
                Minute = StaticFiles.TimerPageUISettings.setsRestTime / 60;
                Second = StaticFiles.TimerPageUISettings.setsRestTime % 60;
                addedString = "Set Break";
            }
            else
            {
                addedString = "Finished!";
                onEnd = true;
                return;
            }

            Device.BeginInvokeOnMainThread(() =>
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

                SetText(String.Concat(minuteString, ':', secondString), addedString);
            });
            //totalTimeMin = totalTime / 60;
            //totalTimeSec = totalTime % 60;
            //Minute = getReadyTime / 60;
            //Second = getReadyTime % 60;

            //// Build string and set it to object
            //if (Second <= 9)
            //{
            //    secondString = String.Concat('0', Second);
            //}
            //else
            //{
            //    secondString = Second.ToString();
            //}

            //if (Minute <= 9)
            //{
            //    minuteString = String.Concat('0', Minute);
            //}
            //else
            //{
            //    minuteString = Minute.ToString();
            //}

            //addedString = "Get Ready";

            //if (timeLabel != null)
            //{

            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        SetText(String.Concat(minuteString, ':', secondString), "Get Ready");
            //    });
            //}

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
        
        private void ReduceTotalTime()
        {
            // No matter what the totalTime will reduce by one second
            // Build string and set it to object
            totalTimeSec -= 1;
            if(totalTimeSec < 0)
            {
                totalTimeSec = 59;
                totalTimeMin -= 1;
            }

            string ttSecondString;
            string ttMinuteString;
            if (Second <= 9)
            {
                ttSecondString = String.Concat('0', totalTimeSec);
            }
            else
            {
                ttSecondString = totalTimeSec.ToString();
            }

            if (Minute <= 9)
            {
                ttMinuteString = String.Concat('0', totalTimeMin);
            }
            else
            {
                ttMinuteString = totalTimeMin.ToString();
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                totalTimeLabel.Text = String.Concat(ttMinuteString, ':', ttSecondString);
            });
        }


        private int repCountIndex = 0;
        private int setBreakIndex = 0;
        private int bmCurrentTimeHolder = 0;
        private string addedString = null;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MilliSecond += elapsedMilliSec;
            bmCurrentTimeHolder += 1;

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
            if (onGetReady)
            {
                // Done with get ready time
                if (Minute < 0)
                {

                    bitmapAnimation.UpdateCurrentTime(0);
                    bitmapAnimation.InvalidateSurface();
                    // Move to next phase
                    onGetReady = false;
                    onStart = true;
                    addedString = "Start";
                    // Prep start
                    Minute = startTime / 60;
                    Second = (startTime % 60) - 1;

                    Device.BeginInvokeOnMainThread(() =>
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

                        SetText(String.Concat(minuteString, ':', secondString), addedString);
                    });

                    return;

                }
                else
                {
                    bitmapAnimation.UpdateCurrentTime(bmCurrentTimeHolder);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bitmapAnimation.InvalidateSurface();
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

                        SetText(String.Concat(minuteString, ':', secondString), addedString);
                    });

                    return;
                }

            }

            if (onStart)
            {

                // Done with get ready start rep or set break
                if (Minute < 0)
                {
                    onStart = false;
                    repCountIndex++;

                    // Determine if rep break or set break
                    if (repCountIndex <= repCount - 1)
                    {
                        Minute = repBreakTime / 60;
                        Second = (repBreakTime % 60) - 1;
                        onRepBreak = true;
                        addedString = "Rep Break";

                        Device.BeginInvokeOnMainThread(() =>
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

                            SetText(String.Concat(minuteString, ':', secondString), addedString);
                        });

                        return;

                    }
                    else // Set break
                    {

                        setBreakIndex++;
                        if (setBreakIndex < setCount)
                        {
                            Minute = setBreakTime / 60;
                            Second = (setBreakTime % 60) - 1;
                            onSetBreak = true;
                            addedString = "Set Break";
                            repCountIndex = 0;

                            Device.BeginInvokeOnMainThread(() =>
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
                                SetText(String.Concat(minuteString, ':', secondString), addedString);
                            });

                            return;
                        }
                        else
                        {
                            onEnd = true;
                        }
                    }
                }else
                {

                    Device.BeginInvokeOnMainThread(() =>
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

                        SetText(String.Concat(minuteString, ':', secondString), addedString);
                    });

                    return;
                }
            }

            if (onRepBreak)
            {
                if (Minute < 0)
                {
                    // Breaks done so get ready again
                    onRepBreak = false;
                    onGetReady = true;
                    addedString = "Get Ready";
                    Minute = getReadyTime / 60;
                    Second = (getReadyTime % 60) - 1;

                    Device.BeginInvokeOnMainThread(() =>
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

                        SetText(String.Concat(minuteString, ':', secondString), addedString);
                    });

                    return;
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
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

                        SetText(String.Concat(minuteString, ':', secondString), addedString);
                    });

                    return;
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
                    Second = (getReadyTime % 60) - 1;

                    Device.BeginInvokeOnMainThread(() =>
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

                        SetText(String.Concat(minuteString, ':', secondString), addedString);
                    });

                    return;

                } else
                {
                    Device.BeginInvokeOnMainThread(() =>
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

                        SetText(String.Concat(minuteString, ':', secondString), addedString);
                    });

                    return;
                }
            }

            if (onEnd)
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

        }
    }
}




