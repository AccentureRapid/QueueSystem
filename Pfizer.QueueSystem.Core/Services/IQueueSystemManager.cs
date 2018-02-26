using Abp.Domain.Services;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services
{
    public interface IQueueSystemManager : IDomainService
    {
        Task<int> GetOnlineCustomersCount(DateTime lastActivityFromUtc);
    }
}
