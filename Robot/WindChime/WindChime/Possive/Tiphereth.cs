using System;

public class LoveAndHete : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        int a = ReaderWriter.random.Next(0, 4);
        if (a == 0)
            dam.type = AttackTpye.RED;
        if (a == 1)
            dam.type = AttackTpye.WHITE;
        if (a == 2)
            dam.type = AttackTpye.BLACK;
        if (a == 3)
            dam.type = AttackTpye.PALE;
    }
}

public class BlindGold : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (ReaderWriter.random.NextDouble() < 0.5)
        {
            self.Strong += 1;
        }
    }
}

public class NoEyeCrazy : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        Target.bleeding += 1;
    }
}

public class VoidIllusory : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        dam.damage *= (1 + self.Hp / 100);
    }
}

public class AbhorQueen : Possvie
{
    int cnt = 0;
    public override void Init()
    {
        cnt = 0;
    }
    public override void StartTurn(Hero self, Hero Target)
    {
        cnt++;
        if (cnt < 10)
            return;
        if (cnt > 10)
            return;
        double Ac = self.AllAttack.RED + self.AllAttack.WHITE + self.AllAttack.BLACK + self.AllAttack.PALE;
        double Bc = Target.AllAttack.RED + Target.AllAttack.WHITE + Target.AllAttack.BLACK + Target.AllAttack.PALE;
        if (Ac < Bc)
        {
            self.Hp = 300;
            self.Mp = 300;
            self.Strong = 10;
        }
    }
}
public class DespairKnight : Possvie
{
    int cnt = 0;
    public override void Init()
    {
        cnt = 0;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        cnt++;
        if (Target.weapon.Name != "°®ÓëÔ÷Ö®Ãû")
        {
            dam.damage = 0;
            dam.type = AttackTpye.HEAL;
            return;
        }
        if (cnt >= 3)
        {
            Target.AllAttack.RED += self.Hp / 2;
            self.Hp -= self.Hp / 2;
        }
    }
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        cnt = 0;
    }
}
public class GreedyKing : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (Target.weapon.Name != "Àá·æÖ®½£")
        {
            dam.type = AttackTpye.HEAL; 
            dam.damage = 0;
        }
        dam.damage *= 1.5f;
    }
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        double low = self.weapon.BaseDamage + 0.5f * self.weapon.FloatDamage;
        low *= Math.Pow(1.05, Target.WeaponUp);
        if (dam.damage > low)
        {
            dam.damage *= 2;
        }
    }
}

public class AngryAttendant : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (Target.weapon.Name != "ÉÁ½ð¿ñ±©")
        {
            dam.type = AttackTpye.HEAL;
            dam.damage = 0;
        }
    }
}
public class VoidCourtiers : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (Target.weapon.Name == "Ã¤ÑÛÅ­»ð")
        {
            dam.damage = 9999999;
            self.Hp -= 999999999;
        }
    }
}