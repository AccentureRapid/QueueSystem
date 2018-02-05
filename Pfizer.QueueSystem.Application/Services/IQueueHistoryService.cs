using Abp.Application.Services;
using Pfizer.QueueSystem.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services
{
    public interface IQueueHistoryService : IApplicationService
    {
        Task SaveQueueHistory(QueueHistoryDto history);
    }
}
