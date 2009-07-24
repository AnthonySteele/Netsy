//-----------------------------------------------------------------------
// <copyright file="EnumHelpers.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Helpers on enumerations
    /// </summary>
    public static class EnumHelpers
    {
        /// <summary>
        /// a more modern interface to enumeration parsing
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <param name="value">the value to parse</param>
        /// <returns>the parsed valeu</returns>
        public static T Parse<T>(this string value) 
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Convert an object to a string in lower case
        /// </summary>
        /// <param name="value">the value to convert</param>
        /// <returns>the lower-case string</returns>
        public static string ToStringLower(this object value)
        {
            return value.ToString().ToLower(CultureInfo.InvariantCulture);
        }
    }
}
