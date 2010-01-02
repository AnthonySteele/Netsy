//-----------------------------------------------------------------------
// <copyright file="ValueConverterHelpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ValueConverters
{
    /// <summary>
    /// Helper methods for value converters
    /// </summary>
    public static class ValueConverterHelpers
    {
        /// <summary>
        /// Generate a column count from a width
        /// </summary>
        /// <param name="value">the width in pixels</param>
        /// <param name="dotsPerColumn">pixels per column</param>
        /// <returns>the column count</returns>
        public static object ColumnCount(object value, int dotsPerColumn)
        {
            if (value == null || !(value is double))
            {
                return 1;
            }

            const int MaxColumns = 10;

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
    }
}
