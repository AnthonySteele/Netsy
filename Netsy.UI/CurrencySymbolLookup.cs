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
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Lookup currency symbol by currency code
    /// Cannot do this in Silverlight due to lack of CultureInfo.GetCultures
    /// </summary>
    public static class CurrencySymbolLookup
    {
        /// <summary>
        /// cache for found values
        /// </summary>
        private static readonly Dictionary<string, string> currencyCodesCache = new Dictionary<string, string>();

        /// <summary>
        /// Get a currency symbol from a currency code. 
        /// e.g. map "GPB" to "£" and "USD" to "$"
        /// </summary>
        /// <param name="isoCurrencyCode">the three-letter code for the currency</param>
        /// <returns>the symbol</returns>
        public static string CurrencySymbolFromCurrencyCode(string isoCurrencyCode)
        {
            if (!currencyCodesCache.ContainsKey(isoCurrencyCode))
            {
                RegionInfo regionInfo = 
                    (from c in CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures)
                     let r = new RegionInfo(c.LCID)
                     where r.ISOCurrencySymbol == isoCurrencyCode
                     select r).First();

                currencyCodesCache.Add(isoCurrencyCode, regionInfo.CurrencySymbol);
            }

            return currencyCodesCache[isoCurrencyCode];
        }
    }
}
