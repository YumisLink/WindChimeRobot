using System;
using System.Collections.Generic;
using System.IO;

public class Boss
{
    public Hero hero;
    public int DropWeapon;
    public int DropArrmr;
    public string type;
    public Possvie pos;
    public string Det;
    public double mind => hero.weapon.BaseDamage * (1 + hero.WeaponUp * 0.2);
    public double maxd => (hero.weapon.BaseDamage + hero.weapon.FloatDamage) * (1 + hero.WeaponUp * 0.2);
    public override string ToString()
    {
        string str = hero.name;
        str += "\n����ֵ��" + hero.Hp;
        str += "\n����ֵ��" + hero.Mp;
        str += "\n" + Det;
        str += "\n������" + hero.weapon.Name;
        str += "\n�����������ͣ�" + hero.weapon.Type;
        str += "\n���������" + hero.weapon.AttackSpeed;
        str += "\nboss�ٶȣ�" + hero.Speed+50;
        str += "\n�����˺���" + String.Format("{0:F2}", (hero.weapon.BaseDamage * (1 + hero.WeaponUp * 0.2))) + "~" + String.Format("{0:F2}", ((hero.weapon.BaseDamage  + hero.weapon.FloatDamage) * (1 + hero.WeaponUp * 0.2)));
        str += "\n������(RED)��" + String.Format("{0:F2}", hero.RED);
        str += "\n������(WHITE)��" + String.Format("{0:F2}", hero.WHITE);
        str += "\n��ʴ����(BLACK)��" + String.Format("{0:F2}", hero.BLACK);
        str += "\n��꿹��(PALE)��" + String.Format("{0:F2}", hero.PALE);
        return str;
    }
}
public class EGOController
{
    public static List<Boss> Bosses = new List<Boss>();
    public EGOController()
    {
        try
        {
            using (StreamReader sr = new StreamReader("Bosses.dat"))
            {
                sr.ReadLine();
                string line;
                int cnt = 0;
                while((line = sr.ReadLine()) != null)
                {
                    string[] str = line.Split("\t");
                    Boss boss = new Boss();
                    Hero hero = new Hero();
                    {
                        hero.name = str[0];
                        hero.Hp = Convert.ToDouble(str[1]);
                        hero.Mp = Convert.ToDouble(str[2]);
                        hero.Dodge = Convert.ToInt32(str[3]);
                        hero.Speed = Convert.ToInt32(str[4]);
                        hero.weapon = GameManager.weapon[Convert.ToInt32(str[5])];
                        hero.WeaponUp = Convert.ToInt32(str[6]);
                        hero.RED = Convert.ToDouble(str[7]);
                        hero.WHITE = Convert.ToDouble(str[8]);
                        hero.BLACK = Convert.ToDouble(str[9]);
                        hero.PALE = Convert.ToDouble(str[10]);
                    }
                    boss.hero = hero;
                    boss.DropWeapon = Convert.ToInt32(str[11]);
                    boss.DropArrmr = Convert.ToInt32(str[12]);
                    boss.type = str[13];
                    boss.Det = str[14];
                    Bosses.Add(boss);
                    Console.WriteLine(str[0]);
                }
            }
        }
        catch(Exception e)
        {
            Api.Private("635691684", e.ToString());
        }
        Bosses[15].hero.pos = new SpiderNest();
        Bosses[16].hero.pos = new Leticia();
        Bosses[17].hero.pos = new BlackSwanDream();

        Bosses[19].hero.pos = new MilkyWay();
        Bosses[22].hero.pos = new SilentOrchestra();
        Console.WriteLine("EGOC �������...");
    }
    public static bool Main(string group_id, string user_id, string name, string sp)
    {
        if (sp.Contains("��ս��ѯ"))
        {
            FindBosses(group_id, user_id, name, sp);
            return true;
        }
        if (sp.Contains("��ս"))
        {
            Challenge(group_id, user_id, name, sp);
            return true;
        }
        if (sp.Contains("��������") || sp.Contains("��������"))
        {
            ChangeEGO(group_id, user_id, name, sp);
            return true;
        }
        if (sp.Contains("����"))
        {
            Api.Group(group_id, "������ ���������ס� ���� ������������ ���л�ego��");
            return true;
        }
        return false;
    }
    public static void ChangeEGO(string group_id, string user_id, string name, string sp)
    {
        UserInfo usf = ReaderWriter.GetUserInfo(user_id);
        string[] str = sp.Split(" ");
        if (str[0].Contains("��������"))
        {
            for (int i = 0; i < GameManager.armor.Count; i++)
                if (GameManager.armor[i].Name.Contains(str[1]))
                    if (usf.AllArmor[i])
                    {
                        usf.EGOArmor = i;
                        Api.Group(group_id, Api.GetAtMessage(user_id) + "�л��ɹ���");
                        ReaderWriter.WriteToFile(usf);
                        return;
                    }
            Api.Group(group_id, Api.GetAtMessage(user_id) + "��û��"+ str[1] + "�Ļ��ף�");
            return;
        }
        if (str[0].Contains("��������"))
        {
            for (int i = 0; i < GameManager.weapon.Count; i++)
                if (GameManager.weapon[i].Name.Contains(str[1]))
                    if (usf.AllWeapon[i])
                    {
                        usf.EGOWeapon = i;
                        Api.Group(group_id, Api.GetAtMessage(user_id) + "�л��ɹ���");
                        ReaderWriter.WriteToFile(usf);
                        return;
                    }
            Api.Group(group_id, Api.GetAtMessage(user_id) + "��û��" + str[1] + "��������");
            return;
        }

    }
    public static void Challenge(string group_id, string user_id, string name, string sp)
    {
        string[] str = sp.Split(" ");
        if (str.Length == 1)
        {
            string ss = "ZAYIN��";
            foreach(var a in Bosses)
                if (a.type.Contains("ZAYIN"))
                    ss += a.hero.name + " ";
            ss += "\nTETH��";
            foreach (var a in Bosses)
                if (a.type.Contains("TETH"))
                    ss += a.hero.name + " ";
            ss += "\nHE��";
            foreach (var a in Bosses)
                if (a.type.Contains("HE"))
                    ss += a.hero.name + " ";
            ss += "\nWAW��";
            foreach (var a in Bosses)
                if (a.type.Contains("WAW"))
                    ss += a.hero.name + " ";
            ss += "\nALEPH��";
            foreach (var a in Bosses)
                if (a.type.Contains("ALEPH"))
                    ss += a.hero.name + " ";
            Api.Group(group_id, ss);
            return;
        }
        int BossType = -1;
        for (int i = 0; i < Bosses.Count; i++)
            if (Bosses[i].hero.name.Contains(str[1]))
            {
                BossType = i;
                break;
            }
        if(BossType == -1)
        {
            Api.Group(group_id, "�Ҳ���" + str[1] + "��ص�boss");
            return;
        }
        double maxhp, maxmp;
        Boss bs = Bosses[BossType];
        maxhp = bs.hero.Hp;
        maxmp = bs.hero.Mp;
        UserInfo usf = ReaderWriter.GetUserInfo(user_id);
        if (usf.money <= 500)
        {
            Api.Group(group_id, "��Ҫ��Ҵ���500�����ܿ�ʼ��ս,ÿ����սZAYIN����100��ң�TETH-200,HE-300,WAW-400,ALPEH-500");
            return;
        }
        if (Bosses[BossType].type == "ZAYIN")
            usf.money -= 100;
        if (Bosses[BossType].type == "TETH")
            usf.money -= 200;
        if (Bosses[BossType].type == "HE")
            usf.money -= 300;
        if (Bosses[BossType].type == "WAW")
            usf.money -= 400;
        if (Bosses[BossType].type == "ALPEH")
            usf.money -= 500;
        Console.WriteLine("??");
        Hero hero = new Hero(usf);
        int k = War.battle(hero, bs.hero, user_id);
        bs.hero.Hp = maxhp;
        bs.hero.Mp = maxmp;
        if (k == 1)
        {
            string ss = "��սʤ��";
            if (bs.hero.name == "��ѩ������ƻ��" && !usf.Inhibition[0])
            {
                ss += "\n�������������\nMalkuth���Ƴɹ������Ʋ��Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[0] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "ħ������" && !usf.Inhibition[1])
            {
                ss += "\n׿�����������ԣ�\nYesod���Ƴɹ����鱨���Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1����������õ�PE-BOX���25%���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[1] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "�����֮��" && !usf.Inhibition[2])
            {
                ss += "\n����������ϣ����\nHod���Ƴɹ�����ѵ���Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1��������100����ʱ�����л�õ���������+1���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[2] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "��Ĭ����" && !usf.Inhibition[3])
            {
                ss += "\n������ȥ��������\nNetzach���Ƴɹ����������Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1����ս����ÿһ�غϻָ�1�������;����Ѿ�ˢ�½��չ���������";
                usf.Inhibition[3] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "����Ū��" && !usf.Inhibition[4])
            {
                ss += "\n����������㽣�\nTiphereth���Ƴɹ������뱾���Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[4] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "˵�ѵĴ���" && !usf.Inhibition[5])
            {
                ss += "\nֵ�ó��е����Σ�\nChesed���Ƴɹ����������Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[5] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "һ������" && !usf.Inhibition[6])
            {
                ss += "\n�ػ����˵ľ��⣡\nGebura���Ƴɹ����ͽ䲿�Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[6] = true;
                usf.WorkTime = 0;
            }

            if (bs.hero.name == "��ҹ" && !usf.Inhibition[7])
            {
                ss += "\nӵ��ϣ��������δ����\nHokma���Ƴɹ�����¼���Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[7] = true;
                usf.WorkTime = 0;
                usf.Hokma = 1;
            }
            if (bs.hero.name == "��ĩ��" && !usf.Inhibition[8])
            {
                ss += "\nֱ��־壬ն��ѭ����\nBinah���Ƴɹ����з����Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[8] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "����" && !usf.Inhibition[9])
            {
                ss += "\n��֮����\nKether���Ƴɹ����������Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[9] = true;
                usf.WorkTime = 0;
            }
            if (ReaderWriter.random.NextDouble() <= 0.3f && bs.DropWeapon != 0)
            {
                usf.AllWeapon[bs.DropWeapon] = true;
                ss += "\n��ϲ����EGO������" + GameManager.weapon[bs.DropWeapon].Name;
            }
            if (ReaderWriter.random.NextDouble() <= 0.3f && bs.DropArrmr != 0)
            {
                usf.AllArmor[bs.DropArrmr] = true;
                ss += "\n��ϲ����EGO���ף�" + GameManager.armor[bs.DropArrmr].Name;
            }
            Api.Group(group_id, Api.GetAtMessage(user_id) + ss);
        }
        else
        {
            Api.Group(group_id, "��սʧ��...");
        }
        ReaderWriter.WriteToFile(usf);
    }
    public static void FindBosses(string group_id, string user_id, string name, string sp)
    {
        string[] str = sp.Split(" ");
        if (str.Length == 1)
            return;
        int BossType = -1;
        for (int i = 0; i < Bosses.Count; i++)
            if (Bosses[i].hero.name.Contains(str[1]))
            {
                BossType = i;
                break;
            }
        if (BossType == -1)
        {
            Api.Group(group_id, "�Ҳ���" + str[1] + "��ص�boss");
            return;
        }
        UserInfo usf = ReaderWriter.GetUserInfo(user_id);
        Hero h = new Hero(usf);
        string s = "\n��ÿ�λ��ܵ����˺���" + String.Format("{0:F2}", Bosses[BossType].mind * h.GetDef(Bosses[BossType].hero.weapon.Type)) + "~" + String.Format("{0:F2}", Bosses[BossType].maxd * h.GetDef(Bosses[BossType].hero.weapon.Type));
        Api.Group(group_id, Bosses[BossType].ToString() + s);
    }
}