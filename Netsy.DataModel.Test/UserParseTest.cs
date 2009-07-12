//-----------------------------------------------------------------------
// <copyright file="UserParseTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;

    /// <summary>
    /// Test parsing string Json data into user details
    /// </summary>
    [TestClass]
    public class UserParseTest
    {
        /// <summary>
        /// Data for one user
        /// </summary>
        private const string UserDetails = @"{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":142567472.16,""city"":""London""}";

        /// <summary>
        /// A sample response text containing one user
        /// </summary>
        private const string UsersDetailsResponse = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":142567472.16,""city"":""London""}],""params"":{""user_id"":1234,""detail_level"":""low""},""type"":""user""}";

        /// <summary>
        /// Test parsing a user details
        /// </summary>
        [TestMethod]
        public void UserLowDetailParseTest()
        {
            User user = UserDetails.Deserialize<User>();

            Assert.IsNotNull(user);
            Assert.AreEqual("Fred", user.UserName);
            Assert.AreEqual(1234, user.UserId);

            Assert.AreEqual(@"http://www.etsy.com/shop.php?user_id=1234", user.Url);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_25x25.6043378.jpg", user.ImageUrl25x25);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_30x30.6043378.jpg", user.ImageUrl30x30);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_50x50.6043378.jpg", user.ImageUrl50x50);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_75x75.6043378.jpg", user.ImageUrl75x75);

            Assert.AreEqual(142567472.16, user.JoinEpoch);
            Assert.AreEqual("London", user.City);
        }

        /// <summary>
        /// Test parsing a user details response
        /// </summary>
        [TestMethod]
        public void UsersResponseParse()
        {
            Users users = UsersDetailsResponse.Deserialize<Users>();

            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);

            User user1 = users.Results[0];

            Assert.IsNotNull(user1);
            Assert.AreEqual("Fred", user1.UserName);
            Assert.AreEqual("London", user1.City);
            Assert.AreEqual(1234, user1.UserId);

            QueryParams queryParams = users.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Low, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }
    }
}
