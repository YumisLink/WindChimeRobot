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
    /// <summary>
    /// 是否抑制hokma
    /// </summary>
    public int Hokma;
    /// <summary>
    /// 萌新金币翻倍次数
    /// </summary>
    public int newType;
    /// <summary>
    /// 名称
    /// </summary>
    public string name;


    /// <summary>
    /// 每天可以单挑的次数
    /// </summary>
    public int EGOWeapon;
    public int EGOArmor;
    public int SoloCount;

    /// <summary>
    /// EGO武器增幅
    /// </summary>
    public int WeaponIncrease;
    /// <summary>
    /// EGO防具增幅
    /// </summary>
    public int ArmorIncrease;

    /// <summary>
    /// 正在挑战的异想体
    /// </summary>
    public int NowBoss;
    /// <summary>
    /// 排名
    /// </summary>
    public int Rank;
    /// <summary>
    /// 今天是否领取天梯奖励
    /// </summary>
    public int LadderRew;
    /// <summary>
    /// 所有的武器
    /// </summary>
    public List<bool> AllWeapon;
    /// <summary>
    /// 所有的武器
    /// </summary>
    public List<bool> AllArmor;
    /// <summary>
    /// 抑制的核心
    /// </summary>
    public List<bool> Inhibition;
    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime LastestLogin;
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
        ret.Hokma = 0;
        ret.newType = 10;
        ret.name = "";
        ret.SoloCount = 2;
        ret.EGOWeapon = 0;
        ret.EGOArmor = 0;
        ret.NowBoss = 0;
        ret.WeaponIncrease = 0;
        ret.ArmorIncrease = 0;
        ret.Rank = -1;
        ret.LadderRew = 0;
        ret.AllArmor = new List<bool>();
        ret.AllWeapon = new List<bool>();
        ret.Inhibition = new List<bool>();
        ret.LastestLogin = DateTime.Now;
        for (int i = 0; i < GameManager.weapon.Count + 10; i++)
        {
            ret.AllArmor.Add(false);
            ret.AllWeapon.Add(false);
        }
        for (int i = 0; i < 15; i++)
            ret.Inhibition.Add(false);
        ret.AllArmor[0] = true; 
        ret.AllWeapon[0] = true;
        try
        {
            using (StreamReader sr = new StreamReader("user/" + user_id + ".dat"))
            {
                ret.CanGet = true;
                string line;
                int cnt = 1;
                Console.WriteLine(user_id);
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
                    if (cnt == 11)
                        ret.Hokma = Convert.ToInt32(line);
                    if (cnt == 12)
                        ret.newType = Convert.ToInt32(line);
                    if (cnt == 13)
                        ret.name = line;
                    if (cnt == 14)
                        ret.EGOWeapon = Convert.ToInt32(line);
                    if (cnt == 15)
                        ret.EGOArmor = Convert.ToInt32(line);
                    if (cnt == 16)
                        ret.SoloCount = Convert.ToInt32(line);
                    if (cnt == 17)
                        ret.WeaponIncrease = Convert.ToInt32(line);
                    if (cnt == 18)
                        ret.ArmorIncrease = Convert.ToInt32(line);
                    if (cnt == 19)
                    {
                        string[] str = line.Split(" ");
                        ret.Rank = Convert.ToInt32(str[1]);
                        ret.LadderRew = Convert.ToInt32(str[2]);
                    }
                    if (cnt == 20)
                    {
                        string[] str = line.Split(" ");
                        for (int i = 1; i < str.Length-1; i++)
                            ret.AllWeapon[Convert.ToInt32(str[i])] = true;
                    }
                    if (cnt == 21)
                    {
                        string[] str = line.Split(" ");
                        for (int i = 1; i < str.Length-1; i++)
                            ret.AllArmor[Convert.ToInt32(str[i])] = true;
                    }
                    if (cnt == 22)
                    {
                        string[] str = line.Split(" ");
                        int year = Convert.ToInt32(str[1]), month = Convert.ToInt32(str[2]), day = Convert.ToInt32(str[3]);
                        ret.LastestLogin = new DateTime(year,month,day);
                    }
                    if (cnt == 23)
                    {
                        string[] str = line.Split(" ");
                        for (int i = 1; i < str.Length - 1; i++)
                            ret.Inhibition[Convert.ToInt32(str[i])] = true;
                    }
                    cnt++;
                }
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            ret.id = "0";
            ret.CanGet = false;
            return ret;
        }
        if (!ret.AllArmor[ret.EGOArmor]) ret.EGOArmor = 0;
        if (!ret.AllWeapon[ret.EGOWeapon]) ret.EGOWeapon = 0;

        if (ret.Justice >= 120 && ret.Hokma == 0) ret.Justice = 120;
        if (ret.Discipline >= 120 && ret.Hokma == 0) ret.Discipline = 120;
        if (ret.Cautious >= 120 && ret.Hokma == 0) ret.Cautious = 120;
        if (ret.Courage >= 120 && ret.Hokma == 0) ret.Courage = 120;
        if (ret.Justice >= 180 && ret.Hokma == 0) ret.Justice = 180;
        if (ret.Discipline >= 180 && ret.Hokma == 0) ret.Discipline = 180;
        if (ret.Cautious >= 180 && ret.Hokma == 0) ret.Cautious = 180;
        if (ret.Courage >= 180 && ret.Hokma == 0) ret.Courage = 180;
        if (ret.WorkTime != DateTime.Now.Day)
        {
            ret.WorkTime = DateTime.Now.Day;
            ret.WorkCount = 3;
            if (ret.Inhibition[0])
                ret.WorkCount++;
            ret.SoloCount = 2;
            ret.LadderRew = 0;
            ret.newType += 2;
        }
        if (ret.Inited == 0)
        {
            int all = 0;
            while (all > 170 || all <= 120)
            {
                ret.Cautious = random.Next(15, 50);
                ret.Courage = random.Next(15, 50);
                ret.Discipline = random.Next(15, 50);
                ret.Justice = random.Next(15, 50);
                all = ret.Cautious + ret.Courage + ret.Discipline + ret.Justice;
                ret.Inited = 1;
            }
            WriteToFile(ret);
        }
        return ret;
    }
    public static void WriteToFile(UserInfo user)
    {
        using (StreamWriter sw = new StreamWriter("user/" + user.id + ".dat"))
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
            sw.WriteLine(user.WorkCount);//line10
            sw.WriteLine(user.Hokma);
            sw.WriteLine(user.newType);
            sw.WriteLine(user.name);
            sw.WriteLine(user.EGOWeapon);
            sw.WriteLine(user.EGOArmor);
            sw.WriteLine(user.SoloCount);
            sw.WriteLine(user.WeaponIncrease);
            sw.WriteLine(user.ArmorIncrease);
            sw.WriteLine("天梯赛 "+user.Rank + " " + user.LadderRew);
            sw.WriteLine(EGOW(user));//line20
            sw.WriteLine(EGOA(user));
            sw.WriteLine("年月日 " + DateTime.Now.Year + " "+ DateTime.Now.Month + " " + DateTime.Now.Day);
            sw.WriteLine(Inhibition(user));
        }
    }
    public static string Inhibition(UserInfo user)
    {
        string str = "已经抑制的核心 ";
        for (int i = 0; i < user.Inhibition.Count; i++)
            if (user.Inhibition[i])
                str += i + " ";
        return str;
    }
    public static string EGOW(UserInfo user)
    {
        string str = "EGO武器 ";
        for (int i = 0; i < user.AllWeapon.Count; i++)
            if (user.AllWeapon[i])
                str += i+" ";
        str.Remove(str.Length-1);
        return str;
    }
    public static string EGOA(UserInfo user)
    {
        string str = "EGO护甲 ";
        for (int i = 0; i < user.AllArmor.Count; i++)
            if (user.AllArmor[i])
                str += i + " ";
        str.Remove(str.Length-1);
        return str;
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
            Api.Private("635691684", "妈妈！ReaderWrite类初始化出现问题，系统已经关闭，请你重启！");
            StreamReader sr = new StreamReader("message.dat");
        }
    }
    public static int GetUserHaveEGOs(UserInfo usf)
    {
        int ret = 0;
        foreach (var a in usf.AllArmor)
            if (a == true)
                ret++;
        foreach (var a in usf.AllWeapon)
            if (a == true)
                ret++;
        return ret;
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
        else if (message == "注册")
        {
            Remember(group_id, user_id, name);
            return true;
        }
        else if (message == "初始化属性")
        {
            Init(user_id);
            return true;
        }
        else if (message == "抑制Hokma核心")
        {
            Api.Group(group_id, "还没有做,还在TODO里面...主要是不知道怎么做,但是就是想限制一下属性上限.有想法的群众可以给我留言.");
            return true;
        }else if (message.Contains("强化"))
        {
            Increase(group_id, user_id, name,message);
            return true;
        }
        else return false;
    }
    public static void Touch(string group_id, string user_id, string name)
    {
        UserInfo User = GetUserInfo(user_id);
        User.name = name;
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
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "博士...妈妈说，女孩子不能轻易的被摸头...但是博士的话，是特殊的存在吧。\n好感度已满当前好感度:200");
                }
                User.HeartTime = DateTime.Now.Day;
                if (User.heart >= 100 && User.heart < 200)
                    Api.Group(group_id, "（乖巧的被摸摸）\n" + Api.GetAtMessage(user_id) + "博士，你想听点故事吗，关于曾经我还在脑叶公司里面的经历...唔..不想吗，那风铃就这么依偎着博士好了...\n好感度上升:" + k + "\n当前好感度:" + User.heart);
                if (User.heart >= 50 && User.heart < 100)
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "博士喜欢摸风铃吗，嗯...反正现在也没有事情做，那就让博士好好摸摸吧。\n好感度上升:" + k + "\n当前好感度:" + User.heart);
                if (User.heart >= 0 && User.heart < 50)
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "啊！博士，如果我没有猜错，现在的任务是管理异想体，而不是在这里摸我的头吧？\n好感度上升:" + k + "\n当前好感度:" + User.heart);
                if (user_id == "3534417975")
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "妈妈说，要对刀茶好一点...!");


            }
            else
            {
                Random rd = new Random();
                if (user_id == "3534417975" && rd.Next(0, 5) == 0)
                {
                    User.heart += 1;
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "妈妈说，要对刀茶好一点...!所以刀茶摸摸并不会讨厌的啦！好感度上升:1\n当前好感度:" + User.heart);
                }
                int k = 0;
                if (User.heart >= 0 && User.heart < 50)
                    k = rd.Next(0, 3);
                if (User.heart >= 50 && User.heart < 100)
                    k = rd.Next(2, 5);
                if (User.heart >= 100)
                    k = rd.Next(4, 8);
                if (k == 0)
                    Api.Group(group_id, "博士，一无所有已经出逃了，确定不去看看而是在这里摸我的头吗？");
                if (k == 1)
                    Api.Group(group_id, "Dr." + name + ",脑叶公司与罗德岛的合同上面可没有包括在工作时间在这里摸AI的头。");
                if (k == 2)
                    Api.Group(group_id, "如果博士觉得摸摸风铃的话，可以增加工作效率，那么请便就是了。");
                if (k == 3)
                    Api.Group(group_id, "其实，风铃也不太擅长交流，在曾经也只是一个人统筹着脑叶公司的运作，日常的交流也只有与安吉拉。");
                if (k == 4)
                    Api.Group(group_id, "博士，可以为我戴上我的小礼帽吗？谢谢你...");
                if (k == 5)
                    Api.Group(group_id, "博士，实际上我是在违反《人工智能伦理修订案》的情况下制造出来的，不过，罗德岛这里，是安全的吧？");
                if (k == 6)
                    Api.Group(group_id, "能够在闲暇的时间在博士的身边，对于风铃来说，就算是一种休息了吧！");
                if (k == 7)
                    Api.Group(group_id, "可以稍微抱一下我吗，只要能在你的怀里稍微休息一下——————");
            }
        }
        else
        {
            Api.Group(group_id, name + "您好博士，请问你是想签署与脑叶公司合作的合同吗？只需要输入“注册”即可参加到与脑叶公司的合作中来，至于为什么是注册两个字，如果不解的话......");
            return;
        }
        WriteToFile(User);
    }
    public static int GetLv(int lv)
    {
        if (lv <= 20)
            return 1;
        if (lv <= 40)
            return 2;
        if (lv <= 60)
            return 3;
        if (lv <= 80)
            return 4;
        return 5;
    }
    public static int GetWorkResults(int count, float Probability)
    {
        int ret = 0;
        for (int i = 1; i <= count; i++)
        {
            if (random.NextDouble() < Probability)
                ret++;
        }
        return ret;
    }
    public static void PrintWorkResult(string group_id, string result, int succeed, string box, string type, int NowMoney, int NowTag, int addons, int WorkCount, int Hokma, int newType,int egos)
    {
        if (newType > 0)
            addons *= 5;
        string mes = "\n根据持有的EGO数量提高金币的百分比：" + (egos * 5) + "%";
        string message = "【" + box + "】\n本次工作结果：" + result + mes + "\n获得金币：" + addons + "\n当前金币：" + NowMoney;
        message += "\n";
        if (Hokma == 1)
        {
            if (NowTag >= 180)
            {
                message += type + "180,已达到最大值!!!";
            }
            else
            {
                message += (type + "上升：1\n当前" + type + "：" + NowTag);
            }
        }
        else
        {
            if (NowTag >= 120)
            {
                message += type + "120,已达到上限,请先抑制Hokma核心之后才能提升!!!";
            }
            else
            {
                if(NowTag >= 100)
                {
                    message += (type + "上升：1\n当前" + type + "：" + NowTag+"（如已抑制HOD则增加2）");
                }
                else
                {
                    if (result == "优")
                        message += (type + "上升：7\n当前" + type + "：" + NowTag);
                    if (result == "良")
                        message += (type + "上升：5\n当前" + type + "：" + NowTag);
                    if (result == "差")
                        message += (type + "上升：3\n当前" + type + "：" + NowTag);
                }
            }
        }
        message += "\n剩余工作次数:" + WorkCount;
        if (newType > 0)
        {
            message += "\n金额五倍---您现在剩余的次数:" + (newType - 1);
        }
        message += "\n——直到下次更新之前，每天都有2次5倍金额的次数！";
        //message += "看到没有人去挑战boss获得ego，所以这个版本开始，打工时候，每拥有一件EGO提供5%的经济效应。";
        Api.Group(group_id, message);
    }
    public static int AddExcellent = 7;
    public static int AddGood = 5;
    public static int AddBad = 3;
    public static float GetTarget(float lv, int val)
    {
        if (val > 80)
            return ((val - 80) * 0.0025f) + lv;
        if (val > 60)
            return ((val - 60) * 0.0025f) + lv;
        if (val > 40)
            return ((val - 40) * 0.0025f) + lv;
        if (val > 20)
            return ((val - 20) * 0.0025f) + lv;
        return ((val) * 0.0025f) + lv;

    }

    public static void Work(string group_id, string user_id, string name, string message)
    {
        UserInfo User = GetUserInfo(user_id);
        User.name = name;
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
                    float target = GetTarget(State.sts.Instinct[lv - 1] * 0.01f, User.Courage);
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target);
                    string box = "";
                    int good = succeed;
                    int lk = User.Courage;
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
                    addons += (int)(addons * (0.05 * GetUserHaveEGOs(User)));
                    if (User.Inhibition[1])
                        addons += (int)(addons * (0.25));
                    User.money += addons;
                    if (User.Courage >= 100)
                        User.Courage = lk + 1;
                    if (User.Courage >= 100 && User.Inhibition[2])
                        User.Courage = lk + 1;
                    if (User.newType > 0)
                    {
                        User.money += (addons) * 4;
                        User.newType--;
                    }
                    PrintWorkResult(group_id, result, succeed, box, "勇气", User.money, User.Courage, addons, User.WorkCount, User.Hokma, User.newType, GetUserHaveEGOs(User));
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("洞察"))
                {
                    User.WorkCount--;
                    int addons;
                    int lv = GetLv(User.Cautious);
                    float target = GetTarget(State.sts.Insight[lv - 1] * 0.01f, User.Cautious);
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target);
                    string box = "";
                    int lk = User.Cautious;
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
                    //Console.WriteLine("debug " + addons);
                    addons += (int)(addons * (0.05 * GetUserHaveEGOs(User)));
                    //Console.WriteLine("debug " + addons);
                    if (User.Inhibition[1])
                        addons += (int)(addons * (0.25));
                    //Console.WriteLine("debug " + addons);
                    User.money += addons;
                    if (User.Cautious >= 100)
                        User.Cautious = lk + 1;
                    if (User.Cautious >= 100 && User.Inhibition[2])
                        User.Cautious = lk + 1;
                    if (User.newType > 0)
                    {
                        User.money += (addons) * 4;
                        User.newType--;
                    }
                    PrintWorkResult(group_id, result, succeed, box, "谨慎", User.money, User.Cautious, addons, User.WorkCount, User.Hokma, User.newType, GetUserHaveEGOs(User));
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("沟通"))
                {
                    User.WorkCount--;
                    int addons;
                    int lv = GetLv(User.Discipline);
                    float target = GetTarget(State.sts.Communication[lv - 1] * 0.01f, User.Discipline);
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target);
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
                    int lk = User.Discipline;
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
                    addons += (int)(addons * (0.05 * GetUserHaveEGOs(User)));
                    if (User.Inhibition[1])
                        addons += (int)(addons * (0.25));
                    User.money += addons;
                    if (User.Discipline >= 100)
                        User.Discipline = lk + 1;
                    if (User.Discipline >= 100 && User.Inhibition[2])
                        User.Discipline = lk + 1;
                    if (User.newType > 0)
                    {
                        User.money += (addons) * 4;
                        User.newType--;
                    }
                    PrintWorkResult(group_id, result, succeed, box, "自律", User.money, User.Discipline, addons, User.WorkCount, User.Hokma, User.newType, GetUserHaveEGOs(User));
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("压迫"))
                {
                    User.WorkCount--;
                    int addons;
                    int lv = GetLv(User.Justice);
                    float target = GetTarget(State.sts.Oppression[lv - 1] * 0.01f, User.Justice);
                    int count = 10 + (User.heart / 5);
                    int succeed = GetWorkResults(count, target);
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
                    int lk = User.Justice;
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
                    addons += (int)(addons * (0.05 * GetUserHaveEGOs(User)));
                    if (User.Inhibition[1])
                        addons += (int)(addons * (0.25));
                    User.money += addons;
                    if (User.Justice >= 100)
                        User.Justice = lk + 1;
                    if (User.Justice >= 100 && User.Inhibition[2])
                        User.Justice = lk + 1;
                    if (User.newType > 0)
                    {
                        User.money += (addons) * 4;
                        User.newType--;
                    }
                    PrintWorkResult(group_id, result, succeed, box, "正义", User.money, User.Justice, addons, User.WorkCount, User.Hokma, User.newType, GetUserHaveEGOs(User));
                    WriteToFile(User);
                    return;
                }
                Api.Group(group_id, "工作需要在工作后加上“本能” 或 “洞察” 或 “沟通” 或 “压迫” \n分别对应博士的4个属性 ：“勇气”、“谨慎”、“自律”和“正义”\nps.新版的工作之前，请记得先使用“初始化属性”来获取你的初始属性，另外风铃的日常行为可以通过“查询风铃状态”来获取");
                //Api.Group(group_id, "博士，你是不是又在找小羊了呢...风铃知道了，很快就会消失的。");
                return;
            }
            else
            {
                if (User.heart >= 200)
                {
                    User.money += 1;
                    Api.Group(group_id, "今天已经很努力了，不要再强迫自己工作了啦，风铃偷偷分给你一枚金币啦。嘘---不要跟别人说哦！\n当前金币: " + User.money);
                }
                else
                    Api.Group(group_id, "博士，该收容单元的逆卡巴拉能量融毁了，Chesed正在修复，请博士明天在来吧。（翻译：已经工作过2次了不能在做工了。");
            }

        }
        else
        {
            Api.Group(group_id, name + "您好博士，请问你是想签署与脑叶公司合作的合同吗？只需要输入“注册”即可参加到与脑叶公司的合作中来，至于为什么是注册两个字，如果不解的话......");
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
                Api.Group(group_id, "！我的钱！妈妈你不许拿走我的钱！至少..也是给你自己");
            else
                Api.Group(group_id, (str[1] == "635691684" ? "妈妈" : "刀茶") + "要好好的，养活自己！------");

        }
        if (message.Contains("SetHeart"))
        {
            int before = User.heart;
            User.heart = Convert.ToInt32(str[2]);
            if (User.heart > 200)
            {
                Api.Group(group_id, "好感度不能超过200的哦！所以风铃帮妈妈改成200！(笑)");
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
                        Api.Group(group_id, "不要！才不要讨厌" + (str[1] == "635691684" ? "你" : "刀茶") + "呢！");

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
        //if (message.Contains("Delete"))
        //{
        //    Api.Group(group_id, "该系统已被关闭");
        //    return true;
        //}
        //return false;
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
            string c1 = "\n勇气：" + user.Courage + "(" + GetLv(user.Courage) + "级)";
            string c2 = "\n谨慎：" + user.Cautious + "(" + GetLv(user.Cautious) + "级)";
            string c3 = "\n自律：" + user.Discipline + "(" + GetLv(user.Discipline) + "级)";
            string c4 = "\n正义：" + user.Justice + "(" + GetLv(user.Justice) + "级)";
            string c5 = "\n当前佩戴EGO武器：➕"+user.WeaponIncrease + GameManager.weapon[user.EGOWeapon].Name + "\n当前佩戴EGO护甲：➕" + user.ArmorIncrease + GameManager.armor[user.EGOArmor].Name;
            Hero h = new Hero(user);
            c5 += "\n" + h.ToString();
            Api.Group(group_id, name + "当前好感度：" + user.heart + "\n当前金币为：" + user.money + c1 + c2 + c3 + c4 + c5);

        }
        else
        {
            Api.Group(group_id, name + "您好博士，请问你是想签署与脑叶公司合作的合同吗？只需要输入“注册”即可参加到与脑叶公司的合作中来，至于为什么是注册两个字，如果不解的话......");
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
        return;
        UserInfo user = GetUserInfo(user_id);
        if (user.money >= 2000)
        {
            user.money -= 2000;
            user.WorkTime = 0;
            Api.Group(group_id, "今天" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日！早安博士，今天睡得很舒服吗？");
        }
        else
        {
            Api.Group(group_id, "执行TT2协议需要2000金币。");
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
            using (StreamReader sr = new StreamReader("user/" + user_id + ".dat"))
            {
                Api.Group(group_id, "博士，是觉得脑叶公司的环境不太舒适吗？要不尝试去外面呼吸一下新鲜空气？或者找Netzach要一点脑啡肽...开玩笑的，我给你去泡一杯咖啡吧！");
            }
        }
        else
        {
            FileStream F = new FileStream("user/" + user_id + ".dat", FileMode.Create, FileAccess.Write, FileShare.Write);
            F.Close();
            Init(user_id);
            user.name = name;
            WriteToFile(user);
            Api.Group(group_id, "" + name + "博士你好，我是脑叶公司的AI助理：风铃。在接下来脑叶公司和罗德岛合作合同生效期间，我会作为博士的私人助理，帮助博士管理异想体。");
            Find(group_id, user_id, name);
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
    /// <summary>
    /// 增幅
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static int[] moneyCost = { 10, 50, 100, 200, 400, 600, 1000, 2000, 3500, 5000, 10000, 15000, 20000, 25000, 30000, 35000, 40000,45000,100000000 };
    public static void Increase(string group_id, string user_id, string name, string message)
    {
        if (message.Length > 6)
            return;
        UserInfo user = GetUserInfo(user_id);
        if (message.Contains("武器"))
        {
            if (user.money >= moneyCost[user.WeaponIncrease])
            {
                user.money -= moneyCost[user.WeaponIncrease];
                if (random.NextDouble() < 0.4)
                {
                    user.WeaponIncrease++;
                    Api.Group(group_id, "强化成功，当前武器增幅数值：" + user.WeaponIncrease + "\n消耗了：" + moneyCost[user.WeaponIncrease - 1] + "金币，当前剩余金币：" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += moneyCost[user.WeaponIncrease] / 2;
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化50%的金币\n消耗了："+ moneyCost[user.WeaponIncrease] / 2 + "金币，当前剩余金币：" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += moneyCost[user.WeaponIncrease];
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化100%的金币");
                    }
                    else Api.Group(group_id, "强化失败！乐了！ \n如果觉得风铃刷屏了，可以去私聊强化武器\n消耗了：" + moneyCost[user.WeaponIncrease]  + "金币，当前剩余金币：" + user.money);

                }
            }
            else
            {
                Api.Group(group_id, "金币不足，需要金币：" + moneyCost[user.WeaponIncrease]);
            }
            WriteToFile(user);
            return ;
        }
        if (message.Contains("护甲"))
        {
            if (user.money >= moneyCost[user.ArmorIncrease])
            {
                user.money -= moneyCost[user.ArmorIncrease];
                if (random.NextDouble() < 0.25)
                {
                    user.ArmorIncrease++;
                    Api.Group(group_id, "强化成功，当前防具增幅数值：" + user.ArmorIncrease+ "\n消耗了：" + moneyCost[user.ArmorIncrease-1] + "金币，当前剩余金币：" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += moneyCost[user.ArmorIncrease] / 2;
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化50%的金币\n消耗了：" + moneyCost[user.ArmorIncrease] / 2 + "金币，当前剩余金币：" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += moneyCost[user.ArmorIncrease];
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化100%的金币");
                    }
                    else Api.Group(group_id, "强化失败！乐了！ \n如果觉得风铃刷屏了，可以去私聊强化武器\n消耗了：" + moneyCost[user.ArmorIncrease] + "金币，当前剩余金币：" + user.money);
                }
                WriteToFile(user);
                return ;
            }
            else
            {
                Api.Group(group_id, "金币不足，需要金币：" + moneyCost[user.ArmorIncrease]);
            }
        }
        if (message.Contains("查询"))
        {
            Api.Group(group_id, "本次武器强化需要金币：" + moneyCost[user.WeaponIncrease] + "\n防具强化需要：" + moneyCost[user.ArmorIncrease]);
            return ;
        }

        Api.Group(group_id, "博士，您想对你的EGO武器进行强化吗？可以参考一下下面的格式哦！\n格式：\n强化 [武器、护甲、查询] \n如果觉得风铃刷屏了，可以去私聊强化武器\nPS.强化的EGO不会因为更换EGO而丢失强化数值。");
        return ;
    }
}

