//-----------------------------------------------------------------------
// <copyright file="UserStatus.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    /// <summary>
    /// User status enumeration
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// Default unknown value
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// User is public
        /// </summary>
        Public,

        /// <summary>
        /// User is private
        /// </summary>
        Private
    }
}
