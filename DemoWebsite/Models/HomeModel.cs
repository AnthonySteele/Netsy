namespace Netsy.DemoWeb.Models
{
    using System.Collections.Generic;

    public class HomeModel
    {
        private readonly List<NetsySilverlightModel> netsyControls = new List<NetsySilverlightModel>();

        public string Title { get; set; }
        public string TopText { get; set; }

        public IList<NetsySilverlightModel> NetsyControls
        {
            get { return this.netsyControls; }
        }
    }
}
