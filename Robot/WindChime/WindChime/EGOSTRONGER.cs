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
        if (message.Contains("武器"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.WeaponIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.WeaponIncrease];
                if (random.NextDouble() < 0.4)
                {
                    user.WeaponIncrease++;
                    Api.Group(group_id, "强化成功，当前武器增幅数值：" + user.WeaponIncrease + "\n消耗了：" + ReaderWriter.moneyCost[user.WeaponIncrease - 1] + "金币，当前剩余金币：" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease] / 2;
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化50%的金币\n消耗了：" + ReaderWriter.moneyCost[user.WeaponIncrease] / 2 + "金币，当前剩余金币：" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease];
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化100%的金币");
                    }
                    else Api.Group(group_id, "强化失败！乐了！ \n消耗了：" + ReaderWriter.moneyCost[user.WeaponIncrease] + "金币，当前剩余金币：" + user.money);

                }
            }
            else
            {
                Api.Group(group_id, "金币不足，需要金币：" + ReaderWriter.moneyCost[user.WeaponIncrease]);
            }
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (message.Contains("护甲"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.ArmorIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.ArmorIncrease];
                if (random.NextDouble() < 0.25)
                {
                    user.ArmorIncrease++;
                    Api.Group(group_id, "强化成功，当前防具增幅数值：" + user.ArmorIncrease + "\n消耗了：" + ReaderWriter.moneyCost[user.ArmorIncrease - 1] + "金币，当前剩余金币：" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease] / 2;
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化50%的金币\n消耗了：" + ReaderWriter.moneyCost[user.ArmorIncrease] / 2 + "金币，当前剩余金币：" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease];
                        Api.Group(group_id, "强化失败，风铃决定还你本次强化100%的金币");
                    }
                    else Api.Group(group_id, "强化失败！乐了！\n消耗了：" + ReaderWriter.moneyCost[user.ArmorIncrease] + "金币，当前剩余金币：" + user.money);
                }
                ReaderWriter.WriteToFile(user);
                return;
            }
            else
            {
                Api.Group(group_id, "金币不足，需要金币：" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            }
        }
        if (message.Contains("查询"))
        {
            Api.Group(group_id, "本次武器强化需要金币：" + ReaderWriter.moneyCost[user.WeaponIncrease] + "\n防具强化需要：" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            return;
        }

        Api.Group(group_id, "格式：\n强化 [武器、护甲、查询]");
        return;
    }
    public static void Increase(string user_id, string message)
    {
        UserInfo user = ReaderWriter.GetUserInfo(user_id);
        if (message.Contains("武器"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.WeaponIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.WeaponIncrease];
                if (random.NextDouble() < 0.4)
                {
                    user.WeaponIncrease++;
                    Api.Private(user_id, "强化成功，当前武器增幅数值：" + user.WeaponIncrease + "\n消耗了：" + ReaderWriter.moneyCost[user.WeaponIncrease - 1] + "金币，当前剩余金币：" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease] / 2;
                        Api.Private(user_id, "强化失败，风铃决定还你本次强化50%的金币\n消耗了：" + ReaderWriter.moneyCost[user.WeaponIncrease] / 2 + "金币，当前剩余金币：" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.WeaponIncrease];
                        Api.Private(user_id, "强化失败，风铃决定还你本次强化100%的金币");
                    }
                    else Api.Private(user_id, "强化失败！乐了！ \n消耗了：" + ReaderWriter.moneyCost[user.WeaponIncrease] + "金币，当前剩余金币：" + user.money);

                }
            }
            else
            {
                Api.Private(user_id, "金币不足，需要金币：" + ReaderWriter.moneyCost[user.WeaponIncrease]);
            }
            ReaderWriter.WriteToFile(user);
            return;
        }
        if (message.Contains("护甲"))
        {
            if (user.money >= ReaderWriter.moneyCost[user.ArmorIncrease])
            {
                user.money -= ReaderWriter.moneyCost[user.ArmorIncrease];
                if (random.NextDouble() < 0.25)
                {
                    user.ArmorIncrease++;
                    Api.Private(user_id, "强化成功，当前防具增幅数值：" + user.ArmorIncrease + "\n消耗了：" + ReaderWriter.moneyCost[user.ArmorIncrease - 1] + "金币，当前剩余金币：" + user.money);
                }
                else
                {
                    if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease] / 2;
                        Api.Private(user_id, "强化失败，风铃决定还你本次强化50%的金币\n消耗了：" + ReaderWriter.moneyCost[user.ArmorIncrease] / 2 + "金币，当前剩余金币：" + user.money);
                    }
                    else if (random.NextDouble() < 0.3)
                    {
                        user.money += ReaderWriter.moneyCost[user.ArmorIncrease];
                        Api.Private(user_id, "强化失败，风铃决定还你本次强化100%的金币");
                    }
                    else Api.Private(user_id, "强化失败！乐了！\n消耗了：" + ReaderWriter.moneyCost[user.ArmorIncrease] + "金币，当前剩余金币：" + user.money);
                }
                ReaderWriter.WriteToFile(user);
                return;
            }
            else
            {
                Api.Private(user_id, "金币不足，需要金币：" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            }
        }
        if (message.Contains("查询"))
        {
            Api.Private(user_id, "本次武器强化需要金币：" + ReaderWriter.moneyCost[user.WeaponIncrease] + "\n防具强化需要：" + ReaderWriter.moneyCost[user.ArmorIncrease]);
            return;
        }

        Api.Private(user_id, "格式：\n强化 [武器、护甲、查询]");
        return;
    }
}
