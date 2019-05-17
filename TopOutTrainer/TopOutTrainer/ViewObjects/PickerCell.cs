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
        private View _picker1 { get; set; }
        private View _picker2 { get; set; }
        private StackLayout _layout { get; set; }

        internal string Label
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
            }
        }

        internal View Picker1
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

        internal View Picker2
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

            _label = new Label()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            _layout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(10,0,0,0),
                Children = {_label}
            };

            this.View = _layout;

        }

    }
    
}
