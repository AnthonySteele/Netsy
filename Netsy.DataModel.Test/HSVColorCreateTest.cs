//-----------------------------------------------------------------------
// <copyright file="HsvColorCreateTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test creating the HsvColor
    /// </summary>
    [TestClass]
    public class HsvColorCreateTest
    {
        /// <summary>
        /// Test creation of a HSV color from numbers
        /// </summary>
        [TestMethod]
        public void HsvColorNumericCreateTest()
        {
            HsvColor color = new HsvColor(34, 45, 67);
            Assert.IsNotNull(color);
            Assert.AreEqual(34, color.Hue);
            Assert.AreEqual(45, color.Saturation);
            Assert.AreEqual(67, color.Value);
        }

        /// <summary>
        /// Test creation of a HSV color from a string
        /// </summary>
        [TestMethod]
        public void HsvColorStringCreateTest()
        {
            HsvColor color = new HsvColor("34;45;67");
            Assert.IsNotNull(color);
            Assert.AreEqual(34, color.Hue);
            Assert.AreEqual(45, color.Saturation);
            Assert.AreEqual(67, color.Value);
        }
    }
}
