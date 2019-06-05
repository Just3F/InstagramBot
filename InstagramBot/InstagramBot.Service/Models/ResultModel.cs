namespace InstagramBot.Service.Models
{
    public class ResultModel
    {
        public Result Result { get; set; }
        public string Message { get; set; }

        public ResultModel Fail(string message)
        {
            Result = Result.Failed;
            Message = message;
            return this;
        }
        public ResultModel Success(string message)
        {
            Result = Result.Success;
            Message = message;
            return this;
        }
    }

    public enum Result
    {
        Success = 0,
        Failed = 1
    }
}
