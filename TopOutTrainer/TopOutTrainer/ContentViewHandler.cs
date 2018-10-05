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
        private static View timerPage_Content;

        public static void BuildContentView(View content, EnumViews ViewCreation)
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
                    timerPage_stackLayoutButtonHolder = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(10),
                        BackgroundColor = Color.FromHex("#000000")
                    };
                    timerPage_stacklayout.Children.Add(timerPage_stackLayoutButtonHolder);
                }

                timerPage_Content = timerPage_stacklayout;
            }

        }

    }
}
