//-----------------------------------------------------------------------
// <copyright file="ListingState.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.ListingData
{
    /// <summary>
    /// State of the listing - active or not
    /// </summary>
    public enum ListingState
    {
        /// <summary>
        /// Unknown default value
        /// </summary>
        Unknown,

        /// <summary>
        /// The listing is active
        /// </summary>
        Active, 

        /// <summary>
        /// The listing is removed
        /// </summary>
        Removed, 

        /// <summary>
        /// The listing is sold out
        /// </summary>
        SoldOut, 

        /// <summary>
        /// The listing is expired
        /// </summary>
        Expired, 

        /// <summary>
        /// The listing is voodoo, man
        /// </summary>
        Alchemy,

        /// <summary>
        /// It happens, but is not in the docs, so I don't know what it means
        /// </summary>
        Edit
    }
}
