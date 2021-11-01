/// <summary>
/// ´óÄñ
/// </summary>
public class BigBird : Possvie
{
    int cnt = 0;
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        cnt = 1;
    }
    public override void Init()
    {
        cnt = 0;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        dam.damage = 1;
        if (cnt > 0)
        {
            dam.damage = 0;
            cnt--;
        }
    }
}
/// <summary>
/// ÉóÅÐÄñ
/// </summary>
public class SPBird : Possvie
{
    int cnt = 0;
    public override void Init()
    {
        cnt = 0;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        dam.damage += 10;
        Target.UnderAttack(self,dam);
    }
    public override void StartTurn(Hero self, Hero Target)
    { 
        cnt++;
        if (cnt >= 100)
            self.Hp -= 1000;
    }
}
/// <summary>
/// ÖÕÄ©Äñ
/// </summary>

public class FinalBird : Possvie
{
    int k = 0;
    public override void Init()
    {
        k = 0;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (dam.type == AttackTpye.RED)
            k |= 1;
        if (dam.type == AttackTpye.WHITE)
            k |= 2;
        if (dam.type == AttackTpye.BLACK)
            k |= 4;
        if (dam.type == AttackTpye.PALE)
            k |= 8;
        int cnt = k, lp = 0 ;
        while (cnt > 0)
        {
            if ((cnt & 1) == 1)
                lp++;
            cnt >>= 1;
        }
        if (lp > 3)
        {
            self.RED = 0.5;
            self.BLACK = 0.5;
            self.WHITE = 0.5;
            self.PALE = 0.5;
        }
    }
}

public class Justice : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        dam.damage += Target.Hp * 0.15;
    }
}

public class BoMing : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        dam.type = AttackTpye.RED;
        Target.UnderAttack(self, dam);
        dam.type = AttackTpye.WHITE;
        Target.UnderAttack(self, dam);
        dam.type = AttackTpye.BLACK;
        Target.UnderAttack(self, dam);
        dam.type = AttackTpye.PALE;
    }
}



