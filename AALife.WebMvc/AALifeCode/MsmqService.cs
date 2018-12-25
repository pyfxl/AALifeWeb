using AALife.Model;
using AALife.MSMQ;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

/// <summary>
/// MsmqService 的摘要说明
/// </summary>
public class MsmqService
{
    public static Logger log = LogManager.GetCurrentClassLogger();

    public MsmqService()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static void ReceiveMessage()
    {
        try
        {
            MessageQueue myQueue = MsmqHelper.CreateQueue();
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ItemInfo) });
            myQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(MyReceiveCompleted);
            myQueue.BeginReceive();
        }
        catch (Exception ex)
        {
        }
    }

    private static void MyReceiveCompleted(object source, ReceiveCompletedEventArgs asyncResult)
    {
        try
        {
            // Connect to the queue.          
            MessageQueue mq = (MessageQueue)source;

            // End the asynchronous Receive operation.
            System.Messaging.Message m = mq.EndReceive(asyncResult.AsyncResult);

            //业务处理
            ItemInfo item = (ItemInfo)m.Body;
            log.Info(item.ToString());

            // Restart the asynchronous Receive operation.
            mq.BeginReceive();

        }
        catch (Exception ex)
        {
        }
    }
}