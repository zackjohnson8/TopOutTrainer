﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PCLStorage;
using System.Diagnostics;
using Xamarin.Forms.Internals;

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

        private StopWatch countDownTimer;
        private StopWatch totalTimeTimer;

        //private Image bitmapI;
        //private StackLayout bitmapContainer;
        //private BmpMaker bmpMaker;

        // TODO default button names until determined
        private ImageButton button1;
        private ImageButton button2;
        private ImageButton button3;
        private ImageButton button4;
        private ImageButton startbutton;


        public TimerPage()
        {

            //StaticFiles.TimerPageUISettings.SetFromFile();

            NavigationPage.SetHasNavigationBar(this, false);
            //myTimerSettings = new TopOutTrainer.ContentViews.TimerPageSettings(); // All timer settings
            GridChildrenInitialize();
            MainGridInitialize();
            //StopWatch.AddLabelToDraw(timerNumL);
            SizeChanged += OnSizeChanged;
            Content = mainG;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await StaticFiles.TimerPageUISettings.SetFromFile();

            repsNumL.Text = StaticFiles.TimerPageUISettings.reps.ToString();
            setsNumL.Text = StaticFiles.TimerPageUISettings.sets.ToString();


            int breakReps = StaticFiles.TimerPageUISettings.sets * (StaticFiles.TimerPageUISettings.reps - 1);
            int breakSets = StaticFiles.TimerPageUISettings.sets;
            int totalTime = (breakReps * StaticFiles.TimerPageUISettings.repsRestTime) + (breakSets * StaticFiles.TimerPageUISettings.setsRestTime);

            int totalTimeMinutes = totalTime / 60;
            int totalTimeSeconds = totalTime % 60;

            string timeMin;
            if(totalTimeMinutes <= 9)
            {
                timeMin = string.Concat('0' + totalTimeMinutes.ToString());
            }else
            {
                timeMin = totalTimeMinutes.ToString();
            }

            string timeSec;
            if(totalTimeSeconds <= 9)
            {
                timeSec = string.Concat('0' + totalTimeSeconds.ToString());
            }else
            {
                timeSec = totalTimeSeconds.ToString();
            }

            //string totalTimeString ;
            timerNumL.Text = string.Concat(timeMin + ':' + timeSec);

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
                    new RowDefinition { Height = new GridLength(5.5, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
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

            mainG.Children.Add(button1, 0, 5);
            mainG.Children.Add(button2, 1, 5);
            mainG.Children.Add(button3, 2, 5);
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
                TextColor = textColor,
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
            button1 = new ImageButton
            {

                BackgroundColor = Color.FromHex("#D3EFFC"),
                Margin = 0,
                CornerRadius = 0,
                Source = "stopwatch_white_trans.png",
                Aspect = Aspect.AspectFit

            };
            
            // (4,1)
            button2 = new ImageButton
            {
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "calendar.png",
                Aspect = Aspect.AspectFit


            };
            button2.Clicked += PlannerButtonClicked;
            // (4,2)
            button3 = new ImageButton
            {
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "graph_white.png",
                Aspect = Aspect.AspectFit

            };
            button3.Clicked += GraphButtonClicked;
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

        private void OptionButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new TimerPageSettings() { Title = "Settings" });
        }

        private void GraphButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new GraphPage());
        }

        private void PlannerButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new PlannerPage());
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
            mainG.Children.Add(getReadyG, 0, 4);
            Grid.SetColumnSpan(getReadyG, 4);


            int breakReps = StaticFiles.TimerPageUISettings.sets * (StaticFiles.TimerPageUISettings.reps - 1);
            int breakSets = StaticFiles.TimerPageUISettings.sets;
            int totalTime = (breakReps * StaticFiles.TimerPageUISettings.repsRestTime) + (breakSets * StaticFiles.TimerPageUISettings.setsRestTime);
            totalTimeTimer = new StopWatch(timerNumL, StopWatch.CountDirection.COUNTDOWN, totalTime);

            countDownTimer = new StopWatch(timerL, StopWatch.CountDirection.COUNTDOWN, 5);
            countDownTimer.Start();
            totalTimeTimer.Start();

        }

        private void OnSizeChanged(object sender, EventArgs e)
        {

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
                timerL.FontSize = this.Width / 6;
             

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