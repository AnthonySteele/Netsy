using System.Web.Mvc;

namespace Netsy.DemoWeb.Controllers
{
    using Models;

    [HandleError]
    public class HomeController : Controller
    {
        private const string TopText =
            @"Netsy is <a href=""http://github.com/AnthonySteele/Netsy/"">an Open Source .Net</a>" +
            @" wrapper for the <a href=""http://www.etsy.com/"">the Esty Website.</a>" +
            @" It works in desktop .Net applications, in ASP.Net" +
            @" and is shown here in <a href=""http://www.silverlight.net/"">Silverlight</a> demos.";

        public ActionResult Front()
        {
            HomeModel model = new HomeModel
                {
                    Title = "Netsy front page",
                    TopText = TopText,
                    SilverlightHeading = "Etsy front page listings",
                    SilverlightParams = "Retrieval=FrontListings,ItemsPerPage=16"
                };

            return this.ShowHomeView(model);
        }

        public ActionResult Category()
        {
            HomeModel model = new HomeModel
            {
                Title = "Category: Art",
                TopText = TopText,
                SilverlightHeading = "Etsy listings for the category 'Art'",
                SilverlightParams = "Retrieval=FrontListingsByCategory,Category=Art,ItemsPerPage=16"

            };

            return this.ShowHomeView(model);
        }

        public ActionResult Color()
        {
            HomeModel model = new HomeModel
            {
                Title = "Color: Blue",
                TopText = TopText,
                SilverlightHeading = "Etsy listings in Blue #0000DD",
                SilverlightParams = "Retrieval=FrontListingsByColor,Color=#0000DD,ItemsPerPage=16"

            };

            return this.ShowHomeView(model);
        }

        private ActionResult ShowHomeView(HomeModel model)
        {
            return this.View("Home", model);
        }

    }
}