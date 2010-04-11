namespace Netsy.DemoWeb.Controllers
{
    using System.Web.Mvc;

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

        public ActionResult Category(string category)
        {
            category = category.RemoveNonAlphanumeric();
            if (string.IsNullOrEmpty(category))
            {
                category = DefaultDataConstants.DefaultCategory;
            }

            return this.CategoryByName(category);
        }

        private ActionResult CategoryByName(string category)
        {
            CategoryModel model = new CategoryModel
                {
                    Title = "Category: " + category,
                    TopText = TopText,
                    Category = category
                };

            model.NetsyControl = new NetsySilverlightModel
                {
                        Heading = string.Format("Etsy listings in category '{0}'", category),
                        Params = string.Format("Retrieval=FrontListingsByCategory,Category={0},ItemsPerPage=16", category),
                        Height = 560,
                        Width = 660
                };

            return this.View(model);
        }

        public ActionResult Color(string color)
        {
            color = color.RemoveNonAlphanumeric();
            if (string.IsNullOrEmpty(color))
            {
                color = DefaultDataConstants.DefaultColor;
            }

            return this.ColorByName(color);
        }

        private ActionResult ColorByName(string color)
        {
            ColorModel model = new ColorModel
                {
                    Title = "Color: " + color,
                    TopText = TopText,
                    Color = color
                };

            model.NetsyControl = new NetsySilverlightModel
                {
                    Heading = string.Format("Etsy listings in color '{0}'", color),
                    Params = string.Format("Retrieval=FrontListingsByColor,Color={0},ItemsPerPage=16", color),
                    Height = 560,
                    Width = 660
                };

            return this.View(model);
        }

        public ActionResult Shop(string shop)
        {
            shop = shop.RemoveNonAlphanumeric();
            if (string.IsNullOrEmpty(shop))
            {
                shop = DefaultDataConstants.DefaultShop;
            }

            return this.ShopByName(shop);
        }

        private ActionResult ShopByName(string shop)
        {
            ShopModel model = new ShopModel
                {
                    Title = "Shop: " + shop,
                    TopText = TopText,
                    Shop = shop
                };

            model.NetsyControl = new NetsySilverlightModel
                {
                    Heading = string.Format("Etsy listings for the shop '{0}'", shop),
                    Params = string.Format("Retrieval=ShopListings,UserId={0},ItemsPerPage=16", shop),
                    Height = 560,
                    Width = 660
                };

            return this.View(model);
        }

        public ActionResult Favorites(string shop)
        {
            shop = shop.RemoveNonAlphanumeric();
            if (string.IsNullOrEmpty(shop))
            {
                shop = DefaultDataConstants.DefaultShop;
            }

            return this.FavoritesByName(shop);
        }

        private ActionResult FavoritesByName(string shop)
        {
            FavoritesModel model = new FavoritesModel
                {
                    Title = "Favorites of: " + shop,
                    TopText = TopText,
                    Shop = shop
                };

            model.NetsyControl = new NetsySilverlightModel
                {
                    Heading = string.Format("Etsy favourites for the user '{0}'", shop),
                    Params = string.Format("Retrieval=UserFavorites,UserId={0},ItemsPerPage=16", shop),
                    Height = 560,
                    Width = 660
                };

            return this.View(model);
        }

        private ActionResult ShowHomeView(HomeModel model)
        {
            return this.View("Home", model);
        }
    }
}