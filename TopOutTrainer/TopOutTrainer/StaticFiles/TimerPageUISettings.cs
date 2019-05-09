using System;
namespace TopOutTrainer.StaticFiles
{
    public static class TimerPageUISettings
    {
        // Default values if not set to anything yet.
        public static int reps { get; set; } = 3;
        public static int sets { get; set; } = 3;
        public static int setsRestTime { get; set; } = 180; // seconds
        public static int repsRestTime { get; set; } = 90; // seconds

    }
}
