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
                    Heading = "Home page",
                    TopText = HomePageText
                };

            return this.View(model);
        }
    }
}