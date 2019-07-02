using System;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using TopOutTrainer.Factories;

namespace TopOutTrainer.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public class TimerPage : ContentPage
	{
        //private TopOutTrainer.ContentViews.TimerPageSettings myTimerSettings;

        private TimerContentFactory myTimerContentFactory = new TimerContentFactory();

        public TimerPage()
        {
            // When size is determined have an event that fixes sizes based on phone of choice
            SizeChanged += OnSizeChanged;

            // Hide nav bar and begin building of contentpage
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }

            View myView = myTimerContentFactory.CreateContentFirstView();

            myTimerContentFactory.optionB.Clicked += OptionButtonClicked;
            myTimerContentFactory.calendarButton.Clicked += PlannerButtonClicked;
            myTimerContentFactory.graphButton.Clicked += GraphButtonClicked;

            Content = myView;
        }

        private async Task<bool> GetSavedData()
        {
            await StaticFiles.TimerPageUISettings.SetFromFile();
            return true;
        }

        private void RefreshContent()
        {

            // Set reps and sets for change
            Device.BeginInvokeOnMainThread(() =>
            {
                myTimerContentFactory.repsNumL.Text = StaticFiles.TimerPageUISettings.reps.ToString();
                myTimerContentFactory.setsNumL.Text = StaticFiles.TimerPageUISettings.sets.ToString();
            });
            

            // Set timerNumL with new total minutes and seconds
            // Each rep and set break follows with a get ready and start time
            int getReadyAndStart = StaticFiles.TimerPageUISettings.reps * StaticFiles.TimerPageUISettings.sets * (StaticFiles.TimerPageUISettings.getReadyTime + StaticFiles.TimerPageUISettings.startTime);

            // Number of breaks we'll be taking both rep and set total time
            int breakReps = StaticFiles.TimerPageUISettings.reps * (StaticFiles.TimerPageUISettings.reps - 1);
            int breakSets = StaticFiles.TimerPageUISettings.sets - 1;

            int totalTime = getReadyAndStart + (breakReps * StaticFiles.TimerPageUISettings.repsRestTime) + (breakSets * StaticFiles.TimerPageUISettings.setsRestTime);
            int totalTimeMinutes = totalTime / 60;
            int totalTimeSeconds = totalTime % 60;

            string timeMin;
            if (totalTimeMinutes <= 9)
            {
                timeMin = string.Concat('0' + totalTimeMinutes.ToString());
            }
            else
            {
                timeMin = totalTimeMinutes.ToString();
            }

            string timeSec;
            if (totalTimeSeconds <= 9)
            {
                timeSec = string.Concat('0' + totalTimeSeconds.ToString());
            }
            else
            {
                timeSec = totalTimeSeconds.ToString();
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                myTimerContentFactory.timerNumL.Text = string.Concat(timeMin + ':' + timeSec);
            });

            Content = myTimerContentFactory.CreateContentFirstView();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh all content that exists with saved file when appeared
            // This takes care of the case of coming back from TimerPageSetting contentpage
            RefreshContent();

        }

        private void ResetAll()
        {
            if(myTimerContentFactory.timerHandler != null)
            {
                myTimerContentFactory.timerHandler.Stop();
            }
        }

        private async void OnSizeChanged(object sender, EventArgs e)
        {

            await GetSavedData();
            RefreshContent();


            // Handle sizing of labels based on screen size
            if (this.Width > 0)
            {
                // Handle button sizing
                myTimerContentFactory.startbutton.WidthRequest = myTimerContentFactory.startbutton.Height;
                myTimerContentFactory.startbutton.HeightRequest = myTimerContentFactory.startbutton.Height;
                myTimerContentFactory.startbutton.CornerRadius = (int)myTimerContentFactory.startbutton.Height / 2;

                myTimerContentFactory.resetButton.HeightRequest = this.Height;
                myTimerContentFactory.stopButton.HeightRequest = this.Height;
                myTimerContentFactory.resetButton.Margin = this.Width / 20;
                myTimerContentFactory.stopButton.Margin = this.Width / 20;

                // Numbers
                myTimerContentFactory.timerNumL.FontSize = this.Width / 8;
                myTimerContentFactory.repsNumL.FontSize = this.Width / 8;
                myTimerContentFactory.setsNumL.FontSize = this.Width / 8;

                myTimerContentFactory.getReadyL.FontSize = this.Width / 8;
                myTimerContentFactory.timerL.FontSize = this.Width / 8;

                // 
                myTimerContentFactory.intervalL.FontSize = this.Width / 12;
                myTimerContentFactory.repsL.FontSize = this.Width / 14;
                myTimerContentFactory.setsL.FontSize = this.Width / 14;
                myTimerContentFactory.totalTimeL.FontSize = this.Width / 14;
            }
            //Create_BitMap();
        }

        private async void OptionButtonClicked(object sender, EventArgs args)
        {

            // Blackout/opacity clear
            Content = null;
            await Navigation.PushAsync(new TimerPageSettings() { Title = "Settings" });
            // Reset all
            ResetAll();
        }

        private void GraphButtonClicked(object sender, EventArgs args)
        {
            //Content = null;
            //Navigation.PushAsync(new GraphPage());
            //ResetAll();
        }

        private void PlannerButtonClicked(object sender, EventArgs args)
        {
            Content = null;
            Navigation.PushAsync(new PlannerPage());
            ResetAll();
        }

    }
}