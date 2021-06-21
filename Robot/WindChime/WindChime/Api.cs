using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
class Api {
    public static string GetAtMessage(string user_id)
    {
        return "[CQ:at,qq=" + user_id + "]";
    }
    public static string GetImageMessage(string user_id)
    {
        return "[CQ:image,file=" + user_id + ".image]";
    }
    public Api() { }
    public static void Private(string user_id,string message)
    {
        var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5700/send_private_msg?user_id=" + user_id + "&message=" + message);
        //var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5700/send_private_msg");
        request.Method = "POST";
        request.ContentType = "application/json";
        using var postDataStream = request.GetRequestStream();
        /*
        */
        var response = request.GetResponse();
        request.Abort();
    }
    public static void Group(string group_id, string message)
    {
        long Group_id = Convert.ToInt64(group_id);

        var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5700/send_group_msg?group_id=" + group_id + "&message=" + message);
        request.Method = "POST";
        request.ContentType = "application/json";
        var postDataStream = request.GetRequestStream();
        using var writer = new Utf8JsonWriter(postDataStream);
        //using var file = File.CreateText("json.json");
        //using var writer = new Utf8JsonWriter(file.BaseStream);
        /*
        writer.WriteStartObject();
            writer.WriteNumber("user_id", 635691684);
            writer.WritePropertyName("message");
            writer.WriteStartArray();
                writer.WriteStartObject();
                    writer.WriteString("type", "text");
                    writer.WritePropertyName("data");
                    writer.WriteStartObject();
                        writer.WriteString("message", ""+message);
                    writer.WriteEndObject();
                writer.WriteEndObject();
            writer.WriteEndArray();
        writer.WriteEndObject();
        */
        //Console.WriteLine(writer.ToString());
        //postDataStream.Flush();
        var response = request.GetResponse();
        JsonDocument result = null;
        using (var responseStream = response.GetResponseStream())
        {
            if (responseStream != null)
            {
                result = JsonDocument.Parse(responseStream);
            }
        }
        
        Console.WriteLine(result.RootElement.ToString());
        //request.Abort();
    }
}