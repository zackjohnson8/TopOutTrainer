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

        private BoxView myBoxView = null;
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

            if(myBoxView == null)
            {
                return myBoxView = new BoxView { BackgroundColor = Color.Blue };
            }else
            {
                myBoxView.BackgroundColor = Color.Blue;
                return myBoxView;
            }

            

            // Upper bar with Monday
            var upperBar = CreateUpperView();

            // Middle area with different workouts added
            var middleView = CreateMiddleView();

            // Add new workout round button
            var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            CreateNavBar();

            return null;
        }

        private View CreateTuesdayView()
        {

            if (myBoxView == null)
            {
                return myBoxView = new BoxView { BackgroundColor = Color.Green };
            }
            else
            {
                myBoxView.BackgroundColor = Color.Green;
                return myBoxView;
            }
            

            // Upper bar with Monday
            var upperBar = CreateUpperView();

            // Middle area with different workouts added
            var middleView = CreateMiddleView();

            // Add new workout round button
            var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            var navBar = CreateNavBar();

            return null;
        }

        private View CreateWednesdayView()
        {

            if (myBoxView == null)
            {
                return myBoxView = new BoxView { BackgroundColor = Color.Red };
            }
            else
            {
                myBoxView.BackgroundColor = Color.Red;
                return myBoxView;
            }

            return new BoxView { BackgroundColor = Color.Red };

            // Upper bar with Monday
            var upperBar = CreateUpperView();

            // Middle area with different workouts added
            var middleView = CreateMiddleView();

            // Add new workout round button
            var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            CreateNavBar();

            return null;
        }

        private View CreateThursdayView()
        {

            if (myBoxView == null)
            {
                return myBoxView = new BoxView { BackgroundColor = Color.Black };
            }
            else
            {
                myBoxView.BackgroundColor = Color.Black;
                return myBoxView;
            }
            return new BoxView { BackgroundColor = Color.Black };

            // Upper bar with Monday
            var upperBar = CreateUpperView();

            // Middle area with different workouts added
            var middleView = CreateMiddleView();

            // Add new workout round button
            var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            CreateNavBar();

            return null;
        }

        private View CreateFridayView()
        {
            if (myBoxView == null)
            {
                return myBoxView = new BoxView { BackgroundColor = Color.Maroon };
            }
            else
            {
                myBoxView.BackgroundColor = Color.Maroon;
                return myBoxView;
            }
            return new BoxView { BackgroundColor = Color.Maroon };

            // Upper bar with Monday
            var upperBar = CreateUpperView();

            // Middle area with different workouts added
            var middleView = CreateMiddleView();

            // Add new workout round button
            var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            CreateNavBar();

            return null;
        }

        private View CreateSaturdayView()
        {

            if (myBoxView == null)
            {
                return myBoxView = new BoxView { BackgroundColor = Color.Purple };
            }
            else
            {
                myBoxView.BackgroundColor = Color.Purple;
                return myBoxView;
            }
            return new BoxView { BackgroundColor = Color.Purple };

            // Upper bar with Monday
            var upperBar = CreateUpperView();

            // Middle area with different workouts added
            var middleView = CreateMiddleView();

            // Add new workout round button
            var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            CreateNavBar();

            return null;
        }

        private View CreateSundayView()
        {
            if (myBoxView == null)
            {
                return myBoxView = new BoxView { BackgroundColor = Color.Yellow };
            }
            else
            {
                myBoxView.BackgroundColor = Color.Yellow;
                return myBoxView;
            }
            return new BoxView { BackgroundColor = Color.Yellow };

            // Upper bar with Monday
            var upperBar = CreateUpperView();

            // Middle area with different workouts added
            var middleView = CreateMiddleView();

            // Add new workout round button
            var addButton = CreateAddWorkoutButton();

            // Bottom nav bar
            CreateNavBar();

            return null;
        }

        private View CreateUpperView()
        {
            return null;
        }

        private View CreateMiddleView()
        {

            Grid mainG = new Grid
            {
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                RowDefinitions =
                {
                    // 5 Rows
                    new RowDefinition { Height = new GridLength(10.25, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(.75, GridUnitType.Star) },
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

            return null;
        }

        private View CreateNavBar()
        {
            StackLayout aStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = StaticFiles.ColorSettings.mainGrayColor
            };

            aStack.Children.Add(new Image
            {
                BackgroundColor = StaticFiles.ColorSettings.mainGrayColor,
                Margin = 0,
                Source = "construction.png",
                Aspect = Aspect.AspectFit
            });

            //mainG.Children.Add(aStack, 0, 0);
            //Grid.SetColumnSpan(aStack, 4);

            //ImageButton button1 = new ImageButton
            //{

            //    BackgroundColor = mainColor,
            //    Margin = 0,
            //    CornerRadius = 0,
            //    Source = "stopwatch_white_trans.png",
            //    Aspect = Aspect.AspectFit

            //};
            //button1.Clicked += TimerButtonClicked;
            //// (4,1)
            //ImageButton button2 = new ImageButton
            //{
            //    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
            //    Margin = 0,
            //    CornerRadius = 0,
            //    Source = "calendar_orange.png",
            //    Aspect = Aspect.AspectFit


            //};

            //// (4,2)
            //ImageButton button3 = new ImageButton
            //{
            //    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
            //    Margin = 0,
            //    CornerRadius = 0,
            //    //Source = "graph_white.png",
            //    Aspect = Aspect.AspectFit

            //};
            //button3.Clicked += GraphButtonClicked;
            //// (4,3)
            //ImageButton button4 = new ImageButton
            //{
            //    WidthRequest = 50,
            //    HeightRequest = 50,
            //    BackgroundColor = StaticFiles.ColorSettings.darkGrayColor,
            //    Margin = 0,
            //    CornerRadius = 0

            //};

            return null;
        }

        private View CreateAddWorkoutButton()
        {
            return null;
        }


    }
}
