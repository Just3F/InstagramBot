using InstagramBot.DB.Enums;

namespace InstagramBot.Service.Models
{
    public class ResultModel
    {
        public QueueResult Result { get; set; }
        public string Message { get; set; }
        public string WorkedWithObjectId { get; set; }

        public ResultModel Fail(string message)
        {
            Result = QueueResult.Failed;
            Message = message;
            return this;
        }
        public ResultModel Success(string message, string workedWithObjectId)
        {
            Result = QueueResult.Success;
            Message = message;
            WorkedWithObjectId = workedWithObjectId;
            return this;
        }
    }

    
}
