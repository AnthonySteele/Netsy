//-----------------------------------------------------------------------
// <copyright file="GetUsersByNameTest.cs" company="AFS">
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
    /// Test the GetUsersByName funcion on the users service
    /// </summary>
    [TestClass]
    public class GetUsersByNameTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetUsersByNameApiKeyMissingTest()
        {
            IUsersService etsyUsers = ServiceCreationHelper.MakeUsersService(string.Empty);
            ResultEventArgs<Users> result = null;
            etsyUsers.GetUsersByNameCompleted += (s, e) => result = e;

            // ACT
            etsyUsers.GetUsersByName(Constants.TestName, 0, 3, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }
    }
}
