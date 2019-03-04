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
using Reader.Web.Repository;

namespace Reader.Web.Services 
{
    public class RssService : IRssService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFileRepository _fileRepository;

        public RssService(IHttpClientFactory httpClientFactory, IFileRepository fileRepository) 
        {
            _httpClientFactory = httpClientFactory;
            _fileRepository = fileRepository;
        }

        public async Task<List<FeedModel>> GetRssItemsAsync()
        {
            var feedModels = new List<FeedModel>();
            var items = new List<ItemModel>();

            var feeds = await _fileRepository.LoadFeeds();

            if (!feeds.Any()) return feedModels;

            using(var client = _httpClientFactory.CreateClient())
            {
                foreach(var feed in feeds) 
                {
                    using(var response = await client.GetAsync(feed.FeedUrl))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            feedModels.Add(new FeedModel
                            {
                                FeedName = feed.Name,
                                RssItems = ParseRssItems(result)
                            });
                        }
                    }
                }
            }
            return feedModels;
        }

        public async Task<bool> AddFeed(AddFeedModel feedModel)
        {
            var feed = new Feed
            {
                Name = feedModel.Name,
                FeedUrl = feedModel.FeedUrl
            };
            var success = await _fileRepository.SaveFeed(feed);

            return success;
        }

        private List<ItemModel> ParseRssItems(string rssXml)
        {
            var items = new List<ItemModel>();

            if(string.IsNullOrEmpty(rssXml)) return items;

            var doc = XDocument.Parse(rssXml);
            items = (from x in doc.Descendants("item")
                select new ItemModel 
                {
                    Id = Guid.NewGuid(),
                    Title = x.Element("title").Value,
                    Link = x.Element("link").Value,
                    Description = x.Element("description").Value,
                    PublishDate = ((DateTime)x.Element("pubDate"))
                }).ToList();

            return items;
        }
    }
}