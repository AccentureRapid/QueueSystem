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
            var result = await Task.Run(() => {
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
            var result = await Task.Run(() => {
                var token = _fastTokenRepository.Insert(entity);
                return token;
            });

            return result;
        }
    }
}
