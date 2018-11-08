using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace TopOutTrainer.ContentViews
{
    class ContentViewHandler_TimerPageSelection
    {

        // Parameters
        private readonly Color BACKGROUND_COLOR = Color.FromHex("#303030");
        private readonly Color SELECTED_COLOR = Color.FromHex("#636363");

        // Parent container
        public Grid MainContainer;

        // ScrollView for each number picker
        private ScrollView myScrollViewSecond_Hang;
        private ScrollView myScrollViewMinute_Hang;
        private ScrollView myScrollViewSecond_Rest;
        private ScrollView myScrollViewMinute_Rest;
        private ScrollView myScrollView_IntervalCount;

        // Labels for all the scrollviews
        private Label hangMinuteLabel;
        private Label hangSecondLabel;
        private Label restMinuteLabel;
        private Label restSecondLabel;
        private Label intervalLabel;

        private View Content;
        INavigation myNavigation;

        // Each of the scroll view containers and tracking of each scrollview number selection
        private int focusedSecondScrollViewContent_Hang = 0;
        private int focusedMinuteScrollViewContent_Hang = 0;
        private int focusedSecondScrollViewContent_Rest = 0;
        private int focusedMinuteScrollViewContent_Rest = 0;
        private int focusedIntervalCount = 0;
        private StackLayout NumberPickerLabelSecondContent_Hang;
        private StackLayout NumberPickerLabelMinuteContent_Hang;
        private StackLayout NumberPickerLabelSecondContent_Rest;
        private StackLayout NumberPickerLabelMinuteContent_Rest;
        private StackLayout NumberPickerLabel_IntervalCount;

        // Begin button to go to TimerPage countdown
        public Button buttonStart;

        // Scrollview number range containers
        private const int DIGIT_CONTAINER_SIZE = 60;
        private readonly int[] DIGIT_CONTAINER = 
            {
            0,1,2,3,4,5,6,7,8,9,
            10,11,12,13,14,15,16,17,18,19,
            20,21,22,23,24,25,26,27,28,29,
            30,31,32,33,34,35,36,37,38,39,
            40,41,42,43,44,45,46,47,48,49,
            50,51,52,53,54,55,56,57,58,59,
            };

        /// <summary>
        /// //////////////////////////////
        /// </summary>

        private void Create_SecondMinuteSelection_Rest()
        {

            // Build both number picker scroll views and place into stacklayout
            myScrollViewSecond_Rest = new ScrollView
            {
                Content = NumberPickerLabelSecondContent_Rest,
                BackgroundColor = BACKGROUND_COLOR,
                WidthRequest = 120,
                HeightRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never
            };

            myScrollViewMinute_Rest = new ScrollView
            {
                Content = NumberPickerLabelMinuteContent_Rest,
                BackgroundColor = BACKGROUND_COLOR,
                WidthRequest = 120,
                HeightRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never
            };

            // Default set 0 label with selected color
            NumberPickerLabelSecondContent_Rest.Children[0].BackgroundColor = SELECTED_COLOR;
            NumberPickerLabelMinuteContent_Rest.Children[0].BackgroundColor = SELECTED_COLOR;

            // Scrolled Events
            myScrollViewSecond_Rest.Scrolled += (sender, e) => NumberPickerScrolledSecond_Rest(this, e);
            myScrollViewMinute_Rest.Scrolled += (sender, e) => NumberPickerScrolledMinute_Rest(this, e);

        }

        private void Create_SecondMinuteSelection_Hang()
        {

            // Build both number picker scroll views and place into stacklayout
            myScrollViewSecond_Hang = new ScrollView
            {
                Content = NumberPickerLabelSecondContent_Hang,
                BackgroundColor = BACKGROUND_COLOR,
                WidthRequest = 120,
                HeightRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never
            };
            
            myScrollViewMinute_Hang = new ScrollView
            {
                Content = NumberPickerLabelMinuteContent_Hang,
                BackgroundColor = BACKGROUND_COLOR,
                WidthRequest = 120,
                HeightRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never
            };
            
            // Default set 0 label with selected color
            NumberPickerLabelSecondContent_Hang.Children[0].BackgroundColor = SELECTED_COLOR;
            NumberPickerLabelMinuteContent_Hang.Children[0].BackgroundColor = SELECTED_COLOR;

            // Scrolled Events
            myScrollViewSecond_Hang.Scrolled += (sender,e) => NumberPickerScrolledSecond_Hang(this, e);
            myScrollViewMinute_Hang.Scrolled += (sender, e) => NumberPickerScrolledMinute_Hang(this, e);
            
        }

        private void Create_IntervalCountSelection()
        {

            // Build both number picker scroll views and place into stacklayout
            myScrollView_IntervalCount = new ScrollView
            {
                Content = NumberPickerLabel_IntervalCount,
                BackgroundColor = BACKGROUND_COLOR,
                WidthRequest = 120,
                HeightRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never
            };

            // Default set 0 label with selected color
            NumberPickerLabel_IntervalCount.Children[0].BackgroundColor = SELECTED_COLOR;

            // Scrolled Events
            myScrollView_IntervalCount.Scrolled += (sender, e) => NumberPickerScrolled_IntervalCount(this, e);

        }

        private void Create_BeginButton()
        {
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

            buttonStart.Clicked += async (sender, args) =>
            {
                await myNavigation.PushAsync(new ContentViews.TimerPage(focusedMinuteScrollViewContent_Hang, focusedSecondScrollViewContent_Hang, focusedMinuteScrollViewContent_Rest, focusedSecondScrollViewContent_Rest, focusedIntervalCount));
            };
        }

        private void CreateDescriptionLabels()
        {

            hangMinuteLabel = new Label
            {
                Text = "Minute",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold",
            };

            hangSecondLabel = new Label
            {
                Text = "Second",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
            };

            restMinuteLabel = new Label
            {
                Text = "Minute",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
            };

            restSecondLabel = new Label
            {
                Text = "Second",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
            };

            intervalLabel = new Label
            {
                Text = "Count",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
            };

        }
        
        public ContentViewHandler_TimerPageSelection(INavigation navigation)
        {

            myNavigation = navigation;

            MainContainer = new Grid
            {


            };

            //Height = new GridLength(1, GridUnitType.Star)
            MainContainer.RowDefinitions.Add(new RowDefinition {}); // Hang Selection Row
            MainContainer.RowDefinitions.Add(new RowDefinition {}); // Hang Labels Row
            MainContainer.RowDefinitions.Add(new RowDefinition {}); // Rest Selection Row
            MainContainer.RowDefinitions.Add(new RowDefinition {}); // Rest Labels Row
            MainContainer.RowDefinitions.Add(new RowDefinition {}); // Interval Selection Row
            MainContainer.RowDefinitions.Add(new RowDefinition {}); // Button Row


            //Width = new GridLength(1, GridUnitType.Star) 
            MainContainer.ColumnDefinitions.Add(new ColumnDefinition {}); // Label Column
            MainContainer.ColumnDefinitions.Add(new ColumnDefinition {}); // Minute Column
            MainContainer.ColumnDefinitions.Add(new ColumnDefinition {}); // Second Column

            // Fill with label values
            NumberPickerLabelSecondContent_Hang = new StackLayout();
            NumberPickerLabelMinuteContent_Hang = new StackLayout();
            NumberPickerLabelSecondContent_Rest = new StackLayout();
            NumberPickerLabelMinuteContent_Rest = new StackLayout();
            NumberPickerLabel_IntervalCount = new StackLayout();
            foreach (int val in DIGIT_CONTAINER)
            {
                NumberPickerLabelSecondContent_Hang.Children.Add(
                    new Label
                    {
                        Text = val.ToString(),
                        FontSize = 64,
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
                    }
                    );
                NumberPickerLabelMinuteContent_Hang.Children.Add(
                    new Label
                    {
                        Text = val.ToString(),
                        FontSize = 64,
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
                    }
                    );
                NumberPickerLabelSecondContent_Rest.Children.Add(
                    new Label
                    {
                        Text = val.ToString(),
                        FontSize = 64,
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
                    }
                    );
                NumberPickerLabelMinuteContent_Rest.Children.Add(
                    new Label
                    {
                        Text = val.ToString(),
                        FontSize = 64,
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
                    }
                    );
                NumberPickerLabel_IntervalCount.Children.Add(
                    new Label
                    {
                        Text = val.ToString(),
                        FontSize = 64,
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
                    }
                    );
            }



        }

        private void AddContainersToMain()
        {

            //NumberPickerContainer_Second.Children.Add(myScrollViewSecond_Hang);
            //NumberPickerContainer_Second.Children.Add(hangSecondLabel);
            MainContainer.Children.Add(myScrollViewSecond_Hang, 2, 0);
            MainContainer.Children.Add(hangSecondLabel, 2, 1);

            //NumberPickerContainer_Minute.Children.Add(myScrollViewMinute_Hang);
            //NumberPickerContainer_Minute.Children.Add(hangMinuteLabel);
            MainContainer.Children.Add(myScrollViewMinute_Hang, 1, 0);
            MainContainer.Children.Add(hangMinuteLabel, 1, 1);


            //NumberPickerContainer_Second.Children.Add(myScrollViewSecond_Rest);
            //NumberPickerContainer_Second.Children.Add(restSecondLabel);
            MainContainer.Children.Add(myScrollViewSecond_Rest, 2, 2);
            MainContainer.Children.Add(restSecondLabel, 2, 3);

            
            MainContainer.Children.Add(myScrollViewMinute_Rest, 1, 2);
            MainContainer.Children.Add(restMinuteLabel, 1, 3);

            // Need spacer

            MainContainer.Children.Add(myScrollView_IntervalCount, 1, 4);
            MainContainer.Children.Add(intervalLabel, 1, 5);

            //MainContainer.Children.Add(NumberPickerContainer_Minute);
            //MainContainer.Children.Add(NumberPickerContainer_Second);

        }

        public View GetContentView()
        {
            // Create all the labels that will be added to picker containers
            CreateDescriptionLabels();

            // Create both number pickers for hang
            Create_SecondMinuteSelection_Hang();

            // Create both number pickers for rest
            Create_SecondMinuteSelection_Rest();

            // Create Interval Count
            Create_IntervalCountSelection();

            // Create button holder and buttons
            //Create_BeginButton();

            AddContainersToMain();

            return Content = MainContainer;

        }

        // INTERVAL EVENT
        private void NumberPickerScrolled_IntervalCount(object sender, EventArgs args)
        {

            // Using the new yPosition determine the label that is being focused and highlight it
            Size contentSize = this.myScrollView_IntervalCount.ContentSize;

            // Select the content closest to the center
            int position = (int)((myScrollView_IntervalCount.ScrollY + (contentSize.Height / DIGIT_CONTAINER_SIZE / 2)) / (contentSize.Height / DIGIT_CONTAINER_SIZE));

            if (position >= 0 && position <= DIGIT_CONTAINER_SIZE)
            {
                NumberPickerLabel_IntervalCount.Children[position].BackgroundColor = SELECTED_COLOR;

                if (focusedIntervalCount != position)
                {
                    NumberPickerLabel_IntervalCount.Children[focusedIntervalCount].BackgroundColor = BACKGROUND_COLOR;
                    focusedIntervalCount = position;
                }
            }
        }

        // HANG EVENTS
        private void NumberPickerScrolledSecond_Hang(object sender, EventArgs args)
        {

            // Using the new yPosition determine the label that is being focused and highlight it
            Size contentSize = this.myScrollViewSecond_Hang.ContentSize;

            // Select the content closest to the center
            int position = (int)((myScrollViewSecond_Hang.ScrollY + (contentSize.Height / DIGIT_CONTAINER_SIZE / 2)) / (contentSize.Height / DIGIT_CONTAINER_SIZE));

            if (position >= 0 && position <= DIGIT_CONTAINER_SIZE)
            {
                NumberPickerLabelSecondContent_Hang.Children[position].BackgroundColor = SELECTED_COLOR;

                if (focusedSecondScrollViewContent_Hang != position)
                {
                    NumberPickerLabelSecondContent_Hang.Children[focusedSecondScrollViewContent_Hang].BackgroundColor = BACKGROUND_COLOR;
                    focusedSecondScrollViewContent_Hang = position;
                }
            }
        }

        private void NumberPickerScrolledMinute_Hang(object sender, EventArgs args)
        {

            // Using the new yPosition determine the label that is being focused and highlight it
            Size contentSize = this.myScrollViewMinute_Hang.ContentSize;

            // Select the content closest to the center
            int position = (int)((myScrollViewMinute_Hang.ScrollY + (contentSize.Height / DIGIT_CONTAINER_SIZE / 2)) / (contentSize.Height / DIGIT_CONTAINER_SIZE));

            if (position >= 0 && position <= DIGIT_CONTAINER_SIZE)
            {
                NumberPickerLabelMinuteContent_Hang.Children[position].BackgroundColor = SELECTED_COLOR;

                if (focusedMinuteScrollViewContent_Hang != position)
                {
                    NumberPickerLabelMinuteContent_Hang.Children[focusedMinuteScrollViewContent_Hang].BackgroundColor = BACKGROUND_COLOR;
                    focusedMinuteScrollViewContent_Hang = position;
                }
            }
        }

        // REST EVENTS
        private void NumberPickerScrolledSecond_Rest(object sender, EventArgs args)
        {

            // Using the new yPosition determine the label that is being focused and highlight it
            Size contentSize = this.myScrollViewSecond_Rest.ContentSize;

            // Select the content closest to the center
            int position = (int)((myScrollViewSecond_Rest.ScrollY + (contentSize.Height / DIGIT_CONTAINER_SIZE / 2)) / (contentSize.Height / DIGIT_CONTAINER_SIZE));

            if (position >= 0 && position <= DIGIT_CONTAINER_SIZE)
            {
                NumberPickerLabelSecondContent_Rest.Children[position].BackgroundColor = SELECTED_COLOR;

                if (focusedSecondScrollViewContent_Rest != position)
                {
                    NumberPickerLabelSecondContent_Rest.Children[focusedSecondScrollViewContent_Rest].BackgroundColor = BACKGROUND_COLOR;
                    focusedSecondScrollViewContent_Rest = position;
                }
            }
        }

        private void NumberPickerScrolledMinute_Rest(object sender, EventArgs args)
        {

            // Using the new yPosition determine the label that is being focused and highlight it
            Size contentSize = this.myScrollViewMinute_Rest.ContentSize;

            // Select the content closest to the center
            int position = (int)((myScrollViewMinute_Rest.ScrollY + (contentSize.Height / DIGIT_CONTAINER_SIZE / 2)) / (contentSize.Height / DIGIT_CONTAINER_SIZE));

            if (position >= 0 && position <= DIGIT_CONTAINER_SIZE)
            {
                NumberPickerLabelMinuteContent_Rest.Children[position].BackgroundColor = SELECTED_COLOR;

                if (focusedMinuteScrollViewContent_Rest != position)
                {
                    NumberPickerLabelMinuteContent_Rest.Children[focusedMinuteScrollViewContent_Rest].BackgroundColor = BACKGROUND_COLOR;
                    focusedMinuteScrollViewContent_Rest = position;
                }
            }
        }
        
}
}
