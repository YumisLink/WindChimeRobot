using System;

public enum AttackTpye
{
    RED, WHITE, BLACK, PALE, HEAL
}
public struct Damage
{
    public AttackTpye type;
    public double damage;
}
public class Hero
{
    public Hero(UserInfo user)
    {
        name = user.name;
        Hp = user.Courage;
        Mp = user.Cautious;
        Dodge = user.Discipline;
        Speed = user.Justice;
        weapon = GameManager.weapon[user.EGOWeapon];
        Hp += weapon.Health;
        Mp += weapon.Sprite;
        Dodge += (int)Math.Round(weapon.Dodge);
        Speed += (int)Math.Round(weapon.Speed);
        Armor ar = GameManager.armor[user.EGOArmor];
        Hp += ar.Health;
        Mp += ar.Sprite;
        Dodge += (int)Math.Round(ar.Dodge);
        Speed += (int)Math.Round(ar.Speed);


        RED = GameManager.armor[user.EGOArmor].RED;
        WHITE = GameManager.armor[user.EGOArmor].WHITE;
        BLACK = GameManager.armor[user.EGOArmor].BLACK;
        PALE = GameManager.armor[user.EGOArmor].PALE;
    }
    public string name;
    /// <summary>
    /// ��ɫ
    /// </summary>
    public double Hp;
    /// <summary>
    /// ��ɫ
    /// </summary>
    public double Mp;
    /// <summary>
    /// �ٶ�
    /// </summary>
    public int Speed;
    /// <summary>
    /// ������
    /// </summary>
    public int Dodge;
    /// <summary>
    /// ����
    /// </summary>
    public Weapon weapon;
    public double RED;
    public double WHITE;
    public double BLACK;
    public double PALE;
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        if (Hp <= 0) return true;
        if (Mp <= 0) return true;
        else return false;
    }
    public void UnderAttack(Damage dam)
    {
        if (dam.type == AttackTpye.RED)
            Hp -= (dam.damage * RED);

        if (dam.type == AttackTpye.WHITE)
            Mp -= (dam.damage * WHITE);

        if (dam.type == AttackTpye.BLACK)
        {
            Hp -= (dam.damage * BLACK * 0.5f);
            Mp -= (dam.damage * BLACK * 0.5f);
        }

        if (dam.type == AttackTpye.PALE)
            Hp -= (dam.damage * PALE);

        if (dam.type == AttackTpye.HEAL)
            Hp += dam.damage;
    }
    public override string ToString()
    {
        string str = "";
        str += "\n����ֵ��" + Hp;
        str += "\n����ֵ��" + Mp;
        str += "\n�����ʣ�" + Dodge * 0.25 + "%";
        str += "\n����ֵ��" + Speed;
        return str;
    }
}
public class War
{
    public static bool main(string group_id, string user_id, string name, string message)
    {
        string[] ss = message.Split(" ");
        if (message.Contains("Battle"))
        {
            UserInfo A, B;
            A = ReaderWriter.GetUserInfo(user_id);
            if (A.SoloCount <= 0)
            {
                Api.Group(group_id, "�Ѵﵽ����ս�����ޣ�");
                return true;
            }
            A.SoloCount--;
            B = ReaderWriter.GetUserInfo(ss[1]);
            if (!B.CanGet)
            {
                Api.Group(group_id, "�޷��ҵ�" + ss[1]);
                return true;
            }
            int ret = battle(new Hero(A), new Hero(B), user_id);
            if (ret == 1)
            {
                int k = Math.Min(A.money / 2, B.money / 10);
                A.money += k;
                B.money -= k;
                Api.Group(group_id, "��ʤ���ˣ������" + k + "���\n�����ȡս�����飬����������Ǻ��ѵ�����½���ս����");
            }
            else if (ret == 0)
            {
                Api.Group(group_id, "ƽ���ˣ�û�л���κ����棬����Ҳû���κ���ʧ��\n�����ȡս�����飬����������Ǻ��ѵ�����½���ս����");
            }
            else if (ret == -1)
            {
                int k = A.money / 10;
                Api.Group(group_id, "�����ˣ�ʧȥ��" + k + "���\n�����ȡս�����飬����������Ǻ��ѵ�����½���ս����");
                A.money -= k;
                B.money += k;
            }
            ReaderWriter.WriteToFile(A);
            ReaderWriter.WriteToFile(B);
            return true;
        }
        if (message == "��ѯEGO")
        {
            UserInfo A = ReaderWriter.GetUserInfo(user_id);
            if (!A.CanGet)
                return true;
            string str = "";
            Weapon wp =  GameManager.weapon[A.EGOWeapon];
            Armor ar = GameManager.armor[A.EGOArmor];
            str += "��ǰ���ego������" + wp.name;
            str += "\n�����ٶȣ�" + wp.speed;
            str += "\n��������" + wp.atk;
            str += "\n��ǰ���EGO���ߣ�"+ ar.name;
            str += "\n������(RED)��" + String.Format("{0:F1}", ar.RED); ;
            str += "\n������(WHITE)��" + String.Format("{0:F1}", ar.WHITE); ;
            str += "\n��ʴ����(BLACK)��" + String.Format("{0:F1}", ar.BLACK); ;
            str += "\n��꿹��(PALE)��" + String.Format("{0:F1}", ar.PALE); ;
            Api.Group(group_id, str);
                return true;
        }
        return false;
    }
    public static int battle(Hero A, Hero B, string user_id)
    {
        Random rd = new Random();
        int speedA = 0, speedB = 0;
        int cnt = 0;
        string str = "";
        while (!A.IsDead() && !B.IsDead() && cnt <= 200)
        {
            cnt++;
            speedA += (A.Speed + 100);
            speedB += (B.Speed + 100);
            if (speedA >= A.weapon.speed)
            {
                speedA -= A.weapon.speed;
                str += "\n��" + A.weapon.Attack(B, rd.NextDouble(), rd.NextDouble());
            }
            if (speedB >= B.weapon.speed)
            {
                speedB -= B.weapon.speed;
                str += "\n" + B.name + B.weapon.Attack(A, rd.NextDouble(), rd.NextDouble());
            }
        }
        //Api.Private(user_id, str);
        Api.Private(user_id, str);
        if (A.IsDead() && B.IsDead()) return 0;
        if (A.IsDead()) return -1;
        if (B.IsDead()) return 1;
        return 114514;
    }
}

