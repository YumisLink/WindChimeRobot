using System;
public class Cat : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (Target.weapon.Name == "¹é³²±¾ÄÜ")
            dam.damage *= 100;
    }
}
public class LieTouChangBa : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (ReaderWriter.random.NextDouble() < 0.5f)
        {
            Target.weapon.Attack(self,Target);
        }
    }
}
public class TuiSeJiYi : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        self.Strong++;
    }
}