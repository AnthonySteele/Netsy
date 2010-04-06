namespace Netsy.DemoWeb.Models
{
    public class SearchModel
    {
        public string Title { get; set; }
        public string TopText { get; set; }

        public string SearchTerm { get; set; }

        public NetsySilverlightModel NetsyControl { get; set; }
    }
}
