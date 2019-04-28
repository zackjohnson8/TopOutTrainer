using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TopOutTrainer.CustomOption;

namespace TopOutTrainer.ContentViews
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
        private TimerOption myTimerOption;

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

        private Image bitmapI;
        private StackLayout bitmapContainer;
        private BmpMaker bmpMaker;

        // TODO default button names until determined
        private ImageButton button1;
        private ImageButton button2;
        private ImageButton button3;
        private ImageButton button4;

        private double screenWidth;
        private double screenHeight;

        public TimerPage()
        {

            NavigationPage.SetHasNavigationBar(this, false);
            myTimerOption = new TimerOption();
            GridChildrenInitialize();
            MainGridInitialize();
            StopWatch.AddLabelToDraw(timerNumL);
            SizeChanged += OnSizeChanged;
            Content = mainG;

        }


        void OnSizeChanged(object sender, EventArgs e)
        {
            screenWidth = mainG.Width;
            screenHeight = mainG.Height;
            //Create_BitMap();
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

            mainG.Children.Add(bitmapContainer, 0, 4);
            Grid.SetColumnSpan(bitmapContainer, 4);

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
                Source = "options_gimp.png",
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.Fill

            };
            optionB.Clicked += OptionButtonClicked;

            // Row 0 left (0,0) leave blank for now TODO

            // Row 1 interval training label
            intervalL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                Text = "Interval Training",
                BackgroundColor = mainColor,
                FontSize = 20
            };

            // Row 2 reps label
            repsL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "Reps"
            };

            // Row 2 total time label
            totalTimeL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "Total Time"
            };

            timerNumL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "00:00",
            };
            setsNumL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "0",
            };
            repsNumL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Brown,
                BackgroundColor = mainColor,
                Text = "0",
            };

            // Row 2 sets label
            setsL = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                BackgroundColor = mainColor,
                Text = "Sets"
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
            }


            // Row 3,4 BITMAP
            bitmapContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Blue,
                Orientation = StackOrientation.Horizontal,
            };

            // Row 5 tab bar buttons
            // (4,0)
            button1 = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0

            };
            // (4,1)
            button2 = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0

            };
            // (4,2)
            button3 = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0

            };
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

        public void Create_BitMap()
        {


            //// Bitmap Size
            //double rows = screenHeight;
            //double cols = screenHeight;
            //bmpMaker = new BmpMaker((int)rows, (int)cols);


            //// Convert hang & rest into seconds then draw images to the bitmap
            //int hangSeconds = 10;
            //int restSeconds = 10;
            //bmpMaker.DrawCircle((int)rows, (int)cols, 30, hangSeconds, restSeconds, 3); // TODO remove hardcode
            //ImageSource imageSource = bmpMaker.Generate();

            //bitmapI = new Image
            //{
            //    HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    Aspect = Aspect.Fill,
            //    Source = imageSource
            //};

            //bitmapContainer.Children.Add(bitmapI);
        }

        private void OptionButtonClicked(object sender, EventArgs args)
        {
            // Navigate to option page for option selection customization.
            // Might need a database but would rather use dynamic pages saved on hardware.
            // TODO(zack): Option Page

        }
    }
}



//namespace TopOutTrainer
//{
//    public partial class MainPage : ContentPage // Partial class InitializeComponent in XamlSamples\XamlSamples\obj\Debug
//    {



//    }
//}

//private Color myBackgroundColor = Color.FromHex("#303030");
//      private ContentViewHandler_TimerPage myTimerView;

//      public TimerPage (int hangTime_min, int hangTime_sec, int restTime_min, int restTime_sec, int hangInterval_count)
//{
//    myTimerView = new ContentViewHandler_TimerPage(hangTime_min, hangTime_sec, restTime_min, restTime_sec, hangInterval_count);

//    // TODO: what is initializecomponent
//    //InitializeComponent ();
//    InitializeView();
//}

//void InitializeView()
//{

//    // Set the background color of TimerPage
//    BackgroundColor = myBackgroundColor;

//    // Build the content view with ContentViewHandler_TimerPage 
//    Content = myTimerView.GetContentView();

//    StopWatch.AddLabelToDraw(myTimerView.labelTimerText);
//    //StopWatch.Start();

//}

//void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
//{
//    //valueLabel.Text = args.NewValue.ToString("F3");
//    //valueLabel.Text = ((Slider)sender).Value.ToString("F3");
//}