//-----------------------------------------------------------------------
// <copyright file="DetailLevel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    /// <summary>
    /// Detail level of response
    /// </summary>
    public enum DetailLevel
    {
        /// <summary>
        /// Unknown default value
        /// </summary>
        Unknown  = 0,

        /// <summary>
        /// Low detail
        /// </summary>
        Low,

        /// <summary>
        /// Medium detail
        /// </summary>
        Medium,

        /// <summary>
        /// High detail
        /// </summary>
        High
    }
}
