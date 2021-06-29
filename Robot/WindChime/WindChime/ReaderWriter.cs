using System;
using System.Collections.Generic;
using System.IO;
public struct UserInfo
{
    public bool CanGet;
    /// <summary>
    /// �øж�
    /// </summary>
    public int heart;
    /// <summary>
    /// ���
    /// </summary>
    public int money;
    /// <summary>
    /// ǩ������
    /// </summary>
    public int HeartTime;
    /// <summary>
    /// ��������
    /// </summary>
    public int WorkTime;
    /// <summary>
    /// QQ��
    /// </summary>
    public string id;
    /// <summary>
    /// ���Ӵʻ�Ĵ���
    /// </summary>
    public int AddTimes;
    /// <summary>
    /// �Ƿ��ʼ����
    /// </summary>
    public int Inited;

    /// <summary>
    /// ����
    /// </summary>
    public int Courage;
    /// <summary>
    /// ����
    /// </summary>
    public int Cautious;
    /// <summary>
    /// ����
    /// </summary>
    public int Discipline;
    /// <summary>
    /// ����
    /// </summary>
    public int Justice;
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
                    cnt++;
                }
            }
        }
        catch
        {
            return ret;
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
            Api.Private("635691684", "���ˣ�ReaderWrite���ʼ���������⣬ϵͳ�Ѿ��رգ�����������");
            using (StreamReader sr = new StreamReader("message.dat")) ;
        }
    }


    public static bool Main(string group_id, string user_id, string name, string message)
    {
        Change(group_id, user_id, message);

        if (message == "��ѯ")
        {
            Find(group_id, user_id, name);
            return true;
        }
        else if (message == "��������")
        {
            Touch(group_id, user_id, name);
            return true;
        }
        else if (message.Contains("����") && message.Length <= 5)
        {
            Work(group_id, user_id, name, message);
            return true;
        }
        else if (message == "����������")
        {
            Remember(group_id, user_id, name);
            return true;
        }
        else if (message == "��ʼ������")
        {
            Init(group_id, user_id, name);
            return true;
        }
        else if (message == "���¿�ʼ��һ��")
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
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "���Ա��������𣡷�����쳬��Ŭ���ģ������֣�������ҲҪ�úü���Ŷ��\n�øж�������ǰ�øж�:200");
                }
                User.HeartTime = DateTime.Now.Day;
                if (User.heart >= 100 && User.heart < 200)
                    Api.Group(group_id, "�����ɵı�������\nлл��" + Api.GetAtMessage(user_id) + "������������Ԫ�������ģ�ϣ����Ҳ��Ŷ��\n�øж�����:" + k + "\n��ǰ�øж�:" + User.heart);
                if (User.heart >= 50 && User.heart < 100)
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "ż��������һ��ͷҲ���ǲ���...��֮���첻Ҫ�������ˣ�\n�øж�����:" + k + "\n��ǰ�øж�:" + User.heart);
                if (User.heart >= 0 && User.heart < 50)
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "��ô��ͻȻ���ҵ�ͷ��....������һ����...\n�øж�����:" + k + "\n��ǰ�øж�:" + User.heart);
                if (user_id == "3534417975")
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "����˵��Ҫ�Ե����һ��...!");


            }
            else
            {
                Random rd = new Random();
                if (user_id == "3534417975" && rd.Next(0, 5) == 0)
                {
                    User.heart += 1;
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "����˵��Ҫ�Ե����һ��...!���Ե�����������������������øж�����:1\n��ǰ�øж�:" + User.heart);
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
                    Api.Group(group_id, "��������Ҫ����ͷ�ˣ����컹�кܶ�����Ҫ����...");
                if (k == 1)
                    Api.Group(group_id, "ѽ��" + name + "�����Ұ���");
                if (k == 2)
                    Api.Group(group_id, "�����Ѿ�����С�����ˣ��Ѿ������˾Ͳ�Ҫ������......����");
                if (k == 3)
                    Api.Group(group_id, "���������ͷ�Ļ����Ǿ�����������һ�°ɣ�");
                if (k == 4)
                    Api.Group(group_id, "�����ɵı�������\nлл��������������Ԫ�������ģ�ϣ����Ҳ��Ŷ");
                if (k == 5)
                    Api.Group(group_id, "�Ǿ���������һ�Σ����һ�Σ�");
                if (k == 6)
                    Api.Group(group_id, "�ء�������ֻ��������һ���𣬲��ڶ��һ�����𡪡�����");
                if (k == 7)
                    Api.Group(group_id, "������쳬Ŭ�����ڹ����ˣ�������΢��һ������ֻҪ������Ļ�����΢��Ϣһ�¡�����������");
            }
        }
        else
        {
            Api.Group(group_id, name + "��˭������ͷ��Ϊʲô����һ��ӡ��û�У�\nps:ͨ������\"����������\"���������Ŷ��");
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
    public static void PrintWorkResult(string group_id,string result,int succeed,string box,string type,int NowMoney,int NowTag,int addons)
    {
        string message = "��" + box + "��\n���ι��������" + result + "\n��ý�ң�" + addons + "\n��ǰ��ң�"+ NowMoney;
        message += "\n";
        if (result == "��")
            message += (type + "������2\n��ǰ" + type + "��" + NowTag);
        if (result == "��")
            message += (type + "������1\n��ǰ" + type + "��" + NowTag);
        if (result == "��")
            message += (type + "������0\n��ǰ" + type + "��" + NowTag);
        Api.Group(group_id, message);
    }
    public static void Work(string group_id, string user_id, string name, string message)
    {
        UserInfo User = GetUserInfo(user_id);
        if (User.CanGet)
        {
            if (DateTime.Now.Day != User.WorkTime)
            {
                User.WorkTime = DateTime.Now.Day;
                if (message.Contains("����"))
                {
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
                            box += "��";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Courage += 2;
                        addons = succeed * 4;
                        result = "��";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Courage += 1;
                        addons = succeed * 2;
                        result = "��";
                    }
                    else
                    {
                        User.Courage += 0;
                        addons = succeed;
                        result = "��";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "����", User.money, User.Courage, addons);
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("����"))
                {
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
                            box += "��";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Cautious += 2;
                        addons = succeed * 4;
                        result = "��";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Cautious += 1;
                        addons = succeed * 2;
                        result = "��";
                    }
                    else
                    {
                        User.Cautious += 0;
                        addons = succeed;
                        result = "��";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "����", User.money, User.Cautious, addons);
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("��ͨ"))
                {
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
                            box += "��";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Discipline += 2;
                        addons = succeed * 4;
                        result = "��";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Discipline += 1;
                        addons = succeed * 2;
                        result = "��";
                    }
                    else
                    {
                        User.Discipline += 0;
                        addons = succeed;
                        result = "��";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "����", User.money, User.Discipline, addons);
                    WriteToFile(User);
                    return;
                }
                if (message.Contains("ѹ��"))
                {
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
                            box += "��";
                        }
                        else
                            box += "X";
                    string result;
                    if ((float)succeed / (float)count > 0.7f)
                    {
                        User.Justice += 2;
                        addons = succeed * 4;
                        result = "��";
                    }
                    else if ((float)succeed / (float)count >= 0.25f)
                    {
                        User.Justice += 1;
                        addons = succeed * 2;
                        result = "��";
                    }
                    else
                    {
                        User.Justice += 0;
                        addons = succeed;
                        result = "��";
                    }
                    User.money += addons;
                    PrintWorkResult(group_id, result, succeed, box, "����", User.money, User.Justice, addons);
                    WriteToFile(User);
                    return;
                }
                Api.Group(group_id,"������Ҫ�ڹ�������ϡ����ܡ� �� �����족 �� ����ͨ�� �� ��ѹ�ȡ� \n�ֱ��Ӧ��ʿ��4������ ���������������������������ɡ��͡����塱\nps.�°�Ĺ���֮ǰ����ǵ���ʹ�á���ʼ�����ԡ�����ȡ��ĳ�ʼ���ԣ����������ճ���Ϊ����ͨ������ѯ����״̬������ȡ");
                return;
            }
            else
            {
                if (User.heart >= 200)
                {
                    User.money += 1;
                    Api.Group(group_id, "�����Ѿ���Ŭ���ˣ���Ҫ��ǿ���Լ���������������͵͵�ָ���һö���������---��Ҫ������˵Ŷ��\n��ǰ���: " + User.money);
                }else
                    Api.Group(group_id, "��ʿ�������Ѿ��Է��幤�����ˣ�����ڹ����Ļ���������ǻ�ͻ�����ݵ�Ԫ��Ŷ��");
            }

        }
        else
        {
            Api.Group(group_id, name + "��˭��Ϊʲô��һ��ӡ��û�У�\nps:ͨ������\"����������\"���������Ŷ��");
            return;
        }
        WriteToFile(User);
    }
    /// <summary>
    /// ����ԱȨ�ޣ��޸ĺøж��Լ����
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
                Api.Group(group_id, "���ҵ�Ǯ�������㲻�������ҵ�Ǯ������..Ҳ�Ǹ����Լ�");
            else
                Api.Group(group_id, (str[1] == "635691684" ? "����" : "����") + "Ҫ�úõģ������Լ���------");

        }
        if (message.Contains("SetHeart"))
        {
            int before = User.heart;
            User.heart = Convert.ToInt32(str[2]);
            if (User.heart > 200)
            {
                Api.Group(group_id, "�øжȲ��ܳ���200��Ŷ�����Է�������˸ĳ�200��(Ц)");
                User.heart = 200;
            }
            else
            {
                if (before < User.heart)
                    Api.Group(group_id, "��...ͻȻ���÷���ȥϲ��һ������...�����Ŭ����");
                if (before > User.heart)
                    if (str[1] != "635691684" && str[1] != "3534417975")
                    {
                        Api.Group(group_id, "ΪʲôҪ����ȥ������......");
                    }
                    else
                    {
                        Api.Group(group_id,  "��Ҫ���Ų�Ҫ����"+ (str[1] == "635691684" ? "��" : "����") + "�أ�");

                        User.heart = 200;
                    }

            }
        }
        WriteToFile(User);
    }
    /// <summary>
    /// ɾ���ʻ�
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="message"></param>
    public bool Delete(string group_id, string user_id, string message)
    {
        if (message == "Delete")
        {
            if (!message.Contains("DeleteVague") && !message.Contains("DeleteStatic"))
                Api.Group(group_id, "��ο����������﷨\nDeleteVague �ʻ�(ɾ�����������Ĵʻ�\nDeleteStatic �ʻ�(ɾ����ȫƥ�䴥���Ĵʻ�");
            return true;
        }
        if (!message.Contains("DeleteVague") && !message.Contains("DeleteStatic"))
            return false;
        string[] str = message.Split(" ");
        if (str.Length > 2)
        {
            Api.Group(group_id, "��ʽ����ȷ����ο����������﷨\nDeleteVague �ʻ�(ɾ�����������Ĵʻ�\nDeleteStatic �ʻ�(ɾ����ȫƥ�䴥���Ĵʻ�");
        }
        UserInfo user = GetUserInfo(user_id);

        if (message.Contains("DeleteVague"))
        {
            if (user.money < 500)
            {
                Api.Group(group_id, "ûǮ����ֻ�ܻؼң�\n��ǰ���:" + user.money + "\n��Ҫ��ң�500");
                return true;
            }
            try
            {
                if (Rec.ContainsKey(str[1]))
                {
                    Rec.Remove(str[1]);
                    user.money -= 500;
                    Api.Group(group_id, "ɾ���ɹ���\n���λ��ѣ�500���\nʣ���ң�" + user.money);
                }
            }
            catch
            {
                Api.Group(group_id, "������ " + str[1] + " �Ĵ�����");
            }
        }
        if (message.Contains("DeleteStatic"))
        {
            if (user.money < 100)
            {
                Api.Group(group_id, "ûǮ����ֻ�ܻؼң�\n��ǰ���:" + user.money + "\n��Ҫ��ң�100");
                return true;
            }
            try
            {
                if (Tip.ContainsKey(str[1]))
                {
                    Tip.Remove(str[1]);
                    user.money -= 100;
                    Api.Group(group_id, "ɾ���ɹ���\n���λ��ѣ�100���\nʣ���ң�" + user.money);
                }
            }
            catch
            {
                Api.Group(group_id, "������ " + str[1] + " �Ĵ�����");
            }
        }
        WriteToFile(user);
        return true;
    }
    /// <summary>
    /// �����Ժ���ĳ��
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
            Api.Group(group_id, "�һᾡ����ȥ�ظ�" + str[1] + "����Ϣ�ģ�");
        }
        catch
        {
            Api.Group(group_id, str[1] + "�Ѿ�������ˡ�");
        }
    }
    /// <summary>
    /// ��ѯ
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    public static void Find(string group_id, string user_id, string name)
    {
        UserInfo user = GetUserInfo(user_id);
        if (user.CanGet)
        {
            string c1 = "\n������" + user.Courage + "(" + GetLv(user.Courage) + ")";
            string c2 = "\n������" + user.Cautious + "(" + GetLv(user.Cautious) + ")";
            string c3 = "\n���ɣ�" + user.Discipline + "(" + GetLv(user.Discipline) + ")";
            string c4 = "\n���壺" + user.Justice + "(" + GetLv(user.Justice) + ")";
            Api.Group(group_id, name + "��ǰ�øжȣ�" + user.heart + "\n��ǰ���Ϊ��" + user.money + c1 + c2 +c3 +c4) ;
        }
        else
        {
            Api.Group(group_id, name + "��˭��Ϊʲô��һ��ӡ��û�У�\nps:ͨ������\"����������\"���������Ŷ��");
            return;
        }
    }
    /// <summary>
    /// ���¿�ʼ��һ��
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
            Api.Group(group_id, "��ʿ������~������" + DateTime.Now.Month + "��" + DateTime.Now.Day + "��Ŷ���������Ǯ��Ϊʲô����500��ң���Ҳ���Ǻ����~");
        }
        else
        {
            Api.Group(group_id, "����ִ��TT2Э����Ҫ500��ҡ�");
        }
        WriteToFile(user);
    }
    /// <summary>
    /// ��ʼ����������
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    public static void Init(string group_id, string user_id, string name)
    {
        UserInfo user = GetUserInfo(user_id);
        if (user.Inited == 1)
        {
            Api.Group(group_id, name + "�Ѿ���ʼ����������Ŷ��");
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
        Find(group_id, user_id, name);
    }
    /// <summary>
    /// ����
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
                Api.Group(group_id, "��,,,��ʿ����������...û�µģ�����һֱ�ǵ�ס��ʿ��...");
            }
        }
        else
        {
            FileStream F = new FileStream(user_id + ".dat", FileMode.Create, FileAccess.Write, FileShare.Write);
            F.Close();
            WriteToFile(user);
            Api.Group(group_id, "���� " + name + " �ǰɣ��������Զ����������ģ�\n ps.ͨ������\"��ʼ������\"����ʼ����ɫ����");
        }
    }
    /// <summary>
    /// �ش�
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
    /// ��������
    /// </summary>
    /// <param name="group_id"></param>
    /// <param name="user_id"></param>
    /// <param name="name"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public bool Question(string group_id, string user_id, string name, string message)
    {
        if (!message.Contains("�����ʴ�"))
            return false;

        string[] str;
        if (message.Contains("\r\n"))
            str = message.Split("\r\n");
        else
            str = message.Split("\n");

        if (!str[0].Contains("�����ʴ�"))
            return false;
        if (!str[0].Contains("Vague") && !str[0].Contains("Static"))
        {
            Api.Group(group_id, "1��ʽ�������ʴ� ������ʽ(Vague,Static)\n�ʣ�xxxx\n��xxxx\n��xxxxΪ�Զ������ݣ�����Ҫ��xxxx�ڲ��ỻ��\nVague.1000���.ΪֻҪ��������ʾͻ�ش�Static.100���.Ϊ������Ϣ��ȫƥ��Ż�ش�");
            return true;
        }
        if (!str[1].Contains("�ʣ�"))
        {
            Api.Group(group_id, "2��ʽ�������ʴ� ������ʽ(Vague,Static)\n�ʣ�xxxx\n��xxxx\n��xxxxΪ�Զ������ݣ�����Ҫ��xxxx�ڲ��ỻ��\nVague.1000���.ΪֻҪ��������ʾͻ�ش�Static.100���.Ϊ������Ϣ��ȫƥ��Ż�ش�");
            return true;
        }
        if (!message.Contains("��"))
        {
            Api.Group(group_id, "3��ʽ�������ʴ� ������ʽ(Vague,Static)\n�ʣ�xxxx\n��xxxx\n��xxxxΪ�Զ������ݣ�����Ҫ��xxxx�ڲ��ỻ��\nVague.1000���.ΪֻҪ��������ʾͻ�ش�Static.100���.Ϊ������Ϣ��ȫƥ��Ż�ش�");
            return true;
        }
        if (str[1].Length <= 3)
        {
            Api.Group(group_id, "��ֹ��������С�ڵ���1����");
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
                    Api.Group(group_id, "��ӳɹ�����ô���1000��ң��Ҿ�������Ŷ~\n��ǰ���" + user.money);
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
                    Api.Group(group_id, "�Ѿ����������...");
                }
            }
            else
            {
                Api.Group(group_id, "�ƺ����Ľ���е㲻����~" + name + "\n��ǰ��ң�" + user.money + "\n�����ң�1000");
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
                    Api.Group(group_id, "��ӳɹ�����ô���100��ң��Ҿ�������Ŷ~\n��ǰ���" + user.money);
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
                    Api.Group(group_id, "�Ѿ����������...");
                }
            }
            else
            {
                Api.Group(group_id, "�ƺ����Ľ���е㲻����~" + name + "\n��ǰ��ң�" + user.money + "\n�����ң�100");
            }
        }
        return true;
    }
}

