using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public struct PyRunResult
{
    string result;
}
public class PythonExecutor 
{

    public static void Mastery(string group_id,string name,string skill)
    {
        Task<string> i = mastery(group_id,name,skill);
    }
    private static async Task<string> mastery(string group_id, string name, string skill)
    {
        string ret;
        ret = await Task.Run(() => Pyrun(name + "\n" + skill, "FindCL.py"));
        if (ret == "None")
        {
            Api.Group(group_id, "无法找到" + name + "的技能" + skill);
            return ret;
        }
        ret = ZLcode(ret);
        //Console.WriteLine(ret);
        Api.Group(group_id, ret);
        return ret;
    }

    private static string ZLcode(string str)
    {
        string[] ct = str.Split("\n");
        string ret = ct[0];
        string save = null;
        int cnt = 1;
        for (int i = 2; i < ct.Length; i++)
        {
            if (save == null)
            {
                save = ct[i];
                save = save.Replace("\r", "");
            }
            else
            {
                if (ct[i].Contains("技巧概要"))
                    ret += "\n专精" + cnt++ + "：";
                else
                    ret += "\n        ";
                ret +=  save + "个" + ct[i];
                save = null;
            }
        }
        return ret;
    }





    public static void NextCodeforce(string group_id)
    {

        Task<string> i = nextCodeforce(group_id);
    }
    private static async Task<string> nextCodeforce(string group_id)
    {
        string ret;
        ret = await Task.Run(() => Pyrun(null,"NextRun.py"));
        //Func<string, string> fun = new Func<string, string>(Pyrun);
        //ret = await Task.Run(fun => sid);
        //一大堆解析内容

        ret = ret.Replace("#", "x");
        Api.Group(group_id,ret);
        return ret;
    }





    public static void main(string group_id,string ss)
    {
        Task<string> i = sdnc(group_id,ss);
        //Console.WriteLine("other");
        
    }
    public static async Task<string> sdnc(string group_id,string sid)
    {
        string ret;
        ret = await Task.Run(() => Pyrun(sid,"a.py"));
        //Func<string, string> fun = new Func<string, string>(Pyrun);
        //ret = await Task.Run(fun => sid);
        //一大堆解析内容
        Api.Group(group_id, sid+"的CF分数是："+ret);
        return ret;
        
    }

    public static string Pyrun(string input, string pycode)
    {

        Process process = new Process();
        var startInfo = process.StartInfo;
        startInfo.FileName = "python3";
        //startInfo.FileName = "py ";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardInput = true;
        startInfo.ArgumentList.Add("Python/" + pycode);
        //startInfo.ArgumentList.Add(pycode);

        process.Start();


        var inputSteam = process.StandardInput;
        if (input!=null)
        inputSteam.WriteLine(input);


        //process.WaitForExit();
        bool isExit = process.WaitForExit(5000);
        if (!isExit)
        {
            process.Kill();
            return "草泥马，网络延迟！我也没有办法，傻逼腾讯服务器！";
        }

        var outputStream = process.StandardOutput;
        string output = outputStream.ReadToEnd();
        return output;
    }
}