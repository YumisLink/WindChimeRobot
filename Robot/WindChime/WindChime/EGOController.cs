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
        str += "\nboss�ٶȣ�" + (int)(hero.Speed+50);
        str += "\n�����˺���" + String.Format("{0:F2}", (hero.weapon.BaseDamage * Math.Pow(1.05,hero.WeaponUp))) + "~" + String.Format("{0:F2}", ((hero.weapon.BaseDamage  + hero.weapon.FloatDamage) * Math.Pow(1.05, hero.WeaponUp)));
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
                        hero.Speed = Convert.ToInt32(str[4]) + 50;
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
                    boss.hero.boss = new Bosses();
                    Bosses.Add(boss);
                }
            }
        }
        catch(Exception e)
        {
            Api.Private("635691684", e.ToString());
        }
        Bosses[13].hero.pos = new MagicShooter();
        Bosses[15].hero.pos = new SpiderNest();
        Bosses[16].hero.pos = new Leticia();
        Bosses[17].hero.pos = new BlackSwanDream();

        Bosses[19].hero.pos = new MilkyWay();
        Bosses[22].hero.pos = new SilentOrchestra();

        Bosses[23].hero.pos = new AbhorQueen();
        Bosses[24].hero.pos = new DespairKnight();
        Bosses[25].hero.pos = new GreedyKing();
        Bosses[26].hero.pos = new AngryAttendant();
        Bosses[27].hero.pos = new VoidCourtiers();

        Bosses[30].hero.pos = new CorpseMountain();
        Bosses[32].hero.pos = new NothingatAll();


        Bosses[36].hero.pos = new Cat();

        Bosses[38].hero.pos = new BigBird();
        Bosses[39].hero.pos = new SPBird();
        Bosses[40].hero.pos = new FinalBird();

        Bosses[41].hero.pos = new InfiltrateHeaven();
        Bosses[42].hero.pos = new Silent();
        Bosses[43].hero.pos = new BlueStar();
        Console.WriteLine("Bosses �������...");
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
        if (sp.Contains("����") || sp.Contains("�л�"))
        {
            ChangeEGO(group_id, user_id, name, sp);
            return true;
        }
        if(sp == "��ѯȱ�ٵ�����")
        {
            FindLostEGO(group_id, user_id, name);
        }
        return false;
    }
    public static void FindLostEGO(string group_id, string user_id, string name)
    {
        string str = Api.GetAtMessage(user_id);
        var usf = ReaderWriter.GetUserInfo(user_id);
        if (!usf.CanGet)
            return;
        str += "\n������";
        for (int i = 0; i < GameManager.weapon.Count; i++)
            if (!usf.AllWeapon[i])
                str += $"{GameManager.weapon[i].Name} ";
        str += "\n���ף�";
        for (int i = 0; i < GameManager.armor.Count; i++)
            if (!usf.AllArmor[i])
                str += $"{GameManager.armor[i].Name} ";
        Api.Group(group_id,str);
        return;

    }
    public static void ChangeEGO(string group_id, string user_id, string name, string sp)
    {
        UserInfo usf = ReaderWriter.GetUserInfo(user_id);
        string[] str = sp.Split(" ");
        if (str.Length < 2)
        {
            if (str[0].Contains("����"))
            {
                UserInfo A = ReaderWriter.GetUserInfo(user_id);
                string strs = "";
                strs += "��ӵ�е�EGO������";
                string strz = "", strt = "", strh = "", strw = "", stra = "", strf = "";
                for (int i = 0; i < GameManager.weapon.Count; i++)
                {
                    if (A.AllWeapon[i] && GameManager.weapon[i].Level == ItemLevel.ZAYIN)
                        strz += GameManager.weapon[i] + "(" + Damage.GetAttackTypeString(GameManager.weapon[i].Type) + GameManager.weapon[i].GetWeaponSpeed() + ")  ";
                    if (A.AllWeapon[i] && GameManager.weapon[i].Level == ItemLevel.TETH)
                        strt += GameManager.weapon[i] + "(" + Damage.GetAttackTypeString(GameManager.weapon[i].Type) + GameManager.weapon[i].GetWeaponSpeed() + ")  ";
                    if (A.AllWeapon[i] && GameManager.weapon[i].Level == ItemLevel.HE)
                        strh += GameManager.weapon[i] + "(" + Damage.GetAttackTypeString(GameManager.weapon[i].Type) + GameManager.weapon[i].GetWeaponSpeed() + ")  ";
                    if (A.AllWeapon[i] && GameManager.weapon[i].Level == ItemLevel.WAW)
                        strw += GameManager.weapon[i] + "(" + Damage.GetAttackTypeString(GameManager.weapon[i].Type) + GameManager.weapon[i].GetWeaponSpeed() + ")  ";
                    if (A.AllWeapon[i] && GameManager.weapon[i].Level == ItemLevel.ALEPH)
                        stra += GameManager.weapon[i] + "(" + Damage.GetAttackTypeString(GameManager.weapon[i].Type) + GameManager.weapon[i].GetWeaponSpeed() + ")  ";
                    if (A.AllWeapon[i] && GameManager.weapon[i].Level == ItemLevel.FINAL)
                        strf += GameManager.weapon[i] + "(" + Damage.GetAttackTypeString(GameManager.weapon[i].Type) + GameManager.weapon[i].GetWeaponSpeed() + ")  ";
                }
                Api.Group(group_id, strs + "\nZAYIN��" + strz + "\nTETH��" + strt + "\nHE��" + strh + "\nWAW��" + strw + "\nALEPH��" + stra + "\nFINAL��" + strf);
                return;
            }
            if (str[0].Contains("����"))
            {
                UserInfo A = ReaderWriter.GetUserInfo(user_id);
                string strs = "";
                strs += "��ӵ�е�EGO���ף�";
                string strz = "", strt = "", strh = "", strw = "", stra = "", strf = "";
                for (int i = 0; i < GameManager.armor.Count; i++)
                {
                    if (A.AllArmor[i] && GameManager.armor[i].Level == ItemLevel.ZAYIN)
                        strz += GameManager.armor[i] + "(" + GameManager.armor[i].Easy + ")  ";
                    if (A.AllArmor[i] && GameManager.armor[i].Level == ItemLevel.TETH)
                        strt += GameManager.armor[i] + "(" + GameManager.armor[i].Easy + ")  ";
                    if (A.AllArmor[i] && GameManager.armor[i].Level == ItemLevel.HE)
                        strh += GameManager.armor[i] + "(" + GameManager.armor[i].Easy + ")  ";
                    if (A.AllArmor[i] && GameManager.armor[i].Level == ItemLevel.WAW)
                        strw += GameManager.armor[i] + "(" + GameManager.armor[i].Easy + ")  ";
                    if (A.AllArmor[i] && GameManager.armor[i].Level == ItemLevel.ALEPH)
                        stra += GameManager.armor[i] + "(" + GameManager.armor[i].Easy + ")  ";
                    if (A.AllArmor[i] && GameManager.armor[i].Level == ItemLevel.FINAL)
                        strf += GameManager.armor[i] + "(" + GameManager.armor[i].Easy + ")��";
                }
                Api.Group(group_id, strs + "\nZAYIN��" + strz + "\nTETH��" + strt + "\nHE��" + strh + "\nWAW��" + strw + "\nALEPH��" + stra + "\nFINAL��" + strf);
                return;
            }
            //Api./*G*/roup(group_id, "������ ���������ס� ���� ������������ �����ego���м��ÿո���������л�ego�ɣ�");
            return;
        }
        if (str[0].Contains("����ego") || str[0].Contains("�л�ego"))
        {
            string mes = "";
            for (int i = 0; i < GameManager.armor.Count; i++)
                if (GameManager.armor[i].Name.Contains(str[1]))
                    if (usf.AllArmor[i])
                    {
                        usf.EGOArmor = i;
                        mes += "�����л�Ϊ"+ GameManager.armor[usf.EGOArmor].Name;
                        ReaderWriter.WriteToFile(usf);
                        break;
                    }
            for (int i = 0; i < GameManager.weapon.Count; i++)
                if (GameManager.weapon[i].Name.Contains(str[1]))
                    if (usf.AllWeapon[i])
                    {
                        usf.EGOWeapon = i;
                        mes += "\n�����л�Ϊ" + GameManager.weapon[usf.EGOWeapon].Name;
                        ReaderWriter.WriteToFile(usf);
                        break;
                    }
            Api.Group(group_id, mes);
            return;
        }
        if (str[0].Contains("��������") || str[0].Contains("�л�����"))
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
        if (str[0].Contains("��������") || str[0].Contains("�л�����"))
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
            ss += "\nFINAL��";
            foreach (var a in Bosses)
                if (a.type.Contains("FINAL"))
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
        double maxhp, maxmp,spd;
        Boss bs = Bosses[BossType];
        maxhp = bs.hero.Hp;
        maxmp = bs.hero.Mp;
        spd = bs.hero.Speed;
        UserInfo usf = ReaderWriter.GetUserInfo(user_id);
        if (usf.money <= getBossMoney(Bosses[BossType].type))
        {
            Api.Group(group_id, "��Ҫ��Ҵ���"+ getBossMoney(Bosses[BossType].type) + "�����ܿ�ʼ��ս"+ Bosses[BossType].hero.name+ "��");
            return;
        }
        usf.money -= getBossMoney(Bosses[BossType].type);
        Hero hero = new Hero(usf);
        Hero boss = new Hero(bs);
        int k = War.Battle(hero, boss, user_id);
        bs.hero.Hp = maxhp;
        bs.hero.Mp = maxmp;
        bs.hero.Speed = (int)spd;
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
                ss += "\n����������㽣�\nTiphereth���Ƴɹ������뱾���Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����Ѿ�ˢ�½��չ���������";
                usf.Inhibition[4] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "������" && !usf.Inhibition[5])
            {
                ss += "\nֵ�ó��е����Σ�\nChesed���Ƴɹ����������Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1��������֮ǰ�и��ʻظ���������ֵ�Ѿ�ˢ�½��չ���������";
                usf.Inhibition[5] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "һ������" && !usf.Inhibition[6])
            {
                ss += "\n�ػ����˵ľ��⣡\nGebura���Ƴɹ����ͽ䲿�Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�������������10%�Ѿ�ˢ�½��չ���������";
                usf.Inhibition[6] = true;
                usf.WorkTime = 0;
            }

            if (bs.hero.name == "��ҹ" && !usf.Inhibition[7])
            {
                ss += "\nӵ��ϣ��������δ����\nHokma���Ƴɹ�����¼���Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1���������������޿��ŵ�180���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[7] = true;
                usf.WorkTime = 0;
                usf.Hokma = 1;
            }
            if (bs.hero.name == "��ĩ��" && !usf.Inhibition[8])
            {
                ss += "\nֱ��־壬ն��ѭ����\nBinah���Ƴɹ����з����Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����������0.1�Ѿ�ˢ�½��չ���������";
                usf.Inhibition[8] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "��Ĭ����" && !usf.Inhibition[9])
            {
                ss += "\n��֮����\nKether���Ƴɹ����������Ѿ������濨���������ڻ�Ӱ�죨ÿ�չ�������+1�����ٶ��������30���Ѿ�ˢ�½��չ���������";
                usf.Inhibition[9] = true;
                usf.WorkTime = 0;
            }
            if (ReaderWriter.random.NextDouble() <= 0.3f && bs.DropWeapon != 0)
            {
                ss += "\n��ϲ����EGO������" + GameManager.weapon[bs.DropWeapon].Name;
                if (usf.AllWeapon[bs.DropWeapon]){
                    var a = getBossMoney(Bosses[BossType].type);
                    ss += "�����ࣩ����" + a + "���";
                    usf.money += a;
                }
                usf.AllWeapon[bs.DropWeapon] = true;
            }
            if (ReaderWriter.random.NextDouble() <= 0.3f && bs.DropArrmr != 0)
            {
                ss += "\n��ϲ����EGO���ף�" + GameManager.armor[bs.DropArrmr].Name;
                if (usf.AllArmor[bs.DropArrmr])
                {
                    var a = getBossMoney(Bosses[BossType].type);
                    ss += "�����ࣩ����" + a + "���";
                    usf.money += a;
                }
                usf.AllArmor[bs.DropArrmr] = true;
            }
            Api.Group(group_id, Api.GetAtMessage(user_id) + ss);
        }
        else
        {
            Api.Group(group_id, "��սʧ��...�������֪��Ϊʲô�������� ��ս��ѯ+boss���ֽ��в�ѯboss��ϸ��ϢŶ��");
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
        double att = GameManager.weapon[usf.EGOWeapon].BaseDamage * Math.Pow(1.07, usf.WeaponIncrease) + GameManager.weapon[usf.EGOWeapon].FloatDamage * Math.Pow(1.07, usf.WeaponIncrease) * 0.5;
        double k = 0;
        if (GameManager.weapon[usf.EGOWeapon].Type == AttackTpye.BLACK)
            k = Math.Min(Bosses[BossType].hero.Hp / (att*0.7), Bosses[BossType].hero.Mp / (att * 0.7 * Bosses[BossType].hero.BLACK));
        if (GameManager.weapon[usf.EGOWeapon].Type == AttackTpye.RED)
            k = Bosses[BossType].hero.Hp /(att * Bosses[BossType].hero.RED);
        if (GameManager.weapon[usf.EGOWeapon].Type == AttackTpye.PALE)
            k = Bosses[BossType].hero.Hp / (att * Bosses[BossType].hero.PALE);
        if (GameManager.weapon[usf.EGOWeapon].Type == AttackTpye.WHITE)
            k = Bosses[BossType].hero.Hp / (att * Bosses[BossType].hero.WHITE);

        string s2 = $"\nԤ�ƹ�������{String.Format("{0:F2}",k)}";
        Api.Group(group_id, Bosses[BossType].ToString() + s + s2);
    }
    public static int getBossMoney(string str)
    {
        if (str == "ZAYIN")
            return 100;
        if (str == "TEHT")
            return 200;
        if (str == "HE")
            return 300;
        if (str == "WAW")
            return 400;
        if (str == "ALPEH")
            return 500;
        return 600;
    }
}