//-----------------------------------------------------------------------
// <copyright file="GetUserDetailsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.TagCategory
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Test etsy users retrieval
    /// </summary>
    [TestClass]
    public class GetUserDetailsTest
    {
        /// <summary>
        /// Test the GetUserDetails funciton on the users service
        /// </summary>
        [TestMethod]
        public void GetUserDetailsApiKeyMissingTest()
        {
            IUsersService etsyUsers = ServiceCreationHelper.MakeUsersService(string.Empty);
            ResultEventArgs<Users> result = null;
            etsyUsers.GetUserDetailsCompleted += (s, e) => result = e;

            // ACT
            etsyUsers.GetUserDetails(Constants.TestId, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }
    }
}
