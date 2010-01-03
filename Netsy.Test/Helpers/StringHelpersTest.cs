//-----------------------------------------------------------------------
// <copyright file="StringHelpersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Helpers
{
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Helpers;

    /// <summary>
    /// Test the StringHelpers
    /// </summary>
    [TestClass]
    public class StringHelpersTest
    {
        /// <summary>
        /// Test integer
        /// </summary>
        [TestMethod]
        public void ToStringLowerSimpleIntTest()
        {
            string result = 33.ToStringLower();

            Assert.AreEqual("33", result);
        }

        /// <summary>
        /// Test a string
        /// </summary>
        [TestMethod]
        public void ToStringLowerCapsStringTest()
        {
            string result = "This IS a STRING".ToStringLower();

            Assert.AreEqual("this is a string", result);
        }

        /// <summary>
        /// Test on an enum
        /// </summary>
        [TestMethod]
        public void ToStringLowerCapsEnumTest()
        {
            string result = WebExceptionStatus.RequestProhibitedByCachePolicy.ToStringLower();

            Assert.AreEqual("requestprohibitedbycachepolicy", result);
        }

        /// <summary>
        /// Test sucess cases on IsNullEmptyOrWhiteSpace
        /// </summary>
        [TestMethod]
        public void IsNullEmptyOrWhiteSpaceSuccessTest()
        {
            Assert.IsTrue(((string)null).IsNullEmptyOrWhiteSpace());
            Assert.IsTrue(string.Empty.IsNullEmptyOrWhiteSpace());
            Assert.IsTrue(" ".IsNullEmptyOrWhiteSpace());
            Assert.IsTrue("  ".IsNullEmptyOrWhiteSpace());
            Assert.IsTrue("   ".IsNullEmptyOrWhiteSpace());
            Assert.IsTrue(" ".IsNullEmptyOrWhiteSpace());
            Assert.IsTrue("  ".IsNullEmptyOrWhiteSpace());
        }

        /// <summary>
        /// Test failure cases on IsNullEmptyOrWhiteSpace
        /// </summary>
        [TestMethod]
        public void IsNullEmptyOrWhiteSpaceFailTest()
        {
            Assert.IsFalse("a".IsNullEmptyOrWhiteSpace());
            Assert.IsFalse("a ".IsNullEmptyOrWhiteSpace());
            Assert.IsFalse(" a".IsNullEmptyOrWhiteSpace());
            Assert.IsFalse(" a ".IsNullEmptyOrWhiteSpace());
            Assert.IsFalse(" a b ".IsNullEmptyOrWhiteSpace());
        }
    }
}
