//-----------------------------------------------------------------------
// <copyright file="GetFeedbackAsBuyerTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.FeedbackTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Test;
    using Netsy.Test.Services;

    /// <summary>
    /// Test the GetFeedbackAsBuyer API Function
    /// </summary>
    [TestClass]
    public class GetFeedbackAsBuyerTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerMissingApiKeyTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(string.Empty);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(Constants.TestId, 0, 10);

            // check the data
            TestHelpers.CheckResultFailure(result, "Empty API key");
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerByNameMissingApiKeyTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(string.Empty);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(Constants.TestName, 0, 10);

            // check the data
            TestHelpers.CheckResultFailure(result, "Empty API key");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerNegativeOffsetTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(Constants.TestId, -1, 10);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerZeroLimitTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(Constants.TestId, 0, 0);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerByNameNegativeOffsetTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(Constants.TestName, -1, 10);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerByNameZeroLimitTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(Constants.TestName, 0, 0);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
