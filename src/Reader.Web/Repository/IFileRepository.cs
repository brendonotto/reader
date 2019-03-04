using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reader.Web.Repository
{
    public interface IFileRepository
    {
         Task<List<Feed>> LoadFeeds();
         Task<bool> SaveFeed(Feed feed);
    }
}