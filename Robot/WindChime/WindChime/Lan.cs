using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
class Lan
{
    public static void Call(string group_id)
    {
        Random rd = new Random();
        int k = rd.Next(1, 9);
        if (k == 1)
            Api.Group(group_id, "那么，请回答，漫步在层崖峭壁的山岳地带，有着沉鱼落雁般美貌的魔女究竟是谁呢？没错，就是十八岁的我!");
        if (k == 2)
            Api.Group(group_id, "戴着这枚彰显魔女身份的胸针，披着一头灰色秀发，其美貌与才能散发的光芒，连太阳见了都会不由眯起眼睛的美女，究竟是谁呢？没错，就是我!");
        if (k == 3)
            Api.Group(group_id, "这位不输给色彩斑斓的鲜花美得如花般绽放的人是谁呢？没错，就是我!");
        if (k == 4)
            Api.Group(group_id, "有一位魔女飞在草原上魔女一副兴奋喜悦的模样，下一个国家会是什么样子，下一个遇到的人是什么样子，她满心期待，这位旅行者究竟是谁呢？没错，就是我!");
        if (k == 5)
            Api.Group(group_id, "和平国洛贝塔出现了一位少女，她14岁便通过了魔术考试，这史上最年轻的见习魔女，没错 就是我!");
        if (k == 6)
            Api.Group(group_id, "这个在钟表之乡洛施托夫广场上的，贫穷 饥饿 泫然欲泣，美丽虚幻的女子是谁，没错 就是我!");
        if (k == 7)
            Api.Group(group_id, "在一旁 定会被吸引住目光，有着闭花羞月般美貌的魔女，究竟是谁呢？那就是身在旅途，编制着我和我们的故事魔女之旅的人!");
        if (k == 8)
            Api.Group(group_id, "有一位魔女坐着扫帚飞在空中，灰色头发在风中飘逸，这位像洋娃娃一般漂亮又可爱，连夏天的当空烈日见了都会放出更炙热光芒的美女，究竟是谁呢，没错 就是我!");
        
    }
}