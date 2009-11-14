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

            Helper.AssertDateIs(result, 1970, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Test epoch-date to DateTime conversion with a negative date
        /// </summary>
        [TestMethod]
        public void EpochNegativeToDateTimeFromEpochTest()
        {
            const double Value = -100.0;
            DateTime result = Value.ToDateTimeFromEpoch();

            Helper.AssertDateIs(result, 1969, 12, 31, 23, 58, 20);
        }

        /// <summary>
        /// Test converting a null string to nullable date
        /// </summary>
        [TestMethod]
        public void NullStringEpochDateTest()
        {
            double? value = null;
            DateTime? result = value.ToDateTimeFromEpoch();

            Assert.IsFalse(result.HasValue);
        }

        /// <summary>
        /// Test converting am empty string to nullable date
        /// </summary>
        [TestMethod]
        public void EmptyStringEpochDateTest()
        {
            double? value = null;
            DateTime? result = value.ToDateTimeFromEpoch();

            Assert.IsFalse(result.HasValue);
        }

        /// <summary>
        /// Test converting a valid string to a date
        /// </summary>
        [TestMethod]
        public void ValidStringEpochDateTest()
        {
            double? value = 1;
            DateTime? result = value.ToDateTimeFromEpoch();

            Assert.IsTrue(result.HasValue);
            Helper.AssertDateIs(result.Value, 1970, 1, 1, 0, 0, 1);
        }

        /// <summary>
        /// Test epoch-date to DateTime conversion with a positive  date
        /// </summary>
        [TestMethod]
        public void EpochPositiveToDateTimeFromEpochTest()
        {
            const double Value = 100.0;
            DateTime result = Value.ToDateTimeFromEpoch();

            Helper.AssertDateIs(result, 1970, 1, 1, 0, 1, 40);
        }
    
        /// <summary>
        /// Test with the millenium epoch in a double
        /// </summary>
        [TestMethod]
        public void EpochMilleniumDateToDateTimeFromEpochTest()
        {
            const double Value = 946684800.0;
            DateTime millenium = Value.ToDateTimeFromEpoch();

            Helper.AssertDateIs(millenium, 2000, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Test with the millenium epoch in a double
        /// </summary>
        [TestMethod]
        public void EpochMilleniumDateStringToDateTimeFromEpochTest()
        {
            double? value = 946684800.0;
            DateTime? millenium = value.ToDateTimeFromEpoch();

            Assert.IsTrue(millenium.HasValue);
            Helper.AssertDateIs(millenium.Value, 2000, 1, 1, 0, 0, 0);
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

        /// <summary>
        /// Test coverting to epoch string with a null date
        /// </summary>
        [TestMethod]
        public void NullDateToEpochStringTest()
        {
            DateTime? value = null;
            double? result = value.ToEpochFromDateTime();
            Assert.IsFalse(result.HasValue);
        }

        /// <summary>
        /// Test coverting to epoch string with a zero-point date
        /// </summary>
        [TestMethod]
        public void ZeroEpochDateToEpochStringTest()
        {
            DateTime? value = new DateTime(1970, 1, 1, 0, 0, 0);
            double? result = value.ToEpochFromDateTime();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result.Value);
        }

        /// <summary>
        /// Test coverting to epoch string with a millenium date
        /// </summary>
        [TestMethod]
        public void MilleniumEpochDateToEpochStringTest()
        {
            DateTime? value = new DateTime(2000, 1, 1, 0, 0, 0);
            double? result = value.ToEpochFromDateTime();
            Assert.AreEqual(946684800, result);
        }
    }
}
