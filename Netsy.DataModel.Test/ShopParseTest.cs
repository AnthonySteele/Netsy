//-----------------------------------------------------------------------
// <copyright file="ShopParseTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.ShopData;
    using Netsy.Helpers;

    /// <summary>
    /// Test parsing of shop data 
    /// </summary>
    [TestClass]
    public class ShopParseTest
    {
        /// <summary>
        /// Data on shops
        /// </summary>
        public const string ShopData = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=7394192"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":1242567472.15,""city"":""London"",""banner_image_url"":""http:\/\/ny-image0.etsy.com\/iusb_760x100.6367692.jpg"",""last_updated_epoch"":1247867743.85,""creation_epoch"":1242575846.16,""listing_count"":8}],""params"":{""user_id"":7394192,""detail_level"":""low""},""type"":""shop""}";

        /// <summary>
        /// Test that the shops can be parsed
        /// </summary>
        [TestMethod]
        public void ShopLowDetailParseTest()
        {
            Shops shops = ShopData.Deserialize<Shops>();

            Assert.IsNotNull(shops);
            Assert.AreEqual(1, shops.Count);

            Shop shop1 = shops.Results[0];

            Assert.AreEqual("Fred", shop1.UserName);
            Assert.AreEqual(1234, shop1.UserId);
            Assert.AreEqual("http://ny-image0.etsy.com/iusb_760x100.6367692.jpg", shop1.BannerImageUrl);
        }
    }
}
