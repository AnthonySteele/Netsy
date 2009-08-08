//-----------------------------------------------------------------------
// <copyright file="SortField.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    /// <summary>
    /// Field to sort on
    /// </summary>
    public enum SortField
    {
        /// <summary>
        /// Unknown default value
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Sort by created date
        /// </summary>
        Created, 
        
        /// <summary>
        /// Sort by price
        /// </summary>
        Price
    }
}
