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

        public async Task<FastToken> TakeFastToken(FastTokenDto dto)
        {
            var collection = await this.GetTimeSpanCollection();

            var timespan = collection.Where(x => x.Id == dto.Id).FirstOrDefault();
            if (timespan != null)
            {
                FastToken token = new FastToken
                {
                    UserEID = dto.NtId,
                    StartTime = timespan.StartTime,
                    EndTime = timespan.EndTime
                };

                var entity = await _queueSystemManager.SaveFastToken(token);

                return entity;
            }

            return null;
        }
    }
}
