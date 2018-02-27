using AALife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AALife.MSMQ
{
    public class MsmqHelper
    {
        private static readonly string queuePath = @".\Private$\AALifeItem";

        //创建消息对象
        public static MessageQueue CreateQueue()
        {
            if(!MessageQueue.Exists(queuePath))
            {
                return MessageQueue.Create(queuePath);
            }
            else
            {
                return new MessageQueue(queuePath);
            }            
        }

        //发送消息
        public static bool SendMessage(ItemInfo item)
        {
            try
            {
                MessageQueue myQueue = CreateQueue();

                Message myMessage = new Message();
                myMessage.Body = item;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(ItemInfo) });

                myQueue.Send(myMessage);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //发送消息
        public static bool SendMessage(string json)
        {
            try
            {
                MessageQueue myQueue = CreateQueue();

                Message myMessage = new Message();
                myMessage.Body = json;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                myQueue.Send(myMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        //接收消息
        public static ItemInfo ReceiveMessage()
        {            
            try
            {
                MessageQueue myQueue = CreateQueue();
                myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ItemInfo) });

                Message myMessage = myQueue.Receive();
                ItemInfo item = (ItemInfo)myMessage.Body;

                return item;
            }
            catch(Exception ex)
            {
                return new ItemInfo();
            }
        }
        
        //删除消息对象
        public static bool Delete()
        {
            try
            {
                if (MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Delete(queuePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
