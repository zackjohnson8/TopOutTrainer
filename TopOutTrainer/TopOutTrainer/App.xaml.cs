using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


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
