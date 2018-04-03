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
        Task<bool> CanAccessSystem(UserIdDto dto);
        Task<bool> UserInQueue(UserIdDto dto);
        Task<List<TimeSpanDto>> GetTimeSpanCollection();
        Task<FastTokenResult> TakeFastToken(FastTokenDto dto);

        Task<RefreshQueueInformationDto> GetQueueInfomation(ConnectionDto dto);
        Task<RefreshQueueInformationDto> GetQueueInfomationForFirsttime();
    }
}
