//-----------------------------------------------------------------------
// <copyright file="GetFeedbackForOthersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Feedback
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.Test;

    /// <summary>
    /// Test the GetFeedbackForOthers API Function
    /// </summary>
    [TestClass]
    public class GetFeedbackForOthersTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackForOthersMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Feedbacks> result = null;
            IFeedbackService feedbackService = new FeedbackService(new EtsyContext(string.Empty));
            feedbackService.GetFeedbackForOthersCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackForOthers(NetsyData.TestUserId, 0, 10);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackForOthersByNameMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Feedbacks> result = null;
            IFeedbackService feedbackService = new FeedbackService(new EtsyContext(string.Empty));
            feedbackService.GetFeedbackForOthersCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackForOthers(NetsyData.TestUserName, 0, 10);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackForOthersApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext("InvalidKey"));
                feedbackService.GetFeedbackForOthersCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForOthers(NetsyData.TestUserId, 0, 10);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackForOthersByNameApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext("InvalidKey"));
                feedbackService.GetFeedbackForOthersCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForOthers(NetsyData.TestUserName, 0, 10);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test retrieval
        /// </summary>
        [TestMethod]
        public void GetFeedbackForOthersGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext(NetsyData.EtsyApiKey));
                feedbackService.GetFeedbackForOthersCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForOthers(NetsyData.TestUserId, 0, 10);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should suceed
                TestHelpers.CheckResultSuccess(result);
            }
        }

        /// <summary>
        /// Test retrieval by name
        /// </summary>
        [TestMethod]
        public void GetFeedbackForOthersByNameGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext(NetsyData.EtsyApiKey));
                feedbackService.GetFeedbackForOthersCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForOthers(NetsyData.TestUserName, 0, 10);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should suceed
                TestHelpers.CheckResultSuccess(result);
            }
        }
    }
}
