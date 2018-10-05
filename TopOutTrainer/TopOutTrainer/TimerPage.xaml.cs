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

        public TimerPage ()
		{
			InitializeComponent ();
            BackgroundColor = Color.FromHex(contentPageBackgroundColor);
            InitializeView();
            
        }

        void InitializeView()
        {
            TopOutTrainer.ContentViewHandler.BuildContentView(Content, ContentViewHandler.EnumViews.TimerPageView);
        }

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