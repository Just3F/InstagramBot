using System.Collections;
using InstagramApiSharp.Classes;
using Microsoft.EntityFrameworkCore.Internal;

namespace InstagramBot.Service
{
    public static class InstagramHelper
    {
        public static bool SuccessWithData<T>(this IResult<T> result) where T : IEnumerable
        {
            return result != null && result.Succeeded && result.Value.Any();
        }
    }
}
