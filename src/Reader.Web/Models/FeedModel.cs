using System.Collections.Generic;
using Reader.Web.Models;

namespace Reader.Web.Models
{
    public class FeedModel
    {
        public FeedModel()
        {
            RssItems = new List<ItemModel>();
        }
        public string FeedName { get; set; }
        public List<ItemModel> RssItems { get; set; }
    }
}