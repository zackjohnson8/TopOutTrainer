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
        private ViewObjects.CustomPicker getReadyMinPicker;
        private ViewObjects.CustomPicker getReadySecPicker;
        private ViewObjects.CustomPicker startMinPicker;
        private ViewObjects.CustomPicker startSecPicker;

        private IList<string> numberChoiceReps = new List<string>();
        private IList<string> numberChoiceSets = new List<string>();
        private IList<string> numberChoiceMinutes = new List<string>();
        private IList<string> numberChoiceSeconds = new List<string>();

        private Color textColor = Color.FromHex("#303030");

        /////////
        private int holdRepValue;
        private int holdRepIndex;

        private int holdSetValue;
        private int holdSetIndex;

        private int holdRepSec;
        private int holdRepMin;
        private int holdSetSec;
        private int holdSetMin;
        private int holdGetReadyMin;
        private int holdGetReadySec;
        private int holdStartMin;
        private int holdStartSec;


        public TimerPageSettings()
        {
            // Reps is a value
            holdRepValue = StaticFiles.TimerPageUISettings.reps;
            holdSetValue = StaticFiles.TimerPageUISettings.sets;
            holdRepIndex = StaticFiles.TimerPageUISettings.reps - 1;
            holdSetIndex = StaticFiles.TimerPageUISettings.sets - 1;

            holdSetSec = StaticFiles.TimerPageUISettings.setsRestTime % 60;
            holdSetMin = StaticFiles.TimerPageUISettings.setsRestTime / 60;
            holdRepSec = StaticFiles.TimerPageUISettings.repsRestTime % 60;
            holdRepMin = StaticFiles.TimerPageUISettings.repsRestTime / 60;
            holdGetReadyMin = StaticFiles.TimerPageUISettings.getReadyTime / 60;
            holdGetReadySec = StaticFiles.TimerPageUISettings.getReadyTime % 60;
            holdStartMin = StaticFiles.TimerPageUISettings.startTime / 60;
            holdStartSec = StaticFiles.TimerPageUISettings.startTime % 60;
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

            // Save the value in selected index, all times are their corresponding index
            holdSetSec = setsSecPicker.GetSelectedNumber();
            holdSetMin = setsMinPicker.GetSelectedNumber();
            holdRepSec = repsSecPicker.GetSelectedNumber();
            holdRepMin = repsMinPicker.GetSelectedNumber();
            holdGetReadySec = getReadySecPicker.GetSelectedNumber();
            holdGetReadyMin = getReadyMinPicker.GetSelectedNumber();
            holdStartSec = startSecPicker.GetSelectedNumber();
            holdStartMin = startMinPicker.GetSelectedNumber();

            // Saving the value
            holdRepValue = repsPicker.GetSelectedNumber() + 1;
            holdSetValue = setsPicker.GetSelectedNumber() + 1;


            StaticFiles.TimerPageUISettings.reps = holdRepValue;
            StaticFiles.TimerPageUISettings.sets = holdSetValue;
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

            if (holdGetReadySec == 0)
            {
                StaticFiles.TimerPageUISettings.getReadyTime = (int)(holdGetReadyMin * 60);
            }
            else
            if (holdGetReadyMin == 0)
            {
                StaticFiles.TimerPageUISettings.getReadyTime = (int)holdGetReadySec;
            }
            else
            {
                int totalTimeInSeconds = (int)holdGetReadySec + (int)(holdGetReadyMin * 60);
                StaticFiles.TimerPageUISettings.getReadyTime = totalTimeInSeconds;
            }

            if (holdStartSec == 0)
            {
                StaticFiles.TimerPageUISettings.startTime = (int)(holdStartMin * 60);
            }
            else
            if (holdStartMin == 0)
            {
                StaticFiles.TimerPageUISettings.startTime = (int)holdStartSec;
            }
            else
            {
                int totalTimeInSeconds = (int)holdStartSec + (int)(holdStartMin * 60);
                StaticFiles.TimerPageUISettings.startTime = totalTimeInSeconds;
            }


            await file.WriteAllTextAsync("Reps:" + StaticFiles.TimerPageUISettings.reps.ToString() + ";" +
                                         "Sets:" + StaticFiles.TimerPageUISettings.sets.ToString() + ";" +
                                         "RepsTime:" + StaticFiles.TimerPageUISettings.repsRestTime.ToString() + ";" +
                                         "SetsTime:" + StaticFiles.TimerPageUISettings.setsRestTime.ToString() + ";" +
                                         "GetReadyTime:" + StaticFiles.TimerPageUISettings.getReadyTime.ToString() + ";" +
                                         "StartTime:" + StaticFiles.TimerPageUISettings.startTime.ToString() + ";");

            return true;
        }

        protected override async void OnDisappearing()
        {
            await SaveData();
            base.OnDisappearing();
        }


        private void OnSizeChanged(object sender, EventArgs e)
        {
            // Handle sizing of labels based on screen size
            if (this.Width > 0)
            {
                for (int index = 0; index < 60; index++)
                {
                    numberChoiceMinutes.Add(index.ToString() + " minutes");
                    numberChoiceSeconds.Add(index.ToString() + " seconds");
                    if (index != 0)
                    {
                        numberChoiceReps.Add(index.ToString() + " reps");
                        numberChoiceSets.Add(index.ToString() + " sets");
                    }
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
                                    SelectedIndex = holdRepIndex
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
                                    SelectedIndex = holdSetIndex

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

                            },
                            new ViewObjects.PickerCell()
                            {

                                Label1 = new Label
                                {
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = textColor,
                                    Text = "Get Ready Time:",
                                    FontSize = this.Width/24
                                },
                                Picker1 = getReadyMinPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Minutes",
                                    ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdGetReadyMin
                                },
                                Picker2 = getReadySecPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Seconds",
                                    ItemsSource = (System.Collections.IList)numberChoiceSeconds,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdGetReadySec
                                }

                            },
                            new ViewObjects.PickerCell()
                            {

                                Label1 = new Label
                                {
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = textColor,
                                    Text = "Start Time:",
                                    FontSize = this.Width/24
                                },
                                Picker1 = startMinPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Minutes",
                                    ItemsSource = (System.Collections.IList)numberChoiceMinutes,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdStartMin
                                },
                                Picker2 = startSecPicker = new ViewObjects.CustomPicker()
                                {
                                    Title = "Seconds",
                                    ItemsSource = (System.Collections.IList)numberChoiceSeconds,
                                    HorizontalOptions = LayoutOptions.End,
                                    FontSize = this.Width/20,
                                    SelectedIndex = holdStartSec
                                }

                            },
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
                getReadySecPicker.SelectedIndexChanged += GetReadySecPicker_SelectedIndexChanged;
                getReadyMinPicker.SelectedIndexChanged += GetReadyMinPicker_SelectedIndexChanged;
                startSecPicker.SelectedIndexChanged += StartSecPicker_SelectedIndexChanged;
                startMinPicker.SelectedIndexChanged += StartMinPicker_SelectedIndexChanged;
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
            holdRepIndex = repsPicker.SelectedIndex;
            holdRepValue = repsPicker.SelectedIndex + 1;
        }

        void SetsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Write("changed to: " + setsPicker.SelectedIndex);
            holdSetIndex = setsPicker.SelectedIndex;
            holdSetValue = setsPicker.SelectedIndex + 1;
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

        void GetReadySecPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            holdGetReadySec = getReadySecPicker.SelectedIndex;
        }

        void GetReadyMinPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            holdGetReadyMin = getReadyMinPicker.SelectedIndex;
        }

        void StartSecPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            holdStartSec = startSecPicker.SelectedIndex;
        }

        void StartMinPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            holdStartMin = startMinPicker.SelectedIndex;
        }

    }
}