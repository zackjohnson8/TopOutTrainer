using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


// APPLICATION START POINT
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TopOutTrainer
{
    public partial class App : Application
    {

        private const string bannerBackgroundColor = "#303030";

        public App()
        {
            InitializeComponent();

            // Start with timer page
            MainPage = new NavigationPage(new TopOutTrainer.ContentViews.TimerPage());
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex(bannerBackgroundColor));

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=4e385945-ec3d-4fc5-9c3a-12ec1f669da1;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
