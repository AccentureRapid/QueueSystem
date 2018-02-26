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
    public class QueueSystemManager : DomainService, IQueueSystemManager
    {
        private readonly IRepository<QueueHistory> _queueHistoryRepository;

        public QueueSystemManager(IRepository<QueueHistory> queueHistoryRepository)
        {
            _queueHistoryRepository = queueHistoryRepository;
        }

        public async Task<int> GetOnlineCustomersCount(DateTime lastActivityFromUtc)
        {
            //TODO
            //var query = _customerRepository.Table;
            //query = query.Where(c => lastActivityFromUtc <= c.LastActivityDateUtc);

            var list = await _queueHistoryRepository.GetAllListAsync();
            var count = list.Count();
            return count;
        }
    }
}
