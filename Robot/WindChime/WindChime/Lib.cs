using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;



namespace WindEngine
{
    public static class rd
    {
        private static Random r = new Random();
        public static float Range(float Min, float Max)
        {
            return Min + (float)r.NextDouble() * (Max - Min);
        }
        public static int Range(int Min, int Max)
        {
            return r.Next(Min, Max);
        }
        public static float Range()
        {
            return (float)r.NextDouble();
        }
    }
    public static class Lib
    {
        public static string GetJson(object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All)
            });
        }
        public static string ReadStringFromFile(string path)
        {
            using StreamReader sr = new StreamReader(path);
            return sr.ReadToEnd();
        }
    }
}