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
        private Color BACKGROUND_COLOR = Color.FromHex("#303030");
        private Color SELECTED_COLOR = Color.FromHex("#636363");

        public StackLayout MainContainer;
        public StackLayout NumberPickerContainer_Hang;
        private StackLayout NumberPickerLabelSecondContent_Hang;
        private StackLayout NumberPickerLabelMinuteContent_Hang;
        private ScrollView myScrollViewSecond_Hang;
        private ScrollView myScrollViewMinute_Hang;
        public StackLayout NumberPickerContainer_Rest;
        private StackLayout NumberPickerLabelSecondContent_Rest;
        private StackLayout NumberPickerLabelMinuteContent_Rest;
        private ScrollView myScrollViewSecond_Rest;
        private ScrollView myScrollViewMinute_Rest;
        private View Content;

        public Button buttonStart;
        private StackLayout buttonStartStackLayout;

        private int focusedSecondScrollViewContent_Hang = 0;
        private int focusedMinuteScrollViewContent_Hang = 0;
        private int focusedSecondScrollViewContent_Rest = 0;
        private int focusedMinuteScrollViewContent_Rest = 0;

        private readonly int[] DIGIT_CONTAINER = 
            {
            0,1,2,3,4,5,6,7,8,9,
            10,11,12,13,14,15,16,17,18,19,
            20,21,22,23,24,25,26,27,28,29,
            30,31,32,33,34,35,36,37,38,39,
            40,41,42,43,44,45,46,47,48,49,
            50,51,52,53,54,55,56,57,58,59,
            };
        private const int DIGIT_CONTAINER_SIZE = 60;

        private void Create_SecondMinuteSelection_Rest()
        {

            // Number picker stacklayout container
            NumberPickerContainer_Rest = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = BACKGROUND_COLOR,
                Orientation = StackOrientation.Horizontal,
            };


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

            // Add to containers and maincontainer
            NumberPickerContainer_Rest.Children.Add(myScrollViewSecond_Rest);
            NumberPickerContainer_Rest.Children.Add(myScrollViewMinute_Rest);
            MainContainer.Children.Add(NumberPickerContainer_Rest);

        }

        private void Create_SecondMinuteSelection_Hang()
        {

            // Number picker stacklayout container
            NumberPickerContainer_Hang = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = BACKGROUND_COLOR,
                Orientation = StackOrientation.Horizontal,
            };


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

            // Add to containers and maincontainer
            NumberPickerContainer_Hang.Children.Add(myScrollViewSecond_Hang);
            NumberPickerContainer_Hang.Children.Add(myScrollViewMinute_Hang);
            MainContainer.Children.Add(NumberPickerContainer_Hang);
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

            buttonStartStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
                BackgroundColor = Color.FromHex("#303030"),
                Orientation = StackOrientation.Horizontal,
            };

            buttonStartStackLayout.Children.Add(buttonStart);
            MainContainer.Children.Add(buttonStartStackLayout);
        }

        public ContentViewHandler_TimerPageSelection()
        {

            MainContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            // Fill with label values
            NumberPickerLabelSecondContent_Hang = new StackLayout
            {
                
            };
            NumberPickerLabelMinuteContent_Hang = new StackLayout
            {
                
            };
            NumberPickerLabelSecondContent_Rest = new StackLayout
            {

            };
            NumberPickerLabelMinuteContent_Rest = new StackLayout
            {

            };
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
            }
            
        }

        public View GetContentView()
        {

            // Create both number pickers for hang
            Create_SecondMinuteSelection_Hang();

            // Create both number pickers for rest
            Create_SecondMinuteSelection_Rest();

            // Create button holder and buttons
            Create_BeginButton();

            return Content = MainContainer;

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

        // BUTTON EVENTS
        private void StartButtonClicked(object sender, EventArgs args)
        {
            
        }

        
}
}
