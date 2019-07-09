using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TopOutTrainer.Factories;

namespace TopOutTrainer.ContentViews
{
    public partial class PlannerPage : ContentPage
    {

        private Color mainColor = StaticFiles.ColorSettings.darkGrayColor;
        private PlannerContentFactory myPlannerContentFactory;
        SwipeGestureRecognizer SwipeGestureLeft;
        SwipeGestureRecognizer SwipeGestureRight;
        View currentContent;

        public PlannerPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
            myPlannerContentFactory = new PlannerContentFactory();
            currentContent = myPlannerContentFactory.CreateViewAuto();

            // Set event for SwipeGesture
            SwipeGestureLeft = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            SwipeGestureRight = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            SwipeGestureLeft.Swiped += OnSwiped;
            SwipeGestureRight.Swiped += OnSwiped;
            myPlannerContentFactory.timerButton.Clicked += TimerButtonClicked;
            myPlannerContentFactory.calendarButton.Clicked += CalendarButtonClicked;

            currentContent.GestureRecognizers.Add(SwipeGestureLeft);
            currentContent.GestureRecognizers.Add(SwipeGestureRight);

            Content = currentContent;
        }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {

            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    currentContent = myPlannerContentFactory.CreateViewRight();
                    currentContent.GestureRecognizers.Add(SwipeGestureLeft);
                    currentContent.GestureRecognizers.Add(SwipeGestureRight);
                    Content = currentContent;
                    break;
                case SwipeDirection.Left:
                    currentContent = myPlannerContentFactory.CreateViewLeft();
                    currentContent.GestureRecognizers.Add(SwipeGestureLeft);
                    currentContent.GestureRecognizers.Add(SwipeGestureRight);
                    Content = currentContent;
                    break;
            }
            // WHAT? Deleting previous object before it runs all events
        }

        private void CalendarButtonClicked(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new GraphPage());
        }

        private void TimerButtonClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new TimerPage());
        }
    }
}
