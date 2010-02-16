//-----------------------------------------------------------------------
// <copyright file="WidthToColumnCountValueConverter.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Value converter to determine column count from width
    /// </summary>
    #if (!SILVERLIGHT)
    [ValueConversion(typeof(double), typeof(int))]
    #endif
    public class WidthToColumnCountValueConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Convert width to column count
        /// </summary>
        /// <param name="value">the value to convert</param>
        /// <param name="targetType">the target type</param>
        /// <param name="parameter">conversion params</param>
        /// <param name="culture">culture info</param>
        /// <returns>the converted value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            int dotsPerColumn;
            if (parameter is int)
            {
                dotsPerColumn = (int)parameter;
            }
            else
            {
                dotsPerColumn = int.Parse(parameter.ToString(), CultureInfo.InvariantCulture);
            }

            if (value == null || !(value is double))
            {
                return 1;
            }

            const int MaxColumns = 12;

            double doubleValue = (double)value;
            int count = (int)(doubleValue / dotsPerColumn);
            if (count < 1)
            {
                count = 1;
            }

            if (count > MaxColumns)
            {
                count = MaxColumns;
            }

            return count;
        }

        /// <summary>
        /// Convert back -  not handled
        /// </summary>
        /// <param name="value">the value to convert</param>
        /// <param name="targetType">the target type</param>
        /// <param name="parameter">conversion params</param>
        /// <param name="culture">culture info</param>
        /// <returns>the converted value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
