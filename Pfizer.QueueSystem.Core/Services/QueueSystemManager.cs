using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Pfizer.QueueSystem.Entities;
using Pfizer.QueueSystem.EntityFramework;
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
        private readonly IRepository<FastToken> _fastTokenRepository;
        private readonly IRepository<RequestTableForFace, Guid> _requestTableForFaceRepository;
        private readonly IRepository<JumpQueueUser> _jumpQueueUserRepository;
        private readonly IIocManager _iocManager;
        private FaceDeviceLogDbContext db
        {
            get
            {
                return _iocManager.ResolveAsDisposable<FaceDeviceLogDbContext>().Object;
            }
        }
        public QueueSystemManager(IRepository<QueueHistory> queueHistoryRepository,
             IRepository<FastToken> fastTokenRepository,
             IRepository<RequestTableForFace, Guid> requestTableForFaceRepository,
             IRepository<JumpQueueUser> jumpQueueUserRepository,
            IIocManager iocManager)
        {
            _queueHistoryRepository = queueHistoryRepository;
            _fastTokenRepository = fastTokenRepository;
            _requestTableForFaceRepository = requestTableForFaceRepository;
            _jumpQueueUserRepository = jumpQueueUserRepository;
            _iocManager = iocManager;
        }

        public async Task<int> GetOnlineCustomersCount(DateTime lastActivityFromUtc)
        {
            var result = await Task.Run(() =>
            {
                var query = from r in this.db.RequestTableForFace
                            select r;

                var list = query.Where(x => x.EndDate >= lastActivityFromUtc).ToList();
                var count = list.Count();
                return count;
            });

            return result;
        }

        public async Task<FastToken> SaveFastToken(FastToken entity)
        {
            var result = await Task.Run(() =>
            {
                var token = _fastTokenRepository.Insert(entity);
                return token;
            });

            return result;
        }


        public async Task<bool> Exists(string userId, int timeSpanId)
        {
            var result = await Task.Run(() =>
            {
                var date = DateTime.Now.Date.ToString("yyyyMMdd");
                var count = _fastTokenRepository.Count(x => x.UserEID == userId
                            && x.Date == date && x.TimeSpanId == timeSpanId);

                if (count > 0)
                    return true;
                else
                    return false;

            });


            return result;
        }

        public async Task<int> GetTotalUsersCountBeforeMe(string connectionId)
        {
            var result = await Task.Run(() =>
            {
                var queueHistory = _queueHistoryRepository.GetAll().Where(x => x.ConnectionId == connectionId).ToList().FirstOrDefault();
                if (queueHistory != null)
                {
                    var count = _queueHistoryRepository.Count(x => x.CreationTime < queueHistory.CreationTime);
                    return count;
                }
                return 0;
            });


            return result;

        }

        public async Task<int> GetTotalUsersCountBeforeMe()
        {
            var result = await Task.Run(() =>
            {

                var count = _queueHistoryRepository.Count(x => x.CreationTime < DateTime.Now);
                return count;

            });


            return result;

        }

        public async Task<int> GetTotalCountOfFastTokenForThisTimeSpan(DateTime start, DateTime end)
        {
            var count = await _fastTokenRepository.CountAsync(x => x.StartTime == start && x.EndTime == end);
            return count;
        }

        public async Task<int> GetTotalCountOfFastTokenForUser(string userId)
        {
            var count = await _fastTokenRepository.CountAsync(x => x.UserEID == userId
            && x.CreationTime.Year == DateTime.Now.Year
            && x.CreationTime.Month == DateTime.Now.Month
            && x.CreationTime.Day == DateTime.Now.Day);
            return count;
        }

        public async Task<bool> HaveFastTokenForNow(string userId)
        {
            var result = await Task.Run(() => {

                var tokens = _fastTokenRepository.GetAll().Where(
                    x => x.StartTime <= DateTime.Now && x.EndTime >= DateTime.Now
                    && x.UserEID == userId).ToList();
                return tokens;
            });

            if (result.Any())
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Exist(string userEId)
        {
            var result = await _jumpQueueUserRepository.CountAsync(x => x.UserEID == userEId);
            return result > 0;
        }
    }
}
