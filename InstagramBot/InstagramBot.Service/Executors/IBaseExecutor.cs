using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InstagramBot.DB.Entities;

namespace InstagramBot.Service.Executors
{
    interface IBaseExecutor
    {
        Task Execute(QueueItem queueItem);
    }
}
