using System.Collections.Generic;

namespace InstagramBot.Service.Models
{
    public class PostMediaParameters
    {
        public PostMediaParameters()
        {
            GroupNames = new List<string>();
        }

        public List<string> GroupNames { get; set; }
        public string CustomCaption { get; set; }
        public bool IsShowAuthor { get; set; }
    }
}
