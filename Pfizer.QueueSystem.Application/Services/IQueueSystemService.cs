using Abp.Application.Services;
using Pfizer.QueueSystem.Entities;
using Pfizer.QueueSystem.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services
{
    public interface IQueueSystemService : IApplicationService
    {
        Task<int> GetOnlineCustomersCount();

        Task<bool> CanAccessSystem();

        Task<List<TimeSpanDto>> GetTimeSpanCollection();

        Task<FastToken> TakeFastToken(FastTokenDto dto);
    }
}
