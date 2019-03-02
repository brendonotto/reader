using System;
using Xunit;
using Reader.Web.Services;
using System.Threading.Tasks;
using NSubstitute;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Reader.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Reader.Tests
{
    public class RssServiceTests
    {
        private readonly RssService _rssService;
        private string _url;
        public RssServiceTests() 
        {
            var item = new ItemModel
            {
                Title = "Test title",
                Link = "http://example.com/1",
                Description = "This is an example blog article"
            };

            var item2 = new ItemModel
            {
                Title = "Test title 2",
                Link = "http://example.com/2",
                Description = "This is a second example blog article"
            };

            var items = new List<ItemModel>() { item, item2 };

            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage() {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json")
            });
            
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);

            _rssService = new RssService(httpClientFactoryMock);
        }

        [Fact]
        public async Task TestServiceRetrieval()
        {
            _url = "http://example.com/";
            var actual = await _rssService.GetRssItemsAsync(_url);

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.NotNull(actual.FirstOrDefault());
            Assert.Equal("http://example.com/1", actual.FirstOrDefault().Link);
        }
    }
}
