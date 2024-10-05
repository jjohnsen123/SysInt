using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MYFirstMSMQ
{
    class AirlineCompanyPublisher
    {
        protected MessageQueue inQueue;
        protected MessageQueue outQueueSAS;
        protected MessageQueue outQueueKLM;
        protected MessageQueue outQueueSW;
        public AirlineCompanyPublisher(MessageQueue inQueue, MessageQueue outQueueSAS, MessageQueue outQueueKLM, MessageQueue outQueueSW)
        {
            this.inQueue = inQueue;
            this.outQueueSAS = outQueueSAS;
            this.outQueueKLM = outQueueKLM;
            this.outQueueSW = outQueueSW;
            inQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnMessage);
            inQueue.BeginReceive();
            string label = inQueue.Label;

        }
        private void OnMessage(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)source;
            Message message = mq.EndReceive(asyncResult.AsyncResult);
                string label = message.Label;
                outQueueSAS.Send(message);
                outQueueKLM.Send(message);
                outQueueSW.Send(message);

                mq.BeginReceive();

        }
    }
}
