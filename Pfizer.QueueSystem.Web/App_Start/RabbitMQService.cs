using Abp;
using Abp.Dependency;
using EasyNetQ;
using Pfizer.QueueSystem.Services;
using Pfizer.QueueSystem.Web.App_Start;
using Pfizer.QueueSystem.Web.Models.Message;
using System.Diagnostics;
using System.Web.Optimization;

namespace Pfizer.QueueSystem.Web
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IQueueHistoryService _queueHistoryService;

        private static IBus _bus;
        public RabbitMQService(IQueueHistoryService queueHistoryService)
        {
            _queueHistoryService = queueHistoryService;

            if (_bus == null)
            {
                _bus = BusFactory.GetMessageBus();
            }
        }

        public void Subscribe()
        {
            _bus.SubscribeAsync<ClientQueueMessage>("NewClientQueuedMessage", message
                => System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    var userEID = message.UserEID;
                    var userName = message.UserName;
                    // Save the queue message to data storage, like sql server etc

                    _queueHistoryService.SaveQueueHistory(new Services.Dto.QueueHistoryDto
                    {
                        UserEID = userEID,
                        UserName = userName
                    });

                    Debug.WriteLine(string.Format("Hello : {0} with {1}, you are in the queue now.", userEID, userName));

                }).ContinueWith(task =>
                {
                    if (task.IsCompleted && !task.IsFaulted)
                    {
                        // Everything worked out ok
                    }
                    else
                    {
                        // Dont catch this, it is caught further up the heirarchy and results in being sent to the default error queue
                        // on the broker
                        throw new EasyNetQException("NewClientQueuedMessage processing exception - look in the default error queue (broker)");
                    }
                }));
        }
        public void UnSubscribe()
        {
            _bus.SafeDispose();
        }
    }
}