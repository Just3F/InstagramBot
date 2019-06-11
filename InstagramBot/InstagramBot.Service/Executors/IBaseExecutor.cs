using System.Threading.Tasks;
using InstagramBot.DB.Entities;
using InstagramBot.Service.Models;

namespace InstagramBot.Service.Executors
{
    interface IBaseExecutor
    {
        Task<ResultModel> Run(QueueItem queueItem);
    }
}
