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

    [Table("JumpQueueUser")]
    public class JumpQueueUser : Entity, IHasCreationTime
    {
        public string UserEID { get; set; }
        public DateTime CreationTime { get; set; }
        public JumpQueueUser()
        {
            CreationTime = Clock.Now;
        }
    }
}
