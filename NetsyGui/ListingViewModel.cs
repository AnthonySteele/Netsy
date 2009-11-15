namespace NetsyGui
{
    using Netsy.DataModel;

    public class ListingViewModel
    {
        public Listing Listing
        {
            get;
            set; 
        }

        public string PriceData
        {
            get
            {
                return "$" + string.Format("{0:0.00}", Listing.Price) + " " + Listing.CurrencyCode;
            }
        }

        public string Title
        {
            get
            {
                return Listing.Title;
            }
        }

        public string ThumbnailImageUrl
        {
            get
            {
                return Listing.ImageUrl155X125;
            }
        }
    }
}
