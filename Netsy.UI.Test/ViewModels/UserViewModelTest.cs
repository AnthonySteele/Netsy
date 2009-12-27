//-----------------------------------------------------------------------
// <copyright file="UserViewModelTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.Test.ViewModels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// Tests on the UserViewModel class
    /// </summary>
    [TestClass]
    public class UserViewModelTest
    {
        /// <summary>
        /// Test creating the view model
        /// </summary>
        [TestMethod]
        public void UserViewModelCreateTest()
        {
            User user = new User();
            UserViewModel userViewModel = new UserViewModel(user);

            Assert.IsNotNull(userViewModel);
            Assert.IsNotNull(userViewModel.User);
        }
    }
}
