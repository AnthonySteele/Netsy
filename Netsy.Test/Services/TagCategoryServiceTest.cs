//-----------------------------------------------------------------------
// <copyright file="TagCategoryServiceTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services
{
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Requests;
    using Netsy.Services;
    using Netsy.Test.Requests;

    [TestClass]
    public class TagCategoryServiceTest
    {
        private const string GetTopCategoriesRawResults = @"{""count"":31,""results"":[""accessories"",""art"",""bags_and_purses"",""bath_and_beauty"",""books_and_zines"",""candles"",""ceramics_and_pottery"",""children"",""clothing"",""crochet"",""dolls_and_miniatures"",""everything_else"",""furniture"",""geekery"",""glass"",""holidays"",""housewares"",""jewelry"",""knitting"",""music"",""needlecraft"",""paper_goods"",""patterns"",""pets"",""plants_and_edibles"",""quilts"",""supplies"",""toys"",""vintage"",""weddings"",""woodworking""],""params"":null,""type"":""category""}";

        [TestMethod]
        public void CreateWithMockRequestTest()
        {
            EtsyContext etsyContext = new EtsyContext(string.Empty);
            IRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(string.Empty);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            ITagCategoryService service = new TagCategoryService(etsyContext, dataRetriever);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void GetTopCategoriesTest()
        {
            EtsyContext etsyContext = new EtsyContext(Constants.DummyEtsyApiKey);
            MockFixedDataRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(GetTopCategoriesRawResults);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            ITagCategoryService etsyTagCategoryService = new TagCategoryService(etsyContext, dataRetriever);

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<StringResults> result = null;
                etsyTagCategoryService.GetTopCategoriesCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyTagCategoryService.GetTopCategories();
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                TestHelpers.CheckResultSuccess(result);
            }
        }

    }
}
