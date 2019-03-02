using System;

namespace Reader.Web.Models
{
    public class ItemModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
    }
}