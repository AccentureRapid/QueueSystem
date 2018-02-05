using EasyNetQ;
using Pfizer.QueueSystem.Web.Models.Message;
using System.Diagnostics;
using System.Web.Optimization;

namespace Pfizer.QueueSystem.Web
{
    public static class RabbitMQService
    {
        private static IBus _bus;
        static RabbitMQService()
        {
            if (_bus == null)
            {
                _bus = BusFactory.GetMessageBus();
            }
        }

        public static void Subscribe()
        {
            _bus.SubscribeAsync<ClientQueueMessage>("NewClientQueuedMessage", message
                => System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    var userEID = message.UserEID;
                    var userName = message.UserName;
                    //TODO Save the queue message to data storage, like sql server etc

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
    }
}