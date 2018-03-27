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

    [Table("FastToken")]
    public class FastToken : Entity, IHasCreationTime
    {
        public string UserEID { get; set; }
        public string Date { get; set; }
        public int TimeSpanId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreationTime { get; set; }
        public FastToken()
        {
            CreationTime = Clock.Now;
        }

    }
}
