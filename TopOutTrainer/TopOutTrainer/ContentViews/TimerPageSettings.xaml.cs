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

        private IList<string> numberChoiceReps = new List<string>();
        private IList<string> numberChoiceSets = new List<string>();
        private IList<string> numberChoiceMinutes = new List<string>();
        private IList<string> numberChoiceSeconds = new List<string>();

        private Color textColor = Color.FromHex("#303030");

        /////////
        private int holdRep;
        private int holdSet;
        private int holdRepSec;
        private int holdRepMin;
        private int holdSetSec;
        private int holdSetMin;


        public TimerPageSettings()
        {

            holdRep = StaticFiles.TimerPageUISettings.reps;
            holdSet = StaticFiles.TimerPageUISettings.sets;

            holdSetSec = StaticFiles.TimerPageUISettings.setsRestTime % 60;
            holdSetMin = StaticFiles.TimerPageUISettings.setsRestTime / 60;
            holdRepSec = StaticFiles.TimerPageUISettings.repsRestTime % 60;
            holdRepMin = StaticFiles.TimerPageUISettings.repsRestTime / 60;
            //SetView();
            SizeChanged += OnSizeChanged;
        }

        private async Task<bool> SaveData()
        {
            // Get the data from the saved file to set null values
            String folderName = "timerpage";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            String fileName = "setting.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            holdRep = repsPicker.GetSelectedNumber();
            holdSet = setsPicker.GetSelectedNumber();

            holdSetSec = setsSecPicker.GetSelectedNumber();
            holdSetMin = setsMinPicker.GetSelectedNumber();
            holdRepSec = repsSecPicker.GetSelectedNumber();
            holdRepMin = repsMinPicker.GetSelectedNumber();


            StaticFiles.TimerPageUISettings.reps = holdRep;
            StaticFiles.TimerPageUISettings.sets = holdSet;
            if (holdRepSec == 0 )
            {
                StaticFiles.TimerPageUISettings.repsRestTime = (int)(holdRepMin * 60);
            }
            else
            if (holdRepMin == 0)
            {
                StaticFiles.TimerPageUISettings.repsRestTime = (int)holdRepSec;
            }
            else
            {
                int totalTimeInSeconds = (int)holdRepSec + (int)(holdRepMin * 60);
                StaticFiles.TimerPageUISettings.repsRestTime = totalTimeInSeconds;
            }

            if (holdSetSec == 0)
            {
                StaticFiles.TimerPageUISettings.setsRestTime = (int)(holdSetMin * 60);
            }
            else
            if (holdSetMin == 0)
            {
                StaticFiles.TimerPageUISettings.setsRestTime = (int)holdSetSec;
            }
            else
            {
                int totalTimeInSeconds = (int)holdSetSec + (int)(holdSetMin * 60);
                StaticFiles.TimerPageUISettings.setsRestTime = totalTimeInSeconds;
            }
            

            await file.WriteAllTextAsync("Reps:" + StaticFiles.TimerPageUISettings.reps.ToString() + ";" +
                                         "Sets:" + StaticFiles.TimerPageUISettings.sets.ToString() + ";" +
                                         "RepsTime:" + StaticFiles.TimerPageUISettings.repsRestTime.ToString() + ";" +
                                         "SetsTime:" + StaticFiles.TimerPageUISettings.setsRestTime.ToString() + ";");

            return true;
        }

        protected override async void OnDisappearing()
        {
            await SaveData();
            base.OnDisappearing();
        }


        private void OnSizeChanged(object sender, EventArgs e)
        {
            Debug.Write("well");
            // Handle sizing of labels based on screen size
            if (this.Width > 0)
            {
                for (int index = 0; index < 60; index++)
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
                                Label1 = new Label
                                {
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = textColor,
                                    Text = "Number of Reps:",
                                    FontSize = this.Width/24
                                },
                                Picker1 = repsPicker = new ViewObjects.CustomPicker()
                                {
                                    Title="Repetitions",
                                    ItemsSource = (System.Collections.IList)numberChoiceReps,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdRep
                                }
                            },
                            new ViewObjects.PickerCell()
                            {

                                Label1 = new Label
                                {
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = textColor,
                                    Text = "Number of Sets:",
                                    FontSize = this.Width/24
                                },
                                Picker1 = setsPicker = new ViewObjects.CustomPicker()
                                {
                                    Title="Sets",
                                    ItemsSource = (System.Collections.IList)numberChoiceSets,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdSet

                                }
                            },
                            new ViewObjects.PickerCell()
                            {

                                Label1 = new Label
                                {
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = textColor,
                                    Text = "Rep Break:",
                                    FontSize = this.Width/24
                                },
                                Picker1 = repsMinPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Minutes",
                                    ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdRepMin
                                },
                                Picker2 = repsSecPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Seconds",
                                    ItemsSource = (System.Collections.IList)numberChoiceSeconds,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdRepSec
                                }

                            },
                            new ViewObjects.PickerCell()
                            {
                                Label1 = new Label
                                {
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = textColor,
                                    Text = "Set Break:",
                                    FontSize = this.Width/24
                                },
                                Picker1 = setsMinPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Minutes",
                                    ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdSetMin

                                },
                                Picker2 = setsSecPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Seconds",
                                    ItemsSource = (System.Collections.IList)numberChoiceSeconds,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdSetSec

                                }

                            }
                        }
                    },
                    Intent = TableIntent.Settings
                };

                setsSecPicker.SelectedIndexChanged += SetsSecPicker_SelectedIndexChanged;
                setsMinPicker.SelectedIndexChanged += SetsMinPicker_SelectedIndexChanged;
                repsSecPicker.SelectedIndexChanged += RepsSecPicker_SelectedIndexChanged;
                repsMinPicker.SelectedIndexChanged += RepsMinPicker_SelectedIndexChanged;
                repsPicker.SelectedIndexChanged += RepsPicker_SelectedIndexChanged;
                setsPicker.SelectedIndexChanged += SetsPicker_SelectedIndexChanged;
            }
        }

        void SetsSecPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Write("changed to: " + setsSecPicker.SelectedIndex);
            holdSetSec = setsSecPicker.SelectedIndex;
        }

        void RepsSecPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Write("changed to: " + repsSecPicker.SelectedIndex);
            holdRepSec = repsSecPicker.SelectedIndex;
        }

        void RepsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Write("changed to: " + repsPicker.SelectedIndex);
            holdRep = repsPicker.SelectedIndex;
        }

        void SetsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Write("changed to: " + setsPicker.SelectedIndex);
            holdSet = setsPicker.SelectedIndex;
        }

        void SetsMinPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Write("changed to: " + setsMinPicker.SelectedIndex);
            holdSetMin = setsMinPicker.SelectedIndex;
        }

        void RepsMinPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Write("changed to: " + repsMinPicker.SelectedIndex);
            holdRepMin = repsMinPicker.SelectedIndex;
        }

    }
}