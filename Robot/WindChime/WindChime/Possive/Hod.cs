public class BloodDesire : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        Target.bleeding++;
    }
}

public class Leatiita : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        float low = self.weapon.BaseDamage + 0.2f * self.weapon.FloatDamage;
        if (dam.damage < low)
        {
            dam.damage = self.weapon.BaseDamage + ReaderWriter.random.NextDouble() * self.weapon.FloatDamage;
        }
    }
}
public class BlackSwan : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        self.BLACK += 0.01;
    }
}
public class SpiderNest : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        self.Hp -= dam.damage * 100;
    }
}
public class Leticia : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        float low = Target.weapon.BaseDamage + 0.8f * Target.weapon.FloatDamage;
        if (dam.damage > low)
        {
            self.Hp -= 10.1;
        }
    }
}
public class BlackSwanDream : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (dam.type == AttackTpye.BLACK)
            self.BLACK += 0.1;
        if (dam.type == AttackTpye.PALE)
            self.PALE += 0.1;
        if (dam.type == AttackTpye.RED)
            self.RED += 0.1;
        if (dam.type == AttackTpye.WHITE)
            self.WHITE += 0.1;
    }
}

