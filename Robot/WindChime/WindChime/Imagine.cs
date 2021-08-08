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
            Api.Private("635691684", "���ˣ�Imagine���ʼ���������⣬ϵͳ�Ѿ��رգ�����������");
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
            Api.Private("635691684", "���ˣ�Imagine���ʼ���������⣬ϵͳ�Ѿ��رգ�����������");
            using (StreamReader sr = new StreamReader("Imagine.dat")) ;
        }
    }
    public bool Main(string group_id, string user_id, string name, string message)
    {
        if (message == "��ȡ������")
        {
            Card(group_id, user_id);
            return true;
        }else if (message == "���¼�������������" && user_id == "635691684")
        {
            Reload();
            Api.Group(group_id, "���¼��سɹ���");
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
            prt += "���ܹ�ϲ�㣬����" + imageiner.name + "�������������������˯����(ps.��ǰ������û������Ի���������������κ��뷨�Ļ��������� ���� ������˵�Ļ����������������÷����������������Ի���)";
        else
            prt += imageiner.addmeg;
        prt += Api.GetImageMessage(imageiner.image);
        Api.Group(group_id, prt);
    }

}
public class Imger
{

}