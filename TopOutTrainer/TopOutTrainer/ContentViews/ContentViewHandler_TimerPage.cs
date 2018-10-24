using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public class ContentViewHandler_TimerPage
    {
        
        public StackLayout timerPage_MainContainer;
        public Label timerPage_labelTimerText;
        public Image timerPage_bitmapImage;
        public StackLayout timerPage_stackLayoutButtonHolder;
        public StackLayout timerPage_stackLayoutBitmapHolder;
        public Button timerPage_buttonStart;
        public Button timerPage_buttonStop;
        public View timerPage_Content;
        public String timerPage_totalTime = "00:00";
        private const int GraphicThickness = 80;

        public Color TimerPage_textColor
        {
            private set;
            get;
        } = Color.FromHex("#ffffff");

        public Color TimerPage_backgroundColor
        {
            private set;
            get;
        } = Color.FromHex("#303030");

        private void Create_TPMainContainer()
        {

            timerPage_MainContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
            };

        }

        private void Create_TPTotalTimeLabel()
        {

            // Timer Text
            // Use previous slection screen to determine text number
            timerPage_labelTimerText = new Label
            {
                Text = timerPage_totalTime,
                FontSize = 64,
                FontAttributes = FontAttributes.Bold,
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                HeightRequest = 120,
                BackgroundColor = TimerPage_backgroundColor,
                TextColor = TimerPage_textColor,
                
            };
            

            //Font-Family
            if (Device.RuntimePlatform == Device.iOS)
            {
                timerPage_labelTimerText.FontFamily = "OpenSans-Regular";
            }
            if (Device.RuntimePlatform == Device.Android)
            {
                timerPage_labelTimerText.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"; // Figure out where this is looking to find the string
            }


            timerPage_MainContainer.Children.Add(timerPage_labelTimerText);

        }

        private void Create_TPBitMap()
        {

            timerPage_stackLayoutBitmapHolder = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
                BackgroundColor = TimerPage_backgroundColor,
                Orientation = StackOrientation.Horizontal,
            };

            int rows = 800;
            int cols = 800;
            BmpMaker bmpMaker = new BmpMaker(cols, rows);

            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                {
                    bmpMaker.SetPixel(row, col, TimerPage_backgroundColor);
                }

            // Draw images to the bitmap
            bmpMaker.DrawCircle(rows, cols, GraphicThickness);

            ImageSource imageSource = bmpMaker.Generate();

            timerPage_bitmapImage = new Image
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.Fill,
                Source = imageSource
            };

            timerPage_stackLayoutBitmapHolder.Children.Add(timerPage_bitmapImage);
            timerPage_MainContainer.Children.Add(timerPage_stackLayoutBitmapHolder);
        }

        private void Create_TPStartStopButton()
        {
            // START BUTTON //
            timerPage_buttonStart = new Button
            {
                Text = "Start",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 120,
                HeightRequest = 40,
                BorderColor = Color.FromHex("#66ffff"),
                BorderWidth = 3,
                CornerRadius = 2,
            };
            //Clicked += "StartButtonClicked",
            timerPage_buttonStart.Clicked += StartButtonClicked;


            timerPage_buttonStop = new Button
            {
                Text = "Stop",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 120,
                HeightRequest = 40,
                BorderColor = Color.FromHex("#66ffff"),
                BorderWidth = 3,
                CornerRadius = 2,
                // TODO: Clicked = "OnButtonClicked",
            };

            // Button Holder Creation and fill with new buttons
            timerPage_stackLayoutButtonHolder = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
                BackgroundColor = Color.FromHex("#303030"),
                Orientation = StackOrientation.Horizontal,
            };

            timerPage_stackLayoutButtonHolder.Children.Add(timerPage_buttonStart);
            timerPage_stackLayoutButtonHolder.Children.Add(timerPage_buttonStop);
            timerPage_MainContainer.Children.Add(timerPage_stackLayoutButtonHolder);
        }

        // CONSTRUCTOR
        public ContentViewHandler_TimerPage()
        {
            
        }

        public View GetContentView()
        {

            // Create main container for objects
            Create_TPMainContainer();

            // Create upper label for total time
            Create_TPTotalTimeLabel();

            // Create bitmap for timer animation
            Create_TPBitMap();

            // Create button holder and buttons
            Create_TPStartStopButton();
                
            return timerPage_Content = timerPage_MainContainer;

        }

        // EVENTS
        private void StartButtonClicked(object sender, EventArgs args)
        {
            StopWatch.Start();
        }

        private void StopButtonClicked(object sender, EventArgs args)
        {
            StopWatch.Stop();
        }

        private void ResetButtonClicked(object sender, EventArgs args)
        {
            StopWatch.Reset();
        }

    }

    
}
