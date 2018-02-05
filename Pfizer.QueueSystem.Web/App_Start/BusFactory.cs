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
        private static IBus _bus;

        
        private static IBus CreateMessageBus()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RabbitMQ"];
            if (connectionString == null || connectionString.ConnectionString == string.Empty)
            {
                throw new Exception("messageserver connection string is missing or empty");
            }
            return RabbitHutch.CreateBus(connectionString.ConnectionString);
        }

        public static IBus GetMessageBus()
        {
            if (_bus == null)
            {
                _bus = BusFactory.CreateMessageBus();
            }

            return _bus; 
        }

    }
}