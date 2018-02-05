using Abp.AutoMapper;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services.Dto
{
    [AutoMapTo(typeof(QueueHistory))]
    public class QueueHistoryDto
    {
        public string UserEID { get; set; }
        public string UserName { get; set; }
    }
}
