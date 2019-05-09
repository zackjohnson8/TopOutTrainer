using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PCLStorage;
using System.Diagnostics;

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

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            // Get the data from the saved file to set null values
            String folderName = "timerpage";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            String fileName = "setting.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            if(file)
            {
                String readFileData = await file.ReadAllTextAsync();
                readFileData.IndexOf(':');
            }else
            {

            }






            if ((int)repsPicker.SelectedIndex >= 0)
            {
                StaticFiles.TimerPageUISettings.reps = (int)repsPicker.SelectedIndex;
            }else
            {

            }

            if ((int)setsPicker.SelectedIndex >= 0)
            {
                StaticFiles.TimerPageUISettings.sets = (int)setsPicker.SelectedIndex;
            }else
            {

            }

            if ((int)repsSecPicker.SelectedIndex >= 0 || (int)repsMinPicker.SelectedIndex >= 0)
            {
                int totalTimeInSeconds = (int)repsSecPicker.SelectedIndex + (int)(repsMinPicker.SelectedIndex * 60);
                StaticFiles.TimerPageUISettings.repsRestTime = totalTimeInSeconds;
            }else
            {

            }

            if((int)setsSecPicker.SelectedIndex >= 0 || (int)setsMinPicker.SelectedIndex >= 0)
            {
                int totalTimeInSeconds = (int)setsSecPicker.SelectedIndex + (int)(setsMinPicker.SelectedIndex * 60);
                StaticFiles.TimerPageUISettings.setsRestTime = totalTimeInSeconds;
            }else
            {

            }



            await file.WriteAllTextAsync("Reps: " + StaticFiles.TimerPageUISettings.reps.ToString() + System.Environment.NewLine +
                                         "Sets: " + StaticFiles.TimerPageUISettings.sets.ToString() + System.Environment.NewLine +
                                         "RepsTime: " + StaticFiles.TimerPageUISettings.repsRestTime.ToString() + System.Environment.NewLine +
                                         "SetsTime: " + StaticFiles.TimerPageUISettings.setsRestTime.ToString() + System.Environment.NewLine);

        }

        // LIST OF SETTINGS
        // * Num of reps
        // * Num of sets
        // * Break time between reps
        // * Break time between sets
        // *
        private ViewObjects.CustomPicker repsPicker;
        private ViewObjects.CustomPicker setsPicker;
        private ViewObjects.CustomPicker repsMinPicker;
        private ViewObjects.CustomPicker repsSecPicker;
        private ViewObjects.CustomPicker setsMinPicker;
        private ViewObjects.CustomPicker setsSecPicker;

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
                            Picker1 = repsPicker = new ViewObjects.CustomPicker()
                            {
                                Title="Repetitions",
                                ItemsSource = (System.Collections.IList)numberChoiceReps,
                                HorizontalOptions = LayoutOptions.End

                            }
                        },
                        new ViewObjects.PickerCell()
                        {
                            Label = "Number of Sets:",
                            Picker1 = setsPicker = new ViewObjects.CustomPicker()
                            {
                                Title="Sets",
                                ItemsSource = (System.Collections.IList)numberChoiceSets,
                                HorizontalOptions = LayoutOptions.End

                            }
                        },
                        new ViewObjects.PickerCell()
                        {
                            Label = "Break between Reps:",
                            Picker1 = repsMinPicker = new ViewObjects.CustomPicker()
                            {
                                Title = "Minutes",
                                ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                HorizontalOptions = LayoutOptions.End

                            },
                            Picker2 = repsSecPicker = new ViewObjects.CustomPicker()
                            {
                                Title = "Seconds",
                                ItemsSource = (System.Collections.IList)numberChoiceSeconds,
                                HorizontalOptions = LayoutOptions.End

                            }

                        },
                        new ViewObjects.PickerCell()
                        {
                            Label = "Break between Sets:",
                            Picker1 = setsMinPicker = new ViewObjects.CustomPicker()
                            {
                                Title = "Minutes",
                                ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                HorizontalOptions = LayoutOptions.End

                            },
                            Picker2 = setsSecPicker = new ViewObjects.CustomPicker()
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