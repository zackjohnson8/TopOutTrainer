using System;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace TopOutTrainer.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
        //private TopOutTrainer.ContentViews.TimerPageSettings myTimerSettings;

        private Color mainColor = Color.FromHex("#303030");
        private Color textColor = Color.White;
        private Grid mainG = null;
        private ImageButton optionB = null;
        private Label intervalL = null;
        private Label repsL = null;
        private Label setsL = null;
        private Label totalTimeL = null;
        private Label timerNumL = null;
        private Label repsNumL = null;
        private Label setsNumL = null;

        private Label getReadyL = null;
        private Label timerL = null;

        private TimerPageStopWatch countDownTimer = null;
        private StopWatch totalTimeTimer = null;
        private Bitmap.BitmapCountDown bitmapView = null;
        private Timer.TimerHandler timerHandler = null;

        private ImageButton timerButton = null;
        private ImageButton calendarButton = null;
        private ImageButton graphButton = null;
        private ImageButton button4 = null;
        private ImageButton startbutton = null;
        private ImageButton stopButton = null;
        private ImageButton resetButton = null;

        // Pause, Reset, Resume/Start

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

            GridChildrenInitialize();
            MainGridInitialize();

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

            Content = mainG;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh all content that exists with saved file when appeared
            // This takes care of the case of coming back from TimerPageSetting contentpage
            RefreshContent();

        }

        private void ResetAll()
        {
            if(timerHandler != null)
            {
                timerHandler.Stop();
            }

            // Stop the timer. Reset the timer. Load the grid.
            GridChildrenInitialize();
            MainGridInitialize();
            //Content = mainG;
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
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand
            };



            // Filler
            mainG.Children.Add(new StackLayout { BackgroundColor = StaticFiles.ColorSettings.darkGrayColor }, 0, 0);
            mainG.Children.Add(new StackLayout { BackgroundColor = StaticFiles.ColorSettings.darkGrayColor }, 1, 0);
            mainG.Children.Add(new StackLayout { BackgroundColor = StaticFiles.ColorSettings.darkGrayColor }, 2, 0);
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

            BoxView backBoxView = new BoxView() { BackgroundColor = StaticFiles.ColorSettings.mainGrayColor};
            mainG.Children.Add(backBoxView, 0, 4);
            Grid.SetColumnSpan(backBoxView, 4);
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
                optionB.Clicked += OptionButtonClicked;
            }

            // Row 1 interval training label
            if (intervalL == null) 
            {  
                intervalL = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = textColor,
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
                    TextColor = textColor,
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
                    TextColor = textColor,
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
                    TextColor = textColor,
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
                    TextColor = textColor,
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
                    TextColor = textColor,
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
                    TextColor = textColor,
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
                    TextColor = textColor,
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
                    Source = "calendar.png",
                    Aspect = Aspect.AspectFit


                };
                calendarButton.Clicked += PlannerButtonClicked;
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
                graphButton.Clicked += GraphButtonClicked;
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

        private async void OnSizeChanged(object sender, EventArgs e)
        {

            await GetSavedData();
            RefreshContent();

            // Handle sizing of labels based on screen size
            if (this.Width > 0)
            {
                // Handle button sizing
                startbutton.WidthRequest = startbutton.Height;
                startbutton.HeightRequest = startbutton.Height;
                startbutton.CornerRadius = (int)startbutton.Height / 2;

                resetButton.HeightRequest = this.Height;
                stopButton.HeightRequest = this.Height;
                resetButton.Margin = this.Width / 20;
                stopButton.Margin = this.Width / 20;

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

        private async void OptionButtonClicked(object sender, EventArgs args)
        {

            // Blackout/opacity clear
            Content = null;
            await Navigation.PushAsync(new TimerPageSettings() { Title = "Settings" });
            // Reset all
            ResetAll();
        }

        private void GraphButtonClicked(object sender, EventArgs args)
        {
            //Content = null;
            //Navigation.PushAsync(new GraphPage());
            //ResetAll();
        }

        private void PlannerButtonClicked(object sender, EventArgs args)
        {
            Content = null;
            Navigation.PushAsync(new PlannerPage());
            ResetAll();
        }

        bool stopbool = false;
        void StopButton_Clicked(object sender, EventArgs e)
        {
            if(timerHandler.TimerComplete())
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
                    stopButton.Source = "pause_white";
                });
            }
        }

        void ResetButton_Clicked(object sender, EventArgs e)
        {
            timerHandler.Reset();
        }

    }
}