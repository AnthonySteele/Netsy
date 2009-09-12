//-----------------------------------------------------------------------
// <copyright file="GenericEventArgsTest.cs" company="AFS">
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
    /// Test the generic event args
    /// </summary>
    [TestClass]
    public class GenericEventArgsTest
    {
        /// <summary>
        /// Test creating a generic event args containing an int
        /// </summary>
        [TestMethod]
        public void GenericEventArgsCreateTest()
        {
            EventArgs<int> testValue = new EventArgs<int>(42);

            Assert.IsNotNull(testValue);
            Assert.AreEqual(42, testValue.Value);
        }
    }
}
