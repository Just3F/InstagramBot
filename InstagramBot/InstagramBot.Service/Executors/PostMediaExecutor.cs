using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramBot.DB;
using InstagramBot.DB.Entities;
using InstagramBot.DB.Enums;
using InstagramBot.Service.Models;
using Newtonsoft.Json;

namespace InstagramBot.Service.Executors
{
    public class PostMediaExecutor : BaseExecutor
    {
        public PostMediaExecutor(IInstaApi instaApi, ApiContext db) : base(instaApi, db) { }

        public override async Task<ResultModel> Execute(QueueItem queueItem)
        {
            var result = new ResultModel();

            var postMediaParameters = JsonConvert.DeserializeObject<PostMediaParameters>(queueItem.Parameters);
            if (!postMediaParameters.GroupNames.Any())
            {
                return result.Fail("GroupNames are empty in parameters.");
            }

            string postedMediaId = null;
            foreach (var groupName in postMediaParameters.GroupNames)
            {
                var instaMediaList =
                    await _instaApi.UserProcessor.GetUserMediaAsync(groupName, PaginationParameters.MaxPagesToLoad(0));
                if (!instaMediaList.SuccessWithData())
                {
                    return result.Fail("instaMediaList is empty");
                }

                var mediaForCopy = instaMediaList.Value.Take(1).FirstOrDefault(x =>
                    (x.MediaType == InstaMediaType.Image || x.MediaType == InstaMediaType.Carousel) &&
                    !IsAlreadyPosted(x.InstaIdentifier, queueItem.InstagramUser.Login));
                if (mediaForCopy == null)
                {
                    continue;
                }

                var caption = string.IsNullOrEmpty(postMediaParameters.CustomCaption) ? mediaForCopy.Caption.Text : postMediaParameters.CustomCaption;
                if (postMediaParameters.IsShowAuthor)
                {
                    caption += Environment.NewLine + "Author: @" + mediaForCopy.User.UserName;
                }

                var uriCollection = new List<string>();
                switch (mediaForCopy.MediaType)
                {
                    case InstaMediaType.Carousel:
                        uriCollection = mediaForCopy.Carousel.Select(x => x.Images?.FirstOrDefault()?.Uri).ToList();
                        break;
                    case InstaMediaType.Image:
                        uriCollection = new List<string>() { mediaForCopy.Images?.FirstOrDefault()?.Uri };
                        break;
                }

                var instaImages = LoadImagesAsync(uriCollection);

                if (!instaImages.Any())
                {
                    return result.Fail("Can't load images from instagram.");
                }

                IResult<InstaMedia> postMediaResult;
                if (instaImages.Length > 1)
                {
                    postMediaResult = await _instaApi.MediaProcessor.UploadAlbumAsync(instaImages, null, caption, mediaForCopy.Location);
                }
                else
                {
                    postMediaResult = await _instaApi.MediaProcessor.UploadPhotoAsync(instaImages.FirstOrDefault(), caption,
                        mediaForCopy.Location);
                }

                if (!postMediaResult.Succeeded)
                {
                    return result.Fail("Fail when post media.");
                }

                postedMediaId = mediaForCopy.InstaIdentifier;
                break;
            }

            return result.Success(nameof(PostMediaExecutor), postedMediaId);
        }

        private InstaImageUpload[] LoadImagesAsync(IEnumerable<string> uriCollection)
        {
            List<InstaImageUpload> instaImages = new List<InstaImageUpload>();
            foreach (var uri in uriCollection)
            {
                var guid = Guid.NewGuid();
                var imageNameAndPath = $"c:\\InstaBotImg\\{guid}.jpg";

                WebClient webClient = new WebClient();
                webClient.DownloadFile(uri, imageNameAndPath);

                var instaImage = new InstaImageUpload
                {
                    Height = 1080,
                    Width = 1080,
                    Uri = new Uri(Path.GetFullPath(imageNameAndPath), UriKind.Absolute).LocalPath,
                };
                instaImages.Add(instaImage);
            }

            return instaImages.ToArray();
        }

        private bool IsAlreadyPosted(string id, string userName)
        {
            return _db.QueueHistories.Any(x => x.WorkedWithObjectId == id && x.QueueItem.QueueType == QueueType.PostMedia && x.QueueItem.InstagramUser.Login == userName);
        }
    }
}
