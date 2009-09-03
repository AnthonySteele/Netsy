//-----------------------------------------------------------------------
// <copyright file="GetFeedbackForUserTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Feedback
{
    using System.Net;
    using System.Threading;

    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.DataModel.FeedbackData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test the GetFeedbackForUser API Function
    /// </summary>
    [TestClass]
    public class GetFeedbackForUserTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackForUserMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Feedbacks> result = null;
            IFeedbackService feedbackService = new FeedbackService(new EtsyContext(string.Empty));
            feedbackService.GetFeedbackForUserCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackForUser(NetsyData.TestUserId, 0, 10);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackForUserByNameMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Feedbacks> result = null;
            IFeedbackService feedbackService = new FeedbackService(new EtsyContext(string.Empty));
            feedbackService.GetFeedbackForUserCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackForUser(NetsyData.TestUserName, 0, 10);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackForUserApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext("InvalidKey"));
                feedbackService.GetFeedbackForUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForUser(NetsyData.TestUserId, 0, 10);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

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
        public void GetFeedbackForUserByNameApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext("InvalidKey"));
                feedbackService.GetFeedbackForUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForUser(NetsyData.TestUserName, 0, 10);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

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
        public void GetFeedbackForUserGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext(NetsyData.EtsyApiKey));
                feedbackService.GetFeedbackForUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForUser(NetsyData.TestUserId, 0, 10);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should suceed
                NetsyData.CheckResultSuccess(result);
            }
        }

        /// <summary>
        /// Test retrieval by name
        /// </summary>
        [TestMethod]
        public void GetFeedbackForUserByNameGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext(NetsyData.EtsyApiKey));
                feedbackService.GetFeedbackForUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackForUser(NetsyData.TestUserName, 0, 10);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should suceed
                NetsyData.CheckResultSuccess(result);
            }
        }
    }
}
