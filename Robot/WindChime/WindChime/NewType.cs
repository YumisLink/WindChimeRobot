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
            Api.Private("635691684", "主人！NewType类初始化出现问题，系统已经关闭，请你重启！");
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
        message = message.Replace("F艾特", Api.GetAtMessage(id));
        message = message.Replace("F名字", name);
        return message;
    }
    public static void main(string id,string name,string message)
    {
        Answer(id, name, message);
        Delete(id, message);
        Question(id, message);
        if (message == "关于学习")
        {
            string str = "手机下载中国大学mooc，搜索翁恺，找到《C语言课程设计CAP》并学习；或者电脑网页登录也行。 ";
            str += "\n网址：https://www.icourse163.org/course/ZJU-1001614008?tid=1206448231";
            str += "\n根据学到的知识在pta（网址：https://pintia.cn/）上刷题，邀请码是：b4961a84d992e899";
            str += "\n（使用方法，登入pta后，进入个人中心，把邀请码填入应邀做题的部分，昵称记得也实名一下）选择题目集“集美大学 - 浙大版《C语言程序设计实验与习题指导（第3版）》-21级”（刚开始一定要选择编程题而不是函数题）";
            str += "\n https://pintia.cn/problem-sets/1415325144039194624";
            str += "\n刷题是一个巩固学习成果的好方法\n关于学习C语言的一些建议\n https://blog.csdn.net/qq_43058685/article/details/108162939";
            str += "\n关于PTA的使用方法以及常见错误的解\n https://blog.csdn.net/qq_43058685/article/details/108164798";
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

        if (!str[0].Contains("加入问答"))
            return ;
        if (!str[0].Contains("Vague") && !str[0].Contains("Static"))
        {
            Api.Group(Group_id, "格式：加入问答 [Vague,Static]\n问：[]\n答：[]\n（要求回答内容不会换行");
            return ;
        }
        if (!str[1].Contains("问："))
        {
            Api.Group(Group_id, "格式：加入问答 [Vague,Static]\n问：[]\n答：[]\n（要求回答内容不会换行");
            return ;
        }
        if (!message.Contains("答："))
        {
            Api.Group(Group_id, "格式：加入问答 [Vague,Static]\n问：[]\n答：[]\n（要求回答内容不会换行");
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
                Api.Group(Group_id, "DeleteVague 词汇\nDeleteStatic 词汇");
            return ;
        }
        if (!message.Contains("DeleteVague") && !message.Contains("DeleteStatic"))
            return ;
        string[] str = message.Split(" ");
        if (str.Length > 2)
            Api.Group(Group_id, "DeleteVague 词汇\nDeleteStatic 词汇");

        if (message.Contains("DeleteVague"))
        {
            try
            {
                if (replyVague.ContainsKey(str[1]))
                {
                    replyVague.Remove(str[1]);
                    Api.Group(Group_id, "删除成功！");
                }
            }
            catch
            {
                Api.Group(Group_id, "不存在 " + str[1] + " 的触发词");
            }
        }
        if (message.Contains("DeleteStatic"))
        {
            try
            {
                if (replyStatic.ContainsKey(str[1]))
                {
                    replyStatic.Remove(str[1]);
                    Api.Group(Group_id, "删除成功！");
                }
            }
            catch
            {
                Api.Group(Group_id, "不存在 " + str[1] + " 的触发词");
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