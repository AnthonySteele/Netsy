using System.Web.Mvc;

namespace Netsy.DemoWeb.Controllers
{
    using Models;

    [HandleError]
    public class HomeController : Controller
    {
        private const string HomePageText =
            @"Netsy is <a href=""http://github.com/AnthonySteele/Netsy/"">an Open Source .Net</a>" +
            @" wrapper for the <a href=""http://www.etsy.com/"">the Esty Website.</a>" +
            @" It works in desktop .Net applications, in ASP.Net" +
            @" and is shown here in <a href=""http://www.silverlight.net/"">Silverlight</a>.";

        public ActionResult Index()
        {
            HomeModel model = new HomeModel
                {
                    Title = "Netsy home",
                    TopText = HomePageText,
                    SilverlightHeading = "Etsy front page listings",
                    SilverlightParams = "Retrieval=FrontListings,ItemsPerPage=16"
                };

            return this.View(model);
        }

        public ActionResult Category()
        {
            HomeModel model = new HomeModel
            {
                Title = "Category: Art",
                TopText = "Showing listings for the categorty 'Art'",
                SilverlightHeading = "Etsy listings for the categorty 'Art'",
                SilverlightParams = "Retrieval=FrontListingsByCategory,Category=Art,ItemsPerPage=16"

            };

            return this.View("Index", model);
        }
    }
}