using System;
using System.Collections.Generic;
using System.IO;

public enum ItemLevel
{
    ZAYIN,TETH,HE,WAW,ALEPH,FINAL
}
public class Item
{
    public double Health = 0;
    public double Sprite = 0;
    public double Dodge = 0;
    public double Speed = 0;
    public string Name;
    public ItemLevel Level;
    public string Detail;
    public Possvie pos;
    public override string ToString()
    {
        return Name;
    }
}
public class Weapon : Item
{
    public Weapon(string name,int speed,AttackTpye type,int BD,int FD,ItemLevel lv)
    {
        Name = name;
        AttackSpeed = speed;
        Type = type;
        BaseDamage = BD;
        FloatDamage = FD;
        Level = lv;
    }
    public void SetSpc(int GL, AttackTpye type, int BD, int FD)
    {
        SpcGL = GL;
        SpcType = type;
        SpcBaseDamage = BD;
        SpcFloatDamage = FD;
    }

    public int AttackSpeed;
    public AttackTpye Type;
    public int BaseDamage;
    public int FloatDamage;

    public AttackTpye SpcType;
    public int SpcBaseDamage;
    public int SpcFloatDamage;
    public int SpcGL;
    public virtual string Attack(Hero Self, Hero Target)
    {
        if (EGOSTRONGER.random.NextDouble() * 400 <= Target.Dodge)
        {
            return "使用了 " + Name + " 但是被" + Target.name + "闪避了！";
        }
        string str = "使用了" + Name + "对 " + Target.name + " 造成了 ";
        Damage damage = new Damage
        {
            damage = BaseDamage + EGOSTRONGER.random.NextDouble() * FloatDamage,
            type = Type
        };
        if (EGOSTRONGER.random.NextDouble()* 100 < SpcGL)
        {
            damage.type = SpcType;
            damage.damage = SpcBaseDamage + EGOSTRONGER.random.NextDouble() * SpcFloatDamage;
        }
        damage.damage *= (1 + 0.2 * Self.WeaponUp);
        damage.damage += Self.Strong;
        damage.damage -= Self.weak;
        if (Self.weapon.pos!= null)
            Self.weapon.pos.BeforeDealDamage(Self, Target, damage);
        if (Self.pos != null)
            Self.pos.BeforeDealDamage(Self, Target, damage);
        if (Target.weapon.pos != null)
            Target.weapon.pos.BeforeTakeDamage(Target,Self,damage);
        if (Target.pos != null)
            Target.pos.BeforeTakeDamage(Target, Self, damage);
        if (damage.damage < 0) damage.damage = 0;
        switch (damage.type)
        {
            case (AttackTpye.RED):
                damage.type = AttackTpye.RED;
                Target.UnderAttack(damage);
                damage.damage *= Target.RED;
                str += Math.Round(damage.damage) + " 点红色伤害！" + Target.name;
                str += "剩余血量：" + Math.Round(Target.Hp);
                break;
            case (AttackTpye.WHITE):
                damage.type = AttackTpye.WHITE;
                Target.UnderAttack(damage);
                damage.damage *= Target.WHITE;
                str += Math.Round(damage.damage) + " 点白色伤害！" + Target.name;
                str += "剩余精神：" + Math.Round(Target.Mp);
                break;
            case (AttackTpye.BLACK):
                damage.type = AttackTpye.BLACK;
                Target.UnderAttack(damage);
                damage.damage *= Target.BLACK;
                str += Math.Round(damage.damage) + " 点黑色伤害！" + Target.name;
                str += "剩余精神：" + Math.Round(Target.Mp) + "剩余血量：" + Math.Round(Target.Hp);
                break;
            case (AttackTpye.PALE):
                damage.type = AttackTpye.PALE;
                Target.UnderAttack(damage);
                damage.damage *= Target.PALE;
                str += Math.Round(damage.damage) + " 点蓝色伤害！" + Target.name;
                str += "剩余血量：" + Math.Round(Target.Hp);
                break;
        }
        return str;
    }
}

public class Armor : Item
{
    public double RED;
    public double WHITE;
    public double BLACK;
    public double PALE;
    public Armor(string name, double R,double W,double B,double P, ItemLevel lv)
    {
        Name = name;
        RED = R;
        WHITE = W;
        BLACK = B;
        PALE = P;
        Level = lv;
    }
}

public class GameManager
{
    public static List<Weapon> weapon = new List<Weapon>();
    public static List<Armor> armor = new List<Armor>();
    public GameManager()
    {
        Weapon a;
        Armor b;
        using (StreamReader sr = new StreamReader("Weapon.dat"))
        {
            string line;
            int cnt = 0;
            while((line = sr.ReadLine())!= null)
            {
                string[] str = line.Split("\t");
                weapon.Add(new Weapon(
                    str[1], Convert.ToInt32(str[2]),
                    (AttackTpye)Enum.Parse(typeof(AttackTpye), str[3]),
                    Convert.ToInt32(str[4]), Convert.ToInt32(str[5]),
                    (ItemLevel)Enum.Parse(typeof(ItemLevel), str[6])));
                weapon[cnt].Detail = str[7];
                cnt++;
            }
        }
        weapon[9].pos = new QueenBeeWeapon();
        weapon[5].pos = new AntiWhiteNight();
        weapon[10].pos = new GreenStemWeapon();
        weapon[13].pos = new Chord();
        weapon[14].pos = new HolyEdict();
        weapon[15].pos = new MagicShoot();

        weapon[16].pos = new BloodDesire();
        weapon[18].pos = new Leatiita();
        weapon[19].pos = new BlackSwan();

        weapon[22].Sprite = -75;
        weapon[22].pos = new Joyous();
        weapon[24].pos = new DaCapo();



        using (StreamReader sr = new StreamReader("Armor.dat"))
        {
            string line;
            int cnt = 0;
            while ((line = sr.ReadLine()) != null)
            {
                string[] str = line.Split("\t");
                armor.Add(new Armor(
                    str[1], Convert.ToDouble(str[2]),Convert.ToDouble(str[3]),
                    Convert.ToDouble(str[4]), Convert.ToDouble(str[5]),
                    (ItemLevel)Enum.Parse(typeof(ItemLevel), str[6])));
                armor[cnt].Detail = str[7];
                cnt++;
            }
        }
    }
}