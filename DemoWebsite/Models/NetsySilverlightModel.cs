namespace Netsy.DemoWeb.Models
{
    public class NetsySilverlightModel
    {
        public string Heading { get; set; }
        public string Params { get; set; }

        public bool HasHeading
        {
            get
            {
                return !string.IsNullOrEmpty(Heading);
            }
        }
    }
}
