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
		public TimerPage ()
		{
			InitializeComponent ();

            
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            //valueLabel.Text = args.NewValue.ToString("F3");
            valueLabel.Text = ((Slider)sender).Value.ToString("F3");
        }

        void OnButtonClicked(object sender, EventArgs args)
        {

        }
    }
}