﻿using System;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TopOutTrainer.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
        //private TopOutTrainer.ContentViews.TimerPageSettings myTimerSettings;

        private Color mainColor = Color.FromHex("#303030");
        private Color textColor = Color.White;
        private Grid mainG;
        private ImageButton optionB;
        private Label intervalL;
        private Label repsL;
        private Label setsL;
        private Label totalTimeL;
        private Label timerNumL;
        private Label repsNumL;
        private Label setsNumL;

        private Label getReadyL;
        private Label timerL;

        private TimerPageStopWatch countDownTimer;
        private StopWatch totalTimeTimer;

        //private Image bitmapI;
        //private StackLayout bitmapContainer;
        //private BmpMaker bmpMaker;

        // TODO default button names until determined
        private ImageButton timerButton;
        private ImageButton calendarButton;
        private ImageButton graphButton;
        private ImageButton button4;
        private ImageButton startbutton;

        private Bitmap.BitmapCountDown bitmapView;


        public TimerPage()
        { 

            // Hide nav bar and begin building of contentpage
            NavigationPage.SetHasNavigationBar(this, false);
            GridChildrenInitialize();
            MainGridInitialize();

            // When size is determined have an event that fixes sizes based on phone of choice
            SizeChanged += OnSizeChanged;

            // Load screen 
            Content = mainG;

        }

        private async Task<bool> GetSavedData()
        {
            await StaticFiles.TimerPageUISettings.SetFromFile();
            return true;
        }

        private void RefreshContent()
        {

            // Set reps and sets for change
            repsNumL.Text = StaticFiles.TimerPageUISettings.reps.ToString();
            setsNumL.Text = StaticFiles.TimerPageUISettings.sets.ToString();

            // Set timerNumL with new total minutes and seconds
            // Each rep and set break follows with a get ready and start time
            int getReadyAndStart = StaticFiles.TimerPageUISettings.reps * StaticFiles.TimerPageUISettings.sets * (StaticFiles.TimerPageUISettings.getReadyTime + StaticFiles.TimerPageUISettings.startTime);

            // Number of breaks we'll be taking both rep and set total time
            int breakReps = StaticFiles.TimerPageUISettings.reps * (StaticFiles.TimerPageUISettings.reps - 1);
            int breakSets = StaticFiles.TimerPageUISettings.sets - 1;

            int totalTime = getReadyAndStart + (breakReps * StaticFiles.TimerPageUISettings.repsRestTime) + (breakSets * StaticFiles.TimerPageUISettings.setsRestTime);
            int totalTimeMinutes = totalTime / 60;
            int totalTimeSeconds = totalTime % 60;

            string timeMin;
            if (totalTimeMinutes <= 9)
            {
                timeMin = string.Concat('0' + totalTimeMinutes.ToString());
            }
            else
            {
                timeMin = totalTimeMinutes.ToString();
            }

            string timeSec;
            if (totalTimeSeconds <= 9)
            {
                timeSec = string.Concat('0' + totalTimeSeconds.ToString());
            }
            else
            {
                timeSec = totalTimeSeconds.ToString();
            }
            timerNumL.Text = string.Concat(timeMin + ':' + timeSec);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh all content that exists with saved file when appeared
            // This takes care of the case of coming back from TimerPageSetting contentpage
            RefreshContent();

        }

        private void MainGridInitialize()
        {
            mainG = new Grid
            {
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                BackgroundColor = mainColor,
                RowDefinitions =
                {
                    // 5 Rows
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.5, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(5.75, GridUnitType.Star) },
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
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Filler
            mainG.Children.Add(new StackLayout { BackgroundColor = mainColor }, 0, 0);
            mainG.Children.Add(new StackLayout { BackgroundColor = mainColor }, 1, 0);
            mainG.Children.Add(new StackLayout { BackgroundColor = mainColor }, 2, 0);
            //

            mainG.Children.Add(optionB, 3, 0);

            mainG.Children.Add(intervalL, 0, 1);
            Grid.SetColumnSpan(intervalL, 4);

            mainG.Children.Add(repsL, 0, 2);
            mainG.Children.Add(totalTimeL, 1, 2);
            Grid.SetColumnSpan(totalTimeL, 2);
            mainG.Children.Add(setsL, 3, 2);

            mainG.Children.Add(repsNumL, 0, 3);
            mainG.Children.Add(timerNumL, 1, 3);
            Grid.SetColumnSpan(timerNumL, 2);
            mainG.Children.Add(setsNumL, 3, 3);

            mainG.Children.Add(startbutton, 0, 4);
            Grid.SetColumnSpan(startbutton, 4);

            mainG.Children.Add(timerButton, 0, 5);
            mainG.Children.Add(calendarButton, 1, 5);
            mainG.Children.Add(graphButton, 2, 5);
            mainG.Children.Add(button4, 3, 5);

        }

        private void GridChildrenInitialize()
        {

            // Row 0 right (0, 4) settings button
            optionB = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                Margin = 0,
                CornerRadius = 0,
                BackgroundColor = mainColor,
                Source = "options_gimp_white.png",
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill

            };
            optionB.Clicked += OptionButtonClicked;

            // Row 0 left (0,0) leave blank for now TODO

            // Row 1 interval training label
            intervalL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                Text = "Interval Training",
                BackgroundColor = mainColor,
            };

            // Row 2 reps label
            repsL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "Reps"
            };

            // Row 2 total time label
            totalTimeL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "Total Time"
            };

            timerNumL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "00:00"
            };

            setsNumL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = StaticFiles.TimerPageUISettings.sets.ToString()

            };


            repsNumL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = StaticFiles.TimerPageUISettings.reps.ToString()
            };

            // Row 2 sets label
            setsL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "Sets"
            };

            getReadyL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                TextColor = Color.FromHex("#FF6600"),
                BackgroundColor = mainColor,
                Text = "Get Ready"
            };

            timerL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "00:00"
            };

            //Font-Family
            if (Device.RuntimePlatform == Device.iOS)
            {
                intervalL.FontFamily = "OpenSans-Regular";
                repsL.FontFamily = "OpenSans-Regular";
                totalTimeL.FontFamily = "OpenSans-Regular";
                setsL.FontFamily = "OpenSans-Regular";
                timerNumL.FontFamily = "OpenSans-Regular";
                repsNumL.FontFamily = "OpenSans-Regular";
                setsNumL.FontFamily = "OpenSans-Regular";
                getReadyL.FontFamily = "OpenSans-Regular";
                timerL.FontFamily = "OpenSans-Regular";

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


            // Row 3,4 BITMAP
            //bitmapContainer = new StackLayout
            //{
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    BackgroundColor = mainColor,
            //    Orientation = StackOrientation.Horizontal,
            //};

            startbutton = new ImageButton
            {
                BackgroundColor = mainColor,
                Margin = 0,
                Source = "start.png",
                Aspect = Aspect.AspectFit
            };
            startbutton.Clicked += StartButtonClicked;

            // Row 5 tab bar buttons
            // (4,0)
            timerButton = new ImageButton
            {

                BackgroundColor = Color.FromHex("#D3EFFC"),
                Margin = 0,
                CornerRadius = 0,
                Source = "stopwatch_white_trans.png",
                Aspect = Aspect.AspectFit

            };
            
            // (4,1)
            calendarButton = new ImageButton
            {
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "calendar.png",
                Aspect = Aspect.AspectFit


            };
            calendarButton.Clicked += PlannerButtonClicked;
            // (4,2)
            graphButton = new ImageButton
            {
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "graph_white.png",
                Aspect = Aspect.AspectFit

            };
            graphButton.Clicked += GraphButtonClicked;
            // (4,3)
            button4 = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0

            };


        }

        private void ResetAll()
        {
            try
            {
                countDownTimer.Stop();
                totalTimeTimer.Stop();
                bitmapView.Stop();
            }
            catch(Exception e)
            {
            }


            // Stop the timer. Reset the timer. Load the grid.
            GridChildrenInitialize();
            MainGridInitialize();
            Content = mainG;
        }

        private async void OptionButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new TimerPageSettings() { Title = "Settings" });

            // Reset all
            ResetAll();
        }

        private void GraphButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new GraphPage());

            ResetAll();
        }

        private void PlannerButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new PlannerPage());

            ResetAll();
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
                BackgroundColor = mainColor,
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
                },
                RowSpacing = 0,
                ColumnSpacing = 0,
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand
            };

            getReadyG.Children.Add(getReadyL, 0, 0);
            getReadyG.Children.Add(timerL, 0, 1);

            // The bitmap would be 0, 2 and 0, 3
            int getReadyAndStart = StaticFiles.TimerPageUISettings.reps * StaticFiles.TimerPageUISettings.sets * (StaticFiles.TimerPageUISettings.getReadyTime + StaticFiles.TimerPageUISettings.startTime);
            int breakReps = StaticFiles.TimerPageUISettings.reps * (StaticFiles.TimerPageUISettings.reps - 1);
            int breakSets = StaticFiles.TimerPageUISettings.sets - 1;
            int totalTime = getReadyAndStart + (breakReps * StaticFiles.TimerPageUISettings.repsRestTime) + (breakSets * StaticFiles.TimerPageUISettings.setsRestTime);


            //BmpMaker bitMapLine = new BmpMaker((int)this.Width/16, (int)this.Height/16);
            //getReadyG.Children.Add(new StackLayout().Children.Add(bitMapLine.Generate()), 0, 2);
            //Grid.SetColumnSpan(bitMapLine.Generate(), 2);
            bitmapView = new Bitmap.BitmapCountDown(totalTime, Color.FromHex("#FF6600"), Color.White);
            Thread bitmapViewThread = new Thread(bitmapView.Start);
            //bitmapView.Start();

            getReadyG.Children.Add(bitmapView, 0, 2);
            Grid.SetRowSpan(bitmapView, 2);

            mainG.Children.Add(getReadyG, 0, 4);
            Grid.SetColumnSpan(getReadyG, 4);



            totalTimeTimer = new StopWatch(timerNumL, StopWatch.CountDirection.COUNTDOWN, totalTime);
            Thread totalTimeThread = new Thread(totalTimeTimer.Start);

            // TODO(zack): Combine the two timers into the TimerPageStopWatch.
            //              This should allow for consistent count on both timers
            countDownTimer = new TimerPageStopWatch(timerL, getReadyL);
            Thread countDownTimerThread = new Thread(countDownTimer.Start);
            //countDownTimer.Start();

            bitmapViewThread.Start();
            totalTimeThread.Start();
            countDownTimerThread.Start();


        }


        private async void OnSizeChanged(object sender, EventArgs e)
        {

            await GetSavedData();
            RefreshContent();

            // Handle button sizing
            startbutton.WidthRequest = startbutton.Height;
            startbutton.HeightRequest = startbutton.Height;
            startbutton.CornerRadius = (int)startbutton.Height / 2;


            // Handle sizing of labels based on screen size
            if (this.Width > 0)
            {
                // Numbers
                timerNumL.FontSize = this.Width / 8;
                repsNumL.FontSize = this.Width / 8;
                setsNumL.FontSize = this.Width / 8;

               
                getReadyL.FontSize = this.Width / 8;
                timerL.FontSize = this.Width / 8;
             

                // 
                intervalL.FontSize = this.Width / 12;
                repsL.FontSize = this.Width / 14;
                setsL.FontSize = this.Width / 14;
                totalTimeL.FontSize = this.Width / 14;
            }
            //Create_BitMap();
        }

    }
}