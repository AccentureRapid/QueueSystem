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
        private readonly IRepository<QueueHistoryLog> _queueHistoryLogRepostiory;

        public QueueHistoryManager(IRepository<QueueHistory> queueHistoryRepository,
            IRepository<QueueHistoryLog> queueHistoryLogRepostiory)
        {
            _queueHistoryRepository = queueHistoryRepository;
            _queueHistoryLogRepostiory = queueHistoryLogRepostiory;
        }

        public async Task<int> GetQueueHistoryCount()
        {
            var list = await _queueHistoryRepository.GetAllListAsync();
            var count = list.Count();
            return count;
        }

        public async Task<int> SaveQueueHistory(QueueHistory history)
        {
           var id = await _queueHistoryRepository.InsertAndGetIdAsync(history);
           return id;
        }

        public async Task RemoveQueueHistory(string connectionId)
        {
            await _queueHistoryRepository.DeleteAsync(x => x.ConnectionId == connectionId);
        }

        public async Task UpdateDisconnectedTime(string connectionId)
        {
            var logs = _queueHistoryLogRepostiory.GetAll().Where(x => x.ConnectionId == connectionId).ToList();
            if (logs.Any())
            {
                var log = logs.FirstOrDefault();
                log.DisconnectedTime = DateTime.Now;
                await _queueHistoryLogRepostiory.UpdateAsync(log);
            }
        }

        public async Task<int> GetAveragePredictedMinutes()
        {
            var result = await Task.Run(() =>
            {

                var count = 300;
                var logs = _queueHistoryLogRepostiory.GetAll().OrderByDescending( x => x.CreationTime).Take(count).ToList();

                var calculateMinutes = logs.Select(x =>
                {
                    var start = x.ConnectedTime;
                    var end = x.DisconnectedTime;
                    TimeSpan sp = end - start;
                    var minutes = sp.TotalMinutes;
                    return minutes;
                }).ToList();



                var averageMinutes = calculateMinutes.Sum(x => Convert.ToInt32(x)) / logs.Count;

                return averageMinutes;
            });

            return result;
        }

        public async Task<bool> UserInQueue(string userEid)
        {
            var count = await _queueHistoryRepository.CountAsync(x => x.UserEID == userEid);
            return count > 0;
        }
    }
}
