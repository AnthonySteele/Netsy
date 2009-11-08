//-----------------------------------------------------------------------
// <copyright file="IntHelpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    /// <summary>
    /// Helper methods for integers
    /// </summary>
    public static class IntHelpers
    {
        /// <summary>
        /// Extension method to parse a string into a nullable int
        /// </summary>
        /// <param name="value">the string to parse</param>
        /// <returns>the int value, or null if it could not be parsed</returns>
        public static int? ParseIntNullable(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            int result;
            bool parseSuceeded = int.TryParse(value, out result);
            if (parseSuceeded)
            {
                return result;
            }
                    
            return null;
        }
    }
}
