public class Joyous : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        dam.damage *= 1.5;
    }
}

public class DaCapo : Possvie
{
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if (ReaderWriter.random.NextDouble() <= 0.7)
            return;
        if (ReaderWriter.random.Next(0, 2) == 0)
            Target.weak++;
        else
            Target.EasyAttack++;
    }
}

public class MilkyWay : Possvie
{
    int tg = 0;
    public override void Init()
    {
        tg = 0;
    }
    public override void BeforeTakeDamage(Hero self, Hero Target, Damage dam)
    {
        if (tg != 0)
            return;
        tg = 1;
        self.BLACK=1;
        self.PALE = 1;
        self.RED = 1;
        self.WHITE = 1;
        if (dam.type == AttackTpye.BLACK)
            self.BLACK = 0;
        if (dam.type == AttackTpye.PALE)
            self.PALE = 0;
        if (dam.type == AttackTpye.RED)
            self.RED= 0;
        if (dam.type == AttackTpye.WHITE)
            self.WHITE = 0;
    }
}

public class SilentOrchestra : Possvie
{
    public override void StartTurn(Hero self, Hero Target)
    {
        self.BLACK = 0;
        self.RED = 0;
        self.WHITE = 0;
        self.PALE= 0;

        int a = ReaderWriter.random.Next(0, 6);
        if (a == 0) { self.RED = 1;self.WHITE = 1; }
        if (a == 1) { self.RED = 1; self.BLACK = 1; }
        if (a == 2) { self.RED = 1; self.PALE = 1; }
        if (a == 3) { self.WHITE = 1; self.BLACK = 1; }
        if (a == 4) { self.WHITE = 1; self.PALE = 1; }
        if (a == 5) { self.BLACK = 1; self.PALE = 1; }
        
    }
}