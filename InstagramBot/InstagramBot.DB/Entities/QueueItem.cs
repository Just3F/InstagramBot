using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using InstagramBot.DB.Enums;

namespace InstagramBot.DB.Entities
{
    public class QueueItem : AuditEntity
    {
        public string Parameters { get; set; }
        public QueueStatus QueueStatus { get; set; }

        public long InstagramUserId { get; set; }
        [ForeignKey("InstagramUserId")]
        public InstagramUser InstagramUser { get; set; }

        public List<QueueHistory> QueueHistories = new List<QueueHistory>();
    }
}
