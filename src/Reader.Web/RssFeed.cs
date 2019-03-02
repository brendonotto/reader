
using System.Collections.Generic;
using System.Xml.Linq;
using Reader.Web.Models;

namespace Reader.Web
{
    public class RssFeed
    {
        public RssFeed()
        {
            Items = new List<ItemModel>();
        }
        
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Copyright { get; set; }
        public ICollection<ItemModel> Items { get; set; }

        public string Serialize()
        {
            var doc = new XDocument(new XElement("rss"));
            doc.Root.Add(new XAttribute("version", "2.0"));

            var channel = new XElement("channel");
            channel.Add(new XElement("title", Title));
            if (!string.IsNullOrEmpty(Link)) channel.Add(new XElement("link", Link));
            channel.Add(new XElement("description", Description));
            channel.Add(new XElement("copyright", Copyright));
            doc.Root.Add(channel);

            foreach(var item in Items)
            {
                var itemElement = new XElement("item");
                itemElement.Add(new XElement("title", item.Title));
                itemElement.Add(new XElement("link", item.Link));
                itemElement.Add(new XElement("description", item.Description));
                var pubDate = item.PublishDate.ToString("r");
                itemElement.Add(new XElement("pubDate", pubDate));

                channel.Add(itemElement);
            }

            return doc.ToString();
        }
    }
}