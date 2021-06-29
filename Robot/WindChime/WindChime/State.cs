
using System;
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
    public State()
    {
        sts.Instinct = new float[5];
        sts.Insight = new float[5];
        sts.Communication = new float[5];
        sts.Oppression = new float[5];
        GetInfo();
    }
    public static string GetMessage(float r)
    {
        if (r <= 120)
            return "��";
        if (r <= 240)
            return "һ��";
        return "��";
    }
    public static string Find()
    {
        string ret = "";
        float CountInstinct = 0;
        for (int i = 0; i < 5; i++)
            CountInstinct += sts.Instinct[i];
        ret += "���ܣ�" + GetMessage(CountInstinct) +"\n";

        float CountInsight = 0;
        for (int i = 0; i < 5; i++)
            CountInsight += sts.Insight[i];
        ret += "���죺" + GetMessage(CountInsight) + "\n";

        float CountCommunication = 0;
        for (int i = 0; i < 5; i++)
            CountCommunication += sts.Communication[i];
        ret += "��ͨ��" + GetMessage(CountCommunication) + "\n";

        float CountOppression = 0;
        for (int i = 0; i < 5; i++)
            CountOppression += sts.Oppression[i];
        ret += "ѹ�ȣ�" + GetMessage(CountOppression);

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
            Api.Private("635691684", "д��ʧ��");
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
            Api.Private("635691684", "��ʼ��ʧ��");
        }
    }
}
