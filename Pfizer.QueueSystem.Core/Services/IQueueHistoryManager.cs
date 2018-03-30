using Abp.Domain.Services;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services
{
    public interface IQueueHistoryManager : IDomainService
    {
        Task<int> SaveQueueHistory(QueueHistory history);

        Task<int> GetQueueHistoryCount();

        Task RemoveQueueHistory(string connectionId);
        Task UpdateDisconnectedTime(string connectionId);
        Task<int> GetAveragePredictedMinutes();
    }
}
