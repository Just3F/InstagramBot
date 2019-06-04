using System;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramBot.DB;
using InstagramBot.DB.Entities;
using Newtonsoft.Json;

namespace InstagramBot.Service.Executors
{
    class LikeExecutor : BaseExecutor
    {
        public LikeExecutor(IInstaApi instaApi, ApiContext db) : base(instaApi, db)
        {
        }

        public override async Task Execute(QueueItem queueItem)
        {
            var obj = new LikeExecutorParameters{Tag = "test tag name"};
            var stringObj = JsonConvert.SerializeObject(obj);

           var likeExecutorParameters = JsonConvert.DeserializeObject<LikeExecutorParameters>(stringObj);
        }
    }

    class LikeExecutorParameters
    {
        public string Tag { get; set; }
    }
}
