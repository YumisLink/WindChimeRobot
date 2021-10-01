using System.Collections.Generic;
using System.IO;
using System;

public class Gezi
{
    static List<string> list = new List<string>();
    public static bool main(string user_id, string message)
    {
        list.Clear();
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
                        list.Add(str[1]);
                    else
                        list.Add("请在输入一次，如果依旧是这句话，请联系妈妈！");
                }
                Api.Private("981536105", "新手礼包");
                Api.Private("981536105", list[0]);
            }
            StreamWriter sw = new StreamWriter("gugugu/New.txt");
            for (int i = 1; i < list.Count; i++)
            {
                sw.WriteLine("新手礼包 " + list[i]);
            }
            if (list.Count <= 10)
            {
                Api.Private("981536105", "新手礼包剩下：" + list.Count + "个，请尽快联系妈妈增加！");
            }
            list.Clear();
            return true;
        }
        if (message.Contains("满级礼包"))
        {
            list.Clear();
            using (StreamReader sr = new StreamReader("gugugu/Max.txt"))
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
                Api.Private("981536105", "满级礼包");
                Api.Private("981536105", list[0]);
            }
            StreamWriter sw = new StreamWriter("gugugu/Max.txt");
            for (int i = 1; i < list.Count; i++)
            {
                sw.WriteLine("满级礼包 " + list[i]);
            }
            if (list.Count <= 10)
            {
                Api.Private("981536105", "满级礼包剩下："+list.Count+"个，请尽快联系妈妈增加！");
            }
            list.Clear();
            return true;
        }
        if (message.Contains("推广礼包"))
        {
            list.Clear();
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
                Api.Private("981536105", "推广礼包");
                Api.Private("981536105", list[0]);
            }
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