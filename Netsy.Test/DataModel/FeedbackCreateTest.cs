//-----------------------------------------------------------------------
// <copyright file="FeedbackCreateTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.DataModel
{
    using System;

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
            Assert.IsFalse(feedback.CreationDate.HasValue);
        }

        /// <summary>
        /// Test that the Feedback has no CreationDate by default
        /// </summary>
        [TestMethod]
        public void FeedbackDatesNullByDefaultTest()
        {
            Feedback feedback = new Feedback();

            Assert.IsFalse(feedback.CreationEpoch.HasValue);
            Assert.IsFalse(feedback.CreationDate.HasValue);
        }

        /// <summary>
        /// Test that the creation date can be set to valid date
        /// </summary>
        [TestMethod]
        public void FeedBackEpochOneTest()
        {
            Feedback feedback = new Feedback();
            feedback.CreationEpoch = 1;
            
            Assert.IsTrue(feedback.CreationDate.HasValue);
            Helper.AssertDateIs(feedback.CreationDate.Value, 1970, 1, 1, 0, 0, 1);
        }

        /// <summary>
        /// Test that the creation date can be set to a null date
        /// </summary>
        [TestMethod]
        public void FeedBackEpochNullTest()
        {
            Feedback feedback = new Feedback();
            feedback.CreationEpoch = null;

            Assert.IsFalse(feedback.CreationDate.HasValue);
        }

        /// <summary>
        /// Test setting creation date
        /// </summary>
        [TestMethod]
        public void CreationDateTest()
        {
            Feedback feedback = new Feedback();
            feedback.CreationDate = new DateTime(1970, 1, 1);

            Assert.AreEqual(new DateTime(1970, 1, 1), feedback.CreationDate);
            Assert.AreEqual(0, feedback.CreationEpoch);
        }
    }
}
