//-----------------------------------------------------------------------
// <copyright file="FeedbackCreateTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.DataModel
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;

    /// <summary>
    /// Test creating the feedback
    /// </summary>
    [TestClass]
    public class FeedbackCreateTest
    {
        /// <summary>
        /// Test simple creation of a Feedback
        /// </summary>
        [TestMethod]
        public void FeedbackSimpleCreateTest()
        {
            Feedback feedback = new Feedback();
            Assert.IsNotNull(feedback);
        }
    }
}
