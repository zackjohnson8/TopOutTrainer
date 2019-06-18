using System;
namespace TopOutTrainer.Timer
{
    public static class TimerLock
    {
        private static bool PhaseLocker = false;
        private static bool TotalTimeLocker = false;
        private static bool BitMapLocker = false;
        private static int ReadyCount = 0;

        public static bool ReadyCheck()
        {
            if(PhaseLocker && TotalTimeLocker && BitMapLocker)
            {
                ReadyCount++;
                if(ReadyCount == 3)
                {
                    ResetLockers();
                }
                return true;
            }

            return false;
        }

        public static void ResetLockers()
        {
            ReadyCount = 0;
            PhaseLocker = false;
            TotalTimeLocker = false;
            BitMapLocker = false;
        }

        public static void UnlockPhaseLocker()
        {
            PhaseLocker = true;
        }

        public static void UnlockTotalTimeLocker()
        {
            TotalTimeLocker = true;
        }

        public static void UnlockBitMapLocker()
        {
            BitMapLocker = true;
        }
    }
}
