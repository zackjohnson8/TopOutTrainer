using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TopOutTrainer
{
    public partial class App : Application
    {

        private const string bannerBackgroundColor = "#303030";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
