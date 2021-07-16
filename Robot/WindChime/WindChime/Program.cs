using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;


namespace Yumis
{
    class Program
    {
        public static Recuit recuit;
        public static ReaderWriter reader;
        public static DateTag dater;
        public static State stater;
        public static Imagine img;
        public static int dt = 1;
        public static bool Ai = false;
        public static int day;
        static void Main(string[] args)
        {
            /*
            PythonExecutor py = new PythonExecutor();
            py.main();
            while (true) { }
            return;
            */
            //OneBotHttpApi.SetServerUri(new Uri("http://127.0.0.1:5700"));
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:5710/");
            listener.Start();

            recuit = new Recuit();
            reader = new ReaderWriter();
            dater = new DateTag();
            stater = new State();
            img = new Imagine();
            day = DateTime.Now.Day;

            Console.WriteLine("Listening...");
            while (true)
            {
                try
                {
                    if (DateTime.Now.Day != day)
                    {
                        State.ReNew();
                        State.Write();
                        day = DateTime.Now.Day;
                    }
                    dater.Prt();
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;
                    using (var input = request.InputStream)
                    {
                        JsonDocument json = JsonDocument.Parse(input);
                        JsonElement je = json.RootElement;
                        response.StatusCode = 200;
                        response.Close();
                        if (je.GetProperty("post_type").GetString() == "message")
                            if (je.GetProperty("message_type").GetString() == "group")
                            {
                                string sp = je.GetProperty("raw_message").GetString();
                                string group_id = je.GetProperty("group_id").ToString();
                                string user_id = je.GetProperty("user_id").ToString();
                                string name = je.GetProperty("sender").GetProperty("card").GetString();
                                if (name == "" || name == null)
                                    name = je.GetProperty("sender").GetProperty("nickname").GetString();

                                if(sp == "查询风铃状态")
                                {
                                    Api.Group(group_id, State.Find());
                                    continue;
                                }
                                if (sp == "renew" && user_id == "635691684")
                                {
                                    State.ReNew();
                                    State.Write();
                                    continue;
                                }
                                if (img.Main(group_id, user_id, name, sp))
                                    continue;
                                //上面为获取信息
                                if (sp == "帮助" || sp == "关于风铃" || sp == "/help")
                                {
                                    Api.Group(group_id, "新用户通过“风铃眼熟我”来注册账号" +
                                        "\n摸摸风铃：增加风铃的好感度" +
                                        "\n打工、工作：日常可以进行“打工”来获取金币" +
                                        "\n加入问答：来让风铃会对你的说话产生回应" +
                                        "\nDelete：让风铃遗忘问答的内容" +
                                        "\n查询：获取自己当前的好感度以及金币和属性值" +
                                        "\n如果好感度到达比较高的程度，风铃的回答会产生一些变化哦！" +
                                        "\n公招+tag：来进行明日方舟的公开招募的组合查询。" +
                                        "\n抽取异想体：抽取一个明天晚上陪你睡觉的异常"+
                                        "\n留言：可以让风铃妈妈直接看到你的消息哦！"+
                                        "\n以上的指令如果不知道怎么用打出对应的指令就会有提示的！");
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
                                if (reader.Delete(group_id, user_id, sp))
                                    continue;
                                reader.Ignore(group_id, user_id, sp);
                                if (recuit.PublicOffering(group_id, user_id, sp)) { }
                                else if (ReaderWriter.Main(group_id, user_id, name, sp)) { }
                                else if (reader.Question(group_id, user_id, name, sp)) { }
                                else if (dater.Add(group_id, user_id, sp)) { }
                                else if (Image.Main(group_id, sp)) { }
                                else if (reader.Answer(group_id, sp)) { }
                                else if (DateTag.Main(group_id,user_id,name,sp)) { }
                                //if (NanaClass.GetNa(group_id, sp))
                                //Lan.GroupNa(je);
                            }
                        //a.Group("976813092", je.GetProperty("raw_message").ToString());   
                    }
                }
                catch (Exception e)
                {
                    Api.Private("635691684", "分割错误");
                }
            }
            listener.Stop();
        }
    }

}
