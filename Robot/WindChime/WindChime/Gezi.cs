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
        if (message.Contains("�������"))
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
                        newhand.Add("��������һ�Σ������������仰������ϵ���裡");
                }
            }
            using (StreamWriter sw = new StreamWriter("gugugu/New.txt"))
            {
                for (int i = 1; i < newhand.Count; i++)
                {
                    sw.WriteLine("������� " + newhand[i]);
                }
            }
            if (newhand.Count > 1)
            {
                Api.Private("981536105", "�������");
                Api.Private("981536105", newhand[0]);
            }
            else
            {
                Api.Private("981536105", "�������û��");
            }
            if (newhand.Count <= 10)
            {
                Api.Private("981536105", "�������ʣ�£�" + newhand.Count + "�����뾡����ϵ�������ӣ�");
            }
            newhand.Clear();
        }
        else if (message.Contains("�������"))
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
                        maxhand.Add("��������һ�Σ������������仰������ϵ���裡");
                }
            }
            using (StreamWriter sw = new StreamWriter("gugugu/Max.txt"))
            {
                for (int i = 1; i < maxhand.Count; i++)
                {
                    sw.WriteLine("������� " + maxhand[i]);
                }
            }
            if(maxhand.Count > 1)
            {
                Api.Private("981536105", "�������");
                Api.Private("981536105", maxhand[0]);
            }
            else
            {
                Api.Private("981536105", "�������û��");
            }
            if (maxhand.Count <= 10)
            {
                Api.Private("981536105", "�������ʣ�£�" + maxhand.Count + "�����뾡����ϵ�������ӣ�");
            }
            maxhand.Clear();
        }
        else if (message.Contains("�ƹ����"))
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
                        list.Add("��������һ�Σ������������仰������ϵ���裡");
                }
            }
            Api.Private("981536105", "�ƹ����");
            Api.Private("981536105", list[0]);
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