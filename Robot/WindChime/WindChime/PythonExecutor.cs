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
    public void main()
    {
        Task<string> i = sdnc();
        Console.WriteLine("other");
        
    }
    public string pyrun()
    {

        Process process = new Process();
        var startInfo = process.StartInfo;
        startInfo.FileName = "py";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardInput = true;
        startInfo.ArgumentList.Add("wssb.py");

        process.Start();


        var inputSteam = process.StandardInput;
        inputSteam.WriteLine("shabi py");


        //process.WaitForExit();
        bool isExit = process.WaitForExit(2000);
        if (!isExit) process.Kill();

        var outputStream = process.StandardOutput;
        string output = outputStream.ReadToEnd();
        return output;

        /*
        var compilerProcess = new Process();
        try
        {
            var startInfo = compilerProcess.StartInfo;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "py";
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.WorkingDirectory = workDirectory;
            startInfo.ArgumentList.Add(targetFile.FullName);

            compilerProcess.Start();
            if (!string.IsNullOrEmpty(input))
            {
                await using var inputStream = compilerProcess.StandardInput;
                await inputStream.WriteAsync(input);
            }
            await compilerProcess.WaitForExitAsync(token);

            using var outputStream = compilerProcess.StandardOutput;
            var output = await outputStream.ReadToEndAsync();

            return new ExecuteResult
            {
                ExitCode = compilerProcess.ExitCode,
                Output = output,
                ElapsedTime = compilerProcess.GetRunningTimeMS()
            };
        }
        catch (TaskCanceledException)
        {
            return new ExecuteResult
            {
                ExitCode = -233,
                Output = "TLE"
            };
        }
        catch (Exception e)
        {
            return new ExecuteResult
            {
                ExitCode = -233,
                Output = $"未成功执行,原因:{e.Message}"
            };
        }
        finally
        {
            compilerProcess.Kill(true);
            compilerProcess.Close();
            compilerProcess.Dispose();
        }
        */
    }
    public async Task<string> sdnc()
    {
        string ret = await Task.Run(new Func<string>(pyrun));
        //一大堆解析内容
        Console.Write(ret);
        return ret;
        
    }
}