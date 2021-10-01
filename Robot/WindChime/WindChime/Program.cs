using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;


namespace Yumis
{
    class Program
    {
        public static EGOSTRONGER lib;
        public static Recuit recuit;
        public static ReaderWriter reader;
        public static DateTag dater;
        public static State stater;
        public static Imagine img;
        public static int dt = 1;
        public static bool Ai = false;
        public static int day;
        public static GameManager gm;
        public static War w;
        public static EGOController EgoController;
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
            gm = new GameManager();
            day = DateTime.Now.Day;
            lib = new EGOSTRONGER();
            w = new War();
            EgoController = new EGOController();

            Console.WriteLine("Listening...");
            while (true)
            {
                try
                {
                    if (DateTime.Now.Day != day)
                    {
                        //State.ReNew();
                        //State.Write();
                        day = DateTime.Now.Day;
                    }
                    //dater.Prt();
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
                        {
                            if (je.GetProperty("message_type").GetString() == "group")
                            {
                                string sp = je.GetProperty("raw_message").GetString();
                                string group_id = je.GetProperty("group_id").ToString();
                                string user_id = je.GetProperty("user_id").ToString();
                                string message_id = je.GetProperty("message_id").ToString();
                                AntiHeJieqing.Main(sp,user_id,group_id,message_id);
                                string name = je.GetProperty("sender").GetProperty("card").GetString();
                                if (name == "" || name == null)
                                    name = je.GetProperty("sender").GetProperty("nickname").GetString();
                                //Date.Write("收到" + group_id+"群"+name + "发送的" + sp);
                                if (LoopK(group_id, user_id, name, sp))
                                    continue;
                                //上面为获取信息
                                if (sp == "帮助" || sp == "关于风铃" || sp == "/help")
                                {
                                    Api.Group(group_id, "新用户通过“风铃眼熟我”来注册账号" +
                                        "\n摸摸风铃：     增加风铃的好感度" +
                                        "\n打工、工作：   日常可以进行“打工”来获取金币" +
                                        "\n加入问答：     来让风铃会对你的说话产生回应" +
                                        "\nDelete：      让风铃遗忘问答的内容" +
                                        "\n查询：         获取自己当前的好感度以及金币和属性值" +
                                        "\n查询EGO：      获取自己当前的EGO的详细状态" +
                                        "\n如果好感度到达比较高的程度，风铃的回答会产生一些变化哦！" +
                                        "\n公招+tag：      来进行明日方舟的公开招募的组合查询。" +
                                        "\n抽取异想体：    抽取一个明天晚上陪你睡觉的异常" +
                                        "\n留言：         可以让风铃妈妈直接看到你的消息哦！" +
                                        "\nBattle [QQ号] ：与这个人战斗，如果胜利获得金币，如果输了损失金币" +
                                        "\n强化：          顾名思义 强化你的EGO装备" +
                                        "\n天梯赛：        输入这三个字，直接参加天梯赛，挑战排名前一位的人！每次消耗100金币！" +
                                        "\n领取天梯赛奖励： 每天都可以领取一次天梯赛的奖励，根据排名可以获得相对应的金币哦！" +
                                        "\n挑战：挑战加上boss名字即可挑战boss" +
                                        "\n更换护甲 or 更换武器：后面加上EGO的名字即可切换，通过查询EGO可以知道有什么装备！" +
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
                                else if (Image.Main(group_id, user_id, name, sp)) { }
                                else if (DateTag.Main(group_id, user_id, name, sp)) { }
                                else if (War.main(group_id, user_id, name, sp)) { }
                                else if (EGOController.Main(group_id, user_id, name, sp)) { }
                                else if (reader.Answer(group_id, sp)) { }
                                //if (NanaClass.GetNa(group_id, sp))
                                //Lan.GroupNa(je);
                            }
                            else if (je.GetProperty("message_type").GetString() == "private")
                            {
                                string user_id = je.GetProperty("user_id").ToString();
                                string sp = je.GetProperty("raw_message").GetString();
                                if (Gezi.main(user_id, sp)) { }
                                else if (sp.Contains("强化"))
                                    EGOSTRONGER.Increase(user_id, sp);
                            }
                        }else if (je.GetProperty("post_type").GetString() == "notice")
                        {
                            if (je.GetProperty("notice_type").GetString() == "group_recall")
                            {
                                string mgid = je.GetProperty("message_id").ToString();
                                string user = je.GetProperty("operator_id").ToString();
                                AntiHeJieqing.Say(mgid,user);
                            }
                        }
                            
                        //a.Group("976813092", je.GetProperty("raw_message").ToString());   
                    }
                }
                catch (Exception e)
                {
                    Api.Private("635691684", e.ToString());
                }
            }
            listener.Stop();
        }
        static bool LoopK(string group_id, string user_id, string name, string sp)
        {
            if (group_id == "621976344")
            {
                if (sp.Contains("充值") || sp.Contains("抽奖"))
                    Api.Group("621976344", "本服为公益服，服主自费运营，没有抽奖和充值功能，服主会经常发放点券福利和cdk福利或者道具等，若喜欢并希望支持本服可以找群主赞助，赞助费用会全部用于维护服务器，并会给予赞助人相应的但不影响平衡性的游戏道具。");
            }
            if (group_id == "621976344")
            {
                if (sp.Contains("下载") || sp.Contains("攻略"))
                    Api.Group("621976344", "完整客户端（解压即玩）\n https://yunpan.360.cn/surl_y6IQQTi3TCb \n新手教程 \n泡点2分钟1000代币券，刷图掉代币，70级可学二觉，深渊掉70~90已平衡SS，创建角色后方可开启左右槽，直接无色完成，单人四倍经验组队八倍经验，主线任务都经过优化，繁琐任务简化，可放心跟着主线任务刷图升级，罐子头NPC出售CC武器，幸达风振GSD凯丽歌兰蒂斯米内特出售全职业1~70级紫装，商城出售的三倍经验药和疲劳药无使用次数限制，契约可用点券购买黑钻。商城也可购买升级券直接升，NPC商店应有尽有，任务材料“凯丽”，副职业材料“诺顿”以及相应副职业导师都有出售，“卡妮娜商店”可以购买鸽服特色的一些道具，需要材料“鸽子兑换券”可以通过刷图翻牌获得。所有NPC商店都进行了优化，赛丽亚可以直接购买远古门票盒等。 \n本服以怀旧路线为基础，对大量诟病的机制进行优化并且加入大量特色道具、任务、和自创装备。\n深渊掉落删除“战神之意志 - X职业”之类的毒瘤装备，加入了“天御套”“魔战套”“恍惚套”等趣味装备，所有新加入装备图鉴在【群相册】查看。\n满级日常任务“远古”“异界”“武道”“安图恩”基本都是通关一到三次即可完成。去掉远古异界复活币限制。异界进入次数10次删除了全部异界套改为特色自制左槽，可在异界门口的NPC商店查看。");
            }
            if (group_id == "621976344")
                return true;
            if (sp.Contains("专精材料"))
            {
                string[] str = sp.Split(" ");
                if (str.Length != 3)
                {
                    Api.Group(group_id, "格式错误格式参考如下\n专精材料 【干员名字】 【阿拉伯数字1,2,3代表是第几个技能】");
                    return true;
                }
                int k = 0;
                try
                {
                    k = Convert.ToInt32(str[2]);
                }
                catch
                {
                    Api.Group(group_id, "格式错误格式参考如下\n专精材料 【干员名字】 【阿拉伯数字1,2,3代表是第几个技能】");
                    return true;
                }
                if (k < 1 || k > 3)
                {
                    Api.Group(group_id, "格式错误格式参考如下\n专精材料 【干员名字】 【阿拉伯数字1,2,3代表是第几个技能】");
                    return true;
                }
                PythonExecutor.Mastery(group_id, str[1], str[2]);
            }
            if (sp.Contains("查分"))
            {
                Api.Group(group_id, "正在开始查询......请不要重复提交查询。");
                string str = sp.Replace("查分 ", "");
                str = str.Replace("查分", "");
                PythonExecutor.main(group_id, str);
                return true;
            }
            if (sp == "CF" || sp == "Cf" || sp == "cf")
            {
                Api.Group(group_id,"正在开始查询......请不要重复提交查询。");
                PythonExecutor.NextCodeforce(group_id);
                return true;
            }
            if (sp.Contains("强化"))
            {
                EGOSTRONGER.Increase(group_id, user_id, sp);
                return true;
            }
            if (sp.Contains("群发") && user_id == "635691684")
            {
                string str = sp.Replace("群发 ", "");
                State.GroupsSend(str);
                return true;
            }
            if (sp == "查询风铃状态")
            {
                Api.Group(group_id, State.Find());
                return true;
            }
            if (sp == "renew" && user_id == "635691684")
            {
                State.ReNew();
                State.Write(); return true;
            }
            if (img.Main(group_id, user_id, name, sp)) return true;
            return false;
        }
    }
}
