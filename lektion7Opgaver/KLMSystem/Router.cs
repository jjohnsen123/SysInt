using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace KLMSystem
{
    class Router
    {
        protected MessageQueue inQueue;
        public Router(MessageQueue inQueue)
        {
            this.inQueue = inQueue;
            inQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnMessage);
            inQueue.BeginReceive();
        }
        private void OnMessage(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)source;
            Message m = mq.EndReceive(asyncResult.AsyncResult);
            string messageContent = (string)m.Body;
            Console.WriteLine("Fly Info modtaget: ");
            Console.WriteLine("-----------------------------------------------");
            var etaDeserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<ETA>(messageContent);

            var etaJson = Newtonsoft.Json.JsonConvert.SerializeObject(etaDeserialized);

            Console.WriteLine(etaJson);
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            mq.BeginReceive();
            
            return;
        }
    }
    }
