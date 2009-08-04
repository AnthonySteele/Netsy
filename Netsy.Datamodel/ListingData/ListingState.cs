//-----------------------------------------------------------------------
// <copyright file="ListingState.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
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
        Alchemy
    }
}
