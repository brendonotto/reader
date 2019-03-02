using System.Collections.Generic;
using System.Threading.Tasks;
using Reader.Web.Models;

namespace Reader.Web.Services
{
    public interface IRssService
    {
        Task<List<ItemModel>> GetRssItemsAsync(string url);
    } 
}