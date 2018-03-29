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
            IIocManager iocManager)
        {
            _queueHistoryRepository = queueHistoryRepository;
            _fastTokenRepository = fastTokenRepository;
            _requestTableForFaceRepository = requestTableForFaceRepository;
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
                //TODO add two column: 1 date 2 id in the timespan collection
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
    }
}
