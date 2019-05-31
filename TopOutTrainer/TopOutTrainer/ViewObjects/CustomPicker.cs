using System;

using Xamarin.Forms;
using System.Diagnostics;

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

        protected override void OnTabIndexPropertyChanged(int oldValue, int newValue)
        {
            base.OnTabIndexPropertyChanged(oldValue, newValue);
            SelectedIndex = newValue;
        }
        
    }
}

