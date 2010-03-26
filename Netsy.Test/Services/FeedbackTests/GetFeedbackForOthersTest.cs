//-----------------------------------------------------------------------
// <copyright file="GetFeedbackForOthersTest.cs" company="AFS">
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
    using Netsy.Services;
    using Netsy.Test.Services;

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
            IFeedbackService feedbackService = new FeedbackService(new EtsyContext(string.Empty));
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackForOthersCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackForOthers(Constants.TestId, 0, 10);

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
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(string.Empty);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackForOthersCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackForOthers(Constants.TestName, 0, 10);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
