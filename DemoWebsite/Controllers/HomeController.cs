
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

        const string DefaultShop = "MaidOfClay";
        const string DefaultColor = "#0000DD";
        const string DefaultCategory = "Art";

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
        public ActionResult Category()
        {
            return this.CategoryByName(DefaultCategory);
        }

        [HttpPost]
        public ActionResult Category(string searchTerm)
        {
            searchTerm = SanitizeString(searchTerm);
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = DefaultCategory;
            }

            return this.CategoryByName(searchTerm);
        }

        private ActionResult CategoryByName(string searchTerm)
        {
            SearchModel model = new SearchModel
                {
                        Title = "Category: " + DefaultCategory,
                        TopText = TopText,
                        SearchTerm = searchTerm,
                        ButtonText = "Show listings for category",
                        TargetAction = "Category"

                };

            model.NetsyControl = new NetsySilverlightModel
                {
                        Heading = string.Format("Etsy listings in category '{0}'", searchTerm),
                        Params = string.Format("Retrieval=FrontListingsByCategory,Category={0},ItemsPerPage=16", searchTerm),
                        Height = 560,
                        Width = 660
                };

            return this.ShowSearchView(model);
        }

        [HttpGet]
        public ActionResult Color()
        {
            return this.ColorByName(DefaultColor); 
        }

        [HttpPost]
        public ActionResult Color(string searchTerm)
        {
            searchTerm = SanitizeString(searchTerm);
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = DefaultColor;
            }

            return this.ColorByName(searchTerm);
        }

        private ActionResult ColorByName(string searchTerm)
        {
            SearchModel model = new SearchModel
                {
                        Title = "Color: " + searchTerm,
                        TopText = TopText,
                        SearchTerm = searchTerm,
                        ButtonText = "Show listings for color",
                        TargetAction = "Color"
                };

            model.NetsyControl = new NetsySilverlightModel
                {
                        Heading = string.Format("Etsy listings in color '{0}'", searchTerm),
                        Params = string.Format("Retrieval=FrontListingsByColor,Color={0},ItemsPerPage=16", searchTerm),
                        Height = 560,
                        Width = 660
                };

            return this.ShowSearchView(model);
        }

        [HttpGet]
        public ActionResult Shop()
        {
            return this.ShopByName(DefaultShop);
        }

        [HttpPost]
        public ActionResult Shop(string searchTerm)
        {
            searchTerm = SanitizeString(searchTerm);
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = DefaultShop;
            }

            return this.ShopByName(searchTerm);
        }

        private ActionResult ShopByName(string searchTerm)
        {
            SearchModel model = new SearchModel
                {
                        Title = "Shop: " + searchTerm,
                        TopText = TopText,
                        SearchTerm = searchTerm,
                        ButtonText = "Show listings for shop",
                        TargetAction = "Shop"
                };

            model.NetsyControl = new NetsySilverlightModel
                {
                        Heading = string.Format("Etsy listings for the shop '{0}'", searchTerm),
                        Params = string.Format("Retrieval=ShopListings,UserId={0},ItemsPerPage=16", searchTerm),
                        Height = 560,
                        Width = 660
                };

            return this.ShowSearchView(model);
        }

        [HttpGet]
        public ActionResult Favorites()
        {
            return this.FavoritesByName(DefaultShop);
        }

        [HttpPost]
        public ActionResult Favorites(string searchTerm)
        {
            searchTerm = SanitizeString(searchTerm);
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = DefaultShop;
            }

            return this.FavoritesByName(searchTerm);
        }

        private ActionResult FavoritesByName(string searchTerm)
        {
            SearchModel model = new SearchModel
                {
                        Title = "Favorites of: " + searchTerm,
                        TopText = TopText,
                        SearchTerm = searchTerm,
                        ButtonText = "Show favorites of shop",
                        TargetAction = "Favorites"
                };

            model.NetsyControl = new NetsySilverlightModel
                {
                        Heading = string.Format("Etsy favourites for the user '{0}'", searchTerm),
                        Params = string.Format("Retrieval=UserFavorites,UserId={0},ItemsPerPage=16", searchTerm),
                        Height = 560,
                        Width = 660
                };

            return this.ShowSearchView(model);
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