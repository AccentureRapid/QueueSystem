using EasyNetQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Pfizer.QueueSystem.Web
{
    public static class BusFactory
    {
        public static IBus CreateMessageBus()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RabbitMQ"];
            if (connectionString == null || connectionString.ConnectionString == string.Empty)
            {
                throw new Exception("messageserver connection string is missing or empty");
            }
            return RabbitHutch.CreateBus(connectionString.ConnectionString);
        }
    }
}