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

    using NetsyGui.ViewModels;

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

            this.ViewModel.RequestFrontFeaturedListings();
        }

        private MainWindowViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
        }

        /// <summary>
        /// Click handler for the reload biutton
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void ReloadClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.RequestFrontFeaturedListings();
        }

        private void FirstClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.FirstPage();
            this.ViewModel.RequestFrontFeaturedListings();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.PreviousPage();
            this.ViewModel.RequestFrontFeaturedListings();
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.NextPage();
            this.ViewModel.RequestFrontFeaturedListings();
        }

    }
}
