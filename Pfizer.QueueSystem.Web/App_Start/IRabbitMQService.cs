using Abp;
using Abp.Dependency;
using EasyNetQ;
using Pfizer.QueueSystem.Services;
using Pfizer.QueueSystem.Web.Models.Message;
using System.Diagnostics;
using System.Web.Optimization;

namespace Pfizer.QueueSystem.Web.App_Start
{
   public interface IRabbitMQService : ISingletonDependency
    {
        void Subscribe();
        void UnSubscribe();
    }
}
