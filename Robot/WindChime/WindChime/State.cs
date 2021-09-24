
using System;
using System.Collections.Generic;
using System.IO;

public struct States
{
    public float[] Instinct;
    public float[] Insight;
    public float[] Communication;
    public float[] Oppression;
}

public class State{
    public static States sts;
    public static Dictionary<string,string> Check = new Dictionary<string, string>();
    public static List<string> groups = new List<string>();
    public State()
    {
        sts.Instinct = new float[5];
        sts.Insight = new float[5];
        sts.Communication = new float[5];
        sts.Oppression = new float[5];
        GetInfo();
        Init();
    }
    public static void Save()
    {
        using (StreamWriter sw = new StreamWriter("SignUp.dat"))
        {
            foreach (var i in Check)
            {
                sw.WriteLine(i.Key + " " + i.Value);
            }
        }
    }
    public static void GroupsSend(string message)
    {
        foreach (var i in groups)
            Api.Group(i, message);
    }
    public static void Init()
    {
        using (StreamReader sr = new StreamReader("Groups.dat"))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                groups.Add(line);
            }
        }
    }
    public static void Register(string group_id, string user_id, string name)
    {
        try
        {
            Check.Add(user_id, name);
            Api.Group(group_id, name + "报名成功！");
            Save();
        }
        catch
        {
            Api.Group(group_id, "已经报名过了哦！");
        }
    }
    public static string GetMessage(float r)
    {
        if (r <= 120)
            return "低";
        if (r <= 240)
            return "一般";
        return "高";
    }
    public static string Find()
    {
        string ret = "";
        float CountInstinct = 0;
        for (int i = 0; i < 5; i++)
            CountInstinct += sts.Instinct[i];
        ret += "本能：" + GetMessage(CountInstinct) +"\n";

        float CountInsight = 0;
        for (int i = 0; i < 5; i++)
            CountInsight += sts.Insight[i];
        ret += "洞察：" + GetMessage(CountInsight) + "\n";

        float CountCommunication = 0;
        for (int i = 0; i < 5; i++)
            CountCommunication += sts.Communication[i];
        ret += "沟通：" + GetMessage(CountCommunication) + "\n";

        float CountOppression = 0;
        for (int i = 0; i < 5; i++)
            CountOppression += sts.Oppression[i];
        ret += "压迫：" + GetMessage(CountOppression);

        return ret;
    }
    public static void ReNew()
    {
        Random rd = new Random();
        float n = 0;
        for (int i = 0; i < 5; i++)
        {
            n += rd.Next(5, 30);
            sts.Instinct[i] = n;
        }

        n = 0;
        for (int i = 0; i < 5; i++)
        {
            n += rd.Next(5, 30);
            sts.Insight[i] = n;
        }

        n = 0;
        for (int i = 0; i < 5; i++)
        {
            n += rd.Next(5, 30);
            sts.Communication[i] = n;
        }

        n = 0;
        for (int i = 0; i < 5; i++)
        {
            n += rd.Next(5, 30);
            sts.Oppression[i] = n;
        }
    }
    public static void Write()
    {
        try
        {
            using StreamWriter sw = new StreamWriter("state.dat");
            for (int i = 0; i < 5; i++)
                sw.Write(sts.Instinct[i] + " ");
            sw.WriteLine("");
            for (int i = 0; i < 5; i++)
                sw.Write(sts.Insight[i] + " ");
            sw.WriteLine("");
            for (int i = 0; i < 5; i++)
                sw.Write(sts.Communication[i] + " ");
            sw.WriteLine("");
            for (int i = 0; i < 5; i++)
                sw.Write(sts.Oppression[i] + " ");
        }
        catch
        {
            Api.Private("635691684", "写入失败");
        }
    }
    public static void GetInfo()
    {
        try
        {
            using StreamReader sr = new StreamReader("state.dat");
            string line;
            int cnt = 1;
            while ((line = sr.ReadLine()) != null)
            {
                if (cnt == 1)
                {
                    string[] st = line.Split(" ");
                    for (int i = 0; i < 5; i++)
                        sts.Instinct[i] = Convert.ToSingle(st[i]);
                }
                if (cnt == 2)
                {
                    string[] st = line.Split(" ");
                    for (int i = 0; i < 5; i++)
                        sts.Insight[i] = Convert.ToSingle(st[i]);
                }
                if (cnt == 3)
                {
                    string[] st = line.Split(" ");
                    for (int i = 0; i < 5; i++)
                        sts.Communication[i] = Convert.ToSingle(st[i]);
                }
                if (cnt == 4)
                {
                    string[] st = line.Split(" ");
                    for (int i = 0; i < 5; i++)
                        sts.Oppression[i] = Convert.ToSingle(st[i]);
                }
                cnt++;
            }
        }
        catch
        {
            Api.Private("635691684", "初始化失败");
        }
    }
}
