using System;
using System.Diagnostics;
using Xamarin.Forms;


namespace TopOutTrainer.Factories
{

    public enum ViewChoice
    {
        MONDAY,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY,
        SATURDAY,
        SUNDAY,
        NULL
    }


    public class PlannerContentFactory
    {

        private Grid mainContentView = null;
        private Grid upperBar = null;
        private Grid bottomNavBar = null;
        private Image imageDayOfWeek = null;
        public ImageButton calendarButton { get; private set; } = null;
        public ImageButton timerButton { get; private set; } = null;
        private int holdDateCount = 0;

        public PlannerContentFactory()
        {
            
        }

        public View CreateViewAuto()
        {

            DateTime dt = DateTime.Now;
            var currentDay = dt.DayOfWeek;

            switch(currentDay)
            {
                case DayOfWeek.Monday:
                    return CreateMondayView();
                    
                case DayOfWeek.Tuesday:
                    return CreateTuesdayView();
                    
                case DayOfWeek.Wednesday:
                    return CreateWednesdayView();
                    
                case DayOfWeek.Thursday:
                    return CreateThursdayView();
                    
                case DayOfWeek.Friday:
                    return CreateFridayView();
                    
                case DayOfWeek.Saturday:
                    return CreateSaturdayView();
                    
                case DayOfWeek.Sunday:
                    return CreateSundayView();
                    
                default:
                    return CreateMondayView();
            }

        }

        public View CreateViewSelected(DayOfWeek selectedDay)
        {

            switch (selectedDay)
            {
                case DayOfWeek.Monday:
                    return CreateMondayView();

                case DayOfWeek.Tuesday:
                    return CreateTuesdayView();

                case DayOfWeek.Wednesday:
                    return CreateWednesdayView();

                case DayOfWeek.Thursday:
                    return CreateThursdayView();

                case DayOfWeek.Friday:
                    return CreateFridayView();

                case DayOfWeek.Saturday:
                    return CreateSaturdayView();

                case DayOfWeek.Sunday:
                    return CreateSundayView();

                default:
                    return CreateMondayView();
            }

        }

        public View CreateViewLeft()
        {
            Debug.Write("LeftSelect");
            holdDateCount++;
            return CreateViewSelected(DateTime.Today.AddDays(holdDateCount).DayOfWeek);
            //return CreateViewSelected(DayOfWeek.Friday);
        }

        public View CreateViewRight()
        {
            Debug.Write("RightSelect");
            holdDateCount--;
            return CreateViewSelected(DateTime.Today.AddDays(holdDateCount).DayOfWeek);
            //return CreateViewSelected(DayOfWeek.Saturday);
        }

        private View CreateMondayView()
        {

            if(mainContentView == null)
            {
                mainContentView = new Grid
                {
                    BackgroundColor = Color.Blue,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(1.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(7.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                };
            }
            else
            {
                mainContentView.BackgroundColor = Color.Blue;
            }
            

            // Upper bar with Monday
            if(upperBar == null)
            {
                upperBar = CreateUpperView();
                mainContentView.Children.Add(upperBar, 0, 0);

            }
            else
            {
                imageDayOfWeek.Source = "monday_text_gray.png";
            }



            // Middle area with different workouts added
            //var middleView = CreateMiddleView();

            // Add new workout round button
            //var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            if (bottomNavBar == null)
            {
                bottomNavBar = CreateNavBar();
                mainContentView.Children.Add(bottomNavBar, 0, 3);
            }


            return mainContentView;
        }

        private View CreateTuesdayView()
        {

            if (mainContentView == null)
            {
                mainContentView = new Grid
                {
                    BackgroundColor = Color.Green,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(1.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(7.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                };
            }
            else
            {
                mainContentView.BackgroundColor = Color.Green;
            }


            // Upper bar with Monday
            if (upperBar == null)
            {
                upperBar = CreateUpperView();
                mainContentView.Children.Add(upperBar, 0, 0);

            }
            else
            {
                imageDayOfWeek.Source = "tuesday_text_gray.png";
            }

            // Middle area with different workouts added
            //var middleView = CreateMiddleView();

            // Add new workout round button
            //var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            if (bottomNavBar == null)
            {
                bottomNavBar = CreateNavBar();
                mainContentView.Children.Add(bottomNavBar, 0, 3);
            }

            return mainContentView;
        }

        private View CreateWednesdayView()
        {

            if (mainContentView == null)
            {
                mainContentView = new Grid
                {
                    BackgroundColor = Color.Red,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(1.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(7.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                };
            }
            else
            {
                mainContentView.BackgroundColor = Color.Red;
            }


            // Upper bar with Monday
            if (upperBar == null)
            {
                upperBar = CreateUpperView();
                mainContentView.Children.Add(upperBar, 0, 0);

            }
            else
            {
                imageDayOfWeek.Source = "wednesday_text_gray.png";
            }

            // Middle area with different workouts added
            //var middleView = CreateMiddleView();

            // Add new workout round button
            //var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            if (bottomNavBar == null)
            {
                bottomNavBar = CreateNavBar();
                mainContentView.Children.Add(bottomNavBar, 0, 3);
            }

            return mainContentView;
        }

        private View CreateThursdayView()
        {

            if (mainContentView == null)
            {
                mainContentView = new Grid
                {
                    BackgroundColor = Color.Black,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(1.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(7.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                };
            }
            else
            {
                mainContentView.BackgroundColor = Color.Black;
            }


            // Upper bar with Monday
            if (upperBar == null)
            {
                upperBar = CreateUpperView();
                mainContentView.Children.Add(upperBar, 0, 0);

            }
            else
            {
                imageDayOfWeek.Source = "thursday_text_gray.png";
            }

            // Middle area with different workouts added
            //var middleView = CreateMiddleView();

            // Add new workout round button
            //var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            if (bottomNavBar == null)
            {
                bottomNavBar = CreateNavBar();
                mainContentView.Children.Add(bottomNavBar, 0, 3);
            }

            return mainContentView;
        }

        private View CreateFridayView()
        {
            if (mainContentView == null)
            {
                mainContentView = new Grid
                {
                    BackgroundColor = Color.Maroon,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(1.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(7.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                };
            }
            else
            {
                mainContentView.BackgroundColor = Color.Maroon;
            }


            // Upper bar with Monday
            if (upperBar == null)
            {
                upperBar = CreateUpperView();
                mainContentView.Children.Add(upperBar, 0, 0);

            }
            else
            {
                imageDayOfWeek.Source = "friday_text_gray.png";
            }

            // Middle area with different workouts added
            //var middleView = CreateMiddleView();

            // Add new workout round button
            //var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            if (bottomNavBar == null)
            {
                bottomNavBar = CreateNavBar();
                mainContentView.Children.Add(bottomNavBar, 0, 3);
            }

            return mainContentView;
        }

        private View CreateSaturdayView()
        {

            if (mainContentView == null)
            {
                mainContentView = new Grid
                {
                    BackgroundColor = Color.Purple,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(1.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(7.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                };
            }
            else
            {
                mainContentView.BackgroundColor = Color.Purple;
            }


            // Upper bar with Monday
            if (upperBar == null)
            {
                upperBar = CreateUpperView();
                mainContentView.Children.Add(upperBar, 0, 0);

            }
            else
            {
                imageDayOfWeek.Source = "saturday_text_gray.png";
            }

            // Middle area with different workouts added
            //var middleView = CreateMiddleView();

            // Add new workout round button
            //var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            if (bottomNavBar == null)
            {
                bottomNavBar = CreateNavBar();
                mainContentView.Children.Add(bottomNavBar, 0, 3);
            }

            return mainContentView;
        }

        private View CreateSundayView()
        {
            if (mainContentView == null)
            {
                mainContentView = new Grid
                {
                    BackgroundColor = Color.Yellow,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(1.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(7.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                };
            }
            else
            {
                mainContentView.BackgroundColor = Color.Yellow;
            }


            // Upper bar with Monday
            if (upperBar == null)
            {
                upperBar = CreateUpperView();
                mainContentView.Children.Add(upperBar, 0, 0);

            }
            else
            {
                imageDayOfWeek.Source = "sunday_text_gray.png";
            }

            // Middle area with different workouts added
            //var middleView = CreateMiddleView();

            // Add new workout round button
            //var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            if (bottomNavBar == null)
            {
                bottomNavBar = CreateNavBar();
                mainContentView.Children.Add(bottomNavBar, 0, 3);
            }

            return mainContentView;
        }

        private Grid CreateUpperView()
        {

            // Use a grid to 50/50 the upper bar
            var myGrid = new Grid()
            {
                BackgroundColor = Color.White,
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                RowDefinitions =
                    {
                        // 5 Rows
                        new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                    },
                ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
                    },
                RowSpacing = 0,
                ColumnSpacing = 0,
            };

            // Fill left side 50/50 with an image mon-sun (TODO: Create images for each of the cases)
            switch (DateTime.Today.AddDays(holdDateCount).DayOfWeek)
            {
                case DayOfWeek.Monday:
                    // Create image, no need to check if null since its done before this function call
                    imageDayOfWeek = new Image() { Source = "monday_text_gray.png", Aspect = Aspect.AspectFit };
                    myGrid.Children.Add(imageDayOfWeek, 0, 1);
                    break;
                case DayOfWeek.Tuesday:
                    // Create image, no need to check if null since its done before this function call
                    imageDayOfWeek = new Image() { Source = "tuesday_text_gray.png", Aspect = Aspect.AspectFit };
                    myGrid.Children.Add(imageDayOfWeek, 0, 1);
                    break;
                case DayOfWeek.Wednesday:
                    imageDayOfWeek = new Image() { Source = "wednesday_text_gray.png", Aspect = Aspect.AspectFit };
                    myGrid.Children.Add(imageDayOfWeek, 0, 1);
                    break;
                case DayOfWeek.Thursday:
                    imageDayOfWeek = new Image() { Source = "thursday_text_gray.png", Aspect = Aspect.AspectFit };
                    myGrid.Children.Add(imageDayOfWeek, 0, 1);
                    break;
                case DayOfWeek.Friday:
                    imageDayOfWeek = new Image() { Source = "friday_text_gray.png", Aspect = Aspect.AspectFit };
                    myGrid.Children.Add(imageDayOfWeek, 0, 1);
                    break;
                case DayOfWeek.Saturday:
                    imageDayOfWeek = new Image() { Source = "saturday_text_gray.png", Aspect = Aspect.AspectFit };
                    myGrid.Children.Add(imageDayOfWeek, 0, 1);
                    break;
                case DayOfWeek.Sunday:
                    imageDayOfWeek = new Image() { Source = "sunday_text_gray.png", Aspect = Aspect.AspectFit };
                    myGrid.Children.Add(imageDayOfWeek, 0, 1);
                    break;
            }

            

            return myGrid;
        }

        private View CreateMiddleView()
        {
            /* Create a list of workouts to do each day
             * Each list item should have the ability to,
             * Delete
             * Modify
             * Drag into a specific order (when holding down on a specific task)
             *
             * Information in each list item will be,
             * Type of workout (cardio, lifting weights, body weight)
             * Each type will have different properties
             * Cardio (duration, intensity somehow?, ...)
             * Lifting weights (high/low reps, hight/low sets, type of lifting, 
             * 
             */

            return null;
        }

        private Grid CreateNavBar()
        {

            var myGrid = new Grid
            {
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                },
                RowSpacing = 0,
                ColumnSpacing = 0,
            };

            timerButton = new ImageButton
            {
                BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "stopwatch_white_trans.png",
                Aspect = Aspect.AspectFit
            };
            
            // (4,1)
            calendarButton = new ImageButton
            {
                BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                Margin = 0,
                CornerRadius = 0,
                Source = "calendar_orange.png",
                Aspect = Aspect.AspectFit
            };

            // (4,2)
            ImageButton button3 = new ImageButton
            {
                BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                Margin = 0,
                CornerRadius = 0,
                //Source = "graph_white.png",
                Aspect = Aspect.AspectFit

            };

            // (4,3)
            ImageButton button4 = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
                Margin = 0,
                CornerRadius = 0

            };

            myGrid.Children.Add(timerButton, 0, 0);
            myGrid.Children.Add(calendarButton, 1, 0);
            myGrid.Children.Add(button3, 2, 0);
            myGrid.Children.Add(button4, 3, 0);

            return myGrid;
        }

        private View CreateAddWorkoutButton()
        {
            return null;
        }

        

    }
}
