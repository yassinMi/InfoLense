using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Converters
{

    /// <summary>
    /// this is enum only
    /// </summary>
    [ValueConversion(typeof(Enum),typeof(Style))]
    public class EnumToDynamicStyle : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string[] styleNames = ((string)parameter).Split(',');

                int pickedName = (int) value;

                return (Style) Application.Current.FindResource(styleNames[pickedName]);
            }
            catch 
            {

                return Binding.DoNothing;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }

    


}
