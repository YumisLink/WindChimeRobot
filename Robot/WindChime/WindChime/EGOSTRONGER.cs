using System;

public class EGOSTRONGER {
    public static Random random;
    public EGOSTRONGER()
    {
        random = new Random();
    }
    public static void Increase(string group_id,string user_id, string message)
    {
        UserInfo user = ReaderWriter.GetUserInfo(user_id);
        if (message.Contains("����"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.WeaponIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.WeaponIncrease];
                if (random.NextDouble() < 0.4)
                {
                    user.WeaponIncrease++;
                    Api.Group(group_id, "ǿ���ɹ�����ǰ����������ֵ��" + user.WeaponIncrease + "\n�����ˣ�" + ReaderWriter.moneyCost[user.WeaponIncrease - 1] + "��ң���ǰʣ���ң�" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease] / 2;
                        Api.Group(group_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��50%�Ľ��\n�����ˣ�" + ReaderWriter.moneyCost[user.WeaponIncrease] / 2 + "��ң���ǰʣ���ң�" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease];
                        Api.Group(group_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��100%�Ľ��");
                    }
                    else Api.Group(group_id, "ǿ��ʧ�ܣ����ˣ� \n�����ˣ�" + ReaderWriter.moneyCost[user.WeaponIncrease] + "��ң���ǰʣ���ң�" + user.money);

                }
            }
            else
            {
                Api.Group(group_id, "��Ҳ��㣬��Ҫ��ң�" + ReaderWriter.moneyCost[user.WeaponIncrease]);
            }
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (message.Contains("����"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.ArmorIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.ArmorIncrease];
                if (random.NextDouble() < 0.25)
                {
                    user.ArmorIncrease++;
                    Api.Group(group_id, "ǿ���ɹ�����ǰ����������ֵ��" + user.ArmorIncrease + "\n�����ˣ�" + ReaderWriter.moneyCost[user.ArmorIncrease - 1] + "��ң���ǰʣ���ң�" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease] / 2;
                        Api.Group(group_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��50%�Ľ��\n�����ˣ�" + ReaderWriter.moneyCost[user.ArmorIncrease] / 2 + "��ң���ǰʣ���ң�" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease];
                        Api.Group(group_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��100%�Ľ��");
                    }
                    else Api.Group(group_id, "ǿ��ʧ�ܣ����ˣ�\n�����ˣ�" + ReaderWriter.moneyCost[user.ArmorIncrease] + "��ң���ǰʣ���ң�" + user.money);
                }
                ReaderWriter.WriteToFile(user);
                return;
            }
            else
            {
                Api.Group(group_id, "��Ҳ��㣬��Ҫ��ң�" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            }
        }
        if (message.Contains("��ѯ"))
        {
            Api.Group(group_id, "��������ǿ����Ҫ��ң�" + ReaderWriter.moneyCost[user.WeaponIncrease] + "\n����ǿ����Ҫ��" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            return;
        }

        Api.Group(group_id, "��ʽ��\nǿ�� [���������ס���ѯ]");
        return;
    }
    public static void Increase(string user_id, string message)
    {
        UserInfo user = ReaderWriter.GetUserInfo(user_id);
        if (message.Contains("����"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.WeaponIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.WeaponIncrease];
                if (random.NextDouble() < 0.4)
                {
                    user.WeaponIncrease++;
                    Api.Private(user_id, "ǿ���ɹ�����ǰ����������ֵ��" + user.WeaponIncrease + "\n�����ˣ�" + ReaderWriter.moneyCost[user.WeaponIncrease - 1] + "��ң���ǰʣ���ң�" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease] / 2;
                        Api.Private(user_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��50%�Ľ��\n�����ˣ�" + ReaderWriter.moneyCost[user.WeaponIncrease] / 2 + "��ң���ǰʣ���ң�" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease];
                        Api.Private(user_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��100%�Ľ��");
                    }
                    else Api.Private(user_id, "ǿ��ʧ�ܣ����ˣ� \n�����ˣ�" + ReaderWriter.moneyCost[user.WeaponIncrease] + "��ң���ǰʣ���ң�" + user.money);

                }
            }
            else
            {
                Api.Private(user_id, "��Ҳ��㣬��Ҫ��ң�" + ReaderWriter.moneyCost[user.WeaponIncrease]);
            }
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (message.Contains("����"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.ArmorIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.ArmorIncrease];
                if (random.NextDouble() < 0.25)
                {
                    user.ArmorIncrease++;
                    Api.Private(user_id, "ǿ���ɹ�����ǰ����������ֵ��" + user.ArmorIncrease + "\n�����ˣ�" + ReaderWriter.moneyCost[user.ArmorIncrease - 1] + "��ң���ǰʣ���ң�" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease] / 2;
                        Api.Private(user_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��50%�Ľ��\n�����ˣ�" + ReaderWriter.moneyCost[user.ArmorIncrease] / 2 + "��ң���ǰʣ���ң�" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease];
                        Api.Private(user_id, "ǿ��ʧ�ܣ�����������㱾��ǿ��100%�Ľ��");
                    }
                    else Api.Private(user_id, "ǿ��ʧ�ܣ����ˣ�\n�����ˣ�" + ReaderWriter.moneyCost[user.ArmorIncrease] + "��ң���ǰʣ���ң�" + user.money);
                }
                ReaderWriter.WriteToFile(user);
                return;
            }
            else
            {
                Api.Private(user_id, "��Ҳ��㣬��Ҫ��ң�" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            }
        }
        if (message.Contains("��ѯ"))
        {
            Api.Private(user_id, "��������ǿ����Ҫ��ң�" + ReaderWriter.moneyCost[user.WeaponIncrease] + "\n����ǿ����Ҫ��" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            return;
        }

        Api.Private(user_id, "��ʽ��\nǿ�� [���������ס���ѯ]");
        return;
    }
}
