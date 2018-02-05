using System;
using System.Threading;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Abp.Dependency;
using Abp.WebApi.Validation;
using Castle.Facilities.Logging;
using Pfizer.QueueSystem.Web.App_Start;

namespace Pfizer.QueueSystem.Web
{
    public class MvcApplication : AbpWebApplication<QueueSystemWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
            );
            base.Application_Start(sender, e);

            var rabbit = AbpBootstrapper.IocManager.IocContainer.Resolve<IRabbitMQService>();
            rabbit.Subscribe();
            //RabbitMQService.Subscribe();
        }

        protected override void Application_End(object sender, EventArgs e)
        {
            var rabbit = AbpBootstrapper.IocManager.IocContainer.Resolve<IRabbitMQService>();
            rabbit.UnSubscribe();
            //RabbitMQService.UnSubscribe();
        }
    }
}
