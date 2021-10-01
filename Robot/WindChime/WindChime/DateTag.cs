using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

struct Tip
{
    public int day;
    public int hour;
    public int min;
    public string To;
    public string message;
    public bool tips;
}

public class DateTag
{
    List<Tip> list = new List<Tip>();
    public static bool Main(string group_id, string user_id, string name, string message)
    {
        string[] str = message.Split(" ");
        if (str[0] == "����")
        {
            Question(group_id,user_id,name,message);
            Api.Group(group_id, "���Գɹ�!��ʿ�������˵���Ϣ��������ת���ˣ�");
            return true;
        }
        return false;
    }





    public static void Question(string group_id, string user_id, string name, string message)
    {
        string str = "��Ⱥ��" + group_id + "\n";
        str += "��" + name + "(" + user_id + ")˵�ˣ�\n";
        str += message;
        Api.Private("635691684", str);
    }
    public DateTag()
    {
        try
        {
            using (StreamReader sr = new StreamReader("DateMessage.dat"))
            {
                string ss;
                while ( (ss = sr.ReadLine()) != null)
                {
                    Tip tip;
                    string[] sp = ss.Split();
                    tip.day = Convert.ToInt32(sp[0]);
                    tip.hour = Convert.ToInt32(sp[1]);
                    tip.min = Convert.ToInt32(sp[2]);
                    tip.To = sp[3];
                    tip.message = sp[4];
                    tip.tips = true;
                    list.Add(tip);
                }
            }

        }
        catch (Exception e)
        {
            Api.Private("635691684", "datetag���ʼ���쳣" + e.ToString());
        }
    }
    public void DateWrite()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("DateMessage.dat"))
            {
                foreach(var i in list)
                {
                    if(i.tips)
                        sw.WriteLine(i.day + " " + i.hour + " " + i.min + " " + i.To + " " + i.message);
                }
            }
        }
        catch (Exception e)
        {
            Api.Private("635691684", "datetag��Write�쳣" + e.ToString());
        }
    }
    public void Prt()
    {
        for (int i = 0; i < list.Count; i ++)
        {
            Tip k = list[i];
            if (k.day == DateTime.Now.Day && k.hour == DateTime.Now.Hour && DateTime.Now.Minute == k.min && k.tips)
            {
                Api.Private(k.To, k.message);
                list.Remove(k);
                k.tips = false;
            }
        }
    }
    public bool Add(string group_id, string user_id,string message)
    {
        if (!message.Contains("��������"))
            return false;
        if (!message.Contains("ʱ"))
        {
            Api.Group(group_id, "��ʽ��\n��������\nxx��xxʱxx��\n�������ѵ���Ϣ");
            return true;
        }
        if (!message.Contains("��"))
        {
            Api.Group(group_id, "��ʽ��\n��������\nxx��xxʱxx��\n�������ѵ���Ϣ");
            return true;
        }
        if (!message.Contains("��"))
        {
            Api.Group(group_id, "��ʽ��\n��������\nxx��xxʱxx��\n�������ѵ���Ϣ");
            return true;
        }
        string[] str;
        if (message.Contains("\r\n"))
            str = message.Split("\r\n");
        else
            str = message.Split("\n");
        try
        {
            Tip tip;
            string[] st1, st2, st3;
            st1 = str[1].Split("��");
            tip.day = Convert.ToInt32(st1[0]);
            st2 = st1[1].Split("ʱ");
            tip.hour = Convert.ToInt32(st2[0]);
            st3 = st2[1].Split("��");
            tip.min = Convert.ToInt32(st3[0]);
            tip.To = user_id;
            tip.message = str[2];
            tip.tips = true;
            list.Add(tip);
            DateWrite();
            Api.Group(group_id, "��ӳɹ�����ǰlist����������Ϊ��"+list.Count+"��");
        }
        catch (Exception e)
        {
            Api.Group(group_id, "��ʽ����ȷ");
        }
        return true;
    }
}