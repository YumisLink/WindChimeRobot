using System;
using System.Collections.Generic;
using System.Text;

class NanaClass
{
    public static bool GetNa(string Group_id, string Message)
    {
        try{
            if (Message.Contains("上课"))
            {
                Api.Group(Group_id, "上课溜呐");
                return true;
            }
            if (Message.Contains("晚自习"))
            {
                Api.Group(Group_id, "晚自习溜呐");
                return true;
            }
            if (Message.Contains("睡觉") || Message.Contains("晚安"))
            {
                Api.Group(Group_id, "睡觉溜呐");
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Api.Group(Group_id, e.ToString());
        }
        return false;
    }
}

