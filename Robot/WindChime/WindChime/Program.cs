using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Yumis
{
    class Program
    {
        public static string same1 = "?????";
        public static string same2 = "?????";
        public static string State = "睡觉";
        public static Recuit recuit;
        public static ReaderWriter reader;
        public static DateTag dater;
        public static int dt = 1;
        static void Main(string[] args)
        {
            //OneBotHttpApi.SetServerUri(new Uri("http://127.0.0.1:5700"));
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:5710/");
            listener.Start();

            recuit = new Recuit();
            reader = new ReaderWriter();
            dater = new DateTag();

            Console.WriteLine("Listening...");
            while (true)
            {
                dater.Prt();
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                using (var input = request.InputStream)
                {
                    JsonDocument json = JsonDocument.Parse(input);
                    JsonElement je = json.RootElement;
                    //Console.WriteLine(je.GetProperty("user_id") +"说了:" + je.GetProperty("raw_message"));
                    response.StatusCode = 200;
                    response.Close();
                    //Api a = new Api();
                    //a.Private("635691684",""+je.GetProperty("raw_message"));
                    if (je.GetProperty("post_type").GetString() == "message")
                        if (je.GetProperty("message_type").GetString() == "group")
                        {
                            string sp = je.GetProperty("raw_message").GetString();
                            string group_id = je.GetProperty("group_id").ToString();
                            string user_id = je.GetProperty("user_id").ToString();
                            string name = je.GetProperty("sender").GetProperty("card").GetString();
                            if (sp == "帮助"||sp == "关于风铃" || sp == "/help")
                            {
                                Api.Group(group_id, "新用户通过“风铃眼熟我”来注册账号\n日常可以进行“摸摸风铃”来增加好感度\n日常可以进行“打工”来获取金币\n金币可以用“加入问答”来让风铃会对你的说话产生回应\n同时可以用Delete指令来让风铃遗忘问答的内容\n通过“查询来获取自己当前的好感度以及金币”\n如果好感度到达比较高的程度，风铃的回答会产生一些变化哦！\n另外可以通过“公招”来进行明日方舟的公开招募的组合查询。\n以上的指令如果不知道怎么用打出对应的指令就会有提示的！");
                                continue;
                            }
                            if (user_id == "635691684")
                            {
                                if (sp.Contains("getcqcode"))
                                {
                                    string[] str = sp.Split(" ");
                                    string ss = "";
                                    for (int i = 1; i < str[1].Length - 1; i++)
                                    {
                                        ss += str[1][i];
                                    }
                                    Api.Group(group_id, ss + "\n以上的代码用[]包上即可使用");
                                    continue;
                                }
                            }
                            if (name == "" || name == null)
                            {
                                name = je.GetProperty("sender").GetProperty("nickname").GetString();
                            }
                            ReaderWriter.Change(group_id, user_id, sp);
                            if (reader.Delete(group_id, user_id, sp))
                                continue;
                            reader.Ignore(group_id, user_id, sp);
                            if (recuit.PublicOffering(group_id, user_id, sp)) { }
                            else if (sp == "查询")
                            {
                                ReaderWriter.Find(group_id, user_id, name);
                            }
                            else if (sp == "摸摸风铃")
                            {
                                ReaderWriter.Touch(group_id, user_id, name);
                            }
                            else if (sp == "打工")
                            {
                                ReaderWriter.Work(group_id, user_id, name);
                            }
                            else if (sp == "风铃眼熟我")
                            {
                                ReaderWriter.Remember(group_id, user_id, name);
                            }
                            else if (sp == "风铃喊我名字！")
                            {
                                Api.Group(group_id, "好的！\n" + name);
                            }
                            else if (sp == "风铃艾特我！")
                            {
                                Api.Group(group_id, "好的！\n" + Api.GetAtMessage(user_id));
                            }
                            else if (reader.Question(group_id, user_id, name, sp)) { }
                            else if (dater.Add(group_id, user_id, sp)) { }
                            else if (reader.Answer(group_id, sp)) { }
                            //if (NanaClass.GetNa(group_id, sp))
                            //Lan.GroupNa(je);
                        }
                    //a.Group("976813092", je.GetProperty("raw_message").ToString());   
                }
            }
            listener.Stop();
        }
    }
}
