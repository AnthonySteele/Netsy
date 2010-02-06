//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    /// <summary>
    /// Constants used in this application
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Key to access the Etsy API
        /// </summary>
        public const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";
        
        /// <summary>
        /// Default number of items to show per page
        /// </summary>
        public const int DefaultItemsPerPage = 6;

        /// <summary>
        /// Default data to retrieve
        /// </summary>
        public const ListingsRetrievalMode DefaultRetrieval = ListingsRetrievalMode.FrontListings;
    }
}
