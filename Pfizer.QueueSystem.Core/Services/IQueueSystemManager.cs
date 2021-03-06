﻿using Abp.Domain.Services;
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
        Task<bool> Exist(string userEId);
        Task<FastToken> SaveFastToken(FastToken entity);
        Task<bool> HaveFastTokenForNow(string userId);
        Task<bool> Exists(string userId, int timeSpanId);
        Task<int> GetTotalCountOfFastTokenForThisTimeSpan(DateTime start, DateTime end);
        Task<int> GetTotalCountOfFastTokenForUser(string userId);
        Task<int> GetTotalUsersCountBeforeMe(string connectionId);
        Task<int> GetTotalUsersCountBeforeMe();

    }
}
