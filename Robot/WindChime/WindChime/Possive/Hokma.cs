/// <summary>
/// 渗透天堂
/// </summary>
public class InfiltrateHeaven : Possvie
{
    int cnt = 0;
    public override void Init()
    {
        cnt = 0;
    }
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (cnt > 1)
        {
            dam.damage += 1000000;
        }
        cnt++;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        cnt = 0;
    }
}
/// <summary>
/// 沉默的代价
/// </summary>
public class Silent : Possvie
{
    int die = 0;
    int under = 0;
    double ut = 350;
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if(under <= 0)
        {
            ut -= dam.damage * 0.7;
            dam.damage = 0;
        }
        else
        {
            under--;
        }
        if (ut <= 0)
        {
            die++;
            ut = 350;
        }
        if (die == 11)
        {
            under = 10;
        }
    }
}

public class BlueStar : Possvie
{
    int cnt = 0;
    public override void Init()
    {
        cnt = 0;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (dam.type == AttackTpye.WHITE)
        {
            dam.damage += 1000;
        }
        if (dam.type == AttackTpye.BLACK)
        {
            dam.damage += 500;
        }
    }
}


public class SiJiBa : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        Target.Speed--;
    }
}
public class NewStar : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    { 
        Target.UnderAttack(self, dam);
        Target.UnderAttack(self, dam);
    }
}