//-----------------------------------------------------------------------
// <copyright file="UserStatus.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.UserData
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
