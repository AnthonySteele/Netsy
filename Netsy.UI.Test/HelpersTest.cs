//-----------------------------------------------------------------------
// <copyright file="StringHelpersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.Test
{
    using System.Windows;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test the string helpers
    /// </summary>
    [TestClass]
    public class HelpersTest
    {
        /// <summary>
        /// Test ToVisibility
        /// </summary>
        [TestMethod]
        public void ToVisibilityTrueTest()
        {
            Visibility visibility = true.ToVisibility();
            Assert.AreEqual(Visibility.Visible, visibility);
        }

        /// <summary>
        /// Test ToVisibility
        /// </summary>
        [TestMethod]
        public void ToVisibilityFalseTest()
        {
            Visibility visibility = false.ToVisibility();
            Assert.AreEqual(Visibility.Collapsed, visibility);
        }
    }
}
