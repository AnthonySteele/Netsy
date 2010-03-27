//-----------------------------------------------------------------------
// <copyright file="GetFeedbackTest.cs" company="AFS">
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
    using Netsy.Test.Services;

    /// <summary>
    /// Test the GetFeedback API Function
    /// </summary>
    [TestClass]
    public class GetFeedbackTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackMissingApiKeyTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(string.Empty);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedback(Constants.TestId);

            // check the data
            TestHelpers.CheckResultFailure(result, "Empty API key");
        }
    }
}
