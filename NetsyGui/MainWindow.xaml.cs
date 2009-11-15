//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace NetsyGui
{
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ViewModelLocator.RegisterInstance(typeof(Dispatcher), this.Dispatcher);
            this.DataContext = ViewModelLocator.Resolve<MainWindowViewModel>();

            this.LoadListings();
        }

        /// <summary>
        /// Click handler for the reload biutton
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void ReloadClick(object sender, RoutedEventArgs e)
        {
            this.LoadListings();
        }

        /// <summary>
        /// Start the lo0ad of listings
        /// </summary>
        private void LoadListings()
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)this.DataContext;
            viewModel.RequestFrontFeaturedListings();
        }
    }
}
