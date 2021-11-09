using System;
using System.Globalization;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class EClimbingTypeToColorConverter : IValueConverter
    {
        #region Public

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var climbingType = (EClimbingType)value;
            return climbingType switch
            {
                EClimbingType.Boulder => Color.FromHex("004d40"),
                EClimbingType.Length => Color.FromHex("880e4f"),
                _ => throw new ArgumentException($"Color not found for {climbingType}"),
            };
        }

        //Not required
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
