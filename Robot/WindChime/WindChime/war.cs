using System;

public enum AttackTpye
{
    RED, WHITE, BLACK, PALE, HEAL
}
public struct Damage
{
    public AttackTpye type;
    public double damage;
}
public class Hero
{
    public Hero(UserInfo user)
    {
        name = user.name;
        Hp = user.Courage;
        Mp = user.Cautious;
        Dodge = user.Discipline;
        Speed = user.Justice;
        weapon = GameManager.weapon[user.EGOWeapon];
        Hp += weapon.Health;
        Mp += weapon.Sprite;
        Dodge += (int)Math.Round(weapon.Dodge);
        Speed += (int)Math.Round(weapon.Speed);
        Armor ar = GameManager.armor[user.EGOArmor];
        Hp += ar.Health;
        Mp += ar.Sprite;
        Dodge += (int)Math.Round(ar.Dodge);
        Speed += (int)Math.Round(ar.Speed);


        RED = GameManager.armor[user.EGOArmor].RED;
        WHITE = GameManager.armor[user.EGOArmor].WHITE;
        BLACK = GameManager.armor[user.EGOArmor].BLACK;
        PALE = GameManager.armor[user.EGOArmor].PALE;
    }
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
        if (dam.type == AttackTpye.RED)
            Hp -= (dam.damage * RED);

        if (dam.type == AttackTpye.WHITE)
            Mp -= (dam.damage * WHITE);

        if (dam.type == AttackTpye.BLACK)
        {
            Hp -= (dam.damage * BLACK * 0.5f);
            Mp -= (dam.damage * BLACK * 0.5f);
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
        str += "\n生命值：" + Speed;
        return str;
    }
}
public class War
{
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
                Api.Group(group_id, "你胜利了，获得了" + k + "金币\n如需获取战斗详情，请在与风铃是好友的情况下进行战斗。");
            }
            else if (ret == 0)
            {
                Api.Group(group_id, "平局了，没有获得任何收益，但是也没有任何损失。\n如需获取战斗详情，请在与风铃是好友的情况下进行战斗。");
            }
            else if (ret == -1)
            {
                int k = A.money / 10;
                Api.Group(group_id, "你输了，失去了" + k + "金币\n如需获取战斗详情，请在与风铃是好友的情况下进行战斗。");
                A.money -= k;
                B.money += k;
            }
            ReaderWriter.WriteToFile(A);
            ReaderWriter.WriteToFile(B);
            return true;
        }
        if (message == "查询EGO")
        {
            UserInfo A = ReaderWriter.GetUserInfo(user_id);
            if (!A.CanGet)
                return true;
            string str = "";
            Weapon wp =  GameManager.weapon[A.EGOWeapon];
            Armor ar = GameManager.armor[A.EGOArmor];
            str += "当前佩戴ego武器：" + wp.name;
            str += "\n攻击速度：" + wp.speed;
            str += "\n攻击力：" + wp.atk;
            str += "\n当前佩戴EGO防具："+ ar.name;
            str += "\n物理抗性(RED)：" + String.Format("{0:F1}", ar.RED); ;
            str += "\n精神抗性(WHITE)：" + String.Format("{0:F1}", ar.WHITE); ;
            str += "\n腐蚀抗性(BLACK)：" + String.Format("{0:F1}", ar.BLACK); ;
            str += "\n灵魂抗性(PALE)：" + String.Format("{0:F1}", ar.PALE); ;
            Api.Group(group_id, str);
                return true;
        }
        return false;
    }
    public static int battle(Hero A, Hero B, string user_id)
    {
        Random rd = new Random();
        int speedA = 0, speedB = 0;
        int cnt = 0;
        string str = "";
        while (!A.IsDead() && !B.IsDead() && cnt <= 200)
        {
            cnt++;
            speedA += (A.Speed + 100);
            speedB += (B.Speed + 100);
            if (speedA >= A.weapon.speed)
            {
                speedA -= A.weapon.speed;
                str += "\n你" + A.weapon.Attack(B, rd.NextDouble(), rd.NextDouble());
            }
            if (speedB >= B.weapon.speed)
            {
                speedB -= B.weapon.speed;
                str += "\n" + B.name + B.weapon.Attack(A, rd.NextDouble(), rd.NextDouble());
            }
        }
        //Api.Private(user_id, str);
        Api.Private(user_id, str);
        if (A.IsDead() && B.IsDead()) return 0;
        if (A.IsDead()) return -1;
        if (B.IsDead()) return 1;
        return 114514;
    }
}

