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
        if (message.Contains("�������"))
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
                        list.Add("��������һ�Σ������������仰������ϵ���裡");
                }
                Api.Private("981536105", "�������");
                Api.Private("981536105", list[0]);
            }
            StreamWriter sw = new StreamWriter("gugugu/New.txt");
            for (int i = 1; i < list.Count; i++)
            {
                sw.WriteLine("������� " + list[i]);
            }
            if (list.Count <= 10)
            {
                Api.Private("981536105", "�������ʣ�£�" + list.Count + "�����뾡����ϵ�������ӣ�");
            }
            list.Clear();
            return true;
        }
        if (message.Contains("�������"))
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
                        list.Add("��������һ�Σ������������仰������ϵ���裡");
                }
                Api.Private("981536105", "�������");
                Api.Private("981536105", list[0]);
            }
            StreamWriter sw = new StreamWriter("gugugu/Max.txt");
            for (int i = 1; i < list.Count; i++)
            {
                sw.WriteLine("������� " + list[i]);
            }
            if (list.Count <= 10)
            {
                Api.Private("981536105", "�������ʣ�£�"+list.Count+"�����뾡����ϵ�������ӣ�");
            }
            list.Clear();
            return true;
        }
        if (message.Contains("�ƹ����"))
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
                        list.Add("��������һ�Σ������������仰������ϵ���裡");
                }
                Api.Private("981536105", "�ƹ����");
                Api.Private("981536105", list[0]);
            }
            StreamWriter sw = new StreamWriter("gugugu/Tui.txt");
            for (int i = 1; i < list.Count; i++)
            {
                sw.WriteLine("�ƹ���� " + list[i]);
            }
            if (list.Count <= 10)
            {
                Api.Private("981536105", "�ƹ����ʣ�£�" + list.Count + "�����뾡����ϵ�������ӣ�");
            }
            list.Clear();
            return true;
        }
        return true;
    }
}