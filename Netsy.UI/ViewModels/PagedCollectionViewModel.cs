//-----------------------------------------------------------------------
// <copyright file="PagedCollectionViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Commands;

    /// <summary>
    /// Base class for view models of paged collections of items 
    /// Subclass must set the loadPageCommand
    /// </summary>
    /// <typeparam name="T">The type of item in the collection</typeparam>
    public abstract class PagedCollectionViewModel<T> : BaseViewModel where T : class 
    {
        /// <summary>
        /// the listings shown on the gui
        /// </summary>
        private readonly ObservableCollection<T> items = new ObservableCollection<T>();

        /// <summary>
        /// Number of items to retrieve
        /// </summary>
        private int itemsPerPage = Constants.DefaultItemsPerPage;

        /// <summary>
        /// the page index into the results
        /// </summary>
        private int pageNumber = 1;

        /// <summary>
        /// Text to show as status
        /// </summary>
        private string statusText;

        /// <summary>
        /// Flag to indicte if this is the last page of results
        /// </summary>
        private bool hasNextPage = true;

        /// <summary>
        /// Initializes a new instance of the PagedCollectionViewModel class
        /// </summary>
        protected PagedCollectionViewModel()
        {
            this.MakeCommands();
        }

        /// <summary>
        /// Gets the Items 
        /// </summary>
        public ObservableCollection<T> Items
        {
            get { return this.items; }
        }

        /// <summary>
        /// Gets or sets the page index into the results
        /// </summary>
        public int PageNumber
        {
            get
            {
                return this.pageNumber;
            }

            set
            {
                if (this.pageNumber != value)
                {
                    this.pageNumber = value;
                    this.OnPropertyChanged("PageNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of items to retrieve
        /// </summary>
        public int ItemsPerPage
        {
            get
            {
                return this.itemsPerPage;
            }

            set
            {
                if (this.itemsPerPage != value)
                {
                    this.itemsPerPage = value;
                    this.OnPropertyChanged("ItemsPerPage");
                }
            }
        }

        /// <summary>
        /// Gets the offset
        /// </summary>
        protected int Offset
        {
            get
            {
                return (this.PageNumber - 1) * this.ItemsPerPage;
            }
        }

        /// <summary>
        /// Gets or sets the text to show as status
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    this.OnPropertyChanged("StatusText");
                }
            }
        }

        /// <summary>
        /// Gets the command to go to the first page of results
        /// </summary>
        public ICommand FirstPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the next page of results
        /// </summary>
        public ICommand NextPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the previous page of results
        /// </summary>
        public ICommand PreviousPageCommand { get; private set; }

        /// <summary>
        /// Gets or sets the command to load the current page of results
        /// </summary>
        public ICommand LoadPageCommand { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the last page of results
        /// </summary>
        protected bool HasNextPage
        {
            get { return this.hasNextPage; }
            set { this.hasNextPage = value; }
        }

        /// <summary>
        /// Set up the commands that can be done in the base class
        /// </summary>
        private void MakeCommands()
        {
            // the first, next and previous command can be defined in terms of data on this class and the "load" command
            this.FirstPageCommand = new DelegateCommand<T>(
                t =>
                {
                    if ((this.PageNumber > 1) && (this.LoadPageCommand != null))
                    {
                        this.PageNumber = 1;
                        this.LoadPageCommand.Execute(this);
                    }
                },
                t => (this.PageNumber > 1) && (this.LoadPageCommand != null));
            
            this.PreviousPageCommand = new DelegateCommand<T>(
                t =>
                {
                    if ((this.PageNumber > 1) && (this.LoadPageCommand != null))
                    {
                        this.PageNumber--;
                        this.LoadPageCommand.Execute(this);
                    }
                },
                t => (this.PageNumber > 1) && (this.LoadPageCommand != null));
            
            this.NextPageCommand = new DelegateCommand<T>(
                t =>
                {
                    if (this.LoadPageCommand != null)
                    {
                        this.PageNumber++;
                        this.LoadPageCommand.Execute(this);
                    }
                },
                t => (this.LoadPageCommand != null && (this.items.Count > 0) && this.HasNextPage));
        }
    }
}
