//-----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Settings to read from the commandline
    /// </summary>
    internal class AppSettings
    {
        /// <summary>
        /// Initializes a new instance of the AppSettings class
        /// </summary>
        public AppSettings()
        {
            this.ColumnCount = Constants.DefaultColumnCount;
            this.ItemsPerPage = Constants.DefaultItemsPerPage;
            this.Retrieval = Constants.DefaultRetrieval;
        }

        /// <summary>
        /// Gets the user to show 
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// Gets the columns count 
        /// </summary>
        public int ColumnCount { get; private set; }

        /// <summary>
        /// Gets the items per page 
        /// </summary>
        public int ItemsPerPage { get; private set; }

        /// <summary>
        /// Gets the category to show for listings by category
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the color to show for listings by color
        /// </summary>
        public string Color { get; private set; }

        /// <summary>
        /// Gets the data to to retrieve
        /// </summary>
        public ListingsRetrievalMode Retrieval { get; private set; }

        /// <summary>
        /// Read the commandline parameters
        /// </summary>
        /// <param name="initParams">the control params</param>
        public void ReadParams(IDictionary<string, string> initParams)
        {
            const string ColumnCountKey = "ColumnCount";
            const string ItemsPerPageKey = "ItemsPerPage";
            const string CategoryKey = "Category";
            const string ColorKey = "Color";
            
            this.ReadRetrieval(initParams);
            this.ReadUserId(initParams);

            // Column count is optional 
            if (initParams.ContainsKey(ColumnCountKey))
            {
                int columnCountRead;
                if (int.TryParse(initParams[ColumnCountKey], out columnCountRead))
                {
                    this.ColumnCount = columnCountRead;
                }
            }

            // Items per page is optional 
            if (initParams.ContainsKey(ItemsPerPageKey))
            {
                int itemsPerPageRead;
                if (int.TryParse(initParams[ItemsPerPageKey], out itemsPerPageRead))
                {
                    this.ColumnCount = itemsPerPageRead;
                }
            }

            // category is optional 
            if (initParams.ContainsKey(CategoryKey))
            {
                this.Category = initParams[CategoryKey];
            }

            // color is optional 
            if (initParams.ContainsKey(ColorKey))
            {
                this.Color = initParams[ColorKey];

                if (this.Color[0] == '#')
                {
                    this.Color = this.Color.Substring(1);
                }
            }
        }

        /// <summary>
        /// Read the user id from the params
        /// </summary>
        /// <param name="initParams">the params to read from</param>
        private void ReadUserId(IDictionary<string, string> initParams)
        {
            const string UserIdKey = "UserId";

            if (this.UserIdIsRequired() && !initParams.ContainsKey(UserIdKey))
            {
                throw new ArgumentException("No User id found in Control params");
            }

            if (initParams.ContainsKey(UserIdKey))
            {
                this.UserId = initParams[UserIdKey];
            }
        }

        /// <summary>
        /// Indicates if the retrieval mode will require a user id
        /// </summary>
        /// <returns>true if a user id is required</returns>
        private bool UserIdIsRequired()
        {
            return (this.Retrieval != ListingsRetrievalMode.FrontListings) &&
                (this.Retrieval != ListingsRetrievalMode.FrontListingsByCategory) &&
                (this.Retrieval != ListingsRetrievalMode.FrontListingsByColor);
        }

        /// <summary>
        /// Read the retrieval parameter from the params
        /// </summary>
        /// <param name="initParams">the params to read from</param>
        private void ReadRetrieval(IDictionary<string, string> initParams)
        {
            const string RetrievalKey = "Retrieval";

            // retrieval mode is optional
            if (initParams.ContainsKey(RetrievalKey))
            {
                try
                {
                    this.Retrieval = (ListingsRetrievalMode)Enum.Parse(typeof(ListingsRetrievalMode), initParams[RetrievalKey], true);
                }
                catch (Exception)
                {
                    // couln't parse the param. Bummer. 
                }
            }
        }
    }
}
