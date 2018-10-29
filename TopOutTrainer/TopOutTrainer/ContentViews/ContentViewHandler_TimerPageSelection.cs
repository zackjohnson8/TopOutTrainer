using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace TopOutTrainer.ContentViews
{
    class ContentViewHandler_TimerPageSelection
    {

        private Color BACKGROUND_COLOR = Color.FromHex("#303030");
        private Color SELECTED_COLOR = Color.FromHex("#636363");

        public StackLayout MainContainer;
        public StackLayout NumberPickerContainer;
        private StackLayout NumberPickerLabelSecondContent;
        private StackLayout NumberPickerLabelMinuteContent;
        private View Content;

        private ScrollView myScrollViewSecond;
        private ScrollView myScrollViewMinute;

        private int focusedSecondScrollViewContent = 0;
        private int focusedMinuteScrollViewContent = 0;

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

        private void Create_SecondMinuteSelection()
        {

            // Number picker stacklayout container
            NumberPickerContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = BACKGROUND_COLOR,
                Orientation = StackOrientation.Horizontal,
            };


            // Build both number picker scroll views and place into stacklayout
            myScrollViewSecond = new ScrollView
            {
                Content = NumberPickerLabelSecondContent,
                BackgroundColor = BACKGROUND_COLOR,
                WidthRequest = 120,
                HeightRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never
            };
            
            myScrollViewMinute = new ScrollView
            {
                Content = NumberPickerLabelMinuteContent,
                BackgroundColor = BACKGROUND_COLOR,
                WidthRequest = 120,
                HeightRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never
            };
            
            NumberPickerLabelSecondContent.Children[0].BackgroundColor = SELECTED_COLOR;
            NumberPickerLabelMinuteContent.Children[0].BackgroundColor = SELECTED_COLOR;

            NumberPickerContainer.Children.Add(myScrollViewSecond);
            NumberPickerContainer.Children.Add(myScrollViewMinute);
            MainContainer.Children.Add(NumberPickerContainer);

            // Need to add a current number feature so that when it begins it can
            // pass that value to the next screen.
            myScrollViewSecond.Scrolled += (sender,e) => NumberPickerScrolledSecond(this, e);
            myScrollViewMinute.Scrolled += (sender, e) => NumberPickerScrolledMinute(this, e);
        }

        private void NumberPickerScrolledSecond(object sender, EventArgs args)
        {

            // Using the new yPosition determine the label that is being focused and highlight it
            Size contentSize = this.myScrollViewSecond.ContentSize;

            // Select the content closest to the center
            int position = (int)((myScrollViewSecond.ScrollY + (contentSize.Height / DIGIT_CONTAINER_SIZE / 2)) / (contentSize.Height / DIGIT_CONTAINER_SIZE));

            if (position >= 0 && position <= DIGIT_CONTAINER_SIZE)
            {
                NumberPickerLabelSecondContent.Children[position].BackgroundColor = SELECTED_COLOR;

                if (focusedSecondScrollViewContent != position)
                {
                    NumberPickerLabelSecondContent.Children[focusedSecondScrollViewContent].BackgroundColor = BACKGROUND_COLOR;
                    focusedSecondScrollViewContent = position;
                }
            }
        }

        private void NumberPickerScrolledMinute(object sender, EventArgs args)
        {

            // Using the new yPosition determine the label that is being focused and highlight it
            Size contentSize = this.myScrollViewMinute.ContentSize;

            // Select the content closest to the center
            int position = (int)((myScrollViewMinute.ScrollY + (contentSize.Height / DIGIT_CONTAINER_SIZE / 2)) / (contentSize.Height / DIGIT_CONTAINER_SIZE));

            if (position >= 0 && position <= DIGIT_CONTAINER_SIZE)
            {
                NumberPickerLabelMinuteContent.Children[position].BackgroundColor = SELECTED_COLOR;

                if (focusedMinuteScrollViewContent != position)
                {
                    NumberPickerLabelMinuteContent.Children[focusedMinuteScrollViewContent].BackgroundColor = BACKGROUND_COLOR;
                    focusedMinuteScrollViewContent = position;
                }
            }
        }

        private void Create_BeginButton()
        {



        }

        public ContentViewHandler_TimerPageSelection()
        {

            MainContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            // Fill with label values
            NumberPickerLabelSecondContent = new StackLayout
            {
                
            };
            NumberPickerLabelMinuteContent = new StackLayout
            {
                
            };
            foreach(int val in DIGIT_CONTAINER)
            {
                NumberPickerLabelSecondContent.Children.Add(
                    new Label
                    {
                        Text = val.ToString(),
                        FontSize = 64,
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"
                    }
                    );
                NumberPickerLabelMinuteContent.Children.Add(
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

            // Create both number picker values
            Create_SecondMinuteSelection();

            // Create button holder and buttons
            Create_BeginButton();

            return Content = MainContainer;

        }

    }
}
