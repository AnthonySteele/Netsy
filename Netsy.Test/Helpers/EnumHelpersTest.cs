//-----------------------------------------------------------------------
// <copyright file="EnumHelpersTest.cs" company="AFS">
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
    /// An enumeration for testing
    /// </summary>
    public enum TestEnum
    {
        /// <summary>
        ///  The foo value
        /// </summary>
        Foo,

        /// <summary>
        /// The bar value
        /// </summary>
        Bar,

        /// <summary>
        /// THe fish value
        /// </summary>
        Fish
    }

    /// <summary>
    /// Test on the EnumHelpers
    /// </summary>
    [TestClass]
    public class EnumHelpersTest
    {
        /// <summary>
        /// Test parse success
        /// </summary>
        [TestMethod]
        public void EnumParseSuccessTest()
        {
            TestEnum result = "fish".Parse<TestEnum>();

            Assert.AreEqual(TestEnum.Fish, result);
        }

        /// <summary>
        /// Test parse failure
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnumParseFailsTest()
        {
            TestEnum result = "fssssh".Parse<TestEnum>();

            Assert.AreEqual(TestEnum.Fish, result);
        }
    }
}
