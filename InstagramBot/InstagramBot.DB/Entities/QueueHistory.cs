using System;
using System.ComponentModel.DataAnnotations.Schema;
using InstagramBot.DB.Enums;

namespace InstagramBot.DB.Entities
{
    public class QueueHistory : BaseEntity
    {
        public QueueResult Result { get; set; }
        public DateTime CreatedOn { get; set; }
        public string WorkedWithObjectId { get; set; }

        public long QueueItemId { get; set; }
        [ForeignKey("QueueItemId")]
        public QueueItem QueueItem { get; set; }

        public long CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public AppUser CreatedBy { get; set; }
    }
}
