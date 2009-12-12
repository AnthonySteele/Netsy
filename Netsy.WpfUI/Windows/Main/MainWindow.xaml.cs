//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Main
{
    using System.Windows;

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
            // use this window's dispacther for thread wrangling
            Locator.RegisterInstance(this.Dispatcher);

            InitializeComponent();
            MainWindowViewModel viewModel = Locator.Resolve<MainWindowViewModel>();
            this.DataContext = viewModel;
            
            // start with a page of front listings
            viewModel.FrontFeaturedListings.LoadPageCommand.Execute(viewModel);
        }
    }
}
