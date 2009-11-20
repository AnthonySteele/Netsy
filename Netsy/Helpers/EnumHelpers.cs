//-----------------------------------------------------------------------
// <copyright file="EnumHelpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;
    using System.Threading;

    /// <summary>
    /// Generic helpers on enumerations
    /// </summary>
    public static class EnumHelpers
    {
        /// <summary>
        /// a more modern interface to enumeration parsing
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <param name="value">the value to parse</param>
        /// <returns>the parsed value</returns>
        public static T Parse<T>(this string value) 
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (Exception ex)
            {
                string message = "Value " + value + " not found in enum " + typeof(T);
                throw new NetsyException(message, ex);
            }
        }
    }
}
