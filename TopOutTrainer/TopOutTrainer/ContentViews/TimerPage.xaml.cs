using System;
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

            NavigationPage.SetHasNavigationBar(this, false);
            //myTimerSettings = new TopOutTrainer.ContentViews.TimerPageSettings(); // All timer settings
            GridChildrenInitialize();
            MainGridInitialize();
            StopWatch.AddLabelToDraw(timerNumL);
            SizeChanged += OnSizeChanged;
            Content = mainG;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String folderName = "timerpage";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            String fileName = "setting.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            Debug.Write("here" + System.Environment.NewLine);
            Debug.Write(await file.ReadAllTextAsync());
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
                CornerRadius = 200,
                Source = "start.png",
                Aspect = Aspect.AspectFit
            };

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
            Navigation.PushAsync(new TimerPageSettings());
        }

        private void GraphButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new GraphPage());
        }

        private void PlannerButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new PlannerPage());
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            // Handle sizing of labels based on screen size
            if (this.Width > 0)
            {
                // Numbers
                timerNumL.FontSize = this.Width / 8;
                repsNumL.FontSize = this.Width / 8;
                setsNumL.FontSize = this.Width / 8;

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


//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using Xamarin.Forms;

//namespace TopOutTrainer
//{
//    public class ContentViewHandler_TimerPage
//    {

//        public StackLayout MainContainer;
//        public Label labelTimerText;
//        public Image bitmapImage;
//        public StackLayout stackLayoutButtonHolder;
//        public StackLayout stackLayoutBitmapHolder;
//        public Button buttonStart;
//        public Button buttonStop;
//        public Button buttonReset;
//        public View Content;
//        public String totalTime = "00:00";

//        private int myHangTime_Min;
//        private int myHangTime_Sec;
//        private int myRestTime_Min;
//        private int myRestTime_Sec;
//        private int myHangInterval_Count;

//        // Circle Graphic Thickness
//        private const int GraphicThickness = 100;

//        public Color TextColor
//        {
//            private set;
//            get;
//        } = Color.FromHex("#ffffff");

//        public Color BackgroundColor
//        {
//            private set;
//            get;
//        } = Color.FromHex("#303030");

//        private void Create_TPMainContainer()
//        {

//            MainContainer = new StackLayout
//            {
//                VerticalOptions = LayoutOptions.FillAndExpand,
//                Margin = new Thickness(10),
//            };

//        }

//        private void Create_TPTotalTimeLabel()
//        {

//            // Timer Text
//            // Use previous slection screen to determine text number
//            labelTimerText = new Label
//            {
//                Text = totalTime,
//                FontSize = 64,
//                FontAttributes = FontAttributes.Bold,
//                //HorizontalOptions = LayoutOptions.CenterAndExpand,
//                //VerticalOptions = LayoutOptions.CenterAndExpand,
//                HorizontalTextAlignment = TextAlignment.Center,
//                HeightRequest = 120,
//                BackgroundColor = BackgroundColor,
//                TextColor = TextColor,

//            };


//            //Font-Family
//            if (Device.RuntimePlatform == Device.iOS)
//            {
//                labelTimerText.FontFamily = "OpenSans-Regular";
//            }
//            if (Device.RuntimePlatform == Device.Android)
//            {
//                labelTimerText.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"; // Figure out where this is looking to find the string
//            }


//            MainContainer.Children.Add(labelTimerText);

//        }

//        private void Create_TPBitMap()
//        {

//            // Bitmap stack container
//            stackLayoutBitmapHolder = new StackLayout
//            {
//                VerticalOptions = LayoutOptions.Center,
//                HorizontalOptions = LayoutOptions.FillAndExpand,
//                Margin = new Thickness(10),
//                BackgroundColor = BackgroundColor,
//                Orientation = StackOrientation.Horizontal,
//            };

//            // Bitmap Size
//            int rows = 800;
//            int cols = 800;
//            BmpMaker bmpMaker = new BmpMaker(cols, rows);


//            // Convert hang & rest into seconds then draw images to the bitmap
//            int hangSeconds = (myHangTime_Min * 60) + myHangTime_Sec;
//            int restSeconds = (myRestTime_Min * 60) + myRestTime_Sec;
//            bmpMaker.DrawCircle(rows, cols, GraphicThickness, hangSeconds, restSeconds, myHangInterval_Count); // TODO remove hardcode

//            ImageSource imageSource = bmpMaker.Generate();

//            bitmapImage = new Image
//            {
//                HorizontalOptions = LayoutOptions.CenterAndExpand,
//                VerticalOptions = LayoutOptions.CenterAndExpand,
//                Aspect = Aspect.Fill,
//                Source = imageSource
//            };

//            stackLayoutBitmapHolder.Children.Add(bitmapImage);
//            MainContainer.Children.Add(stackLayoutBitmapHolder);
//        }

//        private void Create_TPStartStopButton()
//        {
//            // START BUTTON //
//            buttonStart = new Button
//            {
//                Text = "Start",
//                VerticalOptions = LayoutOptions.CenterAndExpand,
//                HorizontalOptions = LayoutOptions.CenterAndExpand,
//                WidthRequest = 120,
//                HeightRequest = 40,
//                BorderColor = Color.FromHex("#66ffff"),
//                BorderWidth = 3,
//                CornerRadius = 2,
//            };
//            buttonStart.Clicked += StartButtonClicked;


//            buttonStop = new Button
//            {
//                Text = "Stop",
//                VerticalOptions = LayoutOptions.CenterAndExpand,
//                HorizontalOptions = LayoutOptions.CenterAndExpand,
//                WidthRequest = 120,
//                HeightRequest = 40,
//                BorderColor = Color.FromHex("#66ffff"),
//                BorderWidth = 3,
//                CornerRadius = 2,
//            };
//            buttonStop.Clicked += StopButtonClicked;

//            buttonReset = new Button
//            {
//                Text = "Reset",
//                VerticalOptions = LayoutOptions.CenterAndExpand,
//                HorizontalOptions = LayoutOptions.CenterAndExpand,
//                WidthRequest = 120,
//                HeightRequest = 40,
//                BorderColor = Color.FromHex("#66ffff"),
//                BorderWidth = 3,
//                CornerRadius = 2,
//                IsVisible = false
//            };
//            buttonReset.Clicked += ResetButtonClicked;

//            // Button Holder Creation and fill with new buttons
//            stackLayoutButtonHolder = new StackLayout
//            {
//                VerticalOptions = LayoutOptions.FillAndExpand,
//                HorizontalOptions = LayoutOptions.FillAndExpand,
//                Margin = new Thickness(10),
//                BackgroundColor = Color.FromHex("#303030"),
//                Orientation = StackOrientation.Horizontal,
//            };

//            stackLayoutButtonHolder.Children.Add(buttonStart);
//            stackLayoutButtonHolder.Children.Add(buttonStop);
//            stackLayoutButtonHolder.Children.Add(buttonReset);
//            MainContainer.Children.Add(stackLayoutButtonHolder);
//        }

//        // CONSTRUCTOR
//        public ContentViewHandler_TimerPage(int hangTime_min, int hangTime_sec, int restTime_min, int restTime_sec, int hangInterval_count)
//        {

//            // Take the passed in values and build the image using these
//            myHangTime_Min = hangTime_min;
//            myHangTime_Sec = hangTime_sec;
//            myRestTime_Min = restTime_min;
//            myRestTime_Sec = restTime_sec;
//            myHangInterval_Count = hangInterval_count;

//        }

//        public View GetContentView()
//        {

//            // Create main container for objects
//            Create_TPMainContainer();

//            // Create upper label for total time
//            Create_TPTotalTimeLabel();

//            // Create bitmap for timer animation
//            Create_TPBitMap();

//            // Create button holder and buttons
//            Create_TPStartStopButton();

//            return Content = MainContainer;

//        }

//        // EVENTS
//        private void StartButtonClicked(object sender, EventArgs args)
//        {
//            StopWatch.Start();
//            buttonReset.IsVisible = false;
//            buttonStop.IsVisible = true;
//        }

//        private void StopButtonClicked(object sender, EventArgs args)
//        {
//            StopWatch.Stop();
//            buttonReset.IsVisible = true;
//            buttonStop.IsVisible = false;
//        }

//        private void ResetButtonClicked(object sender, EventArgs args)
//        {
//            StopWatch.Reset();
//        }

//    }


//}

//public void Create_BitMap()
//{


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
//}