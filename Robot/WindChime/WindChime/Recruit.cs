using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

public struct Staff : IComparable<Staff>
{
    public string name;
    public int star;

    public int CompareTo([AllowNull] Staff other)
    {
        return Comparer<int>.Default.Compare(star,other.star);
    }
    public string GetString()
    {
        return name;
    }
}
public struct Pt : IComparable<Pt>
{
    public string str;
    public string forword;
    public int star;

    public int CompareTo([AllowNull] Pt other)
    {
        return Comparer<int>.Default.Compare(other.star, star);
    }
    public string GetString()
    {
        return str;
    }
}
public class Recuit
{
    Dictionary<string ,List<Staff> > Rec = new Dictionary<string, List<Staff> >();
    List<string> type = new List<string>();
    public Recuit()
    {
        init();
    }

    public void init()
    {
        using (StreamReader sr = new StreamReader("recruit.dat"))
        {
            int n = Convert.ToInt32(sr.ReadLine());
            type.Add("null");
            for (int i = 1; i <= n; i++)
            {
                string ss = sr.ReadLine();
                string[] sf = ss.Split(" ");
                int l = Convert.ToInt32(sf[1]);
                string typ = sf[0];
                var list = new List<Staff>();
                for (int j = 1; j <= l; j++)
                {
                    string[] s1 = sr.ReadLine().Split();
                    Staff stf;
                    stf.star = Convert.ToInt32(s1[0]);
                    stf.name = s1[1];
                    list.Add(stf);
                }
                Rec.Add(typ, list);
                ss = sr.ReadLine();
            }
        }
    }
    public List<Staff> GetByName(string ss)
    {
        if (Rec.TryGetValue(ss,out var l))
            return l;
        return null;
    } 
    public string GetString(string ss,bool AddSix)
    {
        List<Staff> list;
        list = GetByName(ss);
        if (list == null)
        {
            return $"无法找到 {ss} 相对应的关键字";
        }
        list.Sort();
        string ret = "包含的角色为：";
        foreach (var i in list)
        {
            if (i.star != 6 && !AddSix)
                ret += i.GetString() + " ";
            if (i.star == 6 && AddSix)
                ret += i.GetString() + " ";
        }
        return ret;
    }
    public string GetStringByList(List<Staff> list, bool AddSix)
    {
        list.Sort();
        string ret = "：";
        int cnt = 0;
        foreach (var i in list)
        {
            if (i.star != 6 && !AddSix)
            {
                ret += i.GetString() + " ";
                cnt++;
            }
            if (i.star == 6 && AddSix)
            {
                ret += i.GetString() + " ";
                cnt++;
            }
        }
        if (cnt == 0)
            return null;
        return ret;
    }
    public bool PublicOffering(string group_id, string name, string raw_massage)
    {
        bool SixOnly = false;
        string[] StringSplit = raw_massage.Split(" ");
        if (StringSplit[0] != "公招")
            return false;
        if (StringSplit.Length >= 5)
        {
            List<Staff>[] list = new List<Staff>[10];
            for (int i = 1; i < StringSplit.Length; i++)
            {
                list[i - 1] = GetByName(StringSplit[i]);
                if (list[i-1] == null)
                {
                    Api.Group(group_id, $"无法找到 {StringSplit[i]} 相对应的关键字");
                    return true;
                }
                if(StringSplit[i] == "高级资深干员")
                {
                    SixOnly = true;
                }
            }
            List<Pt> ptlist = new List<Pt>();
            for (int i = 1; i < StringSplit.Length; i++)
            {
                string arg1 = GetStringByList(list[i-1],SixOnly);
                if (arg1 != null)
                {
                    int arg3 = list[i-1][0].star;
                    string arg2 = StringSplit[i];
                    Pt pt;
                    pt.str = arg1;
                    pt.forword = arg2;
                    pt.star = arg3;
                    ptlist.Add(pt);
                }
            }

            for (int i = 1; i < StringSplit.Length-1; i++)
            {
                for (int j = i+1; j < StringSplit.Length; j++)
                {
                    if (StringSplit[i] == StringSplit[j])
                        continue;
                    List<Staff> lista = new List<Staff>();
                    lista = list[i-1].Intersect(list[j-1]).ToList();
                    string arg1 = GetStringByList(lista, SixOnly);
                    if (arg1 != null)
                    {
                        int arg3 = lista[0].star;
                        string arg2 = $"{StringSplit[i]}+{StringSplit[j]}";
                        Pt pt;
                        pt.str = arg1;
                        pt.forword = arg2;
                        pt.star = arg3;
                        ptlist.Add(pt);
                    }
                }
            }
            for (int i = 1; i < StringSplit.Length-2; i++)
            {
                for (int j = i + 1; j < StringSplit.Length-1; j++)
                {
                    for (int k = j + 1; k < StringSplit.Length; k++)
                    {
                        if (StringSplit[i] == StringSplit[j] ||StringSplit[i] == StringSplit[k] || StringSplit[k] == StringSplit[j])
                            continue;
                        List<Staff> lista = new List<Staff>();
                        lista = list[i-1].Intersect(list[j-1].Intersect(list[k-1])).ToList();
                        string arg1 = GetStringByList(lista, SixOnly);
                        if (arg1 != null)
                        {
                            int arg3 = lista[0].star;
                            string arg2 = $"{StringSplit[i]}+{StringSplit[j]}+{StringSplit[k]}" ;
                            Pt pt;
                            pt.str = arg1;
                            pt.forword = arg2;
                            pt.star = arg3;
                            ptlist.Add(pt);
                        }
                    }
                }
            }
            ptlist.Sort();
            string pts;
            pts = "Best Match：\n";
            //for (int i = 0; i < Math.Min(3,ptlist.Count); i++)
            for (int i = 0; i < Math.Min(7, ptlist.Count); i++)
            {
                pts += ptlist[i].forword + ptlist[i].str + "\n";
            }
            Api.Group(group_id, pts);
            return true;
        }
        else
        {
            List<Staff>[] list = new List<Staff>[5];
            for (int i = 1; i < StringSplit.Length; i++)
            {
                list[i - 1] = GetByName(StringSplit[i]);
                if (list[i - 1] == null)
                {
                    Api.Group(group_id, $"无法找到 {StringSplit[i]} 相对应的关键字");
                    return true;
                }
                if (StringSplit[i] == "高级资深干员")
                {
                    SixOnly = true;
                }
            }
            List<Staff> lista = new List<Staff>();
            string str = "";
            for (int i = 1; i < StringSplit.Length; i++)
            {
                if (i == 1)
                    lista = list[i - 1].Union(lista).ToList();
                else
                    lista = list[i - 1].Intersect(lista).ToList();
                str += StringSplit[i];
            }
            str = GetStringByList(lista, SixOnly);
            Api.Group(group_id, str);
            return true;
        }
    }
}