using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TopOutTrainer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
        private const string contentPageBackgroundColor = "#303030";
        private StackLayout stacklayout;
        private Label labelTimerText;
        private Label labelVisualHold;
        private StackLayout stackLayoutButtonHolder;

        public TimerPage ()
		{
			InitializeComponent ();
            BackgroundColor = Color.FromHex(contentPageBackgroundColor);
            InitializeView();
            
        }

        private void InitializeView()
        {
            // Main Layout
            stacklayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
            };

            // Top Text Label
            labelTimerText = new Label
            {

                Text = "TIMERTEXT",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 120,
                BackgroundColor = Color.FromHex("#000000")
            };
            stacklayout.Children.Add(labelTimerText);

            // Visual Hold Label
            labelVisualHold = new Label
            {

                Text = "TIMERTEXT",
                FontSize = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 120,
                BackgroundColor = Color.FromHex("#000000")
            };
            stacklayout.Children.Add(labelVisualHold);

            // StackLayout to fill with buttons
            stackLayoutButtonHolder = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10),
                BackgroundColor = Color.FromHex("#000000")
            };
            stacklayout.Children.Add(stackLayoutButtonHolder);

            Content = stacklayout;
        }

        /*
        <!-- Timer Number -->
            <Label x:Name="valueLabel"
                   Text="Welcome to Xamarin.Forms!"
                   VerticalOptions="CenterAndExpand" 
                   HorizontalOptions="CenterAndExpand" 
                   HeightRequest="120"
                   BackgroundColor="Blue"
                   />

            <!-- Visual Representation -->
            <Slider VerticalOptions = "CenterAndExpand"
                    ValueChanged="OnSliderValueChanged"
                    HeightRequest="120"
                    BackgroundColor="Blue"
                    />

            <!-- Buttons -->
            <Label Text = "A simple Label"
                   Font="Large"
                   HorizontalOptions="Center"
                   VerticalOptions="CenterAndExpand"
                   HeightRequest="120"
                   BackgroundColor="Green"
                   />
        */

        // TODO: REMOVE
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            //valueLabel.Text = args.NewValue.ToString("F3");
            //valueLabel.Text = ((Slider)sender).Value.ToString("F3");
        }

        // TODO: REMOVE
        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("Clicked!",
                "The button labeled '" + button.Text + "' has been clicked",
                "OK");
        }
    }
}