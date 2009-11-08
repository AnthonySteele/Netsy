//-----------------------------------------------------------------------
// <copyright file="Gender.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    /// <summary>
    /// Gender of the user
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Unknown default value
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The user is male
        /// </summary>
        Male = 1,

        /// <summary>
        /// The user is female
        /// </summary>
        Female = 2,

        /// <summary>
        /// The user has not disclosed thier gender
        /// </summary>
        Private = 3
    }
}
