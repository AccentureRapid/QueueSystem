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

    [Table("RequestTableForFace")]
    public class RequestTableForFace : IEntity<Guid>
    {
        public RequestTableForFace()
        {

        }
        [Key]
        public Guid RanID { get; set; }
        public DateTime? RequestTime { get; set; }
        public string IP { get; set; }
        public string URL { get; set; }
        public string Devices { get; set; }
        public string ComputerNames { get; set; }
        [MaxLength(256)]
        public string NTID { get; set; }
        [MaxLength(10)]
        public string RecordDate { get; set; }
        public bool? IsCalculated { get; set; }
        public DateTime? EndDate { get; set; }

        [NotMapped]
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
   
}
