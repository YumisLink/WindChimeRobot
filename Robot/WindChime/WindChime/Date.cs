using System;
using System.IO;

public class Date
{
    public static void Write(string ss)
    {
        try
        {
            StreamWriter w = File.AppendText("date.dat");
            w.WriteLine(ss);
            w.Close();
        }
        catch 
        {
            Api.Private("635691684", "DateError");
        }
    }
}