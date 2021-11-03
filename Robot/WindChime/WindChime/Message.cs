using System;
using System.Collections.Generic;
using System.Text;
using WindEngine;
/// <summary>
/// 发送信息的类型。
/// </summary>
public enum SendMessageType
{
    Private,
    Group
}
/// <summary>
/// 消息的类型
/// </summary>
public enum MessageType
{
    text,
    image
}
public class Message
{
    public string type { get; set; }
    public object data { get; set; }
    public Message(string type, IFMessageData data)
    {
        this.type = type;
        this.data = data;
    }
}
/// <summary>
/// 用于发送消息
/// </summary>
public class SendMessage
{
    public Int64 user_id { set; get; }
    public Int64 group_id { set; get; }
    public List<Message> message { set; get; } = new List<Message>();
    public Boolean auto_escape { set; get; }
    public string ToJson()
    {
        return Lib.GetJson(this);
    }
}
/// <summary>
/// 最终会在Api中把SendMessage转化为sendFinalMessage
/// </summary>
public class sendFinalMessage
{
    public string message_type { set; get; }
    public Int64 user_id { set; get; }
    public Int64 group_id { set; get; }
    public List<Message> message { set; get; }
    public Boolean auto_escape { set; get; }
    public sendFinalMessage(SendMessage msg)
    {
        if (msg.group_id > 1)
            message_type = "group";
        else
            message_type = "private";
        user_id = msg.user_id;
        group_id = msg.group_id;
        message = msg.message;
        auto_escape = msg.auto_escape;
    }
    public string ToJson()
    {
        return Lib.GetJson(this);
    }

}

/// <summary>
/// message 中data的用的接口
/// </summary>
public interface IFMessageData { }
/// <summary>
/// 纯文本
/// </summary>
public class DataPureText : IFMessageData
{
    public string text { get; set; }
}
public class DataImage : IFMessageData
{
    public string file { get; set; }
}
public class DataAt : IFMessageData
{
    public string qq { get; set; }
}
public class DataReplay : IFMessageData
{
    public string id { get; set; }
}