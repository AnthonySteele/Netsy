//-----------------------------------------------------------------------
// <copyright file="UserCreateTest.cs" company="AFS">
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
    using Netsy.Helpers;

    /// <summary>
    /// Test creating the user details
    /// </summary>
    [TestClass]
    public class UserCreateTest
    {
        /// <summary>
        /// Test simple creation of a user
        /// </summary>
        [TestMethod]
        public void UserSimpleCreateTest()
        {
            User user = new User();
            Assert.IsNotNull(user);
        }

        /// <summary>
        /// Test simple creation of users
        /// </summary>
        [TestMethod]
        public void UsersSimpleCreateTest()
        {
            Users users = new Users();
            Assert.IsNotNull(users);
        }

        /// <summary>
        /// Test setting the user status to public
        /// </summary>
        [TestMethod]
        public void UserSetStatusPublicTest()
        {
            User user = new User();
            user.StatusString = "public";

            Assert.AreEqual(UserStatus.Public, user.StatusEnum);
        }

        /// <summary>
        /// Test setting the user status to private
        /// </summary>
        [TestMethod]
        public void UserSetStatusPrivateTest()
        {
            User user = new User();
            user.StatusString = "private";

            Assert.AreEqual(UserStatus.Private, user.StatusEnum);
        }

        /// <summary>
        /// Test setting the user status to a bad value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NetsyException))]
        public void UserSetStatusBadValueTest()
        {
            User user = new User();
            user.StatusString = "goofy";
        }

        /// <summary>
        /// Test setting the user gender to male
        /// </summary>
        [TestMethod]
        public void UserSetGenderMaleTest()
        {
            User user = new User();
            user.GenderString = "male";

            Assert.AreEqual(Gender.Male, user.GenderEnum);
        }

        /// <summary>
        /// Test setting the user gender to female
        /// </summary>
        [TestMethod]
        public void UserSetGenderFemaleTest()
        {
            User user = new User();
            user.GenderString = "female";

            Assert.AreEqual(Gender.Female, user.GenderEnum);
        }

        /// <summary>
        /// Test setting the user gender to private
        /// </summary>
        [TestMethod]
        public void UserSetGenderPrivateTest()
        {
            User user = new User();
            user.GenderString = "private";

            Assert.AreEqual(Gender.Private, user.GenderEnum);
        }

        /// <summary>
        /// Test setting the user gender to a bad value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NetsyException))]
        public void UserSetGenderBadValueTest()
        {
            User user = new User();
            user.GenderString = "neuter";
        }
    }
}
