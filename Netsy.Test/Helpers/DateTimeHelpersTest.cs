//-----------------------------------------------------------------------
// <copyright file="DateTimeHelpersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Helpers
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Helpers;

    /// <summary>
    /// Test the DateTime helpers
    /// </summary>
    [TestClass]
    public class DateTimeHelpersTest
    {
        /// <summary>
        /// Test epoch-date to DateTime conversion with a zero date
        /// </summary>
        [TestMethod]
        public void EpochZeroToDateTimeFromEpochTest()
        {
            const double Value = 0.0;
            DateTime result = Value.ToDateTimeFromEpoch();

            Assert.AreEqual(1970, result.Year);
            Assert.AreEqual(1, result.Month);
            Assert.AreEqual(1, result.Day);
            Assert.AreEqual(0, result.Hour);
            Assert.AreEqual(0, result.Minute);
            Assert.AreEqual(0, result.Second);
        }

        /// <summary>
        /// Test epoch-date to DateTime conversion with a negative date
        /// </summary>
        [TestMethod]
        public void EpochNegativeToDateTimeFromEpochTest()
        {
            const double Value = -100.0;
            DateTime result = Value.ToDateTimeFromEpoch();

            Assert.AreEqual(1969, result.Year);
        }

        /// <summary>
        /// Test epoch-date to DateTime conversion with a positive  date
        /// </summary>
        [TestMethod]
        public void EpochPositiveToDateTimeFromEpochTest()
        {
            const double Value = 100.0;
            DateTime result = Value.ToDateTimeFromEpoch();

            Assert.AreEqual(1970, result.Year);
        }
    
        /// <summary>
        /// Test with the millenium epoch
        /// </summary>
        [TestMethod]
        public void EpochMilleniumDateToDateTimeFromEpochTest()
        {
            const double Value = 946684800.0;
            DateTime millenium = Value.ToDateTimeFromEpoch();

            Assert.AreEqual(2000, millenium.Year);
            Assert.AreEqual(1, millenium.Month);
            Assert.AreEqual(1, millenium.Day);
            Assert.AreEqual(1, millenium.Day);
            Assert.AreEqual(0, millenium.Hour);
            Assert.AreEqual(0, millenium.Minute);
            Assert.AreEqual(0, millenium.Second);
        }

        /// <summary>
        /// Test epoch-date to DateTime conversion with a zero date
        /// </summary>
        [TestMethod]
        public void EpochZeroToEpochFromDateTimeTest()
        {
            DateTime value = new DateTime(1970, 1, 1, 0, 0, 0);
            double result = value.ToEpochFromDateTime();

            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test epoch-date to DateTime conversion with a negative date
        /// </summary>
        [TestMethod]
        public void EpochNegativeToEpochFromDateTimeTest()
        {
            DateTime value = new DateTime(1969, 1, 1, 0, 0, 0);
            double result = value.ToEpochFromDateTime();

            Assert.IsTrue(result < 0);
        }

        /// <summary>
        /// Test epoch-date to DateTime conversion with a positive  date
        /// </summary>
        [TestMethod]
        public void EpochPositiveToEpochFromDateTimeTest()
        {
            DateTime value = new DateTime(1971, 1, 1, 0, 0, 0);
            double result = value.ToEpochFromDateTime();

            Assert.IsTrue(result > 0);
        }

        /// <summary>
        /// Test with the millenium epoch
        /// </summary>
        [TestMethod]
        public void EpochMilleniumDateToEpochFromDateTimeTest()
        {
            DateTime value = new DateTime(2000, 1, 1, 0, 0, 0);
            double result = value.ToEpochFromDateTime();

            const double ExpectedValue = 946684800.0;

            Assert.AreEqual(ExpectedValue, result);
        }
    }
}
