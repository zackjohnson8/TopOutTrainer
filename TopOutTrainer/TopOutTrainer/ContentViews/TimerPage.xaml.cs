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

        private StopWatch stopwatch;

        public TimerPage ()
		{

			InitializeComponent ();
            InitializeView();
            
        }

        void InitializeView()
        {

            BackgroundColor = Color.FromHex(contentPageBackgroundColor);

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

        }

    }
}