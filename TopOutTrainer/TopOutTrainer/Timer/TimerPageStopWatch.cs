﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace TopOutTrainer
{


    public class TimerPageStopWatch
    {
        private struct TimerType
        {
            public string displayText;
            public int minute;
            public int second;
            public Color color;
            public bool active; // does the user have this set larger than 0
            public bool focus; // currently being used
        };

        private const int elapsedMilliSec = 10;
        private System.Timers.Timer aTimer = new System.Timers.Timer(elapsedMilliSec);
        private bool eventRunning = false;

        private string minuteString;
        private string secondString;

        private Label timeLabel = null;
        private Label descriptionLabel = null;
        private int repCountIndex = 0;
        private int setBreakIndex = 0;

        private TimerType readyTimerType;
        private TimerType startTimerType;
        private TimerType setTimerType;
        private TimerType repTimerType;
        private TimerType endTimerType;

        private Color currentColor;

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

            // Labels to be changed during lifecycle
            timeLabel = timeLabelP;
            descriptionLabel = descriptionLabelP;

            HandleTimerType();

            if(readyTimerType.active)
            {
                readyTimerType.focus = true;
                currentColor = readyTimerType.color;
                Minute = readyTimerType.minute;
                Second = readyTimerType.second;
                SetMinuteString(Minute);
                SetSecondString(Second);
                SetText(minuteString, secondString, readyTimerType);
            }
            else
            if(startTimerType.active)
            {
                startTimerType.focus = true;
                currentColor = startTimerType.color;
                Minute = startTimerType.minute;
                Second = startTimerType.second;
                SetMinuteString(Minute);
                SetSecondString(Second);
                SetText(minuteString, secondString, startTimerType);
            }
            else
            if (repTimerType.active)
            {
                repTimerType.focus = true;
                currentColor = repTimerType.color;
                Minute = repTimerType.minute;
                Second = repTimerType.second;
                SetMinuteString(Minute);
                SetSecondString(Second);
                SetText(minuteString, secondString, repTimerType);
            }
            else
            if (setTimerType.active)
            {
                setTimerType.focus = true;
                currentColor = setTimerType.color;
                Minute = setTimerType.minute;
                Second = setTimerType.second;
                SetMinuteString(Minute);
                SetSecondString(Second);
                SetText(minuteString, secondString, setTimerType);
            }
            else
            {
                this.Stop();
                return;
            }



        }

        public void Reset()
        {
            MilliSecond = 0;
            Second = 0;
            Minute = 0;
            repCountIndex = 0;
            setBreakIndex = 0;
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

        public void Stop()
        {
            aTimer.Enabled = false;
        }

        private void SetText(string minute, string second, TimerType selectedTimerType)
        {
            descriptionLabel.TextColor = currentColor;

            if(selectedTimerType.Equals(endTimerType))
            {
                descriptionLabel.Text = selectedTimerType.displayText;
                timeLabel.Text = "";
            }else
            {
                descriptionLabel.Text = selectedTimerType.displayText;
                timeLabel.Text = string.Concat(minute, ":", second);
            }
        }

        private void SetMinuteString(int minuteValue)
        {
            if (Minute <= 9)
            {
                minuteString = String.Concat('0', Minute);
            }
            else
            {
                minuteString = Minute.ToString();
            }
        }

        private void SetSecondString(int secondValue)
        {
            if (Second <= 9)
            {
                secondString = String.Concat('0', Second);
            }
            else
            {
                secondString = Second.ToString();
            }
        }

        private bool ChangeFocusTimerType()
        {
            // Move to next phase
            if (readyTimerType.focus)
            {
                readyTimerType.focus = false;
                startTimerType.focus = true;
                Minute = startTimerType.minute;
                Second = startTimerType.second - 1;
                currentColor = startTimerType.color;
                SetMinuteString(Minute);
                SetSecondString(Second);
                return true;
            }

            if(startTimerType.focus)
            {
                startTimerType.focus = false;

                // Either rep or set break
                repCountIndex++;
                if(repCountIndex <= StaticFiles.TimerPageUISettings.reps)
                {
                    repTimerType.focus = true;
                    Minute = repTimerType.minute;
                    Second = repTimerType.second - 1;
                    currentColor = repTimerType.color;
                    SetMinuteString(Minute);
                    SetSecondString(Second);
                    return true;
                }
                else // sets
                {
                    setBreakIndex++;
                    if (setBreakIndex < StaticFiles.TimerPageUISettings.sets)
                    {
                        repCountIndex = 0;
                        setTimerType.focus = true;
                        Minute = setTimerType.minute;
                        Second = setTimerType.second - 1;
                        currentColor = setTimerType.color;
                        SetMinuteString(Minute);
                        SetSecondString(Second);
                        return true;
                    }
                    else
                    {
                        this.Stop(); // No more sets- end process
                        return false;
                    }
                }
            }

            if(repTimerType.focus || setTimerType.focus)
            {
                repTimerType.focus = false;
                setTimerType.focus = false;
                readyTimerType.focus = true;
                Minute = readyTimerType.minute;
                Second = readyTimerType.second - 1;
                currentColor = readyTimerType.color;
                SetMinuteString(Minute);
                SetSecondString(Second);
                return true;
            }

            return false;
        }

        private void HandleTimerType()
        {
            if(StaticFiles.TimerPageUISettings.getReadyTime > 0)
            {
                readyTimerType.color = StaticFiles.ColorSettings.getReadyColor;
                readyTimerType.displayText = "Get Ready";
                readyTimerType.active = true;
                readyTimerType.focus = false;
                readyTimerType.minute = StaticFiles.TimerPageUISettings.getReadyTime / 60;
                readyTimerType.second = StaticFiles.TimerPageUISettings.getReadyTime % 60;
            }else
            {
                readyTimerType.color = StaticFiles.ColorSettings.getReadyColor;
                readyTimerType.displayText = "Get Ready";
                readyTimerType.active = false;
                readyTimerType.focus = false;
                readyTimerType.minute = StaticFiles.TimerPageUISettings.getReadyTime / 60;
                readyTimerType.second = StaticFiles.TimerPageUISettings.getReadyTime % 60;
            }

            if (StaticFiles.TimerPageUISettings.startTime > 0)
            {
                startTimerType.color = StaticFiles.ColorSettings.startColor;
                startTimerType.displayText = "Start";
                startTimerType.active = true;
                startTimerType.focus = false;
                startTimerType.minute = StaticFiles.TimerPageUISettings.startTime / 60;
                startTimerType.second = StaticFiles.TimerPageUISettings.startTime % 60;
            }else
            {
                startTimerType.color = StaticFiles.ColorSettings.startColor;
                startTimerType.displayText = "Start";
                startTimerType.active = false;
                startTimerType.focus = false;
                startTimerType.minute = StaticFiles.TimerPageUISettings.startTime / 60;
                startTimerType.second = StaticFiles.TimerPageUISettings.startTime % 60;
            }

            if (StaticFiles.TimerPageUISettings.reps > 0)
            {
                repTimerType.color = StaticFiles.ColorSettings.repBreakColor;
                repTimerType.displayText = "Rep Break";
                repTimerType.active = true;
                repTimerType.focus = false;
                repTimerType.minute = StaticFiles.TimerPageUISettings.repsRestTime / 60;
                repTimerType.second = StaticFiles.TimerPageUISettings.repsRestTime % 60;
            }else
            {
                repTimerType.color = StaticFiles.ColorSettings.repBreakColor;
                repTimerType.displayText = "Rep Break";
                repTimerType.active = false;
                repTimerType.focus = false;
                repTimerType.minute = StaticFiles.TimerPageUISettings.repsRestTime / 60;
                repTimerType.second = StaticFiles.TimerPageUISettings.repsRestTime % 60;
            }

            if (StaticFiles.TimerPageUISettings.sets > 0)
            {
                setTimerType.color = StaticFiles.ColorSettings.setBreakColor;
                setTimerType.displayText = "Set Break";
                setTimerType.active = true;
                setTimerType.focus = false;
                setTimerType.minute = StaticFiles.TimerPageUISettings.setsRestTime / 60;
                setTimerType.second = StaticFiles.TimerPageUISettings.setsRestTime % 60;
            }
            else
            {
                setTimerType.color = StaticFiles.ColorSettings.setBreakColor;
                setTimerType.displayText = "Set Break";
                setTimerType.active = false;
                setTimerType.focus = false;
                setTimerType.minute = StaticFiles.TimerPageUISettings.setsRestTime / 60;
                setTimerType.second = StaticFiles.TimerPageUISettings.setsRestTime % 60;
            }

            endTimerType.color = Color.White;
            endTimerType.displayText = "Finished";
            endTimerType.active = true;
            endTimerType.focus = false;

        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MilliSecond += elapsedMilliSec;

            if (MilliSecond >= 1000)
            {
                MilliSecond = 0;
                Second -= 1;

                SetSecondString(Second);

                if (Second < 0)
                {
                    Second = 59;
                    Minute -= 1;

                    secondString = "59";
                    SetMinuteString(Minute);
                }
            }

            // Build string and set it to object
            if (readyTimerType.focus)
            {
                // Done with get ready time
                if (Minute < 0)
                {

                    if (ChangeFocusTimerType())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText(minuteString, secondString, readyTimerType);
                        });
                    }

                }
                else
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText(minuteString, secondString, readyTimerType);
                    });
                }

            }else
            if (startTimerType.focus)
            {
                // Done with get ready start rep or set break
                if (Minute < 0)
                {

                    if (ChangeFocusTimerType())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText(minuteString, secondString, startTimerType);
                        });
                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText(minuteString, secondString, startTimerType);
                    });
                }
            }else
            if (repTimerType.focus)
            {
                if (Minute < 0)
                {

                    if(ChangeFocusTimerType())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText(minuteString, secondString, repTimerType);
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText(minuteString, secondString, repTimerType);
                    });
                }


            }else
            if (setTimerType.focus)
            {
                if (Minute < 0)
                {
                    if (ChangeFocusTimerType())
                    { 
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SetText(minuteString, secondString, setTimerType);
                        });
                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetText(minuteString, secondString, setTimerType);
                    });
                }
            }
        }
    }
}




