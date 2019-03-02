using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reader.Web.Models;
using Reader.Web.Services;

namespace Reader.Web.Controllers
{
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
            var items = await _rssService.GetRssItemsAsync("http://feeds.hanselman.com/ScottHanselman");

            if (!items.Any())
            {
                return BadRequest();
            }

            return Ok(items);
        }
    }
}