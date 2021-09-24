using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;


public class Image
{
    public static bool Main(string group_id,string user_id,string name,string message)
    {
        if (message == "来点猫图")
        {
            Random rd = new Random();
            int k = rd.Next(1, 14);
            if (k == 1) Api.Group(group_id, Api.GetImageMessage("fa50fc3ae957a55ac533bfff12ffa423"));
            if (k == 2) Api.Group(group_id, Api.GetImageMessage("f5bda16d7621e7e4cbff5fafe5fbcec7"));
            if (k == 3) Api.Group(group_id, Api.GetImageMessage("5eca44b257db43db030de2e18b9f39de"));
            if (k == 4) Api.Group(group_id, Api.GetImageMessage("9a1e309ce7741aa69d4bca6f9e553fa7"));
            if (k == 5) Api.Group(group_id, Api.GetImageMessage("49a0f6ff95c8835786c461dc98840bf5"));
            if (k == 6) Api.Group(group_id, Api.GetImageMessage("491fa5576ce746ebe787d94caf76b11e"));
            if (k == 7) Api.Group(group_id, Api.GetImageMessage("e36ac3fb9568612541e211dac49efa7e"));
            if (k == 8) Api.Group(group_id, Api.GetImageMessage("e5fcfdf0afa88b01d452f8b03212d6dd")); 
            if (k == 7) Api.Group(group_id, Api.GetImageMessage("e36ac3fb9568612541e211dac49efa7e"));
            if (k == 9) Api.Group(group_id, Api.GetImageMessage("d33f42d08a1a8cd1a6f4bb8a9f19ad74"));
            if (k == 10) Api.Group(group_id, Api.GetImageMessage("f9a1dc7922d347f11940ea08e68a5746"));
            if (k == 11) Api.Group(group_id, Api.GetImageMessage("62a9536ea93b80df7ee011693db41aee"));
            if (k == 12) Api.Group(group_id, Api.GetImageMessage("50a8860463b68451038f92198c072806"));
            if (k == 13) Api.Group(group_id, Api.GetImageMessage("ed273cd636b55ae9ef7233234372ce1c"));
            return true;
        }else if (message.Contains("风铃发图"))
        {
            string[] str = message.Split(" ");
            if (str.Length >= 2)
            {
                Api.Group(group_id, Api.GetImageMessage(str[1]));
                return true;
            }
            else return false;
        }else if (message == "风铃")
        {
            UserInfo User = ReaderWriter.GetUserInfo(user_id);
            int k = 0;
            if (User.heart >= 0 && User.heart < 50)
                k = EGOSTRONGER.random.Next(0, 3);
            if (User.heart >= 50 && User.heart < 100)
                k = EGOSTRONGER.random.Next(0, 5);
            if (User.heart >= 100)
                k = EGOSTRONGER.random.Next(0, 8);
            if (k == 0)
                Api.Group(group_id, "泰拉的天灾吗？似乎在脑叶公司也有，只是被称作考验...啊不是那种的吗？");
            if (k == 1)
                Api.Group(group_id, "博士，你吃了那么多源石，要不要试试脑叶公司特产的Cogito？效果跟源石差不多，都是可以恢复理智的。你说我是骗你的？AI不会说谎的！");
            if (k == 2)
                Api.Group(group_id, "你想唤醒你的EGO吗？博士，脑叶公司里面的员工都有EGO，那些都是Carmen的功劳，不过那些EGO都不是他们自己的。");
            if (k == 3)
                Api.Group(group_id, "风铃累了，先去睡一觉了，没有二级警报就不要叫我了。");
            if (k == 4)
                Api.Group(group_id, "博士，可以为我戴上我的小礼帽吗？谢谢你...");
            if (k == 5)
                Api.Group(group_id, "博士，实际上我是在违反《人工智能伦理修订案》的情况下制造出来的，虽然这么说，但是在SEPHIRAH中，也是有曾经是首脑的调率者的存在。");
            if (k == 6)
                Api.Group(group_id, "能够在闲暇的时间在博士的身边，对于风铃来说，就算是一种休息了吧！");
            if (k == 7)
                Api.Group(group_id, "脑叶公司的奇点技术，就是Cogito，虽然说是能源公司。但是实际上是吧怪物产生的脑啡肽转化成能量，你看，那些看起来像是怪物，实际上很多都是脑叶公司的员工。");
        }
        return false;
    }
}