using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramBot.DB;
using InstagramBot.DB.Entities;

namespace InstagramBot.Service.Executors
{
    class LikeExecutor : BaseExecutor
    {
        public LikeExecutor(IInstaApi instaApi, ApiContext db) : base(instaApi, db)
        {
        }

        public override async Task Execute(QueueItem queueItem)
        {
            string tag = queueItem.LoadId;
            var instaTagFeed = await _instaApi.GetTagFeedAsync(tag, PaginationParameters.MaxPagesToLoad(0));
            var lastPost = instaTagFeed.Value?.Medias?.FirstOrDefault();

            if (instaTagFeed.Succeeded && lastPost != null)
            {
                await _instaApi.LikeMediaAsync(lastPost.InstaIdentifier);
                await UpdateQueueLastActivityAsync(queueEntity, db);
                Console.WriteLine("LikeExecutor for " + queueEntity.LoginData.Name);
            }
        }
    }
}
