using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


public class ApiAsnyc
{
    public static async void SendMessageAsync(SendMessage msg)
    {
        await Task.Run(() => sendMessage(msg));
    }
    private static void sendMessage(SendMessage msg)
    {
        var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5700/send_msg");
        request.Accept = "application/json";
        request.Method = "POST";
        request.ContentType = "application/json";
        string json = JsonSerializer.Serialize(new sendFinalMessage(msg));
        byte[] ByteText = Encoding.UTF8.GetBytes(json);
        request.ContentLength = ByteText.Length;
        var Buffer = request.GetRequestStream();
        Buffer.Write(ByteText, 0, json.Length);
        Buffer.Close();

        var response = request.GetResponse();
        string Result = null;
        using (var ResponseStream = response.GetResponseStream())
        {
            if (ResponseStream != null)
            {
                Result = JsonDocument.Parse(ResponseStream).RootElement.ToString();
            }
        }
        Console.WriteLine(Result);
        request.Abort();
    }

}
