namespace NetsyGui
{
    using Microsoft.Practices.Unity;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// class to hold the only singleton we'll need - the IOC container
    /// </summary>
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Container = new UnityContainer();
            RegisterTypes(Container);
        }

        public static IUnityContainer Container
        {
            get;
            set;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            // the etsy context can be a singleton
            const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";
            container.RegisterInstance(typeof(EtsyContext), new EtsyContext(EtsyApiKey));

            container.RegisterType<IListingsService, ListingsService>();
            container.RegisterType<IFavoritesService, FavoritesService>();
        }
    }
}
