using System;
using System.Collections.Generic;
using System.IO;

public struct Imageiner
{
    public string name;
    public string message;
    public string image;
    public string addmeg;
}


public class Imagine
{
    List<Imageiner> ZAYIN;
    List<Imageiner> TETH;
    List<Imageiner> HE;
    List<Imageiner> WAW;
    List<Imageiner> ALEPH;
    Imageiner igr;

    public static Random random = new Random();
    public Imagine()
    {
        ZAYIN = new List<Imageiner>();
        TETH = new List<Imageiner>();
        HE = new List<Imageiner>();
        WAW = new List<Imageiner>();
        ALEPH = new List<Imageiner>();
        try
        {
            using (StreamReader sr = new StreamReader("Imagine.dat"))
            {
                string str;
                int cnt = 0;
                while ((str = sr.ReadLine()) != null)
                {
                    cnt++;
                    string[] sp = str.Split(" ");

                    igr.name = sp[1];
                    igr.message = sp[2];
                    igr.image = sp[3];
                    if (sp.Length >= 5)
                        igr.addmeg = sp[4];
                    else
                        igr.addmeg = null;
                    if (str.Contains("ZAYIN"))
                        ZAYIN.Add(igr);
                    if (str.Contains("TETH"))
                        TETH.Add(igr);
                    if (str.Contains("HE"))
                        HE.Add(igr);
                    if (str.Contains("WAW"))
                        WAW.Add(igr);
                    if (str.Contains("ALEPH"))
                        ALEPH.Add(igr);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Api.Private("635691684", e.ToString());
            Api.Private("635691684", "主人！Imagine类初始化出现问题，系统已经关闭，请你重启！");
            using (StreamReader sr = new StreamReader("Imagine.dat")) ;
        }
    }
    public void Reload()
    {
        ZAYIN.Clear();
        TETH.Clear();
        HE.Clear();
        WAW.Clear();
        ALEPH.Clear();
        try
        {
            using (StreamReader sr = new StreamReader("Imagine.dat"))
            {
                string str;
                int cnt = 0;
                while ((str = sr.ReadLine()) != null)
                {
                    cnt++;
                    string[] sp = str.Split(" ");

                    igr.name = sp[1];
                    igr.message = sp[2];
                    igr.image = sp[3];
                    if (sp.Length >= 5)
                        igr.addmeg = sp[4];
                    else
                        igr.addmeg = null;
                    if (str.Contains("ZAYIN"))
                        ZAYIN.Add(igr);
                    if (str.Contains("TETH"))
                        TETH.Add(igr);
                    if (str.Contains("HE"))
                        HE.Add(igr);
                    if (str.Contains("WAW"))
                        WAW.Add(igr);
                    if (str.Contains("ALEPH"))
                        ALEPH.Add(igr);
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Api.Private("635691684", e.ToString());
            Api.Private("635691684", "主人！Imagine类初始化出现问题，系统已经关闭，请你重启！");
            using (StreamReader sr = new StreamReader("Imagine.dat")) ;
        }
    }
    public bool Main(string group_id, string user_id, string name, string message)
    {
        if (message == "抽取异想体")
        {
            Card(group_id, user_id);
            return true;
        }else if (message == "重新加载异想体数据" && user_id == "635691684")
        {
            Reload();
            Api.Group(group_id, "重新加载成功！");
            return true;
        }
        return false;
    }
    public void Card(string group_id, string user_id)
    {
        string PT = "noon";
        Imageiner imageiner = ZAYIN[0];
        double kk = random.NextDouble();
        if (kk <= 0.1)
            PT = "ALEPH";
        else if (kk <= 0.25)
            PT = "WAW";
        else if (kk <= 0.5)
            PT = "HE";
        else if (kk <= 0.75)
            PT = "TETH";
        else PT = "ZAYIN";

        if (PT == "ALEPH")
            imageiner = ALEPH[random.Next(0, ALEPH.Count)];
        if (PT == "WAW")
            imageiner = WAW[random.Next(0, WAW.Count)];
        if (PT == "HE")
            imageiner = HE[random.Next(0, HE.Count)];
        if (PT == "TETH")
            imageiner = TETH[random.Next(0, TETH.Count)];
        if (PT == "ZAYIN")
            imageiner = ZAYIN[random.Next(0, ZAYIN.Count)];
        string prt = PT + "\n" + imageiner.message + "\n";
        if (imageiner.addmeg == null)
            prt += "主管恭喜你，明天" + imageiner.name + "将会来到你的寝室陪你睡觉！(ps.当前异想体没有特殊对话，如果有主管有任何想法的话，可以用 留言 （你想说的话）来给风铃留言让风铃更改异想体特殊对话。)";
        else
            prt += imageiner.addmeg;
        prt += Api.GetImageMessage(imageiner.image);
        Api.Group(group_id, prt);
    }

}
public class Imger
{

}