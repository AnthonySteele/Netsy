//-----------------------------------------------------------------------
// <copyright file="StringHelpersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test the string helpers
    /// </summary>
    [TestClass]
    public class StringHelpersTest
    {
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
