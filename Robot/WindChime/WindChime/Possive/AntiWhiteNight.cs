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