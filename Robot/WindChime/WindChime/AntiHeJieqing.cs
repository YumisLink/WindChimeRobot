using System.Collections.Generic;
using System;

public class MessageD
{
    public string mg;
    public int min;
    public string id;
}
public class AntiHeJieqing: Possvie
{
    public static List<MessageD> hedalao = new List<MessageD>();
    public static void Main(string message,string user_id,string group_id,string message_id)
    {
        if (user_id != "850419987")
            return;
        MessageD mg = new MessageD()
        {
            mg = message,
            min = DateTime.Now.Minute,
            id = message_id
        };
        hedalao.Add(mg);
        for(int i = 0; i < hedalao.Count; i++)
        {
            if (Math.Abs(hedalao[i].min - DateTime.Now.Minute) > 2)
            {
                hedalao.RemoveAt(i);
                i--;
            }
        }
    }
    public static void Say(string message_id, string user_id)
    {
        if (user_id != "850419987")
            return;
        foreach(var a in hedalao)
        {
            if (a.id == message_id)
                Api.Group("952792192", "何大佬在【" + DateTime.Now + "】撤回了发送的消息：" + a.mg);
        }
    }
}