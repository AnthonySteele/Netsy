//-----------------------------------------------------------------------
// <copyright file="StringHelpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System.Globalization;

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
            return value.ToString().ToLower(CultureInfo.InvariantCulture);
        }
    }
}
