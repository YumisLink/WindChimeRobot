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
        return "��ǰû������,�޷�����˺�";
    }
    public virtual string Attack(Hero Self ,Hero Target, double TZ, double damagefloat)
    {
        return Attack(Target,TZ,damagefloat);
    }
}
/// <summary>
/// �򱩹�
/// </summary>
public class AntiViolence : Weapon
{
    public AntiViolence() { speed = 1200;name = "�򱩹�"; atk = "RED 3-5"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "ʹ���� �򱩹� ���Ǳ�" + Target.name + "�����ˣ�";
        }
        Damage damage;
        damage.damage = 3 + damagefloat * 2;
        damage.type = AttackTpye.RED;
        Target.UnderAttack(damage);
        return "ʹ���� �򱩹� �� " + Target.name + " ����� " + Math.Round(damage.damage) + " �� ��ɫ �˺���"+ Target.name + "ʣ��Ѫ����" + Math.Round(Target.Hp);
    }
}
/// <summary>
/// ������
/// </summary>
public class WristCutter : Weapon
{
    public WristCutter() { speed = 300; name = "������"; atk = "WHITE 2-4"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "ʹ���� ������ ���Ǳ�" + Target.name + "�����ˣ�";
        }
        Damage damage; 
        if (TZ <= 0.1)
        {
            damage.damage = 10 + damagefloat * 10;
            damage.type = AttackTpye.RED;
            Target.UnderAttack(damage);
            return "ʹ���� ***�����ߴ��������⹥��*** �� " + Target.name + " ����� " + Math.Round(damage.damage) + " �� ��ɫ �˺���" + Target.name + "ʣ��Ѫ����" + Math.Round(Target.Hp);
        }
        damage.damage = 2 + damagefloat * 2;
        damage.type = AttackTpye.WHITE;
        Target.UnderAttack(damage);
        return "ʹ���� ������ �� " + Target.name + " ����� " + Math.Round(damage.damage) + " �� ��ɫ �˺���" + Target.name + "ʣ�ྫ��" + Math.Round(Target.Mp);
    }
}
/// <summary>
/// ����
/// </summary>
public class Sonar : Weapon
{
    public Sonar() { speed = 300; name = "����"; atk = "WHITE 14-28"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "ʹ���� ���� ���Ǳ�" + Target.name + "�����ˣ�";
        }
        Damage damage;
        damage.damage = 14 + damagefloat * 14;
        damage.type = AttackTpye.WHITE;
        Target.UnderAttack(damage);
        return "ʹ���� ���� �� " + Target.name + " ����� " + Math.Round(damage.damage) + " �� ��ɫ �˺���" + Target.name + "ʣ�ྫ��" + Math.Round(Target.Mp);
    }
}
public class Angle : Weapon
{
    public static int cnt = 0;
    public Angle() { speed = 600; name = "��ʹ��ְ��"; atk = "BLACK 20-22"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "ʹ���� ��ʹ��ְ�� ���Ǳ�" + Target.name + "�����ˣ�";
        }
        Damage damage;
        if (cnt == 3)
        {
            cnt = 0;
            damage.damage = 80;
            damage.type = AttackTpye.RED;
            Target.UnderAttack(damage);
            return "ʹ���� ***��ʹ��ְ�𴥷������⹥��*** �� " + Target.name + " ����� " + Math.Round(damage.damage) + " �� ��ɫ �˺���" + Target.name + "ʣ��Ѫ����" + Math.Round(Target.Hp);
        }
        cnt++;
        damage.damage = 2 + damagefloat * 2;
        damage.type = AttackTpye.BLACK;
        Target.UnderAttack(damage);
        return "ʹ���� ��ʹ��ְ�� �� " + Target.name + " ����� " + Math.Round(damage.damage * 0.5) + " �� ��ɫ �� ��ɫ �˺���" + Target.name + "ʣ�ྫ��" + Math.Round(Target.Mp) + "ʣ��Ѫ����" + Math.Round(Target.Hp);
    }
}
public class WindChime : Weapon
{
    public static int cnt = 0;
    public WindChime() { speed = 600; name = "��ʹ��ְ��"; atk = "BLACK 20-22"; }
    public override string Attack(Hero Target, double TZ, double damagefloat)
    {
        if (TZ * 400 <= Target.Dodge)
        {
            return "ʹ���� ��ʹ��ְ�� ���Ǳ�" + Target.name + "�����ˣ�";
        }
        Damage damage;
        if (cnt == 3)
        {
            cnt = 0;
            damage.damage = 80;
            damage.type = AttackTpye.RED;
            Target.UnderAttack(damage);
            return "ʹ���� ***��ʹ��ְ�𴥷������⹥��*** �� " + Target.name + " ����� " + Math.Round(damage.damage) + " �� ��ɫ �˺���" + Target.name + "ʣ��Ѫ����" + Math.Round(Target.Hp);
        }
        cnt++;
        damage.damage = 2 + damagefloat * 2;
        damage.type = AttackTpye.BLACK;
        Target.UnderAttack(damage);
        return "ʹ���� ��ʹ��ְ�� �� " + Target.name + " ����� " + Math.Round(damage.damage * 0.5) + " �� ��ɫ �� ��ɫ �˺���" + Target.name + "ʣ�ྫ��" + Math.Round(Target.Mp) + "ʣ��Ѫ����" + Math.Round(Target.Hp);
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
    public SuitArmor() { RED = 1; WHITE = 1; BLACK = 1.5; PALE = 2;name = "��װ"; }
}
public class WristCutterArmor : Armor
{
    public WristCutterArmor() { RED = 1; WHITE = 0.6; BLACK = 1.2; PALE = 2; name = "������"; }
}
public class SonarArmor : Armor
{
    public SonarArmor() { RED = 0.7; WHITE = 0.7; BLACK = 1.0; PALE = 1.0; name = "����"; Speed = 50; }
}
public class SuperHell : Armor
{
    public SuperHell() { RED = 1.5; WHITE = 1.0; BLACK = 0.5; PALE = 1.5; name = "���Ƶ���"; Health = -30; Sprite = -30; Dodge += 30; Speed += 30;}
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