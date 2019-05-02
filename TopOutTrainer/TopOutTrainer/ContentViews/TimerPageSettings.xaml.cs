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

        // LIST OF SETTINGS
        // * Num of reps
        // * Num of sets
        // * Break time between reps
        // * Break time between sets
        // *

        private void SetView()
        {


            IList<string> numberChoiceReps = new List<string>();
            IList<string> numberChoiceSets = new List<string>();
            IList<string> numberChoiceMinutes = new List<string>();
            IList<string> numberChoiceSeconds = new List<string>();
            for(int index = 0; index < 60; index++)
            {
                numberChoiceMinutes.Add(index.ToString() + " minutes");
                numberChoiceSeconds.Add(index.ToString() + " seconds");
                numberChoiceReps.Add(index.ToString() + " reps");
                numberChoiceSets.Add(index.ToString() + " sets");

            }

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

                        },
                        new ViewObjects.PickerCell()
                        {
                            Label = "Number of Reps:",
                            Picker1 = new ViewObjects.CustomPicker()
                            {
                                Title="Repetitions",
                                ItemsSource = (System.Collections.IList)numberChoiceReps,
                                HorizontalOptions = LayoutOptions.End

                            }
                        },
                        new ViewObjects.PickerCell()
                        {
                            Label = "Number of Sets:",
                            Picker1 = new ViewObjects.CustomPicker()
                            {
                                Title="Sets",
                                ItemsSource = (System.Collections.IList)numberChoiceSets,
                                HorizontalOptions = LayoutOptions.End

                            }
                        },
                        new ViewObjects.PickerCell()
                        {
                            Label = "Break between Reps:",
                            Picker1 = new ViewObjects.CustomPicker()
                            {
                                Title = "Minutes",
                                ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                HorizontalOptions = LayoutOptions.End

                            },
                            Picker2 = new ViewObjects.CustomPicker()
                            {
                                Title = "Seconds",
                                ItemsSource = (System.Collections.IList)numberChoiceSeconds,
                                HorizontalOptions = LayoutOptions.End

                            }

                        },
                        new ViewObjects.PickerCell()
                        {
                            Label = "Break between Sets:",
                            Picker1 = new ViewObjects.CustomPicker()
                            {
                                Title = "Minutes",
                                ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                HorizontalOptions = LayoutOptions.End

                            },
                            Picker2 = new ViewObjects.CustomPicker()
                            {
                                Title = "Seconds",
                                ItemsSource = (System.Collections.IList)numberChoiceSeconds,
                                HorizontalOptions = LayoutOptions.End

                            }

                        }
                    }
                },
                Intent = TableIntent.Settings
            };
        }


    }
}