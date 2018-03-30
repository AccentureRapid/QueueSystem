using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Entities
{

    [Table("QueueHistoryLog")]
    public class QueueHistoryLog : Entity, IHasCreationTime
    {

        public const int MaxUserEIDLength = 50;
        public const int MaxUserNameLength = 50;

        [Required]
        [MaxLength(MaxUserEIDLength)]
        public string UserEID { get; set; }

        [MaxLength(MaxUserNameLength)]
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ConnectedTime { get; set; }
        public DateTime DisconnectedTime { get; set; }
        public QueueHistoryLog()
        {
            CreationTime = Clock.Now;
        }

        public QueueHistoryLog(string userEID, string userName)
            : this()
        {
            UserEID = userEID;
            UserName = userName;
        }
    }
}
