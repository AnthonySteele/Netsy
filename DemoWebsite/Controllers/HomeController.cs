
namespace Netsy.DemoWeb.Controllers
{
    using System.Web.Mvc;
    using System.Text.RegularExpressions;

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
                    TopText = TopText
                };

            model.NetsyControls.Add(new NetsySilverlightModel
                {
                    Heading = "Etsy front page listings",
                    Params = "Retrieval=FrontListings,ItemsPerPage=16",
                    Width = 660,
                    Height = 560
                });

            return this.ShowHomeView(model);
        }

        public ActionResult Category()
        {
            HomeModel model = new HomeModel
            {
                Title = "Category: Art",
                TopText = TopText
            };

            model.NetsyControls.Add(new NetsySilverlightModel
                {
                    Heading = "Etsy listings for the category 'Art'",
                    Params = "Retrieval=FrontListingsByCategory,Category=Art,ItemsPerPage=16",
                    Height = 560,
                    Width = 660
                });

            return this.ShowHomeView(model);
        }

        public ActionResult Color()
        {
            HomeModel model = new HomeModel
            {
                Title = "Color: Blue",
                TopText = TopText
            };

            model.NetsyControls.Add(new NetsySilverlightModel
            {
                Heading = "Etsy listings in Blue #0000DD",
                Params = "Retrieval=FrontListingsByColor,Color=#0000DD,ItemsPerPage=16",
                Height = 560,
                Width = 660
            });

            return this.ShowHomeView(model);
        }

        public ActionResult Sizes()
        {
            HomeModel model = new HomeModel
            {
                Title = "Netsy front page",
                TopText = TopText
            };

            model.NetsyControls.Add(new NetsySilverlightModel
            {
                Heading = "Wide listings",
                Params = "Retrieval=FrontListings,ItemsPerPage=4",
                Height = 140,
                Width = 660
            });

            model.NetsyControls.Add(new NetsySilverlightModel
            {
                Heading = "Square listings",
                Params = "Retrieval=FrontListings,ItemsPerPage=4",
                Height = 290,
                Width = 330
            });

            model.NetsyControls.Add(new NetsySilverlightModel
            {
                Heading = "Tall listings",
                Params = "Retrieval=FrontListings,ItemsPerPage=4",
                Height = 560,
                Width = 165
            });
            
            return this.ShowHomeView(model);
        }

        [HttpGet]
        public ActionResult Shop()
        {
            SearchModel model = new SearchModel
            {
                Title = "Shop: MaidOfClay",
                TopText = TopText,
                SearchTerm = string.Empty
            };

            model.NetsyControl = new NetsySilverlightModel
            {
                Heading = "Etsy listings for the shop 'MaidOfClay'",
                Params = "Retrieval=ShopListings,UserId=MaidOfClay,ItemsPerPage=16",
                Height = 560,
                Width = 660
            };

            return this.ShowSearchView(model);
        }

        [HttpPost]
        public ActionResult Shop(string searchTerm)
        {
            searchTerm = SanitizeString(searchTerm);
            if (string.IsNullOrEmpty(searchTerm))
            {
                return Shop();
            }

            SearchModel model = new SearchModel
            {
                Title = "Shop: " + searchTerm,
                TopText = TopText,
                SearchTerm = string.Empty
            };

            model.NetsyControl = new NetsySilverlightModel
            {
                Heading = "Etsy listings for the shop '" + searchTerm + "'",
                Params = "Retrieval=ShopListings,UserId=" + searchTerm + ",ItemsPerPage=16",
                Height = 560,
                Width = 660
            };

            return this.ShowSearchView(model);
        }

        public ActionResult Favorites()
        {
            HomeModel model = new HomeModel
            {
                Title = "Favourites of: MaidOfClay",
                TopText = TopText
            };

            model.NetsyControls.Add(new NetsySilverlightModel
            {
                Heading = "Etsy favourites for the user 'MaidOfClay'",
                Params = "Retrieval=UserFavorites,UserId=MaidOfClay,ItemsPerPage=16",
                Height = 560,
                Width = 660
            });

            return this.ShowHomeView(model);
        }

        private static string SanitizeString(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return string.Empty;
            }

            inputString = inputString.Trim();
            if (inputString.Length > 256)
            {
                inputString = inputString.Substring(0, 256);
            }
            inputString = Regex.Replace(inputString, "[^A-Za-z0-9]", "");
            return inputString;
        }

        private ActionResult ShowHomeView(HomeModel model)
        {
            return this.View("Home", model);
        }

        private ActionResult ShowSearchView(SearchModel model)
        {
            return this.View("Search", model);
        }

    }
}