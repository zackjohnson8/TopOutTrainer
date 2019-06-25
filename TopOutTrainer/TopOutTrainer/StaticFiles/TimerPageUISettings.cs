using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using PCLStorage;
namespace TopOutTrainer.StaticFiles
{
    public static class TimerPageUISettings
    {
        // Default values if not set to anything yet.
        public static int reps { get; set; } = 3;
        public static int sets { get; set; } = 3;
        public static int setsRestTime { get; set; } = 180; // seconds
        public static int repsRestTime { get; set; } = 90; // seconds
        public static int getReadyTime { get; set; } = 7;
        public static int startTime { get; set; } = 10;

        public static async Task<bool> SetFromFile()
        {
            try
            {
                String folderName = "timerpage";
                IFolder folder = FileSystem.Current.LocalStorage;
                folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

                String fileName = "setting.txt";
                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

                IList<String> stringList = new List<String>();
                String readFileData = await file.ReadAllTextAsync();
                String indexString;

                String settingName;
                int settingChoice;
                while (readFileData.IndexOf(';') != -1)
                {

                    indexString = readFileData.Substring(0, readFileData.IndexOf(';') + 1);
                    readFileData = readFileData.Substring(readFileData.IndexOf(';') + 1);

                    settingName = indexString.Substring(0, indexString.IndexOf(':'));
                    indexString = indexString.Substring(indexString.IndexOf(':') + 1);
                    settingChoice = Int32.Parse(indexString.Substring(0, indexString.IndexOf(';')));

                    SetValue(settingName, settingChoice);

                }

                return true;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        private static void SetValue(String name, int value)
        {
            if(name == "Reps")
            {
                reps = value;
            }else
            if( name == "Sets")
            {
                sets = value;
            }
            else
            if (name == "SetsTime")
            {
                setsRestTime = value;
            }
            else
            if (name == "RepsTime")
            {
                repsRestTime = value;
            }else
            if (name == "GetReadyTime") 
            {
                getReadyTime = value;
            }
            else
            if (name == "StartTime")
            {
                startTime = value;
            }
        }

    }
}
