using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using EasyNetQ;
using Pfizer.QueueSystem.Web.Models.Events;
using Pfizer.QueueSystem.Web.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Web.EventHandler
{
    public class LoginEventEventHandler : IEventHandler<LoginEventData>, ITransientDependency
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IBus _bus;

        public ILogger Logger { get; set; }
        public LoginEventEventHandler(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
            _bus = BusFactory.GetMessageBus();

            Logger = NullLogger.Instance;
        }

        public void HandleEvent(LoginEventData eventData)
        {
            _bus.Publish(new ClientQueueMessage
            {
                UserEID = eventData.UserEID,
                UserName = eventData.UserName,
                ConnectionId = eventData.ConnectionId
            });
        }
    }

}
