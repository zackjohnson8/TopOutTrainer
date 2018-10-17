using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TopOutTrainer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
        private const string contentPageBackgroundColor = "#303030";

        private Timer timer;
        private int timerSecond;
        private int timerMinute;

        public TimerPage ()
		{
            timerSecond = 0;
            timerMinute = 0;
			InitializeComponent ();
            BackgroundColor = Color.FromHex(contentPageBackgroundColor);
            InitializeView();
            
        }

        void InitializeView()
        {

            Content = ContentViewHandler.BuildContentView(ContentViewHandler.EnumViews.TimerPageView);
            ContentViewHandler.timerPage_buttonStart.Clicked += new EventHandler(OnButtonClicked);

        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            //valueLabel.Text = args.NewValue.ToString("F3");
            //valueLabel.Text = ((Slider)sender).Value.ToString("F3");
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(1000); // Using 1000ms decreases lag
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            timerSecond += 1;
            ContentViewHandler.timerPage_labelTimerText.Text = "HELLO";// String.Concat(timerSecond, ':', timerSecond);
            Application.DoEvents();

        }
    }
}