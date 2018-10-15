using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TopOutTrainer
{
    public partial class MainPage : ContentPage // Partial class InitializeComponent in XamlSamples\XamlSamples\obj\Debug
    {
        public MainPage()
        {
            InitializeComponent();
            //Navigation.PushAsync(new TimerPage());

            Button button = new Button
            {
                Text = "Navigate!",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new TimerPage());
            };

            Content = button;
        }
    }
}
