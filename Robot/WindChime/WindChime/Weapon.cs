using System;
using System.Collections.Generic;
public class Item
{
    public double Health = 0;
    public double Sprite = 0;
    public double Dodge = 0;
    public double Speed = 0;
    public string name;
}
public class Weapon : Item
{
    public int speed;
    public string atk;
    public virtual string Attack(Hero Target, double TZ, double damagefloat)
    {
        return "当前没有武器,无法造成伤害";
    }
    public virtual string Attack(Hero Self ,Hero Target, double TZ, double damagefloat)
    {
        return Attack(Target,TZ,damagefloat);
    }
}
/// <summary>
/// 镇暴棍
/// </summary>
public class AntiViolence : Weapon
{
    public AntiViolence() { speed = 1200;name = "镇暴棍"; atk = "RED 3-5"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "使用了 镇暴棍 但是被" + Target.name + "闪避了！";
        }
        Damage damage;
        damage.damage = 3 + damagefloat * 2;
        damage.type = AttackTpye.RED;
        Target.UnderAttack(damage);
        return "使用了 镇暴棍 对 " + Target.name + " 造成了 " + Math.Round(damage.damage) + " 点 红色 伤害！"+ Target.name + "剩余血量：" + Math.Round(Target.Hp);
    }
}
/// <summary>
/// 割腕者
/// </summary>
public class WristCutter : Weapon
{
    public WristCutter() { speed = 300; name = "割腕者"; atk = "WHITE 2-4"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "使用了 割腕者 但是被" + Target.name + "闪避了！";
        }
        Damage damage; 
        if (TZ <= 0.1)
        {
            damage.damage = 10 + damagefloat * 10;
            damage.type = AttackTpye.RED;
            Target.UnderAttack(damage);
            return "使用了 ***割腕者触发了特殊攻击*** 对 " + Target.name + " 造成了 " + Math.Round(damage.damage) + " 点 红色 伤害！" + Target.name + "剩余血量：" + Math.Round(Target.Hp);
        }
        damage.damage = 2 + damagefloat * 2;
        damage.type = AttackTpye.WHITE;
        Target.UnderAttack(damage);
        return "使用了 割腕者 对 " + Target.name + " 造成了 " + Math.Round(damage.damage) + " 点 白色 伤害！" + Target.name + "剩余精神：" + Math.Round(Target.Mp);
    }
}
/// <summary>
/// 声呐
/// </summary>
public class Sonar : Weapon
{
    public Sonar() { speed = 300; name = "声呐"; atk = "WHITE 14-28"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "使用了 声呐 但是被" + Target.name + "闪避了！";
        }
        Damage damage;
        damage.damage = 14 + damagefloat * 14;
        damage.type = AttackTpye.WHITE;
        Target.UnderAttack(damage);
        return "使用了 声呐 对 " + Target.name + " 造成了 " + Math.Round(damage.damage) + " 点 白色 伤害！" + Target.name + "剩余精神：" + Math.Round(Target.Mp);
    }
}
public class Angle : Weapon
{
    public static int cnt = 0;
    public Angle() { speed = 600; name = "天使的职责"; atk = "BLACK 20-22"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "使用了 天使的职责 但是被" + Target.name + "闪避了！";
        }
        Damage damage;
        if (cnt == 3)
        {
            cnt = 0;
            damage.damage = 80;
            damage.type = AttackTpye.RED;
            Target.UnderAttack(damage);
            return "使用了 ***天使的职责触发了特殊攻击*** 对 " + Target.name + " 造成了 " + Math.Round(damage.damage) + " 点 红色 伤害！" + Target.name + "剩余血量：" + Math.Round(Target.Hp);
        }
        cnt++;
        damage.damage = 2 + damagefloat * 2;
        damage.type = AttackTpye.BLACK;
        Target.UnderAttack(damage);
        return "使用了 天使的职责 对 " + Target.name + " 造成了 " + Math.Round(damage.damage * 0.5) + " 点 白色 和 红色 伤害！" + Target.name + "剩余精神：" + Math.Round(Target.Mp) + "剩余血量：" + Math.Round(Target.Hp);
    }
}
public class WindChime : Weapon
{
    public static int cnt = 0;
    public WindChime() { speed = 600; name = "天使的职责"; atk = "BLACK 20-22"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "使用了 天使的职责 但是被" + Target.name + "闪避了！";
        }
        Damage damage;
        if (cnt == 3)
        {
            cnt = 0;
            damage.damage = 80;
            damage.type = AttackTpye.RED;
            Target.UnderAttack(damage);
            return "使用了 ***天使的职责触发了特殊攻击*** 对 " + Target.name + " 造成了 " + Math.Round(damage.damage) + " 点 红色 伤害！" + Target.name + "剩余血量：" + Math.Round(Target.Hp);
        }
        cnt++;
        damage.damage = 2 + damagefloat * 2;
        damage.type = AttackTpye.BLACK;
        Target.UnderAttack(damage);
        return "使用了 天使的职责 对 " + Target.name + " 造成了 " + Math.Round(damage.damage * 0.5) + " 点 白色 和 红色 伤害！" + Target.name + "剩余精神：" + Math.Round(Target.Mp) + "剩余血量：" + Math.Round(Target.Hp);
    }
}
public class Armor : Item
{

    public double RED;
    public double WHITE;
    public double BLACK;
    public double PALE;
}
public class SuitArmor : Armor
{
    public SuitArmor() { RED = 1; WHITE = 1; BLACK = 1.5; PALE = 2;name = "西装"; }
}
public class WristCutterArmor : Armor
{
    public WristCutterArmor() { RED = 1; WHITE = 0.6; BLACK = 1.2; PALE = 2; name = "割腕者"; }
}
public class SonarArmor : Armor
{
    public SonarArmor() { RED = 0.7; WHITE = 0.7; BLACK = 1.0; PALE = 1.0; name = "声呐"; Speed = 50; }
}
public class SuperHell : Armor
{
    public SuperHell() { RED = 1.5; WHITE = 1.0; BLACK = 0.5; PALE = 1.5; name = "特制地狱"; Health = -30; Sprite = -30; Dodge += 30; Speed += 30;}
}
public class GameManager
{
    public static List<Weapon> weapon = new List<Weapon>();
    public static List<Armor> armor = new List<Armor>();
    public GameManager()
    {
        weapon.Add(new AntiViolence());
        weapon.Add(new WristCutter());
        weapon.Add(new Sonar());
        weapon.Add(new Angle());

        armor.Add(new SuitArmor());
        armor.Add(new WristCutterArmor());
        armor.Add(new SonarArmor());
        armor.Add(new SuperHell());
    }

}