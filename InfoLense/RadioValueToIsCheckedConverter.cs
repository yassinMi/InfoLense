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
    /// gen purpose mvvm radio button binding bridge
    /// </summary>
    [ValueConversion(typeof(Enum),typeof(bool))]
    public class RadioValueToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ( Enum.GetName(value.GetType(),(Enum)value) == (string)parameter) return true;
            }
            catch 
            {

                
            }
            
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ((bool)value == true) return Enum.Parse(targetType,(string) parameter);
            }
            catch
            {


            }

            return Binding.DoNothing;

        }
    }

    


}
