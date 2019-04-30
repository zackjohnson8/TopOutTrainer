using System;
namespace TopOutTrainer.StaticFiles
{
    public static class TimerPageUISettings
    {
        // Default values if not set to anything yet.
        private static int reps = 3;
        private static int sets = 3;
        private static int setsRestTime = 180; // seconds
        private static int repsRestTime = 90; // seconds



        public static int Reps
        {
            get
            {
                // TODO(zack): check later for saved values to use.
                return reps;
            }
            private set
            {
                reps = value;
            }
        }

        public static int Sets
        {
            get
            {
                return sets;
            }
            private set
            {
                sets = value;
            }
        }

        public static int SetsRestTime
        {
            get
            {
                return setsRestTime;
            }
            private set
            {
                setsRestTime = value;
            }
        }

        public static int RepsRestTime
        {
            get
            {
                return repsRestTime;
            }
            private set
            {
                repsRestTime = value;
            }
        }
    }
}
