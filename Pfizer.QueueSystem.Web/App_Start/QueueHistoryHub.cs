using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using Pfizer.QueueSystem.Services;
using Pfizer.QueueSystem.Web.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
public class QueueHistoryHub : Hub, ISingletonDependency
{
    private readonly IQueueHistoryService _queueHistoryService;
    private readonly IQueueSystemService _queueSystemService;
    private readonly IEventBus _eventBus;

    public IAbpSession AbpSession { get; set; }

    public ILogger Logger { get; set; }

    public QueueHistoryHub(IQueueHistoryService queueHistoryService,
        IQueueSystemService queueSystemService,
        IEventBus eventBus)
    {
        _queueHistoryService = queueHistoryService;
        _queueSystemService = queueSystemService;
        _eventBus = eventBus;

        AbpSession = NullAbpSession.Instance;
        Logger = NullLogger.Instance;
    }

    public async Task Queue(string ntid)
    {
        var userId = ntid;
        var connectionId = Context.ConnectionId;
        var userName = userId;

        _eventBus.Trigger(new LoginEventData
        {
            UserEID = userId,
            UserName = userName,
            ConnectionId = connectionId
        });

        var refreshInformation = await _queueSystemService.GetQueueInfomationForFirsttime();
        Clients.Client(Context.ConnectionId).setQueueInformation(refreshInformation.UsersCountBeforeMe, refreshInformation.PredictedMinutes);

        var count = await _queueHistoryService.GetQueueHistoryCount();
        Clients.All.getMessage(string.Format("User {0}, currently there are {1} users in the queue. Message from client {2}", AbpSession.UserId, count, ntid));
    }

    public async override Task OnConnected()
    {
        await base.OnConnected();
        
        Clients.Client(Context.ConnectionId).getMessage(string.Format("Welcome you are here."));
        Logger.Debug("A client connected to QueueHistoryHub: " + Context.ConnectionId);
    }

    public async override Task OnDisconnected(bool stopCalled)
    {
        await base.OnDisconnected(stopCalled);
        await _queueHistoryService.RemoveQueueHistory(Context.ConnectionId);

        await _queueHistoryService.UpdateDisconnectedTime(Context.ConnectionId);

        Logger.Debug("A client disconnected from QueueHistoryHub: " + Context.ConnectionId);
    }
}