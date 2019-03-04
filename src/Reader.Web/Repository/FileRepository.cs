using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Reader.Web.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly IOptions<ReaderConfig> _readerConfig;
        public FileRepository(IOptions<ReaderConfig> readerConfig) 
        {
            _readerConfig = readerConfig;
        }
        public async Task<List<Feed>> LoadFeeds()
        {
            var feeds = new List<Feed>();

            try
            {
                var feedFilePath = Directory.GetCurrentDirectory();
                var lines = await File.ReadAllLinesAsync(Path.Combine(feedFilePath, _readerConfig.Value.FeedFile));
                foreach(var line in lines)
                {
                    var halves = line.Split("~");
                    var feed = new Feed
                    {
                        Name = halves[0],
                        FeedUrl = halves[1]
                    };
                    feeds.Add(feed);
                }

                return feeds;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return feeds;
            }
        }

        public async Task<bool> SaveFeed(Feed feed)
        {
            try
            {
                var feedFilePath = Directory.GetCurrentDirectory();
                using(var sw = File.AppendText(Path.Combine(feedFilePath, _readerConfig.Value.FeedFile)))
                {
                    await sw.WriteLineAsync($"{feed.Name}~{feed.FeedUrl}");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return false;
            }            
        }
    }
}