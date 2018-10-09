﻿using System;
using System.Collections.Generic;
using System.IO;
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
        private static Image timerPage_bitmapImage;
        private static StackLayout timerPage_stackLayoutButtonHolder;
        private static StackLayout timerPage_stackLayoutBitmapHolder;
        private static Button timerPage_buttonStart;
        private static Button timerPage_buttonStop;
        private static View timerPage_Content;
        private static String timerPage_totalTime = "00:00";
        private static Color timerPage_backgroundColor = Color.FromHex("#303030");
        private static Color timerPage_textColor = Color.FromHex("#ffffff");


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

                    // Timer Text
                    // Use previous slection screen to determine text number
                    timerPage_labelTimerText = new Label
                    {
                        Text = timerPage_totalTime,
                        FontSize = 64,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HeightRequest = 120,
                        BackgroundColor = timerPage_backgroundColor,
                        TextColor = timerPage_textColor,
                    };

                    //Font-Family
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        timerPage_labelTimerText.FontFamily = "OpenSans-Regular";
                    }
                    if(Device.RuntimePlatform == Device.Android)
                    {
                        timerPage_labelTimerText.FontFamily = "font/montserrat/MontserratAlternates-Bold.otf#MontserratAlternates-Bold"; // Figure out where this is looking to find the string
                    }

                    timerPage_stacklayout.Children.Add(timerPage_labelTimerText);
                }


                // StackLayout to fill with buttons
                if (timerPage_stackLayoutBitmapHolder != null)
                {

                }
                else
                {
                    // Visual Hold Label
                    if (timerPage_bitmapImage != null)
                    {

                    }
                    else
                    {





                        int rows = 128;
                        int cols = 64;
                        BmpMaker bmpMaker = new BmpMaker(cols, rows);

                        for (int row = 0; row < rows; row++)
                            for (int col = 0; col < cols; col++)
                            {
                                bmpMaker.SetPixel(row, col, 2 * row, 0, 2 * (128 - row));
                            }

                        ImageSource imageSource = bmpMaker.Generate();

                        timerPage_bitmapImage = new Image
                        {
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Aspect = Aspect.Fill,
                            Source = imageSource
                        };

                        // Button Holder Creation and fill with new buttons
                        timerPage_stackLayoutBitmapHolder = new StackLayout
                        {
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Margin = new Thickness(10),
                            BackgroundColor = Color.FromHex("#ffffff"),
                            Orientation = StackOrientation.Horizontal,
                        };

                        timerPage_stackLayoutBitmapHolder.Children.Add(timerPage_bitmapImage);
                        timerPage_stacklayout.Children.Add(timerPage_stackLayoutBitmapHolder);
                    }
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

        private static ImageSource BuildBitmapImage()
        {

            MemoryStream memoryStream = new MemoryStream();

            return null;
        }

    }

    
}
