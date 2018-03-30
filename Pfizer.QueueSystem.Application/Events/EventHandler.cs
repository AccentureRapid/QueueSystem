using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Application.Events
{
    public class LoginEventEventHandler : IEventHandler<QueueHistorySavedEventData>, ITransientDependency
    {
        private readonly IRepository<QueueHistoryLog> _queueHistoryLogRepository;
        public ILogger Logger { get; set; }
        public LoginEventEventHandler(IRepository<QueueHistoryLog> queueHistoryLogRepository)
        {
            _queueHistoryLogRepository = queueHistoryLogRepository;

            Logger = NullLogger.Instance;
        }

        public async void HandleEvent(QueueHistorySavedEventData eventData)
        {
            var log = new QueueHistoryLog {
                UserEID = eventData.History.UserEID,
                UserName = eventData.History.UserName,
                CreationTime = DateTime.Now,
                ConnectionId = eventData.History.ConnectionId,
                ConnectedTime = eventData.History.CreationTime,
                DisconnectedTime = DateTime.Now
            };

            await _queueHistoryLogRepository.InsertAsync(log);
        }
    }

}
