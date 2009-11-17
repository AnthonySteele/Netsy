//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace NetsyGui.Main
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
            ViewModelLocator.RegisterInstance(typeof(Dispatcher), this.Dispatcher);

            InitializeComponent();

            this.ViewModel.RequestFrontFeaturedListings();
        }

        /// <summary>
        /// Gets the view model
        /// </summary>
        private MainWindowViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
        }
    }
}
