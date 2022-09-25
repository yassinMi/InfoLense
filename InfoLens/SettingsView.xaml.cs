using Mi.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InfoLens
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            colorPicker.Color = Config.Instance.CoverColor;
            opacitySlider.Value = Config.Instance.Opacity;
            colorPicker.ColorChanged += ColorPicker_ColorChanged;
            colorPicker.MouseMove += (s, e) =>
            {
                hndlColorPickerChanged();
            };
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow mw = (Application.Current.MainWindow as MainWindow);

            if (mw != null)
            {
                if (colorPicker == null) return;
                Color b = colorPicker.Color;
                Config.Instance.Opacity = e.NewValue;
                mw.CoverBrush = new SolidColorBrush( Color.FromArgb((byte)(e.NewValue * 255 / 100), b.R, b.G, b.B));
            }
        }

        void hndlColorPickerChanged()
        {
            Debug.Write("hzk");
            MainWindow mw = (Application.Current.MainWindow as MainWindow);
            Debug.WriteLine("ok");
            if (mw != null)
            {
                if (colorPicker == null) return;
                Color b = colorPicker.Color;
                Config.Instance.CoverColor = b;
                mw.CoverBrush = new SolidColorBrush(Color.FromArgb((byte)(opacitySlider.Value * 255 / 100), b.R, b.G, b.B));
            }
        }
        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            hndlColorPickerChanged();
        }

        private void RadioItemborderStyleLines_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void RadioItemborderStyleArea_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioItemborderStyleDots_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
