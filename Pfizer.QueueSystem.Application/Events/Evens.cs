using Abp.Events.Bus;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pfizer.QueueSystem.Application.Events
{
    public class QueueHistorySavedEventData: EventData
    {
        public QueueHistory History { get; set; }

    }
}