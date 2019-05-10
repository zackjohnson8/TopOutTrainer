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
            
            SetView();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            repsPicker.SelectedIndex = StaticFiles.TimerPageUISettings.reps;
            setsPicker.SelectedIndex = StaticFiles.TimerPageUISettings.sets;

            setsSecPicker.SelectedIndex = StaticFiles.TimerPageUISettings.setsRestTime % 60;
            setsMinPicker.SelectedIndex = StaticFiles.TimerPageUISettings.setsRestTime / 60;
            repsSecPicker.SelectedIndex = StaticFiles.TimerPageUISettings.repsRestTime % 60;
            repsMinPicker.SelectedIndex = StaticFiles.TimerPageUISettings.repsRestTime / 60;
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            // Get the data from the saved file to set null values
            String folderName = "timerpage";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            String fileName = "setting.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);


            if ((int)repsPicker.SelectedIndex >= 0)
            {
                StaticFiles.TimerPageUISettings.reps = (int)repsPicker.SelectedIndex;
            }

            if ((int)setsPicker.SelectedIndex >= 0)
            {
                StaticFiles.TimerPageUISettings.sets = (int)setsPicker.SelectedIndex;
            }

            if ((int)repsSecPicker.SelectedIndex >= 0 || (int)repsMinPicker.SelectedIndex >= 0)
            {
                if(repsSecPicker.SelectedIndex < 0)
                {
                    StaticFiles.TimerPageUISettings.repsRestTime = (int)(repsMinPicker.SelectedIndex * 60);
                }else
                if(repsMinPicker.SelectedIndex < 0)
                {
                    StaticFiles.TimerPageUISettings.repsRestTime = (int)repsSecPicker.SelectedIndex;
                }else
                {
                    int totalTimeInSeconds = (int)repsSecPicker.SelectedIndex + (int)(repsMinPicker.SelectedIndex * 60);
                    StaticFiles.TimerPageUISettings.repsRestTime = totalTimeInSeconds;
                }

            }

            if((int)setsSecPicker.SelectedIndex >= 0 || (int)setsMinPicker.SelectedIndex >= 0)
            {

                if (setsSecPicker.SelectedIndex < 0)
                {
                    StaticFiles.TimerPageUISettings.setsRestTime = (int)(setsMinPicker.SelectedIndex * 60);
                }
                else
                if (repsMinPicker.SelectedIndex < 0)
                {
                    StaticFiles.TimerPageUISettings.setsRestTime = (int)setsSecPicker.SelectedIndex;
                }
                else
                {
                    int totalTimeInSeconds = (int)setsSecPicker.SelectedIndex + (int)(setsMinPicker.SelectedIndex * 60);
                    StaticFiles.TimerPageUISettings.setsRestTime = totalTimeInSeconds;
                }
            }

            //file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            await file.WriteAllTextAsync("Reps:" + StaticFiles.TimerPageUISettings.reps.ToString() + ";" +
                                         "Sets:" + StaticFiles.TimerPageUISettings.sets.ToString() + ";" +
                                         "RepsTime:" + StaticFiles.TimerPageUISettings.repsRestTime.ToString() + ";" +
                                         "SetsTime:" + StaticFiles.TimerPageUISettings.setsRestTime.ToString() + ";");


            await StaticFiles.TimerPageUISettings.SetFromFile();
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