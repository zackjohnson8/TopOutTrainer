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

        public StackLayout MainContainer;
        public StackLayout NumberPickerContainer;
        private StackLayout NumberPickerLabelSecondContent;
        private StackLayout NumberPickerLabelMinuteContent;
        private View Content;

        private ScrollView myScrollViewSecond;
        private ScrollView myScrollViewMinute;

        private readonly int[] DIGIT_CONTAINER = 
            {
            1,2,3,4,5,6,7,8,9,
            10,11,12,13,14,15,16,17,18,19,
            20,21,22,23,24,25,26,27,28,29,
            30,31,32,33,34,35,36,37,38,39,
            40,41,42,43,44,45,46,47,48,49,
            50,51,52,53,54,55,56,57,58,59,
            };

        private void Create_SecondMinuteSelection()
        {

            NumberPickerContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = BACKGROUND_COLOR,
                Orientation = StackOrientation.Horizontal,
            };

            myScrollViewSecond = new ScrollView
            {
                BackgroundColor = BACKGROUND_COLOR,
                HeightRequest = 120,
                WidthRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
            };
            myScrollViewSecond.Content = NumberPickerLabelSecondContent;

            myScrollViewMinute = new ScrollView
            {
                BackgroundColor = BACKGROUND_COLOR,
                HeightRequest = 120,
                WidthRequest = 120,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = ScrollOrientation.Vertical,
                
            };
            myScrollViewMinute.Content = NumberPickerLabelMinuteContent;

            NumberPickerContainer.Children.Add(myScrollViewSecond);
            NumberPickerContainer.Children.Add(myScrollViewMinute);
            MainContainer.Children.Add(NumberPickerContainer);

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

            // Fill with values
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
