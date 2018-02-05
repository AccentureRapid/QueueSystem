using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services
{
    public class QueueHistoryManager: DomainService, IQueueHistoryManager
    {
        private readonly IRepository<QueueHistory> _queueHistoryRepository;

        public QueueHistoryManager(IRepository<QueueHistory> queueHistoryRepository)
        {
            _queueHistoryRepository = queueHistoryRepository;
        }

        public async Task<int> SaveQueueHistory(QueueHistory history)
        {
           var id = await _queueHistoryRepository.InsertAndGetIdAsync(history);
           return id;
        }
    }
}
