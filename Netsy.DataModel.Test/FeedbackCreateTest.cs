//-----------------------------------------------------------------------
// <copyright file="FeedbackCreateTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.FeedbackData;

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
