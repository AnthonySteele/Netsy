//-----------------------------------------------------------------------
// <copyright file="ListingsRetrievalMode.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Listings
{
    /// <summary>
    /// WHat data to retrieve
    /// </summary>
    public enum ListingsRetrievalMode
    {
        /// <summary>
        /// Default / null value
        /// </summary>
        None = 0,

        /// <summary>
        /// Retrieve favorites of a user
        /// </summary>
        UserFavorites = 1,

        /// <summary>
        /// Retrieve a shop's listings
        /// </summary>
        ShopListings = 2,

        /// <summary>
        /// Retrieve the etsy site's front listings
        /// </summary>
        FrontListings = 3,

        /// <summary>
        /// Retrieve the etsy site's front listings in a category
        /// </summary>
        FrontListingsByCategory = 4,

        /// <summary>
        /// Retrieve the etsy site's front listings by category
        /// </summary>
        FrontListingsByColor = 5
    }
}
