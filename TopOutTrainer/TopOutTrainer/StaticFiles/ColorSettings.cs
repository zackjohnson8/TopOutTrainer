using System;
using Xamarin.Forms;
namespace TopOutTrainer.StaticFiles
{
    public static class ColorSettings
    {
        public static Color getReadyColor { get; set; } = Color.FromHex("#FFFF00");
        public static Color mainGrayColor { get; set; } = Color.FromHex("#303030");
        public static Color startColor { get; set; } = Color.FromHex("#34e200");
        public static Color repBreakColor { get; set; } = Color.FromHex("#e20003");
        public static Color setBreakColor { get; set; } = Color.FromHex("#e20003");
    }
}
