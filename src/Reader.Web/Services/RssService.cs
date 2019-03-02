using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Reader.Web.Models;

namespace Reader.Web.Services 
{
    public class RssService : IRssService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RssService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ItemModel>> GetRssItemsAsync(string url)
        {
            var items = new List<ItemModel>();

            using(var client = _httpClientFactory.CreateClient())
            using(var response = await client.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var doc = XDocument.Parse(result);
                    items = (from x in doc.Descendants("item")
                        select new ItemModel 
                        {
                            Title = x.Element("title").Value,
                            Link = x.Element("link").Value,
                            Description = x.Element("description").Value
                        }).ToList();

                    return items;
                }
                return null;
            }
        }
    }
}