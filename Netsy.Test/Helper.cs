//-----------------------------------------------------------------------
// <copyright file="Helper.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test helpers
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Assert that the date matches the given 
        /// </summary>
        /// <param name="dateTime">the date and time to check</param>
        /// <param name="expectedYear">the expected year</param>
        /// <param name="exectedMonth">the expected month</param>
        /// <param name="expectedDay">the expected day</param>
        /// <param name="expectedHour">the expected hour</param>
        /// <param name="expectedMinute">the expected minute</param>
        /// <param name="expectedSecond">the expected second</param>
        public static void AssertDateIs(DateTime dateTime, int expectedYear, int exectedMonth, int expectedDay, int expectedHour, int expectedMinute, int expectedSecond)
        {
            Assert.AreEqual(expectedYear, dateTime.Year);
            Assert.AreEqual(exectedMonth, dateTime.Month);
            Assert.AreEqual(expectedDay, dateTime.Day);
            Assert.AreEqual(expectedHour, dateTime.Hour);
            Assert.AreEqual(expectedMinute, dateTime.Minute);
            Assert.AreEqual(expectedSecond, dateTime.Second);
        }
    }
}
