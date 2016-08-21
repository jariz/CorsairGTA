using System;
using System.IO;
using System.Runtime.CompilerServices;
using GTA;

namespace CorsairGTA
{
    class Log
    {
        static StreamWriter writer;
        static bool firstWrite = false;
        
        public static void Open()
        {
            var dt = DateTime.Now;
            var filename = $"CorsairGTA-{dt.Year}-{dt.Day.ToString("D2")}-{dt.Month.ToString("D2")}.log";
            var stream = File.Open(filename, FileMode.Append);
            writer = new StreamWriter(stream);
        }

        public static void Close()
        {
            writer.Close();
        }

        public static void Write(String line, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "")
        {
            try
            {
                Open();
                var dt = DateTime.Now;
                memberName = (memberName == ".ctor" ? "" : "." + memberName);
                writer.WriteLine($"[{dt.Hour.ToString("D2")}:{dt.Minute.ToString("D2")}:{dt.Second.ToString("D2")}] [{Path.GetFileNameWithoutExtension(sourceFilePath)}{memberName}] " + line);
                Close();
            }
            catch(Exception e)
            {
                //don't nag user if this isn't the first time this went wrong
                if(firstWrite)
                {
                    firstWrite = false;
                    UI.Notify("Unable to initialize CorsairGTA logging system: " + e.Message);
                }
            }
        }
    }
}
