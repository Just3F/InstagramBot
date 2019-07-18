using System;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramBot.DB;
using InstagramBot.DB.Entities;
using InstagramBot.Service.Models;
using Newtonsoft.Json;

namespace InstagramBot.Service.Executors
{
    class LikeExecutor : BaseExecutor
    {
        public LikeExecutor(IInstaApi instaApi, ApiContext db) : base(instaApi, db)
        {
        }

        public override async Task<ResultModel> Execute(QueueItem queueItem)
        {
            var result = new ResultModel();

            var likeExecutorParameters = JsonConvert.DeserializeObject<LikeExecutorParameters>(queueItem.Parameters);

            var instaTagFeed = await _instaApi.FeedProcessor.GetTagFeedAsync(likeExecutorParameters.Tag, PaginationParameters.MaxPagesToLoad(0));
            if (!instaTagFeed.Succeeded)
            {
                return result.Fail($"Cannot load instagram feed in {nameof(LikeExecutor)}");
            }
            var lastPost = instaTagFeed.Value?.Medias?.FirstOrDefault(x => !x.HasLiked);
            if (lastPost == null)
            {
                return result.Fail($"Cannot find post(has no liked) in {nameof(LikeExecutor)}");
            }

            var likeResult = await _instaApi.MediaProcessor.LikeMediaAsync(lastPost.InstaIdentifier);
            if (!likeResult.Succeeded)
            {
                return result.Fail($"Cannot like post {nameof(LikeExecutor)}");
            }

            return result.Success(nameof(LikeExecutor));
        }
    }
}
