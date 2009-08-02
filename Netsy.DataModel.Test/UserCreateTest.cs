//-----------------------------------------------------------------------
// <copyright file="UserCreateTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.UserData;

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
        [ExpectedException(typeof(ArgumentException))]
        public void UserSetStatusBadValueTest()
        {
            User user = new User();
            user.StatusString = "goofy";
        }
    }
}
