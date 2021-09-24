using System;
using System.Collections.Generic;
using System.IO;

public enum AttackTpye
{
    RED, WHITE, BLACK, PALE, HEAL
}
public class Damage
{
    public AttackTpye type;
    public double damage;
}
public class Hero
{
    /// <summary>
    /// 被动
    /// </summary>
    public Possvie pos;
    public static double max(double A, double B)
    {
        if (A > B)
            return A;
        return B;
    }
    public Hero()
    {

    }
    public Hero(UserInfo user)
    {
        name = user.name;
        Hp = user.Courage + 50;
        Mp = user.Cautious + 50;
        Dodge = user.Discipline;
        Speed = user.Justice;
        weapon = GameManager.weapon[user.EGOWeapon];
        if (weapon.pos != null)
            weapon.pos.Init();
        Hp += weapon.Health;
        Mp += weapon.Sprite;
        Dodge += (int)Math.Round(weapon.Dodge);
        Speed += (int)Math.Round(weapon.Speed);
        Armor ar = GameManager.armor[user.EGOArmor];
        Hp += ar.Health;
        Mp += ar.Sprite;
        Dodge += (int)Math.Round(ar.Dodge);
        Speed += (int)Math.Round(ar.Speed);
        WeaponUp = user.WeaponIncrease;
        ArmorUp = user.ArmorIncrease;

        RED = GameManager.armor[user.EGOArmor].RED;
        RED = max(RED * (1 - user.ArmorIncrease * 0.04), RED - (user.ArmorIncrease * 0.03));
        WHITE = GameManager.armor[user.EGOArmor].WHITE;
        WHITE = max(WHITE * (1 - user.ArmorIncrease * 0.04), WHITE - (user.ArmorIncrease * 0.03));
        BLACK = GameManager.armor[user.EGOArmor].BLACK;
        BLACK = max(BLACK * (1 - user.ArmorIncrease * 0.04), BLACK - (user.ArmorIncrease * 0.03));
        PALE = GameManager.armor[user.EGOArmor].PALE;
        PALE = max(PALE * (1 - user.ArmorIncrease * 0.04), PALE - (user.ArmorIncrease * 0.03));
        if (user.Inhibition[0])
            Speed += 50;
    }
    public int WeaponUp;
    public int ArmorUp;
    public string name;
    /// <summary>
    /// 红色
    /// </summary>
    public double Hp;
    /// <summary>
    /// 白色
    /// </summary>
    public double Mp;
    /// <summary>
    /// 速度
    /// </summary>
    public int Speed;
    /// <summary>
    /// 闪避率
    /// </summary>
    public int Dodge;
    /// <summary>
    /// 武器
    /// </summary>
    public Weapon weapon;
    public double RED;
    public double WHITE;
    public double BLACK;
    public double PALE;
    public int EasyAttack;
    public int DefAttack;
    public int Strong;
    public int weak;
    public int bleeding;
    /// <summary>
    /// 是否死了
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        if (Hp <= 0) return true;
        if (Mp <= 0) return true;
        else return false;
    }
    public void UnderAttack(Damage dam)
    {
        dam.damage += EasyAttack;
        dam.damage -= DefAttack;
        if (dam.damage < 0) dam.damage = 0;
        if (dam.type == AttackTpye.RED)
            Hp -= (dam.damage * RED);

        if (dam.type == AttackTpye.WHITE)
            Mp -= (dam.damage * WHITE);

        if (dam.type == AttackTpye.BLACK)
        {
            Hp -= (dam.damage * BLACK * 0.75f);
            Mp -= (dam.damage * BLACK * 0.75f);
        }

        if (dam.type == AttackTpye.PALE)
            Hp -= (dam.damage * PALE);

        if (dam.type == AttackTpye.HEAL)
            Hp += dam.damage;
    }
    public override string ToString()
    {
        string str = "";
        str += "\n生命值：" + Hp;
        str += "\n精神值：" + Mp;
        str += "\n闪避率：" + Dodge * 0.25 + "%";
        str += "\n攻击速度：" + (Speed + 50);
        return str;
    }
    public double GetDef(AttackTpye tpye)
    {
        if (tpye == AttackTpye.BLACK)
            return BLACK;
        if (tpye == AttackTpye.RED)
            return RED;
        if (tpye == AttackTpye.WHITE)
            return WHITE;
        return PALE;
    }
}
public class War
{
    public static List<string> Rank = new List<string>();
    public War()
    {
        using (StreamReader sr = new StreamReader("Rank.dat"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                UserInfo user = ReaderWriter.GetUserInfo(line);
                user.Rank = Rank.Count;
                user.name = "null(未知)";
                ReaderWriter.WriteToFile(user);
                Rank.Add(line);
            }
        }
    }
    public static void Save()
    {
        using (StreamWriter sw = new StreamWriter("Rank.dat"))
            foreach (var i in Rank)
                sw.WriteLine(i);
    }
    public static string FindBYRank(int rank)
    {
        return Rank[rank];
    }
    public static int FindBYid(string id)
    {
        for (int i = 0; i < Rank.Count; i++)
        {
            if (id == Rank[i])
                return i;
        }
        return -1;
    }
    public static bool main(string group_id, string user_id, string name, string message)
    {
        string[] ss = message.Split(" ");
        if (message.Contains("Battle"))
        {
            UserInfo A, B;
            A = ReaderWriter.GetUserInfo(user_id);
            if (A.SoloCount <= 0)
            {
                Api.Group(group_id, "已达到今日战斗上限！");
                return true;
            }
            A.SoloCount--;
            B = ReaderWriter.GetUserInfo(ss[1]);
            if (!B.CanGet)
            {
                Api.Group(group_id, "无法找到" + ss[1]);
                return true;
            }
            int ret = battle(new Hero(A), new Hero(B), user_id);
            if (ret == 1)
            {
                int k = Math.Min(A.money / 2, B.money / 10);
                A.money += k;
                B.money -= k;
                Api.Group(group_id, "在与" + B.name + "的战斗中你胜利了，获得了" + k + "金币\n如需获取战斗详情，请在与风铃是好友的情况下进行战斗。");
            }
            else if (ret == 0)
            {
                Api.Group(group_id, "在与" + B.name + "的战斗中平局了，没有获得任何收益，但是也没有任何损失。\n如需获取战斗详情，请在与风铃是好友的情况下进行战斗。");
            }
            else if (ret == -1)
            {
                int k = A.money / 10;
                Api.Group(group_id, "在与" + B.name + "的战斗中输了，失去了" + k + "金币\n如需获取战斗详情，请在与风铃是好友的情况下进行战斗。");
                A.money -= k;
                B.money += k;
            }
            ReaderWriter.WriteToFile(A);
            ReaderWriter.WriteToFile(B);
            return true;
        }
        if (message == "查询EGO" || message == "查询ego" || message == "EGO查询" || message == "ego查询")
        {
            
            UserInfo A = ReaderWriter.GetUserInfo(user_id);
            string str = "";
            Weapon wp = GameManager.weapon[A.EGOWeapon];
            Armor ar = GameManager.armor[A.EGOArmor];
            str += "当前佩戴ego武器：➕" + A.WeaponIncrease + wp.Name;
            str += "\n武器攻击类型：" + wp.Type;
            str += "\n攻击速度：" + wp.AttackSpeed;
            if (wp.Detail != null)
                str += wp.Detail;
            double BD = wp.BaseDamage;
            double FD = wp.FloatDamage;
            double WI = A.WeaponIncrease;
            str += "\n攻击力：" + String.Format("{0:F2}", (BD * (1 + WI * 0.2))) + "~" + String.Format("{0:F2}", ((BD + FD) * (1 + WI * 0.2)));
            str += "\n当前佩戴EGO防具：➕" + A.ArmorIncrease + ar.Name;
            str += "\n物理抗性(RED)：" + String.Format("{0:F2}", Hero.max(ar.RED * (1 - A.ArmorIncrease * 0.04), ar.RED - (A.ArmorIncrease * 0.03)));
            str += "\n精神抗性(WHITE)：" + String.Format("{0:F2}", Hero.max(ar.WHITE * (1 - A.ArmorIncrease * 0.04), ar.WHITE - (A.ArmorIncrease * 0.03)));
            str += "\n腐蚀抗性(BLACK)：" + String.Format("{0:F2}", Hero.max(ar.BLACK * (1 - A.ArmorIncrease * 0.04), ar.BLACK - (A.ArmorIncrease * 0.03)));
            str += "\n灵魂抗性(PALE)：" + String.Format("{0:F2}", Hero.max(ar.PALE * (1 - A.ArmorIncrease * 0.04), ar.PALE - (A.ArmorIncrease * 0.03)));

            
                str += "\n你拥有的EGO武器：";
                for (int i = 0; i < GameManager.weapon.Count; i++)
                    if (A.AllWeapon[i])
                        str += GameManager.weapon[i].Name + " ";
                str += "\n你拥有的EGO护甲：";
                for (int i = 0; i < GameManager.armor.Count; i++)
                    if (A.AllArmor[i])
                        str += GameManager.armor[i].Name + " ";
            
            Api.Group(group_id, str);
            return true;
        }
        if (message == "天梯赛" && message.Length <= 10)
        {
            TNT(group_id, user_id);
            Save();
        }
        if (message == "领取天梯赛奖励" && message.Length <= 10)
        {
            GetWER(group_id, user_id, name);
            Save();
        }
        return false;
    }
    public static void GetWER(string group_id, string user_id,string name)
    {
        UserInfo user;
        user = ReaderWriter.GetUserInfo(user_id);
        if (user.Rank == -1)
        {
            Api.Group(group_id, "请先参加一次天梯赛，才能领取奖励哦！");
            return;
        }
        if (user.LadderRew == 1)
        {
            Api.Group(group_id, name + "您今天已经领取过奖励了...要不明天再来呢，风铃手上的钱也不多了...");
            return;
        }
        user.LadderRew = 1;
        if (user.Rank == 0)
        {
            Api.Group(group_id, name + "今天的比赛，你拿到了第一名哦！恭喜一下自己吧！收下这个2000金币吧！这是风铃给你的奖励哦！");
            user.money += 2000;
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (user.Rank == 1)
        {
            Api.Group(group_id, name + "今天的比赛，你拿到了第二名！恭喜一下自己吧！收下这个1500金币吧！这是风铃给你的奖励哦！");
            user.money += 1500;
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (user.Rank == 2)
        {
            Api.Group(group_id, name + "今天的比赛，你拿到了第三名,恭喜一下自己吧！收下这个1000金币吧！这是风铃给你的奖励哦！");
            user.money += 1000;
            ReaderWriter.WriteToFile(user);
            return;
        }
        float up = user.Rank;
        float down = Rank.Count;
        float cs = up / down;
        if (cs <= 0.1)
        {
            Api.Group(group_id, name + "在今天的的比赛中，获得了前10%的名次，而这750金币，则是今天的奖励！");
            user.money += 750;
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (cs <= 0.3)
        {
            Api.Group(group_id, name + "在今天的的比赛中，获得了前30%的名次，而这500金币，则是今天的奖励！");
            user.money += 500;
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (cs <= 0.5)
        {
            Api.Group(group_id, name + "在今天的的比赛中，获得了前50%的名次，这是你的200金币。");
            user.money += 200;
            ReaderWriter.WriteToFile(user);
            return;
        }
        Api.Group(group_id, name + "在今天的的比赛中获得了100金币！");
        user.money += 100;
        ReaderWriter.WriteToFile(user);
        return;
    }
    public static void TNT(string group_id, string user_id)
    {
        UserInfo A, B;
        A = ReaderWriter.GetUserInfo(user_id);
        if (A.money < 100)
        {
            Api.Group(group_id, "参加天梯赛至少需要100金币！");
            return;
        }
        if (A.Rank == -1)
        {
            Rank.Add(A.id);
            A.Rank = Rank.Count-1;
            ReaderWriter.WriteToFile(A);
        }
        if (A.Rank == 0)
        {
            Api.Group(group_id, "你现在是位于天梯排行榜第一名了！已经不再需要战斗了！");
            ReaderWriter.WriteToFile(A);
            return;
        }
        B = ReaderWriter.GetUserInfo(Rank[A.Rank-1]);
        Hero AA, BB;
        AA = new Hero(A);
        BB = new Hero(B);
        int result = battle(AA, BB, A.id);
        if (result == 1)
        {
            int k = B.Rank;
            B.Rank = A.Rank;
            A.Rank = k;

            Rank[A.Rank] = A.id;
            Rank[B.Rank] = B.id;
            Api.Group(group_id, "恭喜你战胜了位于天梯排行榜Rank." + (A.Rank+1) + "的" + B.name + "你成功成为了天梯排行榜的Rank." + (A.Rank+1));
        }
        else
        {
            Api.Group(group_id, "挑战失败了...如果有添加风铃好友，则可以查看战斗详情。");
        }
        ReaderWriter.WriteToFile(A);
        ReaderWriter.WriteToFile(B);
    }
    public static int battle(Hero A, Hero B, string user_id)
    {
        Console.WriteLine(A.name + " VS " + B.name);
        Random rd = new Random();
        int speedA = 0, speedB = 0;
        int cnt = 0;
        string str = "";
        A.weak = 0;
        A.Strong = 0;
        A.EasyAttack = 0;
        A.DefAttack = 0;
        A.bleeding = 0;
        B.weak = 0;
        B.Strong = 0;
        B.EasyAttack = 0;
        B.DefAttack = 0;
        B.bleeding = 0;
        if (A.pos != null)
            A.pos.Init();
        if (B.pos != null)
            B.pos.Init();
        if (A.weapon.pos != null)
            A.weapon.pos.Init();
        if (B.weapon.pos != null)
            B.weapon.pos.Init();
        while (!A.IsDead() && !B.IsDead() && cnt <= 800)
        {
            if (A.pos != null)
                A.pos.StartTurn(A,B);
            if (B.pos != null)
                B.pos.StartTurn(B,A);
            if (A.weapon.pos != null)
                A.weapon.pos.StartTurn(A,B);
            if (B.weapon.pos != null)
                B.weapon.pos.StartTurn(B,A);
            cnt++;
            if (cnt >= 800)
                break;
            speedA += (A.Speed + 50);
            speedB += (B.Speed + 50);
            if (speedA >= A.weapon.AttackSpeed)
            {
                speedA -= A.weapon.AttackSpeed;
                str += "\n你" + A.weapon.Attack(A, B);
            }
            if (speedB >= B.weapon.AttackSpeed)
            {
                speedB -= B.weapon.AttackSpeed;
                str += "\n" + B.name + B.weapon.Attack(B, A);
            }
            Console.Write(B.RED + "  " + B.WHITE + "  " + B.PALE + "  " + B.BLACK);
        }
        //Api.Private(user_id, str);
        Api.Private(user_id, str);
        if (A.IsDead() && B.IsDead()) return 0;
        if (A.IsDead()) return -1;
        if (B.IsDead()) return 1;
        return 114514;
    }
}

