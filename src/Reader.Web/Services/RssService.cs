using System;
using System.Collections.Generic;
using System.IO;
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

            if(string.IsNullOrEmpty(url)) return items;

            using(var client = _httpClientFactory.CreateClient())
            using(var response = await client.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return ParseRssItems(result);                    
                }
            }

            return items;
        }

        private List<ItemModel> ParseRssItems(string rssXml)
        {
            var items = new List<ItemModel>();

            if(string.IsNullOrEmpty(rssXml)) return items;

            var doc = XDocument.Parse(rssXml);
            items = (from x in doc.Descendants("item")
                select new ItemModel 
                {
                    Title = x.Element("title").Value,
                    Link = x.Element("link").Value,
                    Description = x.Element("description").Value,
                    PublishDate = ((DateTime)x.Element("pubDate"))
                }).ToList();

            return items;
        }
    }
}