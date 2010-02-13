//-----------------------------------------------------------------------
// <copyright file="StringHelpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Helpers on strings
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Convert an object to a string in lower case
        /// </summary>
        /// <param name="value">the value to convert</param>
        /// <returns>the lower-case string</returns>
        public static string ToStringLower(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString().ToLower(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Test if a string is null, empty or just whitespace
        /// </summary>
        /// <param name="value">the string to test</param>
        /// <returns>true if the string is null, empty or contains only white space</returns>
        public static bool IsNullEmptyOrWhiteSpace(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            return string.IsNullOrEmpty(value.Trim());
        }

        /// <summary>
        /// Test if a string has more than just whitespace
        /// </summary>
        /// <param name="value">the string to test</param>
        /// <returns>true if the string is not blank (null, empty or contains only white space)</returns>
        public static bool HasContent(this string value)
        {
            return !value.IsNullEmptyOrWhiteSpace();
        }

        /// <summary>
        /// Convert values to a string with values separated by commas
        /// </summary>
        /// <typeparam name="T">the type of values</typeparam>
        /// <param name="values">the values</param>
        /// <returns>the values in a string, separated by commas</returns>
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
                if (first)
                {
                    first = false;
                }
                else
                {
                    result.Append(", ");
                }

                result.Append(value.ToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// Split a string into a list at spaces and commas
        /// </summary>
        /// <param name="value">the string to convert</param>
        /// <returns>words in an enumerable</returns>
        public static IEnumerable<string> ToEnumerable(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new string[0];
            }

            return value.Split(new[] { ',', ' ' });
        }
    }
}
