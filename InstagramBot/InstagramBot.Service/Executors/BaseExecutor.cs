using System;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramBot.DB;
using InstagramBot.DB.Entities;
using InstagramBot.Service.Models;

namespace InstagramBot.Service.Executors
{
    public abstract class BaseExecutor : IBaseExecutor
    {
        protected readonly IInstaApi _instaApi;
        protected readonly ApiContext _db;

        public abstract Task<ResultModel> Execute(QueueItem queueItem);

        protected BaseExecutor(IInstaApi instaApi, ApiContext db)
        {
            _instaApi = instaApi;
            _db = db;
        }

        public async Task<ResultModel> Run(QueueItem queueItem)
        {
            var result = await Execute(queueItem);
            await AddHistory(queueItem, result);
            return result;
        }

        public virtual async Task AddHistory(QueueItem queueItem, ResultModel result)
        {
            queueItem.Modified = DateTime.UtcNow;
            await _db.QueueHistories.AddAsync(new QueueHistory
            {
                QueueItemId = queueItem.Id,
                CreatedById = 1,
                CreatedOn = DateTime.UtcNow,
                WorkedWithObjectId = result.WorkedWithObjectId,
                Result = result.Result
            });
            await _db.SaveChangesAsync();
        }
    }
}
