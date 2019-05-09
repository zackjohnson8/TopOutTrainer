using System;

using Xamarin.Forms;

namespace TopOutTrainer.ViewObjects
{
    public class CustomPicker : Picker
    {

        public int GetSelectedNumber()
        {
            return this.SelectedIndex;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == Picker.SelectedIndexProperty.PropertyName)
            {
                this.InvalidateMeasure();
            }
        }
    }
}

