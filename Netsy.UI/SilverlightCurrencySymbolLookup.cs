//-----------------------------------------------------------------------
// <copyright file="CurrencySymbolLookup.cs" company="AFS">
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
    /// Lookup currency symbol by code
    /// Cannot use CultureInfo.GetCultures in Silverlight 
    /// So we dummy it with a list of popular currencies
    /// </summary>
    public static class CurrencySymbolLookup
    {
        /// <summary>
        /// in silverlight, we have to use a list of known values
        /// </summary>
        private static readonly Dictionary<string, string> codesCache = new Dictionary<string, string>
            {
              { "AUD", "$" },                                                                       
              { "EUR", "€" },                                                                       
              { "GBP", "£" },                                                                       
              { "USD", "$" },                                                                       
              { "ZAR", "R" },                                                                       
            };

        /// <summary>
        /// Get a currency symbol from a currency code. 
        /// e.g. map "GPB" to "£" and "USD" to "$"
        /// </summary>
        /// <param name="isoCurrencyCode">the three-letter code for the currency</param>
        /// <returns>the symbol</returns>
        public static string CurrencySymbolFromCurrencyCode(string isoCurrencyCode)
        {
            if (!codesCache.ContainsKey(isoCurrencyCode))
            {
                // not found. Nothing we can do
                return "$?";
            }

            return codesCache[isoCurrencyCode];
        }

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
