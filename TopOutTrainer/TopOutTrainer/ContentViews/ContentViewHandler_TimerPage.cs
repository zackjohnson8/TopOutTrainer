using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public class ContentViewHandler_TimerPage
    {
        
        public StackLayout MainContainer;
        public Label labelTimerText;
        public Image bitmapImage;
        public StackLayout stackLayoutButtonHolder;
        public StackLayout stackLayoutBitmapHolder;
        public Button buttonStart;
        public Button buttonStop;
        public Button buttonReset;
        public View Content;
        public String totalTime = "00:00";

        // Circle Graphic Thickness
        private const int GraphicThickness = 100;

        public Color TextColor
        {
            private set;
            get;
        } = Color.FromHex("#ffffff");

        public Color BackgroundColor
        {
            private set;
            get;
        } = Color.FromHex("#303030");

        private void Create_TPMainContainer()
        {

            MainContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
            };

        }

        private void Create_TPTotalTimeLabel()
        {

            // Timer Text
            // Use previous slection screen to determine text number
            labelTimerText = new Label
            {
                Text = totalTime,
                FontSize = 64,
                FontAttributes = FontAttributes.Bold,
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                HeightRequest = 120,
                BackgroundColor = BackgroundColor,
                TextColor = TextColor,
                
            };
            

            //Font-Family
            if (Device.RuntimePlatform == Device.iOS)
            {
                labelTimerText.FontFamily = "OpenSans-Regular";
            }
            if (Device.RuntimePlatform == Device.Android)
            {
                labelTimerText.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"; // Figure out where this is looking to find the string
            }


            MainContainer.Children.Add(labelTimerText);

        }

        private void Create_TPBitMap()
        {

            // Bitmap stack container
            stackLayoutBitmapHolder = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
                BackgroundColor = BackgroundColor,
                Orientation = StackOrientation.Horizontal,
            };

            // Bitmap Size
            int rows = 800;
            int cols = 800;
            BmpMaker bmpMaker = new BmpMaker(cols, rows);

            // Draw images to the bitmap
            bmpMaker.DrawCircle(rows, cols, GraphicThickness, 7, 30, 3); // TODO remove hardcode

            ImageSource imageSource = bmpMaker.Generate();

            bitmapImage = new Image
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.Fill,
                Source = imageSource
            };

            stackLayoutBitmapHolder.Children.Add(bitmapImage);
            MainContainer.Children.Add(stackLayoutBitmapHolder);
        }

        private void Create_TPStartStopButton()
        {
            // START BUTTON //
            buttonStart = new Button
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
            buttonStart.Clicked += StartButtonClicked;


            buttonStop = new Button
            {
                Text = "Stop",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 120,
                HeightRequest = 40,
                BorderColor = Color.FromHex("#66ffff"),
                BorderWidth = 3,
                CornerRadius = 2,
            };
            buttonStop.Clicked += StopButtonClicked;

            buttonReset = new Button
            {
                Text = "Reset",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 120,
                HeightRequest = 40,
                BorderColor = Color.FromHex("#66ffff"),
                BorderWidth = 3,
                CornerRadius = 2,
                IsVisible = false
            };
            buttonReset.Clicked += ResetButtonClicked;

            // Button Holder Creation and fill with new buttons
            stackLayoutButtonHolder = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
                BackgroundColor = Color.FromHex("#303030"),
                Orientation = StackOrientation.Horizontal,
            };

            stackLayoutButtonHolder.Children.Add(buttonStart);
            stackLayoutButtonHolder.Children.Add(buttonStop);
            stackLayoutButtonHolder.Children.Add(buttonReset);
            MainContainer.Children.Add(stackLayoutButtonHolder);
        }

        // CONSTRUCTOR
        public ContentViewHandler_TimerPage(int hangTime_min, int hangTime_sec, int restTime_min, int restTime_sec, int hangInterval_count)
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
                
            return Content = MainContainer;

        }

        // EVENTS
        private void StartButtonClicked(object sender, EventArgs args)
        {
            StopWatch.Start();
            buttonReset.IsVisible = false;
            buttonStop.IsVisible = true;
        }

        private void StopButtonClicked(object sender, EventArgs args)
        {
            StopWatch.Stop();
            buttonReset.IsVisible = true;
            buttonStop.IsVisible = false;
        }

        private void ResetButtonClicked(object sender, EventArgs args)
        {
            StopWatch.Reset();
        }

    }

    
}
