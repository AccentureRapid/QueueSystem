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

    [Table("QueueHistory")]
    public class QueueHistory : Entity, IHasCreationTime
    {

        public const int MaxUserEIDLength = 50;
        public const int MaxUserNameLength = 50;


        [Required]
        [MaxLength(MaxUserEIDLength)]
        public string UserEID { get; set; }

        [MaxLength(MaxUserNameLength)]
        public string UserName { get; set; }
        public DateTime CreationTime { get; set; }

        public QueueStatus Status { get; set; }
        
        public QueueHistory()
        {
            CreationTime = Clock.Now;
            Status = QueueStatus.Valid;
        }

        public QueueHistory(string userEID, string userName)
            : this()
        {
            UserEID = userEID;
            UserName = userName;
        }
    }
    public enum QueueStatus : byte
    {
        Valid = 1,
        InValid = 0
    }

}
