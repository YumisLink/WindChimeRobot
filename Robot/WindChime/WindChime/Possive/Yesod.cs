public class Chord : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        dam.damage *= 2;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        dam.damage *= 2;
    }
}
public class HolyEdict : Possvie
{
    int cnt = 0;
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        cnt++;
        if (cnt >= 2)
        {
            cnt = 0;
            dam.type = AttackTpye.BLACK;
        }
    }
}
public class MagicShoot : Possvie
{
    int cnt = 0;
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        cnt++;
        if (cnt == 7)
        {
            self.UnderAttack(dam);
        }
        else
        {
            Target.UnderAttack(dam);
        }
    }
}

