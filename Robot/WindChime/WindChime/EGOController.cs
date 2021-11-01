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
        str += "\n生命值：" + hero.Hp;
        str += "\n精神值：" + hero.Mp;
        str += "\n" + Det;
        str += "\n武器：" + hero.weapon.Name;
        str += "\n武器攻击类型：" + hero.weapon.Type;
        str += "\n攻击间隔：" + hero.weapon.AttackSpeed;
        str += "\nboss速度：" + (int)(hero.Speed+50);
        str += "\n攻击伤害：" + String.Format("{0:F2}", (hero.weapon.BaseDamage * Math.Pow(1.05,hero.WeaponUp))) + "~" + String.Format("{0:F2}", ((hero.weapon.BaseDamage  + hero.weapon.FloatDamage) * Math.Pow(1.05, hero.WeaponUp)));
        str += "\n物理抗性(RED)：" + String.Format("{0:F2}", hero.RED);
        str += "\n精神抗性(WHITE)：" + String.Format("{0:F2}", hero.WHITE);
        str += "\n腐蚀抗性(BLACK)：" + String.Format("{0:F2}", hero.BLACK);
        str += "\n灵魂抗性(PALE)：" + String.Format("{0:F2}", hero.PALE);
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
        Console.WriteLine("Bosses 加载完毕...");
    }
    public static bool Main(string group_id, string user_id, string name, string sp)
    {
        if (sp.Contains("挑战查询"))
        {
            FindBosses(group_id, user_id, name, sp);
            return true;
        }
        if (sp.Contains("挑战"))
        {
            Challenge(group_id, user_id, name, sp);
            return true;
        }
        if (sp.Contains("更换") || sp.Contains("切换"))
        {
            ChangeEGO(group_id, user_id, name, sp);
            return true;
        }
        if(sp == "查询缺少的武器")
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
        str += "\n武器：";
        for (int i = 0; i < GameManager.weapon.Count; i++)
            if (!usf.AllWeapon[i])
                str += $"{GameManager.weapon[i].Name} ";
        str += "\n护甲：";
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
            if (str[0].Contains("武器"))
            {
                UserInfo A = ReaderWriter.GetUserInfo(user_id);
                string strs = "";
                strs += "你拥有的EGO武器：";
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
                Api.Group(group_id, strs + "\nZAYIN：" + strz + "\nTETH：" + strt + "\nHE：" + strh + "\nWAW：" + strw + "\nALEPH：" + stra + "\nFINAL：" + strf);
                return;
            }
            if (str[0].Contains("武器"))
            {
                UserInfo A = ReaderWriter.GetUserInfo(user_id);
                string strs = "";
                strs += "你拥有的EGO护甲：";
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
                        strf += GameManager.armor[i] + "(" + GameManager.armor[i].Easy + ")、";
                }
                Api.Group(group_id, strs + "\nZAYIN：" + strz + "\nTETH：" + strt + "\nHE：" + strh + "\nWAW：" + strw + "\nALEPH：" + stra + "\nFINAL：" + strf);
                return;
            }
            //Api./*G*/roup(group_id, "请输入 “更换护甲” 或者 “更换武器” 和你的ego（中间用空格隔开）来切换ego吧！");
            return;
        }
        if (str[0].Contains("更换ego") || str[0].Contains("切换ego"))
        {
            string mes = "";
            for (int i = 0; i < GameManager.armor.Count; i++)
                if (GameManager.armor[i].Name.Contains(str[1]))
                    if (usf.AllArmor[i])
                    {
                        usf.EGOArmor = i;
                        mes += "护甲切换为"+ GameManager.armor[usf.EGOArmor].Name;
                        ReaderWriter.WriteToFile(usf);
                        break;
                    }
            for (int i = 0; i < GameManager.weapon.Count; i++)
                if (GameManager.weapon[i].Name.Contains(str[1]))
                    if (usf.AllWeapon[i])
                    {
                        usf.EGOWeapon = i;
                        mes += "\n武器切换为" + GameManager.weapon[usf.EGOWeapon].Name;
                        ReaderWriter.WriteToFile(usf);
                        break;
                    }
            Api.Group(group_id, mes);
            return;
        }
        if (str[0].Contains("更换护甲") || str[0].Contains("切换护甲"))
        {
            for (int i = 0; i < GameManager.armor.Count; i++)
                if (GameManager.armor[i].Name.Contains(str[1]))
                    if (usf.AllArmor[i])
                    {
                        usf.EGOArmor = i;
                        Api.Group(group_id, Api.GetAtMessage(user_id) + "切换成功！");
                        ReaderWriter.WriteToFile(usf);
                        return;
                    }
            Api.Group(group_id, Api.GetAtMessage(user_id) + "你没有"+ str[1] + "的护甲！");
            return;
        }
        if (str[0].Contains("更换武器") || str[0].Contains("切换武器"))
        {
            for (int i = 0; i < GameManager.weapon.Count; i++)
                if (GameManager.weapon[i].Name.Contains(str[1]))
                    if (usf.AllWeapon[i])
                    {
                        usf.EGOWeapon = i;
                        Api.Group(group_id, Api.GetAtMessage(user_id) + "切换成功！");
                        ReaderWriter.WriteToFile(usf);
                        return;
                    }
            Api.Group(group_id, Api.GetAtMessage(user_id) + "你没有" + str[1] + "的武器！");
            return;
        }

    }
    public static void Challenge(string group_id, string user_id, string name, string sp)
    {
        string[] str = sp.Split(" ");
        if (str.Length == 1)
        {
            string ss = "ZAYIN：";
            foreach(var a in Bosses)
                if (a.type.Contains("ZAYIN"))
                    ss += a.hero.name + " ";
            ss += "\nTETH：";
            foreach (var a in Bosses)
                if (a.type.Contains("TETH"))
                    ss += a.hero.name + " ";
            ss += "\nHE：";
            foreach (var a in Bosses)
                if (a.type.Contains("HE"))
                    ss += a.hero.name + " ";
            ss += "\nWAW：";
            foreach (var a in Bosses)
                if (a.type.Contains("WAW"))
                    ss += a.hero.name + " ";
            ss += "\nALEPH：";
            foreach (var a in Bosses)
                if (a.type.Contains("ALEPH"))
                    ss += a.hero.name + " ";
            ss += "\nFINAL：";
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
            Api.Group(group_id, "找不到" + str[1] + "相关的boss");
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
            Api.Group(group_id, "需要金币大于"+ getBossMoney(Bosses[BossType].type) + "个才能开始挑战"+ Bosses[BossType].hero.name+ "。");
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
            string ss = "挑战胜利";
            if (bs.hero.name == "白雪公主的苹果" && !usf.Inhibition[0])
            {
                ss += "\n昂首阔步的信念！\nMalkuth抑制成功，控制部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），速度永久提高30点已经刷新今日工作次数。";
                usf.Inhibition[0] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "魔弹射手" && !usf.Inhibition[1])
            {
                ss += "\n卓尔不凡的理性！\nYesod抑制成功，情报部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），工作获得的PE-BOX提高25%，已经刷新今日工作次数。";
                usf.Inhibition[1] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "黑天鹅之梦" && !usf.Inhibition[2])
            {
                ss += "\n愈加善良的希望！\nHod抑制成功，培训部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），大于100属性时工作中获得的属性提升+1，已经刷新今日工作次数。";
                usf.Inhibition[2] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "沉默乐团" && !usf.Inhibition[3])
            {
                ss += "\n生存下去的勇气！\nNetzach抑制成功，安保部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），战斗中每一回合恢复1点生命和精神，已经刷新今日工作次数。";
                usf.Inhibition[3] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "虚无弄臣" && !usf.Inhibition[4])
            {
                ss += "\n存在意义的憧憬！\nTiphereth抑制成功，中央本部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），已经刷新今日工作次数。";
                usf.Inhibition[4] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "奥兹玛" && !usf.Inhibition[5])
            {
                ss += "\n值得衬托的信任！\nChesed抑制成功，福利部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），死亡之前有概率回复所有生命值已经刷新今日工作次数。";
                usf.Inhibition[5] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "一无所有" && !usf.Inhibition[6])
            {
                ss += "\n守护他人的决意！\nGebura抑制成功，惩戒部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），攻击力提高10%已经刷新今日工作次数。";
                usf.Inhibition[6] = true;
                usf.WorkTime = 0;
            }

            if (bs.hero.name == "白夜" && !usf.Inhibition[7])
            {
                ss += "\n拥抱希望，创造未来！\nHokma抑制成功，记录部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），所有属性上限开放到180，已经刷新今日工作次数。";
                usf.Inhibition[7] = true;
                usf.WorkTime = 0;
                usf.Hokma = 1;
            }
            if (bs.hero.name == "终末鸟" && !usf.Inhibition[8])
            {
                ss += "\n直面恐惧，斩断循环！\nBinah抑制成功，研发部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），抗性提高0.1已经刷新今日工作次数。";
                usf.Inhibition[8] = true;
                usf.WorkTime = 0;
            }
            if (bs.hero.name == "噤默处子" && !usf.Inhibition[9])
            {
                ss += "\n光之树！\nKether抑制成功，构筑部已经不受逆卡巴拉能量融毁影响（每日工作次数+1），速度永久提高30点已经刷新今日工作次数。";
                usf.Inhibition[9] = true;
                usf.WorkTime = 0;
            }
            if (ReaderWriter.random.NextDouble() <= 0.3f && bs.DropWeapon != 0)
            {
                ss += "\n恭喜你获得EGO武器：" + GameManager.weapon[bs.DropWeapon].Name;
                if (usf.AllWeapon[bs.DropWeapon]){
                    var a = getBossMoney(Bosses[BossType].type);
                    ss += "（多余）返还" + a + "金币";
                    usf.money += a;
                }
                usf.AllWeapon[bs.DropWeapon] = true;
            }
            if (ReaderWriter.random.NextDouble() <= 0.3f && bs.DropArrmr != 0)
            {
                ss += "\n恭喜你获得EGO护甲：" + GameManager.armor[bs.DropArrmr].Name;
                if (usf.AllArmor[bs.DropArrmr])
                {
                    var a = getBossMoney(Bosses[BossType].type);
                    ss += "（多余）返还" + a + "金币";
                    usf.money += a;
                }
                usf.AllArmor[bs.DropArrmr] = true;
            }
            Api.Group(group_id, Api.GetAtMessage(user_id) + ss);
        }
        else
        {
            Api.Group(group_id, "挑战失败...，如果不知道为什么可以输入 挑战查询+boss名字进行查询boss详细信息哦！");
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
            Api.Group(group_id, "找不到" + str[1] + "相关的boss");
            return;
        }
        UserInfo usf = ReaderWriter.GetUserInfo(user_id);
        Hero h = new Hero(usf);
        string s = "\n你每次会受到的伤害：" + String.Format("{0:F2}", Bosses[BossType].mind * h.GetDef(Bosses[BossType].hero.weapon.Type)) + "~" + String.Format("{0:F2}", Bosses[BossType].maxd * h.GetDef(Bosses[BossType].hero.weapon.Type));
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

        string s2 = $"\n预计攻击次数{String.Format("{0:F2}",k)}";
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