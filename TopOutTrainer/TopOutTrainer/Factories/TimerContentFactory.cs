using System;
using Xamarin.Forms;

namespace TopOutTrainer.Factories
{
    public class TimerContentFactory
    {

        public ImageButton optionB { get; private set; } = null;
        public Label intervalL { get; private set; } = null;
        public Label repsL { get; private set; } = null;
        public Label setsL { get; private set; } = null;
        public Label totalTimeL { get; private set; } = null;
        public Label timerNumL { get; private set; } = null;
        public Label repsNumL { get; private set; } = null;
        public Label setsNumL { get; private set; } = null;
        public Label getReadyL { get; private set; } = null;
        public Label timerL { get; private set; } = null;
        public ImageButton timerButton { get; private set; } = null;
        public ImageButton calendarButton { get; private set; } = null;
        public ImageButton graphButton { get; private set; } = null;
        public ImageButton button4 { get; private set; } = null;
        public ImageButton startbutton { get; private set; } = null;
        public ImageButton stopButton { get; private set; } = null;
        public ImageButton resetButton { get; private set; } = null;

        public TimerPageStopWatch countDownTimer { get; private set; } = null;
        public StopWatch totalTimeTimer { get; private set; } = null;
        public Bitmap.BitmapCountDown bitmapView { get; private set; } = null;
        public Timer.TimerHandler timerHandler { get; private set; } = null;


        private Grid mainG = null;

        public View CreateContentFirstView()
        {

            CreateButtons();
            CreateLabels();
            CreateGrid();
            PopulateGrid();

            return mainG;

        }

        private void CreateButtons()
        {

            // Row 0 right (0, 4) settings button
            if (optionB == null)
            {
                optionB = new ImageButton
                {
                    WidthRequest = 50,
                    HeightRequest = 50,
                    Margin = 0,
                    CornerRadius = 0,
                    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                    Source = "options_gimp_white.png",
                    //HorizontalOptions = LayoutOptions.CenterAndExpand,
                    //VerticalOptions = LayoutOptions.CenterAndExpand,
                    Aspect = Aspect.AspectFill

                };
                
            }

            

            if (stopButton == null)
            {
                stopButton = new ImageButton
                {
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Margin = 0,
                    Source = "pause_white.png",
                    BorderColor = Color.White,
                    BorderWidth = 2,
                    Aspect = Aspect.AspectFit
                };
                stopButton.Clicked += StopButton_Clicked;
            }
            else
            {
                stopButton.Source = "pause_white.png";
                if (stopbool)
                {
                    stopbool = false;
                }
            }

            if (resetButton == null)
            {
                resetButton = new ImageButton
                {
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Margin = 0,
                    Source = "reset_white.png",
                    BorderColor = Color.White,
                    BorderWidth = 2,
                    Aspect = Aspect.AspectFit
                };
                resetButton.Clicked += ResetButton_Clicked;
            }

            if (startbutton == null)
            {
                startbutton = new ImageButton
                {
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Margin = 0,
                    Source = "start_orange.png",
                    Aspect = Aspect.AspectFit
                };
                startbutton.Clicked += StartButtonClicked;
            }

            // Row 5 tab bar buttons
            // (4,0)
            if (timerButton == null)
            {
                timerButton = new ImageButton
                {

                    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                    Margin = 0,
                    CornerRadius = 0,
                    Source = "stopwatch_orange_trans.png",
                    Aspect = Aspect.AspectFit

                };
            }

            // (4,1)
            if (calendarButton == null)
            {
                calendarButton = new ImageButton
                {
                    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                    Margin = 0,
                    CornerRadius = 0,
                    Source = "calendar_white.png",
                    Aspect = Aspect.AspectFit


                };
                
            }

            // (4,2)
            if (graphButton == null)
            {
                graphButton = new ImageButton
                {
                    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                    Margin = 0,
                    CornerRadius = 0,
                    //Source = "graph_white.png",
                    Aspect = Aspect.AspectFit

                };
            }
            // (4,3)
            if (button4 == null)
            {
                button4 = new ImageButton
                {
                    WidthRequest = 50,
                    HeightRequest = 50,
                    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                    Margin = 0,
                    CornerRadius = 0

                };
            }

        }

        private void CreateLabels()
        {
            // Row 1 interval training label
            if (intervalL == null)
            {
                intervalL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    Text = "Interval Training",
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                };
            }

            // Row 2 reps label
            if (repsL == null)
            {
                repsL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = "Reps"
                };
            }

            // Row 2 total time label
            if (totalTimeL == null)
            {
                totalTimeL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = "Total Time"
                };
            }

            if (timerNumL == null)
            {
                timerNumL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = "00:00"
                };
            }

            if (setsNumL == null)
            {
                setsNumL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = StaticFiles.TimerPageUISettings.sets.ToString()

                };
            }

            if (repsNumL == null)
            {
                repsNumL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = StaticFiles.TimerPageUISettings.reps.ToString()
                };
            }

            // Row 2 sets label
            if (setsL == null)
            {
                setsL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = "Sets"
                };
            }

            if (getReadyL == null)
            {
                getReadyL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start,
                    TextColor = Color.FromHex("#FF6600"),
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = "Get Ready"
                };
            }

            if (timerL == null)
            {
                timerL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = StaticFiles.ColorSettings.textColorWhite,
                    BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                    Text = "00:00"
                };
            }

            //Font-Family
            if (Device.RuntimePlatform == Device.iOS)
            {
                intervalL.FontFamily = "MontserratAlternates-Bold";
                repsL.FontFamily = "MontserratAlternates-Bold";
                totalTimeL.FontFamily = "MontserratAlternates-Bold";
                setsL.FontFamily = "MontserratAlternates-Bold";
                timerNumL.FontFamily = "MontserratAlternates-Bold";
                repsNumL.FontFamily = "MontserratAlternates-Bold";
                setsNumL.FontFamily = "MontserratAlternates-Bold";
                getReadyL.FontFamily = "MontserratAlternates-Bold";
                timerL.FontFamily = "MontserratAlternates-Bold";

            }
            if (Device.RuntimePlatform == Device.Android)
            {
                intervalL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                repsL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                totalTimeL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                setsL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                timerNumL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                repsNumL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                setsNumL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                getReadyL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
                timerL.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold";
            }
        }

        private void CreateGrid()
        {

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    mainG = new Grid
                    {
                        Padding = new Thickness(0),
                        Margin = new Thickness(0),
                        BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                        RowDefinitions =
                        {
                            // 5 Rows
                            new RowDefinition { Height = new GridLength(1.4, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(1.5, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(5.95, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        },
                        RowSpacing = 0,
                        ColumnSpacing = 0,
                    };
                    break;
                case Device.Android:
                    mainG = new Grid
                    {
                        Padding = new Thickness(0),
                        Margin = new Thickness(0),
                        BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                        RowDefinitions =
                        {
                            // 5 Rows
                            new RowDefinition { Height = new GridLength(.8, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(1.5, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(5.95, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        },
                        RowSpacing = 0,
                        ColumnSpacing = 0,
                    };
                    break;
            }

        }

        private void PopulateGrid()
        {
            // Fill empty grid squares
            mainG.Children.Add(new StackLayout { BackgroundColor = StaticFiles.ColorSettings.darkGrayColor }, 0, 0);
            mainG.Children.Add(new StackLayout { BackgroundColor = StaticFiles.ColorSettings.darkGrayColor }, 1, 0);
            mainG.Children.Add(new StackLayout { BackgroundColor = StaticFiles.ColorSettings.darkGrayColor }, 2, 0);

            // Option button
            mainG.Children.Add(optionB, 3, 0);

            // "Interval Training" Label
            mainG.Children.Add(intervalL, 0, 1);
            Grid.SetColumnSpan(intervalL, 4);

            // "Reps" Label
            mainG.Children.Add(repsL, 0, 2);

            // "Total Time" Label
            mainG.Children.Add(totalTimeL, 1, 2);
            Grid.SetColumnSpan(totalTimeL, 2);

            // "Sets" Label
            mainG.Children.Add(setsL, 3, 2);

            // int of reps
            mainG.Children.Add(repsNumL, 0, 3);

            // total time to finish full workout
            mainG.Children.Add(timerNumL, 1, 3);
            Grid.SetColumnSpan(timerNumL, 2);

            // int of sets
            mainG.Children.Add(setsNumL, 3, 3);

            // a background and start button
            BoxView backBoxView = new BoxView() { BackgroundColor = StaticFiles.ColorSettings.mainGrayColor };
            mainG.Children.Add(backBoxView, 0, 4);
            Grid.SetColumnSpan(backBoxView, 4);
            mainG.Children.Add(startbutton, 0, 4);
            Grid.SetColumnSpan(startbutton, 4);

            // Bottom navigation bar
            mainG.Children.Add(timerButton, 0, 5);
            mainG.Children.Add(calendarButton, 1, 5);
            mainG.Children.Add(graphButton, 2, 5);
            mainG.Children.Add(button4, 3, 5);
        }

        public void CreateContentSecondView()
        {


        }

        private void StartButtonClicked(object sender, EventArgs args)
        {
            // Remove previous content
            mainG.Children.Remove(startbutton);

            // Add new content
            // List: Get Ready, Start, Rep Break, Set Break or Final Break

            Grid getReadyG = new Grid
            {
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                RowDefinitions =
                {
                    // 5 Rows
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },

                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowSpacing = 0,
                ColumnSpacing = 0,
            };

            // Total time calculations
            int getReadyAndStart = StaticFiles.TimerPageUISettings.reps * StaticFiles.TimerPageUISettings.sets * (StaticFiles.TimerPageUISettings.getReadyTime + StaticFiles.TimerPageUISettings.startTime);
            int breakReps = StaticFiles.TimerPageUISettings.reps * (StaticFiles.TimerPageUISettings.reps - 1);
            int breakSets = StaticFiles.TimerPageUISettings.sets - 1;
            int totalTime = getReadyAndStart + (breakReps * StaticFiles.TimerPageUISettings.repsRestTime) + (breakSets * StaticFiles.TimerPageUISettings.setsRestTime);

            getReadyG.Children.Add(getReadyL, 0, 0);
            getReadyG.Children.Add(timerL, 0, 1);
            Grid.SetColumnSpan(getReadyL, 2);
            Grid.SetColumnSpan(timerL, 2);

            bitmapView = new Bitmap.BitmapCountDown();
            getReadyG.Children.Add(bitmapView, 0, 2);
            Grid.SetColumnSpan(bitmapView, 2);

            //Add buttons for Stop / Reset
            getReadyG.Children.Add(stopButton, 0, 3);
            getReadyG.Children.Add(resetButton, 1, 3);

            mainG.Children.Add(getReadyG, 0, 4);
            Grid.SetColumnSpan(getReadyG, 4);

            totalTimeTimer = new StopWatch(timerNumL, StopWatch.CountDirection.COUNTDOWN, totalTime);
            countDownTimer = new TimerPageStopWatch(timerL, getReadyL);
            timerHandler = new Timer.TimerHandler(totalTimeTimer, countDownTimer, bitmapView);
            timerHandler.Start();

        }

        bool stopbool = false;
        void StopButton_Clicked(object sender, EventArgs e)
        {
            if (timerHandler.TimerComplete())
            {
                return;
            }


            if (!stopbool)
            {
                stopbool = !stopbool;
                timerHandler.Pause();
                Device.BeginInvokeOnMainThread(() =>
                {
                    stopButton.Source = "start_white_text.png";
                });
            }
            else
            {
                stopbool = !stopbool;
                timerHandler.Resume();
                Device.BeginInvokeOnMainThread(() =>
                {
                    stopButton.Source = "pause_white.png";
                });
            }
        }

        void ResetButton_Clicked(object sender, EventArgs e)
        {
            timerHandler.Reset();
        }
    }
}
