//-----------------------------------------------------------------------
// <copyright file="StringHelpers.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Helpers on strings
    /// </summary>
    public static class StringHelpers
    {
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
