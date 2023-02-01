using System;
using System.IO;

namespace WpfApp18
{
    internal class Logger
    {
        static string fileLog = "log.txt";
        internal static void WriteMessage(string message)
        {
            File.AppendAllText(fileLog,$"{DateTime.Now} {message}");
        }
    }
}