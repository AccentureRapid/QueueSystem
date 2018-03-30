using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;
using Abp.ObjectMapping;
using Pfizer.QueueSystem.Application.Events;
using Pfizer.QueueSystem.Entities;
using Pfizer.QueueSystem.Services.Dto;

namespace Pfizer.QueueSystem.Services
{
    public class QueueHistoryService : IQueueHistoryService
    {
        private readonly IQueueHistoryManager _queueHistoryManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IEventBus _eventBus;
        public QueueHistoryService(IQueueHistoryManager queueHistoryManager,
            IEventBus eventBus,
            IObjectMapper objectMapper)
        {
            _queueHistoryManager = queueHistoryManager;
            _eventBus = eventBus;
            _objectMapper = objectMapper;
        }

        public async Task<int> GetQueueHistoryCount()
        {
            var count = await _queueHistoryManager.GetQueueHistoryCount();
            return count;
        }

        public async Task SaveQueueHistory(QueueHistoryDto input)
        {
            var entity = _objectMapper.Map<QueueHistory>(input);
            await _queueHistoryManager.SaveQueueHistory(entity);
            _eventBus.Trigger(new QueueHistorySavedEventData { History = entity });

        }

        public async Task RemoveQueueHistory(string connectionId)
        {
            await _queueHistoryManager.RemoveQueueHistory(connectionId);
        }
    }
}
