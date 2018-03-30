using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
        private readonly IQueueHistoryManager _queueHistoryManager;
        private readonly IObjectMapper _objectMapper;

        public QueueSystemService(IQueueSystemManager queueSystemManager,
            IQueueHistoryManager queueHistoryManager,
            IObjectMapper objectMapper)
        {
            _queueSystemManager = queueSystemManager;
            _queueHistoryManager = queueHistoryManager;

            _objectMapper = objectMapper;
        }

        public async Task<bool> CanAccessSystem()
        {
            var allowedMaxOnlineCustomerCount = Convert.ToInt32(ConfigurationManager.AppSettings["AllowedMaxOnlineCustomerCount"]);
            var onlineCustomersMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineCustomerMinutes"]);
            var onlineCustomersCount = await _queueSystemManager.GetOnlineCustomersCount(DateTime.Now.AddMinutes(-onlineCustomersMinutes));

            if (onlineCustomersCount >= allowedMaxOnlineCustomerCount)
            {
                return false;
            }
            return true;
        }

        public async Task<int> GetOnlineCustomersCount()
        {
            var onlineCustomersMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineCustomerMinutes"]);
            var count = await _queueSystemManager.GetOnlineCustomersCount(DateTime.Now.AddMinutes(-onlineCustomersMinutes));
            return count;
        }

        public async Task<RefreshQueueInformationDto> GetQueueInfomation(ConnectionDto dto)
        {
            //1. get count of users before me, if == 0 : && total online customers < max count, redirectable = true
            //2. get count of users before me for display and predicted total minutes
            var result = new RefreshQueueInformationDto();

            var predictedMinutesForOneUser = await _queueHistoryManager.GetAveragePredictedMinutes();

            var countBeforeMe = await _queueSystemManager.GetTotalUsersCountBeforeMe(dto.ConnectionId);
            var totalOnlineCustomers = await this.GetOnlineCustomersCount();
            bool redirctable = false;
            var canAccessSystem = await this.CanAccessSystem();
            if (countBeforeMe == 0 && canAccessSystem)
            {
                redirctable = true;
            }
            result.Redirectable = redirctable;
            result.UsersCountBeforeMe = countBeforeMe;
            result.PredictedMinutes = predictedMinutesForOneUser * countBeforeMe;

            return result;
        }

        public async Task<RefreshQueueInformationDto> GetQueueInfomationForFirsttime()
        {   //0. invoke for the first time when hub push information to client.
            //1. get count of users before me, if == 0 : && total online customers < max count, redirectable = true
            //2. get count of users before me for display and predicted total minutes
            var result = new RefreshQueueInformationDto();
            var predictedMinutesForOneUser = await _queueHistoryManager.GetAveragePredictedMinutes();

            var countBeforeMe = await _queueSystemManager.GetTotalUsersCountBeforeMe();
            var totalOnlineCustomers = await this.GetOnlineCustomersCount();
            bool redirctable = false;
            var canAccessSystem = await this.CanAccessSystem();
            if (countBeforeMe == 0 && canAccessSystem)
            {
                redirctable = true;
            }
            result.Redirectable = redirctable;
            result.UsersCountBeforeMe = countBeforeMe;
            result.PredictedMinutes = predictedMinutesForOneUser * countBeforeMe;

            return result;
        }

        public async Task<List<TimeSpanDto>> GetTimeSpanCollection()
        {
            var result = await Task.Run(() =>
            {
                List<TimeSpanDto> data = new List<TimeSpanDto>();
                TimeSpanDto dto = null;
                var startDate = DateTime.Now.Date.ToString("yyyy/MM/dd");
                var nextDate = DateTime.Now.AddDays(1).Date.ToString("yyyy/MM/dd");
                DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();

                dtFormat.ShortDatePattern = "yyyy/MM/dd HH:mm";

                for (int i = 0; i < 24; i++)
                {
                    var startTime = string.Format("{0}:00", i.ToString().PadLeft(2, '0'));
                    var endTime = string.Format("{0}:00", (i + 1).ToString().PadLeft(2, '0'));
                    dto = new TimeSpanDto();
                    dto.Id = i + 1;
                    dto.Text = string.Format("{0} {1} - {2}", startDate, startTime, endTime);
                    dto.StartTime = Convert.ToDateTime(string.Format("{0} {1}", startDate, startTime), dtFormat);
                    if (i == 23)
                    {
                        dto.EndTime = Convert.ToDateTime(string.Format("{0} {1}", nextDate, "00:00"), dtFormat);
                    }
                    else
                    {
                        dto.EndTime = Convert.ToDateTime(string.Format("{0} {1}", startDate, endTime), dtFormat);
                    }

                    data.Add(dto);
                }

                return data;
            });

            return result;
        }

        public async Task<FastTokenResult> TakeFastToken(FastTokenDto dto)
        {

            var fastTokenResult = new FastTokenResult();
            var success = true;

            //1. Check UserFastToken is exists or not in the same time span.
            var exists = await _queueSystemManager.Exists(dto.NtId, dto.Id);
            if (exists)
            {
                success = false;
                fastTokenResult.Message = "领取失败，您在此时间段，已领取过快速通行令牌。";
            }

            //2. do not more than TotalCountOfFastTokenForOneTimeSpan
            var collection = await this.GetTimeSpanCollection();
            var timespan = collection.Where(x => x.Id == dto.Id).FirstOrDefault();
            var count = await _queueSystemManager.GetTotalCountOfFastTokenForThisTimeSpan(timespan.StartTime, timespan.EndTime);
            var configuredCount = Convert.ToInt32(ConfigurationManager.AppSettings["TotalCountOfFastTokenForOneTimeSpan"]);
            if (count >= configuredCount)
            {
                success = false;
                fastTokenResult.Message = string.Format("领取失败，在此时间段，系统发放的快速通行令牌 (总数:{0}) 已经被领取完毕。", configuredCount.ToString());
            }

            //3. the total count of fast token for the same day for one user <= TotalCountForFastTokenForOneUser
            var totalCountForFastTokenForOneUser = await _queueSystemManager.GetTotalCountOfFastTokenForUser(dto.NtId);
            var configuredTotalCountForFastTokenForOneUser = Convert.ToInt32(ConfigurationManager.AppSettings["TotalCountForFastTokenForOneUser"]);
            if (totalCountForFastTokenForOneUser >= configuredTotalCountForFastTokenForOneUser)
            {
                success = false;
                fastTokenResult.Message = string.Format("领取失败，您今天领取的快速通行令牌已经超过系统配置的最大数 ({0})。", configuredTotalCountForFastTokenForOneUser.ToString());
            }


            if (fastTokenResult.Success)
            {
                if (timespan != null)
                {
                    FastToken token = new FastToken
                    {
                        UserEID = dto.NtId,
                        Date = DateTime.Now.Date.ToString("yyyyMMdd"),
                        TimeSpanId = timespan.Id,
                        StartTime = timespan.StartTime,
                        EndTime = timespan.EndTime
                    };
                    var entity = await _queueSystemManager.SaveFastToken(token);

                    fastTokenResult.FastToken = entity;

                }
            }

            fastTokenResult.Success = success;

            return fastTokenResult;
        }
    }
}
