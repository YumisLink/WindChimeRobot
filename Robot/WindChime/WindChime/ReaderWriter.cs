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
};

public class ReaderWriter
{
    Dictionary<string, string> Rec = new Dictionary<string, string>();
    Dictionary<string, string> Tip = new Dictionary<string, string>();
    Dictionary<string, bool> IgnoreList = new Dictionary<string, bool>();
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
                        ret.WorkTime = Convert.ToInt32(line);
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
                    for (int i = 2; i < ss.Length;i++)
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
    public static void Touch(string group_id, string user_id, string name)
    {
        UserInfo User = GetUserInfo(user_id);
        if (User.CanGet)
        {
            if (DateTime.Now.Day != User.HeartTime)
            {
                Random rd = new Random();
                int k = rd.Next(2, 5);
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


            }
            else
            {
                Random rd = new Random();
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
    public static void Work(string group_id, string user_id, string name)
    {
        UserInfo User = GetUserInfo(user_id);
        if (User.CanGet)
        {
            if (DateTime.Now.Day != User.WorkTime)
            {
                User.money += (User.heart * 3 + 10);
                User.WorkTime = DateTime.Now.Day;
                if (User.heart >= 200)
                    Api.Group(group_id, Api.GetAtMessage(user_id) + "�����ˣ��������������ɡ���������Ҫ���Լ�̫��������Ŷ��\n��ý��:" + (User.heart * 3 + 10) + "\n��ǰ���: " + User.money);
                if (User.heart >= 100 && User.heart < 200)
                    Api.Group(group_id, "[CQ:at,qq=" + user_id + "] �����ˡ�������ȥ��Ϣһ���ˣ��ȱ�ˮ�ɣ�\n��ý��:" + (User.heart * 3 + 10) + "\n��ǰ���:" + User.money);
                if (User.heart >= 50 && User.heart < 100)
                    Api.Group(group_id, "[CQ:at,qq=" + user_id + "] ������~\n��ý��:" + (User.heart * 3 + 10) + "\n��ǰ���:" + User.money);
                if (User.heart >= 0 && User.heart < 50)
                    Api.Group(group_id, "[CQ:at,qq=" + user_id + "] �������࣡\n��ý��:" + (User.heart * 3 + 10) + "\n��ǰ���:" + User.money);
            }
            else
            {
                if (User.heart >= 200)
                {
                    User.money += 1;
                    Api.Group(group_id, "�����Ѿ���Ŭ���ˣ���Ҫ��ǿ���Լ���������������͵͵�ָ���һö���������---��Ҫ������˵Ŷ��\n��ǰ���: " + User.money);
                }
                if (User.heart >= 100 && User.heart < 200)
                    Api.Group(group_id, "���˺��ˣ���ȥ��Ϣ�ˣ����ھͲ�Ҫ�ٹ����ˡ�");
                if (User.heart >= 50 && User.heart < 100)
                    Api.Group(group_id, "����" + name + "�Ѿ��������ˣ��Ѿ�û��������Ļ��ˡ�");
                if (User.heart >= 0 && User.heart < 50)
                    Api.Group(group_id, "__��װ������__");
                if (DateTime.Now.Day != User.HeartTime && User.heart < 200)
                    Api.Group(group_id, "Ϊʲô�㲻����ͷ����������������Ч����");
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
            Api.Group(group_id, "���ҾͰ�Ǯ������Ŷ��");
        }
        if (message.Contains("SetHeart"))
        {
            int before = User.heart;
            User.heart = Convert.ToInt32(str[2]);
            if (User.heart >= 200)
            {
                Api.Group(group_id, "�øжȲ��ܳ���200��Ŷ�����Է������ĳ�200��(Ц)");
                User.heart = 200;
            }
            else
            {
                if (before >= User.heart)
                    Api.Group(group_id, "ΪʲôҪ����ȥ������......");
                if (before < User.heart)
                    Api.Group(group_id, "��...ͻȻ���÷���ȥϲ��һ������...�����Ŭ����");
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
                    Api.Group(group_id, "ɾ���ɹ���\n���λ��ѣ�100���\nʣ���ң�"+user.money);
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
            Api.Group(group_id, name + "��ǰ�øжȣ�" + user.heart + "\n��ǰ���Ϊ��" + user.money);
        }
        else
        {
            Api.Group(group_id, name + "��˭��Ϊʲô��һ��ӡ��û�У�\nps:ͨ������\"����������\"���������Ŷ��");
            return;
        }
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
            Api.Group(group_id, "���� " + name + " �ǰɣ��������Զ����������ģ�");
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
        if (str[0].Contains("Static"))
        {
            if (user.money >= 100)
            {
                try
                {
                    Tip.Add(str[1].Substring(2), str[2].Substring(2));
                    user.money -= 100;
                    Api.Group(group_id, "��ӳɹ�����ô���100��ң��Ҿ�������Ŷ~\n��ǰ���" + user.money); 
                    using (StreamWriter sr1 = new StreamWriter("MessageStatic.dat"))
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
                Api.Group(group_id, "�ƺ����Ľ���е㲻����~" + name + "\n��ǰ��ң�" + user.money + "\n�����ң�100");
            }
        }
        return true;
    }
}

