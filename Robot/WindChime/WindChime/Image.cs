using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;


public class Image
{
    public static bool Main(string group_id,string user_id,string name,string message)
    {
        if (message == "����èͼ")
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
        }else if (message.Contains("���巢ͼ"))
        {
            string[] str = message.Split(" ");
            if (str.Length >= 2)
            {
                Api.Group(group_id, Api.GetImageMessage(str[1]));
                return true;
            }
            else return false;
        }else if (message == "����")
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
                Api.Group(group_id, "̩�����������ƺ�����Ҷ��˾Ҳ�У�ֻ�Ǳ���������...���������ֵ���");
            if (k == 1)
                Api.Group(group_id, "��ʿ���������ô��Դʯ��Ҫ��Ҫ������Ҷ��˾�ز���Cogito��Ч����Դʯ��࣬���ǿ��Իָ����ǵġ���˵����ƭ��ģ�AI����˵�ѵģ�");
            if (k == 2)
                Api.Group(group_id, "���뻽�����EGO�𣿲�ʿ����Ҷ��˾�����Ա������EGO����Щ����Carmen�Ĺ��ͣ�������ЩEGO�����������Լ��ġ�");
            if (k == 3)
                Api.Group(group_id, "�������ˣ���ȥ˯һ���ˣ�û�ж��������Ͳ�Ҫ�����ˡ�");
            if (k == 4)
                Api.Group(group_id, "��ʿ������Ϊ�Ҵ����ҵ�С��ñ��лл��...");
            if (k == 5)
                Api.Group(group_id, "��ʿ��ʵ����������Υ�����˹����������޶��������������������ģ���Ȼ��ô˵��������SEPHIRAH�У�Ҳ�������������Եĵ����ߵĴ��ڡ�");
            if (k == 6)
                Api.Group(group_id, "�ܹ�����Ͼ��ʱ���ڲ�ʿ����ߣ����ڷ�����˵��������һ����Ϣ�˰ɣ�");
            if (k == 7)
                Api.Group(group_id, "��Ҷ��˾����㼼��������Cogito����Ȼ˵����Դ��˾������ʵ�����ǰɹ���������Է���ת�����������㿴����Щ���������ǹ��ʵ���Ϻܶ඼����Ҷ��˾��Ա����");
        }
        return false;
    }
}