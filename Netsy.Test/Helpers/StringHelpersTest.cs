//-----------------------------------------------------------------------
// <copyright file="StringHelpersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Helpers
{
    using System.Collections.Generic;
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

        /// <summary>
        /// Tests on HasContent
        /// </summary>
        [TestMethod]
        public void HasContentTest()
        {
            Assert.IsFalse(((string)null).HasContent());
            Assert.IsFalse(string.Empty.HasContent());
            Assert.IsFalse(" ".HasContent());
            Assert.IsTrue("a".HasContent());
        }

        /// <summary>
        /// Tests on ToCsv
        /// </summary>
        [TestMethod]
        public void ToCsvNullTest()
        {
            List<string> values = null;
            string result = values.ToCsv();

            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        /// <summary>
        /// Tests on ToCsv
        /// </summary>
        [TestMethod]
        public void ToCsvEmptyTest()
        {
            List<string> values = new List<string>();
            string result = values.ToCsv();

            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        /// <summary>
        /// Tests on ToCsv
        /// </summary>
        [TestMethod]
        public void ToCsvOneItemTest()
        {
            List<int> values = new List<int>();
            values.Add(42);
            string result = values.ToCsv();

            Assert.AreEqual("42", result);
        }

        /// <summary>
        /// Tests on ToCsv
        /// </summary>
        [TestMethod]
        public void ToCsvTwoItemsTest()
        {
            List<int> values = new List<int>();
            values.Add(42);
            values.Add(64);

            string result = values.ToCsv();

            Assert.AreEqual("42, 64", result);
        }
    }
}
