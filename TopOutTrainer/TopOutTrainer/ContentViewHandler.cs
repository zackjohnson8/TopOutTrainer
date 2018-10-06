using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public static class ContentViewHandler
    {

        public enum EnumViews
        {
            TimerPageView,
        };

        private static StackLayout timerPage_stacklayout;
        private static Label timerPage_labelTimerText;
        private static Label timerPage_labelVisualHold;
        private static StackLayout timerPage_stackLayoutButtonHolder;
        private static Button timerPage_buttonStart;
        private static Button timerPage_buttonStop;
        private static View timerPage_Content;

        public static View BuildContentView(EnumViews ViewCreation)
        {
            // TIMERPAGE CONTENT BUILD
            if (ViewCreation == EnumViews.TimerPageView)
            {
 
                // Main Layout
                if (timerPage_stacklayout != null)
                {
                }
                else
                {
                    timerPage_stacklayout = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(10),
                    };
                }


                // Top Text Label
                if (timerPage_labelTimerText != null)
                {
                }
                else
                {
                    timerPage_labelTimerText = new Label
                    {

                        Text = "TIMERTEXT",
                        FontSize = 50,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HeightRequest = 120,
                        BackgroundColor = Color.FromHex("#000000")
                    };
                    timerPage_stacklayout.Children.Add(timerPage_labelTimerText);
                }

                // Visual Hold Label
                if(timerPage_labelVisualHold != null)
                {

                }else
                {
                    timerPage_labelVisualHold = new Label
                    {

                        Text = "TIMERTEXT",
                        FontSize = 50,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HeightRequest = 120,
                        BackgroundColor = Color.FromHex("#000000")
                    };
                    timerPage_stacklayout.Children.Add(timerPage_labelVisualHold);
                }

                // StackLayout to fill with buttons
                if(timerPage_stackLayoutButtonHolder != null)
                {

                }else
                {

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
                        // TODO: Clicked = "OnButtonClicked",
                    };

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
                    timerPage_stacklayout.Children.Add(timerPage_stackLayoutButtonHolder);
                }

                timerPage_Content = timerPage_stacklayout;

                return timerPage_Content;

            }

            return null;
        }

    }
}
