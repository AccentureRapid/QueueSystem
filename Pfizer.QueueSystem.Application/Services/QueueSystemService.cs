using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Pfizer.QueueSystem.Entities;
using Pfizer.QueueSystem.Services.Dto;

namespace Pfizer.QueueSystem.Services
{
    public class QueueSystemService : IQueueSystemService
    {
        private readonly IQueueSystemManager _queueSystemManager;
        private readonly IObjectMapper _objectMapper;
        public QueueSystemService(IQueueSystemManager queueSystemManager, 
            IObjectMapper objectMapper)
        {
            _queueSystemManager = queueSystemManager;
            _objectMapper = objectMapper;
        }

        public async Task<bool> CanAccessSystem()
        {
            var allowedMaxOnlineCustomerCount = Convert.ToInt32(ConfigurationManager.AppSettings["AllowedMaxOnlineCustomerCount"]);
            var onlineCustomersMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineCustomerMinutes"]);
            var onlineCustomersCount = await _queueSystemManager.GetOnlineCustomersCount(DateTime.UtcNow.AddMinutes(-onlineCustomersMinutes));

            if (onlineCustomersCount >= allowedMaxOnlineCustomerCount)
            {
                return false;
            }
            return true;
        }

        public async Task<int> GetOnlineCustomersCount()
        {
            var onlineCustomersMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineCustomerMinutes"]);
            var count = await _queueSystemManager.GetOnlineCustomersCount(DateTime.UtcNow.AddMinutes(-onlineCustomersMinutes));
            return count;
        }   
    }
}
