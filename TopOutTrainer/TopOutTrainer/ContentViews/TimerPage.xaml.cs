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
        private StopWatch stopwatch;

        public TimerPage ()
		{
			InitializeComponent ();
            InitializeView();
        }

        void InitializeView()
        {

            // Set the background color of TimerPage
            BackgroundColor = Color.FromHex(contentPageBackgroundColor);
            
            Content = ContentViewHandler.BuildContentView(ContentViewHandler.EnumViews.TimerPageView);
            ContentViewHandler.timerPage_buttonStart.Clicked += new EventHandler(OnButtonClicked);
            stopwatch = new StopWatch(ContentViewHandler.timerPage_labelTimerText);
            stopwatch.Start();

        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            //valueLabel.Text = args.NewValue.ToString("F3");
            //valueLabel.Text = ((Slider)sender).Value.ToString("F3");
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            if(stopwatch != null)
            {
                stopwatch = new StopWatch(ContentViewHandler.timerPage_labelTimerText);
            }
        }

    }
}