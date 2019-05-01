using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopOutTrainer.ContentViews
{
    public partial class PlannerPage : ContentPage
    {
        public PlannerPage()
        {
            InitializeComponent();

            StackLayout aStack = new StackLayout {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("#FFFFFF")
            };

            aStack.Children.Add(new Image {
                BackgroundColor = Color.FromHex("#FFFFFF"),
                Margin = 0,
                Source = "construction.png", 
                Aspect = Aspect.AspectFit 
                });
            Content = aStack;
            Content.BackgroundColor = Color.FromHex("#FFFFFF");
        }
    }
}
