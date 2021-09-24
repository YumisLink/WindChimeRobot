public class Possvie
{
    public string Show;
    public string Type;
    public override string ToString()
    {
        return Show;
    }
    public virtual void BeforeDealDamage(Hero self,Hero Target,Damage dam){}
    public virtual void BeforeTakeDamage(Hero self, Hero Target, Damage dam){}
    public virtual void Init(){}
    public virtual void StartTurn(Hero self, Hero Target) { }
}