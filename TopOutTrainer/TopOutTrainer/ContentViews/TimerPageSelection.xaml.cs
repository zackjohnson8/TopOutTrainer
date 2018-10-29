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
	public partial class TimerPageSelection : ContentPage
	{
        private Color myBackgroundColor = Color.FromHex("#303030");
        private ContentViewHandler_TimerPageSelection myTimerView;

        public TimerPageSelection()
        {

            myTimerView = new ContentViewHandler_TimerPageSelection();

            InitializeComponent();
            InitializeView();

            myTimerView.buttonStart.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new ContentViews.TimerPage());
            };

        }

        void InitializeView()
        {

            // Set the background color of TimerPage
            BackgroundColor = myBackgroundColor;

            // Build the content view with ContentViewHandler_TimerPage 
            Content = myTimerView.GetContentView();

        }
    }
}