using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopOutTrainer.ContentViews
{
    public partial class GraphPage : ContentPage
    {
        private Color mainColor = Color.FromHex("#303030");

        public GraphPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Grid mainG = new Grid
            {
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                BackgroundColor = Color.FromHex("#303030"),
                RowDefinitions =
                {
                    // 5 Rows
                    new RowDefinition { Height = new GridLength(10, GridUnitType.Star) },
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
            };

            StackLayout aStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("#FFFFFF")
            };

            aStack.Children.Add(new Image
            {
                BackgroundColor = Color.FromHex("#FFFFFF"),
                Margin = 0,
                Source = "construction.png",
                Aspect = Aspect.AspectFit
            });

            mainG.Children.Add(aStack, 0, 0);
            Grid.SetColumnSpan(aStack, 4);

            ImageButton button1 = new ImageButton
            {

                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "stopwatch_white_trans.png",
                Aspect = Aspect.AspectFit

            };
            button1.Clicked += TimerButtonClicked;
            // (4,1)
            ImageButton button2 = new ImageButton
            {
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "calendar.png",
                Aspect = Aspect.AspectFit


            };
            button2.Clicked += PlannerButtonClicked;

            // (4,2)
            ImageButton button3 = new ImageButton
            {
                BackgroundColor = Color.FromHex("#D3EFFC"),
                Margin = 0,
                CornerRadius = 0,
                Source = "graph_white.png",
                Aspect = Aspect.AspectFit

            };

            // (4,3)
            ImageButton button4 = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = mainColor,
                Margin = 0,
                CornerRadius = 0

            };
            mainG.Children.Add(button1, 0, 1);
            mainG.Children.Add(button2, 1, 1);
            mainG.Children.Add(button3, 2, 1);
            mainG.Children.Add(button4, 3, 1);


            Content = mainG;
            Content.BackgroundColor = Color.FromHex("#FFFFFF");
        }


        private void PlannerButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new PlannerPage());
        }

        private void TimerButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new TimerPage());
        }
    }
}
