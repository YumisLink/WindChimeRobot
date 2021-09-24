public class GreenStemWeapon: Possvie
{
    int time = 1;
    public override void Init()
    {
        time = 1;
    }
    public override void BeforeDealDamage(Hero self, Hero Target, Damage dam)
    {
        if(self.Hp <= 50 && time > 0)
        {
            time--;
            dam.damage *= 5;
        }
    }
}