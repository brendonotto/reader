using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reader.Web.Models;
using Reader.Web.Services;

namespace Reader.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RssController : Controller
    {
        private readonly IRssService _rssService;

        public RssController(IRssService rssService)
        {
            _rssService = rssService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRssItems()
        {
            var items = await _rssService.GetRssItemsAsync();

            if (!items.Any())
            {
                return NotFound();
            }

            return Ok(items);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddFeed(AddFeedModel feedModel)
        {
            var added = await _rssService.AddFeed(feedModel);

            return Created("/api/Rss/GetRssItems", feedModel);
        }
    }
}