using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace AALife.MSMQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of MessageQueue. Set its formatter.
            MessageQueue myQueue = new MessageQueue(@".\Private$\MyQueues");
            myQueue.Formatter = new XmlMessageFormatter(new Type[]
                {typeof(String)});

            // Add an event handler for the ReceiveCompleted event.
            myQueue.ReceiveCompleted += new
                ReceiveCompletedEventHandler(MyReceiveCompleted);

            // Begin the asynchronous receive operation.
            myQueue.BeginReceive();

            // Do other work on the current thread.

            //return;
            Console.ReadKey();
        }

        private static void MyReceiveCompleted(object source, ReceiveCompletedEventArgs asyncResult)
        {
            // Connect to the queue.
            MessageQueue mq = (MessageQueue)source;

            // End the asynchronous Receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            // Display message information on the screen.
            Console.WriteLine("Message: " + (string)m.Body);

            // Restart the asynchronous Receive operation.
            mq.BeginReceive();

            return;
        }
    }
}
