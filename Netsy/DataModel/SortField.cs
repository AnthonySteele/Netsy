//-----------------------------------------------------------------------
// <copyright file="SortField.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
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
