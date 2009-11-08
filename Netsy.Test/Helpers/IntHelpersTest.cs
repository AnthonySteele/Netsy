//-----------------------------------------------------------------------
// <copyright file="IntHelpersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Helpers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Helpers;

    /// <summary>
    /// Test int helper methods
    /// </summary>
    [TestClass]
    public class IntHelpersTest
    {
        /// <summary>
        /// Test parsing an empty string
        /// </summary>
        [TestMethod]
        public void NullParseTest()
        {
            int? intValue = string.Empty.ParseIntNullable();
            Assert.IsFalse(intValue.HasValue);
        }

        /// <summary>
        /// Test parsing an invalid string
        /// </summary>
        [TestMethod]
        public void InvalidParseTest()
        {
            int? intValue = "foo".ParseIntNullable();
            Assert.IsFalse(intValue.HasValue);
        }

        /// <summary>
        /// Test parsing a valid string
        /// </summary>
        [TestMethod]
        public void ZeroValidParseTest()
        {
            int? intValue = "0".ParseIntNullable();
            Assert.IsTrue(intValue.HasValue);
            Assert.AreEqual(0, intValue.Value);
        }

        /// <summary>
        /// Test parsing a valid string
        /// </summary>
        [TestMethod]
        public void OneValidParseTest()
        {
            int? intValue = "1".ParseIntNullable();
            Assert.IsTrue(intValue.HasValue);
            Assert.AreEqual(1, intValue.Value);
        }
    }
}
