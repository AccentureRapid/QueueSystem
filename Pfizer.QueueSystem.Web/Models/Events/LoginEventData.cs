using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pfizer.QueueSystem.Web.Models.Events
{
    public class LoginEventData: EventData
    {
        public string UserEID { get; set; }
        public string UserName { get; set; }
        public string ConnectionId { get; set; }

    }
}