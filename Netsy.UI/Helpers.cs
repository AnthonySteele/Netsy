//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Static helper methods
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Convert values to a string sperated by commas
        /// </summary>
        /// <typeparam name="T">the type of values</typeparam>
        /// <param name="values">the values</param>
        /// <returns>the values in a string</returns>
        public static string ToCsv<T>(this IEnumerable<T> values)
        {
            if (values == null)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            bool first = true;

            foreach (T value in values)
            {
                result.Append(value.ToString());
                if (first)
                {
                    first = false;
                }
                else
                {
                    result.Append(", ");
                }
            }

            return result.ToString();
        }
    }
}
