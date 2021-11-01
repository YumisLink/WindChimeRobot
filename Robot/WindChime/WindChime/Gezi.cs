using System.Collections.Generic;
using System.IO;
using System;

public class Gezi
{
    static List<string> list = new List<string>();
    static List<string> newhand = new List<string>();
    static List<string> maxhand = new List<string>();
    public static bool main(string user_id, string message)
    {
        if (user_id != "981536105" && user_id != "635691684")
            return false;
        if (message.Contains("新手礼包"))
        {
            using (StreamReader sr = new StreamReader("gugugu/New.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] str = line.Split(" ");
                    if (str.Length >= 2)
                        newhand.Add(str[1]);
                    else
                        newhand.Add("请在输入一次，如果依旧是这句话，请联系妈妈！");
                }
            }
            using (StreamWriter sw = new StreamWriter("gugugu/New.txt"))
            {
                for (int i = 1; i < newhand.Count; i++)
                {
                    sw.WriteLine("新手礼包 " + newhand[i]);
                }
            }
            if (newhand.Count > 1)
            {
                Api.Private("981536105", "新手礼包");
                Api.Private("981536105", newhand[0]);
            }
            else
            {
                Api.Private("981536105", "新手礼包没了");
            }
            if (newhand.Count <= 10)
            {
                Api.Private("981536105", "新手礼包剩下：" + newhand.Count + "个，请尽快联系妈妈增加！");
            }
            newhand.Clear();
        }
        else if (message.Contains("满级礼包"))
        {
            using (StreamReader sr = new StreamReader("gugugu/Max.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] str = line.Split(" ");
                    if (str.Length >= 2)
                        maxhand.Add(str[1]);
                    else
                        maxhand.Add("请在输入一次，如果依旧是这句话，请联系妈妈！");
                }
            }
            using (StreamWriter sw = new StreamWriter("gugugu/Max.txt"))
            {
                for (int i = 1; i < maxhand.Count; i++)
                {
                    sw.WriteLine("满级礼包 " + maxhand[i]);
                }
            }
            if(maxhand.Count > 1)
            {
                Api.Private("981536105", "满级礼包");
                Api.Private("981536105", maxhand[0]);
            }
            else
            {
                Api.Private("981536105", "满级礼包没了");
            }
            if (maxhand.Count <= 10)
            {
                Api.Private("981536105", "满级礼包剩下：" + maxhand.Count + "个，请尽快联系妈妈增加！");
            }
            maxhand.Clear();
        }
        else if (message.Contains("推广礼包"))
        {
            using (StreamReader sr = new StreamReader("gugugu/Tui.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] str = line.Split(" ");
                    if (str.Length >= 2)
                        list.Add(str[1]);
                    else
                        list.Add("请在输入一次，如果依旧是这句话，请联系妈妈！");
                }
            }
            Api.Private("981536105", "推广礼包");
            Api.Private("981536105", list[0]);
            StreamWriter sw = new StreamWriter("gugugu/Tui.txt");
            for (int i = 1; i < list.Count; i++)
            {
                sw.WriteLine("推广礼包 " + list[i]);
            }
            if (list.Count <= 10)
            {
                Api.Private("981536105", "推广礼包剩下：" + list.Count + "个，请尽快联系妈妈增加！");
            }
            list.Clear();
            return true;
        }
        return true;
    }
}