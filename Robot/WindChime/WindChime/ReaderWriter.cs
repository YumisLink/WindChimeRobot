using System;
using System.Collections.Generic;
using System.IO;
public struct UserInfo
{
    public bool CanGet;
    /// <summary>
    /// 好感度
    /// </summary>
    public int heart;
    /// <summary>
    /// 金币
    /// </summary>
    public int money;
    /// <summary>
    /// 签到日期
    /// </summary>
    public int HeartTime;
    /// <summary>
    /// 工作日期
    /// </summary>
    public int WorkTime;
    /// <summary>
    /// QQ号
    /// </summary>
    public string id;
    /// <summary>
    /// 增加词汇的次数
    /// </summary>
    public int AddTimes;
    /// <summary>
    /// 是否初始化过
    /// </summary>
    public int Inited;

    /// <summary>
    /// 勇气
    /// </summary>
    public int Courage;
    /// <summary>
    /// 谨慎
    /// </summary>
    public int Cautious;
    /// <summary>
    /// 自律
    /// </summary>
    public int Discipline;
    /// <summary>
    /// 正义
    /// </summary>
    public int Justice;
    /// <summary>
    /// 工作计数
    /// </summary>
    public int WorkCount;
};

public class ReaderWriter
{
    Dictionary<string, string> Rec = new Dictionary<string, string>();
    Dictionary<string, string> Tip = new Dictionary<string, string>();
    Dictionary<string, bool> IgnoreList = new Dictionary<string, bool>();
    public static Random random = new Random();
    public static UserInfo GetUserInfo(string user_id)
    {
        UserInfo ret;
        ret.id = user_id;
        ret.heart = 0;
        ret.money = 0;
        ret.WorkTime = 0;
        ret.HeartTime = 0;
        ret.AddTimes = 0;
        ret.CanGet = false;
        ret.Inited = 0;
        ret.Courage = 0;
        ret.Cautious = 0;
        ret.Discipline = 0;
        ret.Justice = 0;
        ret.WorkCount = 2;
        try
        {
            using (StreamReader sr = new StreamReader(user_id + ".dat"))
            {
                ret.CanGet = true;
                string line;
                int cnt = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    if (cnt == 1)
                        ret.HeartTime = Convert.ToInt32(line);
                    if (cnt == 2)
                        ret.heart = Convert.ToInt32(line);
                    if (cnt == 3)
                        ret.money = Convert.ToInt32(line);
                    if (cnt == 4)
                        ret.WorkTime = Convert.ToInt32(line);
                    if (cnt == 5)
                        ret.Inited = Convert.ToInt32(line);
                    if (cnt == 6)
                        ret.Courage = Convert.ToInt32(line);
                    if (cnt == 7)
                        ret.Cautious = Convert.ToInt32(line);
                    if (cnt == 8)
                        ret.Discipline = Convert.ToInt32(line);
                    if (cnt == 9)
                        ret.Justice = Convert.ToInt32(line);
                    if (cnt == 10)
                        ret.WorkCount = Convert.ToInt32(line);
                    cnt++;
                }
            }
        }
        catch
        {
            return ret;
        }
        if (ret.WorkTime != DateTime.Now.Day)
        {
            ret.WorkTime = DateTime.Now.Day;
            ret.WorkCount = 2;
        }
        if (ret.Inited == 0)
        {
            int all = 0;
            while (all > 110 || all <= 90)
            {
                ret.Cautious = random.Next(0, 50);
                ret.Courage = random.Next(0, 50);
                ret.Discipline = random.Next(0, 50);
                ret.Justice = random.Next(0, 50);
                all = ret.Cautious + ret.Courage + ret.Discipline + ret.Justice;
                ret.Inited = 1;
            }
            WriteToFile(ret);
        }
        return ret;
    }
    public static void WriteToFile(UserInfo user)
    {
        using (StreamWriter sw = new StreamWriter(user.id + ".dat"))
        {
            sw.WriteLine(user.HeartTime);
            sw.WriteLine(user.heart);
            sw.WriteLine(user.money);
            sw.WriteLine(user.WorkTime);
            sw.WriteLine(user.Inited);
            sw.WriteLine(user.Courage);
            sw.WriteLine(user.Cautious);
            sw.WriteLine(user.Discipline);
            sw.WriteLine(user.Justice);
            sw.WriteLine(user.WorkCount);
        }
    }
    public ReaderWriter()
    {
        try
        {
            using (StreamReader sr = new StreamReader("MessageVague.dat"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] ss = str.Split(" ");
                    string rs = ss[1];
                    for (int i = 2; i < ss.Length; i++)
                        rs += ss[i];
                    Rec.Add(ss[0], rs);
                }
            }
            using (StreamReader sr = new StreamReader("MessageStatic.dat"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] ss = str.Split(" ");
                    string rs = ss[1];
                    for (int i = 2; i < ss.Length; i++)
                        rs += ss[i];
                    Tip.Add(ss[0], rs);
                }
            }
            using (StreamReader sr = new StreamReader("IgnoreStatic.dat"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    IgnoreList.Add(str, true);
                }
            }
        }
        catch
        {
            Api.Private("635691684", "主人！ReaderWrite类初始化出现问题，系统已经关闭，请你重启！");
            using (StreamReader sr = new StreamReader("message.dat")) ;
        }
    }


    public static bool Main(string group_id, string user_id, string name, string message)
    {
        Change(group_id, user_id, message);

        if (message == "查询")
        {
            Find(group_id, user_id, name);
            return true;
        }
        else if (message == "摸摸风铃")
        {
            Touch(group_id, user_id, name);
            return true;
        }
        else if ((message.Contains("工作") && message.Length <= 5) || (message.Contains("打工") && message.Length <= 5))
        {
            Work(group_id, user_id, name, message);
            return true;
        }
        else if (message == "风铃眼熟我")
        {
            Remember(group_id, user_id, name);
            return true;
        }
        else if (message == "初始化属性")
        {
            Init(user_id);
            return true;
        }
        else if (message == "重新开始这一天")
        {
            Restart(group_id, user_id, name);
            return true;
        }
        else return false;
    }
    public static void Touch(string group_id, string user_id, string name)
    {
        UserInfo User = GetUserInfo(user_id);
        if (User.CanGet)
        {
            if (DateTime.Now.Day != User.HeartTime)
            {
                Random rd = new Random();
                int k = rd.Next(2, 5);
                if (user_id == "3534417975")
                    k += 5;
                User.heart += k;
                if (User.heart >= 200)
                {
                    User.heart = 200;
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "可以抱抱风铃吗！风铃今天超级努力的！（伸手！）今天也要好好加油哦！\n好感度已满当前好感度:200");
                }
                User.HeartTime = DateTime.Now.Day;
                if (User.heart >= 100 && User.heart < 200)
                    Api.Group(group_id, "（乖巧的被摸摸）\n谢谢！" + Api.GetAtMessage(user_id) + "风铃今天会依旧元气满满的！希望你也是哦！\n好感度上升:" + k + "\n当前好感度:" + User.heart);
                if (User.heart >= 50 && User.heart < 100)
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "偶尔让你摸一下头也不是不行...总之今天不要不高兴了！\n好感度上升:" + k + "\n当前好感度:" + User.heart);
                if (User.heart >= 0 && User.heart < 50)
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "怎么就突然摸我的头了....被吓了一跳呢...\n好感度上升:" + k + "\n当前好感度:" + User.heart);
                if (user_id == "3534417975")
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "主人说，要对刀茶好一点...!");


            }
            else
            {
                Random rd = new Random();
                if (user_id == "3534417975" && rd.Next(0, 5) == 0)
                {
                    User.heart += 1;
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "主人说，要对刀茶好一点...!所以刀茶摸摸并不会讨厌的啦！好感度上升:1\n当前好感度:" + User.heart);
                }
                int k = 0;
                if (User.heart >= 0 && User.heart < 50)
                    k = 0;
                if (User.heart >= 50 && User.heart < 100)
                    k = rd.Next(0, 3);
                if (User.heart >= 100 && User.heart < 200)
                    k = rd.Next(2, 5);
                if (User.heart >= 200)
                    k = rd.Next(3, 8);
                if (k == 0)
                    Api.Group(group_id, "好啦，不要在摸头了，今天还有很多事情要做呢...");
                if (k == 1)
                    Api.Group(group_id, "呀！" + name + "别吓我啊！");
                if (k == 2)
                    Api.Group(group_id, "风铃已经不是小孩子了！已经摸过了就不要在摸了......呜呜");
                if (k == 3)
                    Api.Group(group_id, "如果还想摸头的话，那就允许你再摸一下吧！");
                if (k == 4)
                    Api.Group(group_id, "（乖巧的被摸摸）\n谢谢！风铃今天会依旧元气满满的！希望你也是哦");
                if (k == 5)
                    Api.Group(group_id, "那就让你再摸一次，最后一次！");
                if (k == 6)
                    Api.Group(group_id, "呜————只是这样摸一下吗，不在多搓一下我吗————");
                if (k == 7)
                    Api.Group(group_id, "风铃今天超努力的在工作了，可以稍微抱一下我吗，只要能在你的怀里稍微休息一下——————");
            }
        }
        else
        {
            Api.Group(group_id, name + "是谁？（歪头）为什么风铃一点印象都没有？\nps:通过输入\"风铃眼熟我\"来加入测试哦！");
            return;
        }
        WriteToFile(User);
    }
    public static int GetLv(int lv)
    {
        if (lv <= 20)
            return 1;
        if (lv <= 30)
            return 2;
        if (lv <= 50)
            return 3;
        if (lv <= 70)
            return 4;
        return 5;
    }
    public static int GetWorkResults(int count , float Probability)
    {
        int ret = 0;
        for (int i = 1; i <= count; i++)
        {
            if (random.NextDouble() < Probability)
                ret++;
        }
        return ret;
    }
    public static void PrintWorkResult(string group_id,string result,int succeed,string box,string type,int NowMoney,int NowTag,int addons,int WorkCount)
    {
        string message = "【" + box + "】\n本次工作结果：" + result + "\n获得金币：" + addons + "\n当前金币："+ NowMoney;
        message += "\n";
        if (result == "优")
            message += (type + "上升：5\n当前" + type + "：" + NowTag);
        if (result == "良")
            message += (type + "上升：3\n当前" + type + "：" + NowTag);
        if (result == "差")
            message += (type + "上升：1\n当前" + type + "：" + NowTag);
        message += "\n剩余工作次数:" + WorkCount;
        Api.Group(group_id, message);
    }
    public static int AddExcellent = 5;
    public static int AddGood = 3;
    public static int AddBad = 1;
    public static void Work(string group_id, string user_id, string name, string message)
    {
        UserInfo User = GetUserInfo(user_id);
        if (User.CanGet)
        {
            if (User.WorkCount > 0)
            {
                User.WorkTime = DateTime.Now.Day;
                if (message.Contains("本能"))
                {
                    User.WorkCount--;
                    int addons;
                    int lv = GetLv(User.Courage);
                    float target = State.sts.Instinct[lv - 1];
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target * 0.01f);
                    string box = "";
                    int good = succeed;
                    for (int i = 1; i <= count; i++)
                        if (good > 0)
                        {
                            good--;
                            box += "√";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Courage += AddExcellent;
                        addons = succeed * 15;
                        result = "优";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Courage += AddGood;
                        addons = succeed * 8;
                        result = "良";
                    }
                    else
                    {
                        User.Courage += AddBad;
                        addons = succeed * 3;
                        result = "差";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "勇气", User.money, User.Courage, addons, User.WorkCount);
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("洞察"))
                {
                    User.WorkCount--;
                    int addons;
                    int lv = GetLv(User.Cautious);
                    float target = State.sts.Insight[lv - 1];
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target * 0.01f);
                    string box = "";
                    int good = succeed;
                    for (int i = 1; i <= count; i++)
                        if (good > 0)
                        {
                            good--;
                            box += "√";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Cautious += AddExcellent;
                        addons = succeed * 15;
                        result = "优";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Cautious += AddGood;
                        addons = succeed * 8;
                        result = "良";
                    }
                    else
                    {
                        User.Cautious += AddBad;
                        addons = succeed * 3;
                        result = "差";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "谨慎", User.money, User.Cautious, addons, User.WorkCount);
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("沟通"))
                {
                    User.WorkCount--;
                    int addons;
                    int lv = GetLv(User.Discipline);
                    float target = State.sts.Communication[lv - 1];
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target * 0.01f);
                    string box = "";
                    int good = succeed;
                    for (int i = 1; i <= count; i++)
                        if (good > 0)
                        {
                            good--;
                            box += "√";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Discipline += AddExcellent;
                        addons = succeed * 15;
                        result = "优";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Discipline += AddGood;
                        addons = succeed * 8;
                        result = "良";
                    }
                    else
                    {
                        User.Discipline += AddBad;
                        addons = succeed * 3;
                        result = "差";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "自律", User.money, User.Discipline, addons, User.WorkCount);
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("压迫"))
                {
                    User.WorkCount--;
                    int addons;
                    int lv = GetLv(User.Justice);
                    float target = State.sts.Oppression[lv - 1];
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target * 0.01f);
                    string box = "";
                    int good = succeed;
                    for (int i = 1; i <= count; i++)
                        if (good > 0)
                        {
                            good--;
                            box += "√";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Justice += AddExcellent;
                        addons = succeed * 15;
                        result = "优";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Justice += AddGood;
                        addons = succeed * 8;
                        result = "良";
                    }
                    else
                    {
                        User.Justice += AddBad;
                        addons = succeed * 3;
                        result = "差";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "正义", User.money, User.Justice, addons,User.WorkCount);
                    WriteToFile(User);
                    return;
                }
                Api.Group(group_id,"工作需要在工作后加上“本能” 或 “洞察” 或 “沟通” 或 “压迫” \n分别对应博士的4个属性 ：“勇气”、“谨慎”、“自律”和“正义”\nps.新版的工作之前，请记得先使用“初始化属性”来获取你的初始属性，另外风铃的日常行为可以通过“查询风铃状态”来获取");
                return;
            }
            else
            {
                if (User.heart >= 200)
                {
                    User.money += 1;
                    Api.Group(group_id, "今天已经很努力了，不要再强迫自己工作了啦，风铃偷偷分给你一枚金币啦。嘘---不要跟别人说哦！\n当前金币: " + User.money);
                }else
                    Api.Group(group_id, "警告:SEPHIRAH核心崩溃导致逆卡巴拉能量实体化，需要立即让SEPHIRAH自己抑制自己的核心。（翻译：已经工作过2次了不能在做工了。");
            }

        }
        else
        {
            Api.Group(group_id, name + "是谁？为什么我一点印象都没有？\nps:通过输入\"风铃眼熟我\"来加入测试哦！");
            return;
        }
        WriteToFile(User);
    }
    /// <summary>
    /// 管理员权限，修改好感度以及金币
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="message"></param>
    public static void Change(string group_id, string user_id, string message)
    {
        if (user_id != "635691684")
            return;
        if (!message.Contains("Set"))
            return;
        string[] str = message.Split(" ");
        UserInfo User = GetUserInfo(str[1]);
        if (message.Contains("SetMoney"))
        {
            User.money = Convert.ToInt32(str[2]);
            if (str[1] != "635691684" && str[1] != "3534417975")
                Api.Group(group_id, "！我的钱！主人你不许拿走我的钱！至少..也是给你自己");
            else
                Api.Group(group_id, (str[1] == "635691684" ? "主人" : "刀茶") + "要好好的，养活自己！------");

        }
        if (message.Contains("SetHeart"))
        {
            int before = User.heart;
            User.heart = Convert.ToInt32(str[2]);
            if (User.heart > 200)
            {
                Api.Group(group_id, "好感度不能超过200的哦！所以风铃帮主人改成200！(笑)");
                User.heart = 200;
            }
            else
            {
                if (before < User.heart)
                    Api.Group(group_id, "啊...突然想让风铃去喜欢一个人吗...风铃会努力的");
                if (before > User.heart)
                    if (str[1] != "635691684" && str[1] != "3534417975")
                    {
                        Api.Group(group_id, "为什么要让我去讨厌她......");
                    }
                    else
                    {
                        Api.Group(group_id,  "不要！才不要讨厌"+ (str[1] == "635691684" ? "你" : "刀茶") + "呢！");

                        User.heart = 200;
                    }

            }
        }
        WriteToFile(User);
    }
    /// <summary>
    /// 删除词汇
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="message"></param>
    public bool Delete(string group_id, string user_id, string message)
    {
        if (message == "Delete")
        {
            if (!message.Contains("DeleteVague") && !message.Contains("DeleteStatic"))
                Api.Group(group_id, "请参考以下两种语法\nDeleteVague 词汇(删除包含触发的词汇\nDeleteStatic 词汇(删除完全匹配触发的词汇");
            return true;
        }
        if (!message.Contains("DeleteVague") && !message.Contains("DeleteStatic"))
            return false;
        string[] str = message.Split(" ");
        if (str.Length > 2)
        {
            Api.Group(group_id, "格式不正确，请参考以下两种语法\nDeleteVague 词汇(删除包含触发的词汇\nDeleteStatic 词汇(删除完全匹配触发的词汇");
        }
        UserInfo user = GetUserInfo(user_id);

        if (message.Contains("DeleteVague"))
        {
            if (user.money < 500)
            {
                Api.Group(group_id, "没钱，就只能回家！\n当前金币:" + user.money + "\n需要金币：500");
                return true;
            }
            try
            {
                if (Rec.ContainsKey(str[1]))
                {
                    Rec.Remove(str[1]);
                    user.money -= 500;
                    Api.Group(group_id, "删除成功！\n本次花费：500金币\n剩余金币：" + user.money);
                }
            }
            catch
            {
                Api.Group(group_id, "不存在 " + str[1] + " 的触发词");
            }
        }
        if (message.Contains("DeleteStatic"))
        {
            if (user.money < 100)
            {
                Api.Group(group_id, "没钱，就只能回家！\n当前金币:" + user.money + "\n需要金币：100");
                return true;
            }
            try
            {
                if (Tip.ContainsKey(str[1]))
                {
                    Tip.Remove(str[1]);
                    user.money -= 100;
                    Api.Group(group_id, "删除成功！\n本次花费：100金币\n剩余金币：" + user.money);
                }
            }
            catch
            {
                Api.Group(group_id, "不存在 " + str[1] + " 的触发词");
            }
        }
        WriteToFile(user);
        return true;
    }
    /// <summary>
    /// 调用以忽略某人
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="message"></param>
    public void Ignore(string group_id, string user_id, string message)
    {
        if (user_id != "635691684")
            return;
        if (!message.Contains("Ignore"))
            return;
        string[] str = message.Split(" ");
        try
        {
            IgnoreList.Add(str[1], true);
            Api.Group(group_id, "我会尽量不去回复" + str[1] + "的消息的！");
        }
        catch
        {
            Api.Group(group_id, str[1] + "已经加入过了。");
        }
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    public static void Find(string group_id, string user_id, string name)
    {
        UserInfo user = GetUserInfo(user_id);
        if (user.CanGet)
        {
            string c1 = "\n勇气：" + user.Courage + "(" + GetLv(user.Courage) + ")";
            string c2 = "\n谨慎：" + user.Cautious + "(" + GetLv(user.Cautious) + ")";
            string c3 = "\n自律：" + user.Discipline + "(" + GetLv(user.Discipline) + ")";
            string c4 = "\n正义：" + user.Justice + "(" + GetLv(user.Justice) + ")";
            Api.Group(group_id, name + "当前好感度：" + user.heart + "\n当前金币为：" + user.money + c1 + c2 +c3 +c4) ;
        }
        else
        {
            Api.Group(group_id, name + "是谁？为什么我一点印象都没有？\nps:通过输入\"风铃眼熟我\"来加入测试哦！");
            return;
        }
    }
    /// <summary>
    /// 重新开始这一天
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    public static void Restart(string group_id, string user_id, string name)
    {
        UserInfo user = GetUserInfo(user_id);
        if( user.money >= 500)
        {
            user.money -= 500;
            user.WorkTime = 0;
            Api.Group(group_id, "博士你醒了~今天是" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日哦！至于你的钱包为什么少了500金币，我也不是很清楚~");
        }
        else
        {
            Api.Group(group_id, "错误，执行TT2协议需要500金币。");
        }
        WriteToFile(user);
    }
    /// <summary>
    /// 初始化任务属性
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    public static void Init(string user_id)
    {
        UserInfo user = GetUserInfo(user_id);
        if (user.Inited == 1)
        {
            return;
        }
        int all = 200;
        while (all > 110 || all <= 90)
        {
            user.Cautious = random.Next(0, 50);
            user.Courage = random.Next(0, 50);
            user.Discipline = random.Next(0, 50);
            user.Justice = random.Next(0, 50);
            all = user.Cautious + user.Courage + user.Discipline + user.Justice;
            user.Inited = 1;
        }
        WriteToFile(user);
    }
    /// <summary>
    /// 记忆
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    public static void Remember(string group_id, string user_id, string name)
    {
        UserInfo user = GetUserInfo(user_id);
        if (user.CanGet)
        {
            using (StreamReader sr = new StreamReader(user_id + ".dat"))
            {
                Api.Group(group_id, "诶,,,博士把我忘了吗...没事的，风铃一直记得住博士的...");
            }
        }
        else
        {
            FileStream F = new FileStream(user_id + ".dat", FileMode.Create, FileAccess.Write, FileShare.Write);
            F.Close();
            Init(user_id);
            WriteToFile(user);
            Api.Group(group_id, "叫做 " + name + " 是吧，风铃会永远铭记在心里的!");
            Find(group_id,user_id,name);
        }
    }
    /// <summary>
    /// 回答
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public bool Answer(string group_id, string message)
    {
        foreach (var i in Tip)
        {
            if (message == i.Key)
            {
                Api.Group(group_id, i.Value);
                return true;
            }
        }
        foreach (var i in Rec)
        {
            if (message.Contains(i.Key))
            {
                Api.Group(group_id, i.Value);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 增加提问
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public bool Question(string group_id, string user_id, string name, string message)
    {
        if (!message.Contains("加入问答"))
            return false;

        string[] str;
        if (message.Contains("\r\n"))
            str = message.Split("\r\n");
        else
            str = message.Split("\n");

        if (!str[0].Contains("加入问答"))
            return false;
        if (!str[0].Contains("Vague") && !str[0].Contains("Static"))
        {
            Api.Group(group_id, "1格式：加入问答 触发方式(Vague,Static)\n问：xxxx\n答：xxxx\n（xxxx为自定义内容，并且要求xxxx内不会换行\nVague.1000金币.为只要包含这个词就会回答，Static.100金币.为必须消息完全匹配才会回答");
            return true;
        }
        if (!str[1].Contains("问："))
        {
            Api.Group(group_id, "2格式：加入问答 触发方式(Vague,Static)\n问：xxxx\n答：xxxx\n（xxxx为自定义内容，并且要求xxxx内不会换行\nVague.1000金币.为只要包含这个词就会回答，Static.100金币.为必须消息完全匹配才会回答");
            return true;
        }
        if (!message.Contains("答："))
        {
            Api.Group(group_id, "3格式：加入问答 触发方式(Vague,Static)\n问：xxxx\n答：xxxx\n（xxxx为自定义内容，并且要求xxxx内不会换行\nVague.1000金币.为只要包含这个词就会回答，Static.100金币.为必须消息完全匹配才会回答");
            return true;
        }
        if (str[1].Length <= 3)
        {
            Api.Group(group_id, "禁止提问内容小于等于1个字");
            return true;
        }
        UserInfo user = GetUserInfo(user_id);

        if (str[0].Contains("Vague"))
        {
            if (user.money >= 1000)
            {
                try
                {
                    Rec.Add(str[1].Substring(2), str[2].Substring(2));
                    user.money -= 1000;
                    Api.Group(group_id, "添加成功！那么这个1000金币，我就收下了哦~\n当前金币" + user.money);
                    using (StreamWriter sr1 = new StreamWriter("MessageVague.dat"))
                    {
                        foreach (var i in Rec)
                        {
                            sr1.WriteLine(i.Key + " " + i.Value);
                        }
                    }
                    WriteToFile(user);
                }
                catch
                {
                    Api.Group(group_id, "已经有这个词了...");
                }
            }
            else
            {
                Api.Group(group_id, "似乎您的金币有点不够呢~" + name + "\n当前金币：" + user.money + "\n需求金币：1000");
            }
        }
        else if (str[0].Contains("Static"))
        {
            if (user.money >= 100)
            {
                try
                {
                    Tip.Add(str[1].Substring(2), str[2].Substring(2));
                    user.money -= 100;
                    Api.Group(group_id, "添加成功！那么这个100金币，我就收下了哦~\n当前金币" + user.money);
                    using (StreamWriter sr2 = new StreamWriter("MessageStatic.dat"))
                    {
                        foreach (var i in Tip)
                        {
                            sr2.WriteLine(i.Key + " " + i.Value);
                        }
                    }
                    WriteToFile(user);
                }
                catch
                {
                    Api.Group(group_id, "已经有这个词了...");
                }
            }
            else
            {
                Api.Group(group_id, "似乎您的金币有点不够呢~" + name + "\n当前金币：" + user.money + "\n需求金币：100");
            }
        }
        return true;
    }
}

