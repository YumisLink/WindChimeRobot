using System;

public class Happy : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (ReaderWriter.random.NextDouble() <= 0.2)
        {
            dam.damage *= 5;
        }
    }

}
public class BloodDesireSymptom : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        self.Hp += dam.damage * 0.05;
        self.Mp += dam.damage * 0.05;
    }
}
public class Mimicry : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (ReaderWriter.random.NextDouble() <= 0.1)
            dam.damage *= 10;
        self.Hp += dam.damage * 0.25;
    }
}

public class CorpseMountain : Possvie
{
    public double BaseHp = 100;
    public int cnt = 0;
    public override void Init()
    {
        BaseHp = 50;
        cnt = 0;
    }
    public override void StartTurn(Hero self, Hero Target)
    {
        if (self.Hp <= BaseHp)
        {
            cnt++;
            if (cnt >= 10)
            {
                self.Hp = BaseHp * 10;
                BaseHp = self.Hp / 5;
                cnt = 0;
            }
        }
    }
}
public class NothingatAll : Possvie
{
    int cnt = 0;
    public override void Init()
    {
        cnt = 0;
    }
    public override void StartTurn(Hero self, Hero Target)
    {
        cnt++;
        if (cnt == 50)
        {
            self.name = "一无所有形态：2";
            self.Speed = -50;
            self.RED = 0;
            self.WHITE = 0.6;
            self.BLACK = 0.6;
            self.PALE = 1;
            self.Hp = 500;
            self.Mp = 500;
            self.DefAttack = 20;
        }
        if (cnt == 150)
        {
            self.name = "一无所有形态：3";
            self.Speed = 0;
            self.RED = 0;
            self.WHITE = 0.4;
            self.BLACK = 0.4;
            self.PALE = 0.8;
            self.Hp = 500;
            self.Mp = 500;
            self.Strong = 30;
        }
    }
}