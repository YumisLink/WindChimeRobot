public class QueenBeeWeapon:Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (ReaderWriter.random.NextDouble() <= 0.2)
        {
            Target.EasyAttack += 1;
        }
    }
}