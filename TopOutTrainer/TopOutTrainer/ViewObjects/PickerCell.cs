using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TopOutTrainer.ViewObjects
{
    public partial class PickerCell : ViewCell
    {

        private Label _label { get; set; }
        private Picker _picker1 { get; set; }
        private Picker _picker2 { get; set; }
        private StackLayout _layout { get; set; }

        internal Label Label1
        {
            set
            {
                //Remove picker if it exists
                if (_label != null)
                {
                    _layout.Children.Remove(_label);
                }

                //Set its value
                _label = value;
                //Add to layout
                _layout.Children.Add(_label);

            }
        }

        internal Picker Picker1
        {
            set
            {
                //Remove picker if it exists
                if (_picker1 != null)
                {
                    _layout.Children.Remove(_picker1);
                }

                //Set its value
                _picker1 = value;
                //Add to layout
                _layout.Children.Add(_picker1);

            }
        }

        internal Picker Picker2
        {
            set
            {
                //Remove picker if it exists
                if (_picker2 != null)
                {
                    _layout.Children.Remove(_picker2);
                }

                //Set its value
                _picker2 = value;
                //Add to layout
                _layout.Children.Add(_picker2);

            }

        }

        internal PickerCell()
        {

            _layout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(10,0,0,0)
            };

            this.View = _layout;

        }

    }
    
}
