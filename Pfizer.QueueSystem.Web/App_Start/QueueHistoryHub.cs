using Abp.Dependency;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using Pfizer.QueueSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
public class QueueHistoryHub : Hub, ISingletonDependency
{
    private readonly IQueueHistoryService _queueHistoryService;

    public IAbpSession AbpSession { get; set; }

    public ILogger Logger { get; set; }

    public QueueHistoryHub(IQueueHistoryService queueHistoryService)
    {
        _queueHistoryService = queueHistoryService;

        AbpSession = NullAbpSession.Instance;
        Logger = NullLogger.Instance;
    }

    public async Task SendMessage(string message)
    {
        var count = await _queueHistoryService.GetQueueHistoryCount();
        Clients.All.getMessage(string.Format("User {0}, currently there are {1} users in the queue. Message from client {2}", AbpSession.UserId, count, message));
    }

    public async override Task OnConnected()
    {
        await base.OnConnected();
        //TODO get the EID from querystring, then get the count in front of me by eid
        //var eid = Context.QueryString["eid"];
        var count = await _queueHistoryService.GetQueueHistoryCount();
        Clients.Client(Context.ConnectionId).getMessage(string.Format("There are {0} in front of me.", count));
        Logger.Debug("A client connected to QueueHistoryHub: " + Context.ConnectionId);
    }

    public async override Task OnDisconnected(bool stopCalled)
    {
        await base.OnDisconnected(stopCalled);
        Logger.Debug("A client disconnected from QueueHistoryHub: " + Context.ConnectionId);
    }
}