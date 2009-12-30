namespace Netsy.Favorites
{
    using System.Windows.Controls;
    
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.DataModel;

    /// <summary>
    /// Todo: display listing in view model
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            IFavoritesService favoritesService = new FavoritesService(new EtsyContext(Constants.EtsyApiKey));
 
            MainPageViewModel viewModel = new MainPageViewModel(favoritesService, this.Dispatcher);
            this.DataContext = viewModel;

            viewModel.UserId = "5007275";
            viewModel.BeginGetFavorites();
        }

    }
}
