using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TopOutTrainer.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
        private Color myBackgroundColor = Color.FromHex("#303030");
        private ContentViewHandler_TimerPage myTimerView;

        public TimerPage (int hangTime_min, int hangTime_sec, int restTime_min, int restTime_sec, int hangInterval_count)
		{
            myTimerView = new ContentViewHandler_TimerPage(hangTime_min, hangTime_sec, restTime_min, restTime_sec, hangInterval_count);
            
            // TODO: what is initializecomponent
            //InitializeComponent ();
            InitializeView();
        }

        void InitializeView()
        {

            // Set the background color of TimerPage
            BackgroundColor = myBackgroundColor;

            // Build the content view with ContentViewHandler_TimerPage 
            Content = myTimerView.GetContentView();

            StopWatch.AddLabelToDraw(myTimerView.labelTimerText);
            //StopWatch.Start();

        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            //valueLabel.Text = args.NewValue.ToString("F3");
            //valueLabel.Text = ((Slider)sender).Value.ToString("F3");
        }

    }
}