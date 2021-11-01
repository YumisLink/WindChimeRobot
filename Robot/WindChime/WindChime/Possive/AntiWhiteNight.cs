public class AntiWhiteNight : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (Target.name == "°×Ò¹")
        {
            dam.type = AttackTpye.PALE;
            dam.damage = 3330;
        }
        if(ReaderWriter.random.NextDouble() <= 0.05)
        {
            self.Mp += 10;
        }
    }
}
public class ParadiseLost : Possvie
{
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (self.weapon.Name == "Ê§ÀÖÔ°")
        {
            self.RED = 0.2;
            self.WHITE = 0.2;
            self.BLACK = 0.2;
            self.PALE = 0.2;
        }
        dam.damage -= 5;
        if (dam.damage < 0)
            dam.damage = 0;
    }
}


public class Bosses : Possvie
{
    public override void StartTurn(Hero self, Hero Target)
    {
        self.Speed += 0;
    }
}