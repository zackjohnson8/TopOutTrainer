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
    public partial class TimerPageSettings : ContentPage
	{



        /////////

        public TimerPageSettings()
        {

            //NavigationPage.SetHasNavigationBar(this, false);
            SetView();

        }

        private void SetView()
        {
            Content = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection{
                        new ImageCell
                        {
                            ImageSource = "stopwatch_white_trans.png",
                            TextColor = Color.Green,
                            Text = "This is the text written to this option imagecell",

                        }
                    }
                },
                Intent = TableIntent.Settings
            };
        }


    }
}