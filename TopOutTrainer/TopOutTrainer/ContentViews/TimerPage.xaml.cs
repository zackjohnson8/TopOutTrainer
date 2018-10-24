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
        private ContentViewHandler_TimerPage myTimerView;

        public TimerPage ()
		{
            myTimerView = new ContentViewHandler_TimerPage();
            
            InitializeComponent ();
            InitializeView();
        }

        void InitializeView()
        {

            // Set the background color of TimerPage
            BackgroundColor = Color.FromHex(contentPageBackgroundColor);

            // Build the content view with ContentViewHandler_TimerPage 
            Content = myTimerView.GetContentView();

        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            //valueLabel.Text = args.NewValue.ToString("F3");
            //valueLabel.Text = ((Slider)sender).Value.ToString("F3");
        }

    }
}