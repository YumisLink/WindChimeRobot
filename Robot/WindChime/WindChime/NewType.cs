using System;
using System.Collections.Generic;
using System.IO;

public class NewType
{
    public static List<string> Controller = new List<string>();
    public static Dictionary<string, string> replyStatic = new Dictionary<string, string>();
    public static Dictionary<string, string> replyVague = new Dictionary<string, string>();
    public static string Group_id = "372502869";
    public NewType()
    {
        Controller.Add("1121429190");
        Controller.Add("315310152");
        try
        {
            using (StreamReader sr = new StreamReader("NewType/MessageVague.dat"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] ss = str.Split(" ");
                    string rs = ss[1];
                    for (int i = 2; i < ss.Length; i++)
                        rs += ss[i];
                    replyVague.Add(ss[0], rs);
                }
            }
       Console.WriteLine("abc");
            using (StreamReader sr = new StreamReader("NewType/MessageStatic.dat"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] ss = str.Split(" ");
                    string rs = ss[1];
                    for (int i = 2; i < ss.Length; i++)
                        rs += ss[i];
                    replyStatic.Add(ss[0], rs);
                }
            }
            Console.WriteLine("cs");
            using (StreamReader sr = new StreamReader("NewType/Controller.dat"))
            {
                string str;
                    while ((str = sr.ReadLine()) != null)
                        Controller.Add(str);
            }
        }
        catch
        {
            Api.Private("635691684", "���ˣ�NewType���ʼ���������⣬ϵͳ�Ѿ��رգ�����������");
        }
        foreach (var i in replyStatic)
            Console.WriteLine(i.Key + "   " + i.Value + "\n");

    }
    public static void Save()
    {
        using (StreamWriter sr = new StreamWriter("NewTpye/MessageStatic.dat"))
            foreach (var i in replyStatic)
                sr.WriteLine(i.Key + " " + i.Value);
        using (StreamWriter sr = new StreamWriter("NewTpye/MessageVague.dat"))
            foreach (var i in replyVague)
                sr.WriteLine(i.Key + " " + i.Value);
    }
    public static string GetNewString(string id,string name,string message)
    {
        message = message.Replace("F����", Api.GetAtMessage(id));
        message = message.Replace("F����", name);
        return message;
    }
    public static void main(string id,string name,string message)
    {
        Answer(id, name, message);
        Delete(id, message);
        Question(id, message);
        if (message == "����ѧϰ")
        {
            string str = "�ֻ������й���ѧmooc�������������ҵ���C���Կγ����CAP����ѧϰ�����ߵ�����ҳ��¼Ҳ�С� ";
            str += "\n��ַ��https://www.icourse163.org/course/ZJU-1001614008?tid=1206448231";
            str += "\n����ѧ����֪ʶ��pta����ַ��https://pintia.cn/����ˢ�⣬�������ǣ�b4961a84d992e899";
            str += "\n��ʹ�÷���������pta�󣬽���������ģ�������������Ӧ������Ĳ��֣��ǳƼǵ�Ҳʵ��һ�£�ѡ����Ŀ����������ѧ - ���桶C���Գ������ʵ����ϰ��ָ������3�棩��-21�������տ�ʼһ��Ҫѡ����������Ǻ����⣩";
            str += "\n https://pintia.cn/problem-sets/1415325144039194624";
            str += "\nˢ����һ������ѧϰ�ɹ��ĺ÷���\n����ѧϰC���Ե�һЩ����\n https://blog.csdn.net/qq_43058685/article/details/108162939";
            str += "\n����PTA��ʹ�÷����Լ���������Ľ�\n https://blog.csdn.net/qq_43058685/article/details/108164798";
            Api.Group(Group_id, str);
        }
    }
    public static void Question(string id, string message)
    {
        bool a = false;
        foreach (var i in Controller)
            if (i == id)
                a = true;
        if (!a)
            return;
        string[] str;
        if (message.Contains("\r\n"))
            str = message.Split("\r\n");
        else
            str = message.Split("\n");

        if (!str[0].Contains("�����ʴ�"))
            return ;
        if (!str[0].Contains("Vague") && !str[0].Contains("Static"))
        {
            Api.Group(Group_id, "��ʽ�������ʴ� [Vague,Static]\n�ʣ�[]\n��[]\n��Ҫ��ش����ݲ��ỻ��");
            return ;
        }
        if (!str[1].Contains("�ʣ�"))
        {
            Api.Group(Group_id, "��ʽ�������ʴ� [Vague,Static]\n�ʣ�[]\n��[]\n��Ҫ��ش����ݲ��ỻ��");
            return ;
        }
        if (!message.Contains("��"))
        {
            Api.Group(Group_id, "��ʽ�������ʴ� [Vague,Static]\n�ʣ�[]\n��[]\n��Ҫ��ش����ݲ��ỻ��");
            return ;
        }
        if (str[0].Contains("Vague"))
            replyVague.Add(str[1], str[2]);
        if (str[0].Contains("Static"))
            replyStatic.Add(str[1], str[2]);
        Save();
    }
    public static void Delete(string id, string message)
    {
        bool a = false;
        foreach (var i in Controller)
            if (i == id)
                a = true;
        if (!a)
            return;
        if (message == "Delete")
        {
            if (!message.Contains("DeleteVague") && !message.Contains("DeleteStatic"))
                Api.Group(Group_id, "DeleteVague �ʻ�\nDeleteStatic �ʻ�");
            return ;
        }
        if (!message.Contains("DeleteVague") && !message.Contains("DeleteStatic"))
            return ;
        string[] str = message.Split(" ");
        if (str.Length > 2)
            Api.Group(Group_id, "DeleteVague �ʻ�\nDeleteStatic �ʻ�");

        if (message.Contains("DeleteVague"))
        {
            try
            {
                if (replyVague.ContainsKey(str[1]))
                {
                    replyVague.Remove(str[1]);
                    Api.Group(Group_id, "ɾ���ɹ���");
                }
            }
            catch
            {
                Api.Group(Group_id, "������ " + str[1] + " �Ĵ�����");
            }
        }
        if (message.Contains("DeleteStatic"))
        {
            try
            {
                if (replyStatic.ContainsKey(str[1]))
                {
                    replyStatic.Remove(str[1]);
                    Api.Group(Group_id, "ɾ���ɹ���");
                }
            }
            catch
            {
                Api.Group(Group_id, "������ " + str[1] + " �Ĵ�����");
            }
        }
        Save();
    }
    public static void Answer(string id, string name, string message)
    {
        foreach (var i in replyStatic)
        {
            if (message == i.Key)
            {
                //Api.Group(Group_id, GetNewString(id,name, i.Value))
                Api.Group(Group_id, i.Value);
                return ;
            }
        }
        foreach (var i in replyVague)
        {
            if (message.Contains(i.Key))
            {
                Api.Group(Group_id, GetNewString(id, name, i.Value));
                return ;
            }
        }
    }
}