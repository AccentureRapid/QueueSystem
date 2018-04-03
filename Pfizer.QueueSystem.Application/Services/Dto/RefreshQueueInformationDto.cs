using Abp.AutoMapper;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services.Dto
{
    public class RefreshQueueInformationDto
    {
        public bool Redirectable { get; set; }
        public int UsersCountBeforeMe { get; set; }
        public int PredictedMinutes { get; set; }
        public int UsersInQueueCountForFastToken { get; set; }
    }
}
