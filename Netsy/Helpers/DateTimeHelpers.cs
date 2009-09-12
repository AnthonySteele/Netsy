//-----------------------------------------------------------------------
// <copyright file="DateTimeHelpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Extension methods on dates and times
    /// </summary>
    public static class DateTimeHelpers
    {
        /// <summary>
        /// The unix epoch started with 1970
        /// </summary>
        private static DateTime unixEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Convert an integer epoch time to a .Net DateTime
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/249721/how-to-convert-datetime-from-json-to-c"/>
        /// <param name="epochTime">the count of seconds since 1970</param>
        /// <returns>The value converted into a DateTime</returns>
        public static DateTime ToDateTimeFromEpoch(this int epochTime)
        {
            return unixEpochStart.AddSeconds(epochTime);
        }
    }
}
